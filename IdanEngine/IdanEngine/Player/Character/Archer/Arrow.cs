using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Arrow {

        public int ShootTime;
        public bool IsFinished;
        public Animation Animation;
        private int Speed;
        private Direction Direction;
        private Direction ShootUpDownDirection;
        public Text DisplayedText;

        public Arrow(int startX, int startY, Direction direction, Direction shootUpDownDirection) {

            ShootTime = 0;
            IsFinished = false;
            Speed = 20;
            Direction = direction;
            Animation = new Animation(new Image("player/characters/teams/red/archer /weapons/arrow", "arrow_", startX, startY, 44, 21, Color.White), 0, 0, 1, 3, false, false);
            Animation.SetDirection(direction);
            ShootUpDownDirection = shootUpDownDirection;

            DisplayedText = new Text(FontType.Default, string.Empty, new Vector2(startX, startY - 25), Color.Black, true, Color.Gold);
            DisplayedText.SetVisible(false);

            DisplayedText.SetText("Press 'E' to take" +
                "\n" +
                "(+1 Arrow)");
        }

        public void Move() {
            if (ShootTime < 35) {
                ShootTime++;

                switch (Direction) {
                    case Direction.Right:

                        if (ShootUpDownDirection == Direction.Down) {
                            Animation.SetNewRectangle(Animation.GetCurrentSprite().GetRectangle().X + Speed,
                                                      Animation.GetCurrentSprite().GetRectangle().Y + Speed / 10,
                                                      Animation.GetCurrentSprite().GetRectangle().Width,
                                                      Animation.GetCurrentSprite().GetRectangle().Height);
                        }
                        else if(ShootUpDownDirection == Direction.Up) {
                            Animation.SetNewRectangle(Animation.GetCurrentSprite().GetRectangle().X + Speed,
                                                      Animation.GetCurrentSprite().GetRectangle().Y - Speed / 10,
                                                      Animation.GetCurrentSprite().GetRectangle().Width,
                                                      Animation.GetCurrentSprite().GetRectangle().Height);
                        }
                        break;
                    case Direction.Left:

                        if (ShootUpDownDirection == Direction.Down) {
                            Animation.SetNewRectangle(Animation.GetCurrentSprite().GetRectangle().X - Speed,
                                                      Animation.GetCurrentSprite().GetRectangle().Y + Speed / 10,
                                                      Animation.GetCurrentSprite().GetRectangle().Width,
                                                      Animation.GetCurrentSprite().GetRectangle().Height);
                        }
                        else if (ShootUpDownDirection == Direction.Up) {
                            Animation.SetNewRectangle(Animation.GetCurrentSprite().GetRectangle().X - Speed,
                                                      Animation.GetCurrentSprite().GetRectangle().Y - Speed / 10,
                                                      Animation.GetCurrentSprite().GetRectangle().Width,
                                                      Animation.GetCurrentSprite().GetRectangle().Height);
                        }
                        break;
                }

            }
            else {
                ShootTime = 0;
                IsFinished = true;
                Animation.SetRotation(Direction == Direction.Right ? 0.7f : -0.7f);
                DisplayedText.SetPosition(new Vector2(Animation.GetCurrentSprite().GetRectangle().X, Animation.GetCurrentSprite().GetRectangle().Y));
            }
        }

        public void Draw() {
            Animation.Draw();
            DisplayedText.Draw();
        }

    }
}
