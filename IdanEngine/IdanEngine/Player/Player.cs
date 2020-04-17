using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Player {

        private Text Name;
        public Character CurrentCharacter;
        private List<Character> Characters;
        private Rectangle Rectangle;
        private int Speed;
        private PlayerState State;
        private int Stones;
        private int Woods;
        private Team Team;
        public Player(CharacterName character, Team team, string name, int x, int y, int width, int height){

            Name = new Text(FontType.Default, name, new Vector2(0, 0), Color.White, true, Color.Red);
            Team = team;
            Rectangle = new Rectangle(x, y, width, height);
            Speed = 4;
            Characters = new List<Character>();
            State = PlayerState.Afk;
            CurrentCharacter = AddCharacter(character);
            Stones = 0;
            Woods = 0;
        }

        public void ChangeCharacter(CharacterName newCharacter) {

            CurrentCharacter = AddCharacter(newCharacter);

        }

        private Character AddCharacter(CharacterName name) {

            Character character = null;

            switch (name) {
                case CharacterName.Archer:
                    character = new Archer(name, Team, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    break;
                case CharacterName.Knight:
                    character = new Knight(name, Team, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    break;
                case CharacterName.Mage:
                    character = new Mage(name, Team, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    break;
            }

            Characters.Add(character);
            return character;
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

        public bool IsTouchWorldEntity(MapEntity entity) {

            if (Rectangle.Intersects(entity.Animation.GetCurrentSprite().GetRectangle()) && entity.IsTouchable)
                return true;

            return false;
        }

        public bool IsTouchFallenArrow(Arrow arrow) {

            if (Rectangle.Intersects(arrow.Animation.GetCurrentSprite().GetRectangle()) && arrow.IsFinished)
                return true;

            return false;
        }

        public void SetState(PlayerState state) {

            State = state;
            CurrentCharacter.SetCurrentAnimation(state);
        }

        public Team GetTeam() {
            return Team;
        }

        public void Update() {

            CurrentCharacter.Update();

        }

        public void SetDirection(Direction newDirection) {

            CurrentCharacter.SetNewDirection(newDirection);

        }

        public void SetRectangle(Rectangle newRectangle) {

            Rectangle.X = newRectangle.X;
            Rectangle.Y = newRectangle.Y;
            Rectangle.Width = newRectangle.Width;
            Rectangle.Height = newRectangle.Height;

            CurrentCharacter.SetNewRectangle(newRectangle);
            Name.SetPosition(new Vector2(newRectangle.X, newRectangle.Y - 12));
        }

        public Rectangle GetRectangle() {
            return Rectangle;
        }

        public PlayerState GetState() {
            return State;
        }
        public bool IsOnTopMap(Map map) {
            if (Rectangle.Bottom - Rectangle.Height / 2 < map.Grass.GetRectangle().Top)
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

        public int GetStones() {
            return Stones;
        }

        public void AddStones(int stones) {
            Stones += stones;
        }

        public int GetWoods() {
            return Woods;
        }

        public void SetWoods(int woods) {
            Woods += woods;
        }

        public void Draw(int i) {

            if (CurrentCharacter.GetCurrentAnimation().GetCurrentSprite().GetRectangle().Bottom - 10 == i) {
                Name.Draw();
                CurrentCharacter.Draw();
            }
        }
    }
}
