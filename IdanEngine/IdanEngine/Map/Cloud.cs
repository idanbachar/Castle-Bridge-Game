using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Cloud {

        private Animation Animation;
        private bool IsOnDestination;
        private Random Rnd;
        private int Speed;
        public Cloud(int x, int y, int width, int height) {
            Animation = new Animation(new Image("map/clouds", "cloud_", x, y, width, height, Color.White), 0, 2, 2, 15, true, true);
            Animation.Start();
            IsOnDestination = false;
            Speed = 1;
            Rnd = new Random();
        }

        public void Update() {

            Animation.Play();

            if (Animation.GetCurrentSprite().GetRectangle().Right < 0)
                IsOnDestination = true;

            if (!IsOnDestination) {
                Move();
            }
            else {

                ResetPosition();
                IsOnDestination = false;
            }
        }

        public void ResetPosition() {
            Animation.SetNewRectangle(Map.WIDTH * 2, Rnd.Next(0, 200), 125, 75);
        }

        public void Move() {
            Animation.SetNewRectangle(Animation.GetCurrentSprite().GetRectangle().X - Speed, Animation.GetCurrentSprite().GetRectangle().Y, Animation.GetCurrentSprite().GetRectangle().Width, Animation.GetCurrentSprite().GetRectangle().Height);
        }

        public void Draw() {
            Animation.Draw();
        }
    }
}
