using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Player {

        private Text Name;
        public Character CurrentCharacter;
        private Dictionary<string, Character> Characters;
        private Rectangle Rectangle;
        private int Speed;
        private PlayerState State;

        public Player(CharacterName character, string name, int x, int y, int width, int height){

            Name = new Text(FontType.Default, name, new Vector2(0, 0), Color.White, true, Color.Green);
            Rectangle = new Rectangle(x, y, width, height);
            Speed = 4;
            Characters = new Dictionary<string, Character>();
            State = PlayerState.Afk;
            CurrentCharacter = AddCharacter(character);
            
            Init();
        }

        public void Init() {

        }

        private Character AddCharacter(CharacterName name) {

            Character character = new Character(name, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            Characters.Add("Archer", character);

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

        public void SetState(PlayerState state) {

            State = state;

            CurrentCharacter.SetCurrentAnimation(state);


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
        }

        public Rectangle GetRectangle() {
            return Rectangle;
        }

        public PlayerState GetState() {
            return State;
        }

        public void Draw() {
            CurrentCharacter.Draw();
        }
    }
}
