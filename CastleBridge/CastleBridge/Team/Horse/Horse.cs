using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Horse {
 
        private Rectangle Rectangle;
        private Horsestate State;
        private TeamName TeamName;
        public Animation AfkAnimation;
        public Animation WalkAnimation;
        private Animation CurrentAnimation;
        private int Speed;
        private Direction Direction;
        private Player Owner;
        private Text Tooltip;

        public Horse(TeamName teamName, int x, int y, int width, int height) {
            TeamName = teamName;
            Rectangle = new Rectangle(x, y, width, height);
            AfkAnimation = new Animation(new Image("horse/teams/" + teamName + "/afk", "horse_afk_", x, y, width, height, Color.White), 0, 7, 7, 6, true, true);
            WalkAnimation = new Animation(new Image("horse/teams/" + teamName + "/walk", "horse_walk_", x, y, width, height, Color.White), 0, 4, 5, 2, true, true);
            Speed = 7;
            SetState(Horsestate.Afk);
            Direction = Direction.Left;
            Owner = null;
            Tooltip = new Text(FontType.Default, "Press 'E' to mount", new Vector2(x + 50, y - 65), Color.Black, true, Color.Gold);
            Tooltip.SetVisible(false);
        }

        public void Move(Direction direction) {

            switch (direction) {
                case Direction.Up:
                    SetRectangle(new Rectangle(Rectangle.X, Rectangle.Y - Speed, Rectangle.Width, Rectangle.Height));
                    break;
                case Direction.Down:
                    SetRectangle(new Rectangle(Rectangle.X, Rectangle.Y + Speed, Rectangle.Width, Rectangle.Height));
                    break;
                case Direction.Right:
                    SetRectangle(new Rectangle(Rectangle.X + Speed, Rectangle.Y, Rectangle.Width, Rectangle.Height));
                    break;
                case Direction.Left:
                    SetRectangle(new Rectangle(Rectangle.X - Speed, Rectangle.Y, Rectangle.Width, Rectangle.Height));
                    break;
            }
        }

        public void SetState(Horsestate state) {

            State = state;
            switch (State) {
                case Horsestate.Afk:
                    CurrentAnimation = AfkAnimation;
                    break;
                case Horsestate.Walk:
                    CurrentAnimation = WalkAnimation;
                    break;
            }
            CurrentAnimation.Start();
        }

        public void Update() {
            CurrentAnimation.Play();
        }

        public TeamName GetTeamName() {
            return TeamName;
        }

        public int GetSpeed() {
            return Speed;
        }

        public void SetDirection(Direction newDirection) {

            Direction = newDirection;
            WalkAnimation.SetDirection(newDirection);
            AfkAnimation.SetDirection(newDirection);
            CurrentAnimation.SetDirection(newDirection);
        }

        public void SetRectangle(Rectangle newRectangle) {

            Rectangle.X = newRectangle.X;
            Rectangle.Y = newRectangle.Y;
            Rectangle.Width = newRectangle.Width;
            Rectangle.Height = newRectangle.Height;

            WalkAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            AfkAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            CurrentAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
        }

        public Rectangle GetRectangle() {
            return Rectangle;
        }

        public Horsestate GetState() {
            return State;
        }

        public void SetOwner(Player player) {
            Owner = player;
        }

        public void RemoveOwner() {
            Owner = null;
            SetState(Horsestate.Afk);
        }

        public bool IsHasOwner() {
            return Owner != null;
        }

        public bool IsOnTopMap(Map map) {
            if (Rectangle.Bottom - Rectangle.Height / 2 < map.GetGrass().GetRectangle().Top)
                return true;

            return false;
        }

        public bool IsOnLeftMap() {
            if (Rectangle.Left <= 0)
                return true;

            return false;
        }

        public bool IsOnRightMap() {
            if (Rectangle.Right >= Map.WIDTH)
                return true;

            return false;
        }

        public bool IsOnBottomMap() {
            if (Rectangle.Bottom > Map.HEIGHT)
                return true;

            return false;
        }

        public Text GetTooltip() {
            return Tooltip;
        }
        public void Draw(int i) {

            if (CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Bottom - 10 == i) {
                Tooltip.Draw();
                CurrentAnimation.Draw();
            }
        }
    }
}
