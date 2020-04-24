using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class EnergyBall {

        public int ShootTime;
        public bool IsFinished;
        public Animation Animation;
        private int Speed;
        private Direction Direction;
        private Direction ShootUpDownDirection;
        private Location CurrentLocation;

        public EnergyBall(int startX, int startY, Direction direction, Direction shootUpDownDirection, Location location) {

            ShootTime = 0;
            IsFinished = false;
            Speed = 20;
            Direction = direction;
            Animation = new Animation("player/characters/teams/red/mage/weapons/energy ball/energy_ball_", new Rectangle(startX, startY, 44, 21), 0, 0, 1, 3, false, false);
            Animation.SetDirection(direction);
            ShootUpDownDirection = shootUpDownDirection;
            CurrentLocation = location;
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
                Animation.SetRotation(Direction == Direction.Right ? 0.7f : -0.7f);
            }
        }

        public Location GetCurrentLocation() {
            return CurrentLocation;
        }
        public void Draw() {
            Animation.Draw();
        }

    }
}
