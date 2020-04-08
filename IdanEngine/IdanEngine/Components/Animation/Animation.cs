﻿using Microsoft.Xna.Framework;
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
        private bool IsLoop;
        private int NextFrameDelay;
        private int NextFrameDelayTimer;
        private Direction Direction;
        private bool IsReverse;
        private AnimationState AnimationState;

        public Animation(Image image, int startAnimationFromIndex, int endAnimationInIndex, int spritesLength, int nextFrameDelay, bool isReverse, bool isLoop) {

            Sprites = new Image [spritesLength];
            StartAnimationFromIndex = startAnimationFromIndex;
            EndAnimationInIndex = endAnimationInIndex;
            CurrentFrame = StartAnimationFromIndex;
            IsPlaying = false;
            IsLoop = isLoop;
            NextFrameDelay = nextFrameDelay;
            NextFrameDelayTimer = 0;
            Direction = Direction.Right;
            IsReverse = isReverse;
            AnimationState = AnimationState.Forward;

            LoadSprites(image);
        }

        public void SetNewRectangle(int x, int y, int width, int height) {

            foreach (Image sprite in Sprites)
                sprite.SetNewRectangle(x, y, width, height);
        }

        public void SetVisible(bool value) {
            foreach (Image sprite in Sprites)
                sprite.SetVisible(value);
        }

        public void SetDirection(Direction direction) {
            Direction = direction;

            foreach (Image sprite in Sprites)
                sprite.SetNewDirection(direction);
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

        private void RunInLoop() {

            if (NextFrameDelayTimer < NextFrameDelay) {
                NextFrameDelayTimer++;
            }
            else {

                if (!IsReverse) {

                    if (CurrentFrame < EndAnimationInIndex - 1)
                        CurrentFrame++;
                    else {
                        CurrentFrame = StartAnimationFromIndex;
                    }

                }
                else if (IsReverse) {

                    if (AnimationState == AnimationState.Backward) {
                        if (CurrentFrame > StartAnimationFromIndex)
                            CurrentFrame--;
                        else {
                            CurrentFrame = StartAnimationFromIndex;
                            AnimationState = AnimationState.Forward;
                        }
                    }
                    else if (AnimationState == AnimationState.Forward) {
                        if (CurrentFrame < EndAnimationInIndex - 1)
                            CurrentFrame++;
                        else {
                            CurrentFrame = EndAnimationInIndex - 1;
                            AnimationState = AnimationState.Backward;
                        }
                    }
                }

                NextFrameDelayTimer = 0;
            }
        }

        private void RunInOneTime() {

            if (NextFrameDelayTimer < NextFrameDelay) {
                NextFrameDelayTimer++;
            }
            else {

                if (!IsReverse) {

                    if (CurrentFrame < EndAnimationInIndex - 1)
                        CurrentFrame++;
                    else {
                        CurrentFrame = StartAnimationFromIndex;
                        IsPlaying = false;
                    }

                }
                else if (IsReverse) {

                    if (AnimationState == AnimationState.Backward) {
                        if (CurrentFrame > StartAnimationFromIndex)
                            CurrentFrame--;
                        else {
                            CurrentFrame = StartAnimationFromIndex;
                            IsPlaying = false;
                            AnimationState = AnimationState.Forward;
                        }
                    }
                    else if (AnimationState == AnimationState.Forward) {
                        if (CurrentFrame < EndAnimationInIndex - 1)
                            CurrentFrame++;
                        else {
                            CurrentFrame = EndAnimationInIndex - 1;
                            IsPlaying = false;
                            AnimationState = AnimationState.Backward;
                        }
                    }
                }

                NextFrameDelayTimer = 0;
            }
        }

        public void Play() {

            if (IsPlaying) {

                if (IsLoop) {
                    RunInLoop();
                }
                else if(!IsLoop) {
                    RunInOneTime();
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
