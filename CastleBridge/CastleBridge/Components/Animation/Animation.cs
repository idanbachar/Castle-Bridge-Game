using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
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
        public bool IsFinished;
        private Rectangle Rectangle;

        public Animation(string fullPath, Rectangle rectangle, int startAnimationFromIndex, int endAnimationInIndex, int spritesLength, int nextFrameDelay, bool isReverse, bool isLoop) {

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
            IsFinished = false;
            Rectangle = rectangle;
            LoadSprites(fullPath);
        }

        public void SetRectangle(int x, int y, int width, int height) {

            foreach (Image sprite in Sprites)
                sprite.SetRectangle(x, y, width, height);
        }

        public void SetVisible(bool value) {

            foreach (Image sprite in Sprites)
                sprite.SetVisible(value);
        }

        public void SetDirection(Direction direction) {

            Direction = direction;

            foreach (Image sprite in Sprites)
                sprite.SetDirection(direction);
        }

        public void SetRotation(float rotation) {

            foreach (Image sprite in Sprites)
                sprite.SetRotation(rotation);
        }

        public void SetReverse(bool value) {
            IsReverse = value;
        }

        private void LoadSprites(string fullPath) {

            for (int i = 0; i < Sprites.Length; i++) {

                Sprites [i] = new Image(fullPath + i,
                                        Rectangle.X,
                                        Rectangle.Y,
                                        Rectangle.Width,
                                        Rectangle.Height);
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
                        IsFinished = true;
                    }

                }
                else if (IsReverse) {
                    if (AnimationState == AnimationState.Forward) {
                        if (CurrentFrame < EndAnimationInIndex - 1)
                            CurrentFrame++;
                        else {
                            CurrentFrame = EndAnimationInIndex - 1;
                            AnimationState = AnimationState.Backward;
                        }
                    }
                    
                    else if (AnimationState == AnimationState.Backward) {
                        if (CurrentFrame > StartAnimationFromIndex)
                            CurrentFrame--;
                        else {
                            CurrentFrame = StartAnimationFromIndex;
                            IsPlaying = false;
                            IsFinished = true;
                            AnimationState = AnimationState.Forward;
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

        public void Reset() {
            IsFinished = false;
            IsPlaying = false;
        }

        public Image GetCurrentSpriteImage() {
            return Sprites [CurrentFrame];
        }

        public void Draw() {

            GetCurrentSpriteImage().Draw();
        }
    }
}
