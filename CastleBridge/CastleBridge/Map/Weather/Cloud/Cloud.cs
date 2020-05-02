using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Cloud {

        private Animation Animation;
        private bool IsOnDestination;
        private Random Rnd;
        private int Speed;
        public bool IsRain;
        private List<RainDrop> RainDrops;
        private int GenerateRainDropTimer;
        private int ScreenWidth;

        public Cloud(int x, int y, int width, int height, bool isRain, int screenWidth) {
            Animation = new Animation("map/clouds/cloud_", new Rectangle(x, y, width, height), 0, 1, 2, 15, true, true);
            Animation.Start();
            IsOnDestination = false;
            Speed = 1;
            Rnd = new Random();
            IsRain = isRain;
            RainDrops = new List<RainDrop>();
            GenerateRainDropTimer = 0;
            ScreenWidth = screenWidth;
        }

        public void Update() {

            Animation.Play();

            if (Animation.GetCurrentSpriteImage().GetRectangle().Right < 0)
                IsOnDestination = true;

            if (!IsOnDestination) {
                Move();
            }
            else {

                ResetPosition();
                IsOnDestination = false;
            }


            if (IsRain) {

                if (GenerateRainDropTimer < 20)
                    GenerateRainDropTimer++;
                else {
                    GenerateRainDropTimer = 0;
                    GenerateRainDrop();
                }

                for(int i = 0; i < RainDrops.Count; i++) {
                    if (!RainDrops [i].IsFinished)
                        RainDrops [i].Fall();
                    else {
                        RainDrops.RemoveAt(i);
                    }
                }
            }
        }

        public Animation GetCurrentAnimation() {
            return Animation;
        }

        public List<RainDrop> GetRainDrops() {
            return RainDrops;
        }

        private void GenerateRainDrop() {
            RainDrops.Add(new RainDrop(Rnd.Next(Animation.GetCurrentSpriteImage().GetRectangle().Left, Animation.GetCurrentSpriteImage().GetRectangle().Right), Animation.GetCurrentSpriteImage().GetRectangle().Bottom));
        }

        public void ResetPosition() {
            Animation.SetRectangle(ScreenWidth + 100, Rnd.Next(0, 200), 125, 75);
        }

        public void Move() {
            Animation.SetRectangle(Animation.GetCurrentSpriteImage().GetRectangle().X - Speed, Animation.GetCurrentSpriteImage().GetRectangle().Y, Animation.GetCurrentSpriteImage().GetRectangle().Width, Animation.GetCurrentSpriteImage().GetRectangle().Height);
        }

        public void Draw() {

            Animation.Draw();
        }
    }
}
