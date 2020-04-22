using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Arrow {

        private int ShootTime;
        public bool IsFinished;
        private Animation Animation;
        private int Speed;
        private Direction Direction;
        private Direction ShootUpDownDirection;

        public Arrow(int startX, int startY, Direction direction, Direction shootUpDownDirection) {

            ShootTime = 0;
            IsFinished = false;
            Speed = 20;
            Direction = direction;
            Animation = new Animation("player/characters/teams/red/archer/weapons/arrow/arrow_", new Rectangle(startX, startY, 44, 21), 0, 0, 1, 3, false, false);
            Animation.SetDirection(direction);
            ShootUpDownDirection = shootUpDownDirection;
        }

        public void Move() {
            if (ShootTime < 35) {
                ShootTime++;

                switch (Direction) {
                    case Direction.Right:

                        if (ShootUpDownDirection == Direction.Down) {
                            Animation.SetRectangle(Animation.GetCurrentSpriteImage().GetRectangle().X + Speed,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Y + Speed / 10,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Width,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Height);
                        }
                        else if(ShootUpDownDirection == Direction.Up) {
                            Animation.SetRectangle(Animation.GetCurrentSpriteImage().GetRectangle().X + Speed,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Y - Speed / 10,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Width,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Height);
                        }
                        break;
                    case Direction.Left:

                        if (ShootUpDownDirection == Direction.Down) {
                            Animation.SetRectangle(Animation.GetCurrentSpriteImage().GetRectangle().X - Speed,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Y + Speed / 10,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Width,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Height);
                        }
                        else if (ShootUpDownDirection == Direction.Up) {
                            Animation.SetRectangle(Animation.GetCurrentSpriteImage().GetRectangle().X - Speed,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Y - Speed / 10,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Width,
                                                      Animation.GetCurrentSpriteImage().GetRectangle().Height);
                        }
                        break;
                }

            }
            else {
                ShootTime = 0;
                IsFinished = true;
            }
        }

        public Direction GetDirection() {
            return Direction;
        }

        public Animation GetAnimation() {
            return Animation;
        }

        public int GetSpeed() {
            return Speed;
        }

        public void SetSpeed(int speed) {
            Speed = speed;
        }

        public void Draw() {
            Animation.Draw();
        }

    }
}
