using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Animation {

        private Image [] Sprites;
        private int StartAnimationFromIndex;
        private int EndAnimationInIndex;
        private int CurrentFrame;
        private bool IsPlaying;
        private int NextFrameDelay;
        private int NextFrameDelayTimer;

        public Animation(Image image, int startAnimationFromIndex, int endAnimationInIndex, int spritesLength) {

            Sprites = new Image [spritesLength];
            StartAnimationFromIndex = startAnimationFromIndex;
            EndAnimationInIndex = endAnimationInIndex;
            CurrentFrame = StartAnimationFromIndex;
            IsPlaying = false;
            NextFrameDelay = 3;
            NextFrameDelayTimer = 0;
            LoadSprites(image);
        }

        public void SetNewRectangle(int x, int y, int width, int height) {

            foreach (Image sprite in Sprites)
                sprite.SetNewRectangle(x, y, width, height);
        }

        private void LoadSprites(Image image) {

            string path = image.FullPath;

            for(int i = 0; i < Sprites.Length; i++) {
                
                Sprites[i] = new Image(string.Empty, "default", 
                                                     image.GetRectangle().X,
                                                     image.GetRectangle().Y,
                                                     image.GetRectangle().Width,
                                                     image.GetRectangle().Height, Color.White);
                Sprites [i].SetNewImage(path + i);
            }
        }

        public void Play() {
            if (IsPlaying) {

                if (CurrentFrame < EndAnimationInIndex - 1)
                    CurrentFrame++;
                else {
                    CurrentFrame = StartAnimationFromIndex;
                    IsPlaying = false;
                }
            }
        }

        public void Start() {
            IsPlaying = true;
        }

        public void Stop() {
            IsPlaying = false;
        }

        public Image GetCurrentSprite() {
            return Sprites [CurrentFrame];
        }

        public void Draw() {

            GetCurrentSprite().Draw();
        }
    }
}
