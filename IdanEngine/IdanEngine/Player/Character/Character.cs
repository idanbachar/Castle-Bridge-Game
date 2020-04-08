using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Character {

        private CharacterName Name;
        private Animation CurrentAnimation;

        private Animation AfkAnimation;
        private Animation WalkAnimation;
        private Animation AttackAnimation;
        private Animation DefenceAnimation;
        public Character(CharacterName name, int x, int y, int width, int height) {
            Name = name;
            AfkAnimation = new Animation(new Image("player/characters/" + name + "/afk/", name + "_afk_", x, y, width, height, Color.White), 0, 6, 6, true, true);
            CurrentAnimation = AfkAnimation;
            CurrentAnimation.Start();
        }

        public void SetCurrentAnimation(string currentAnimation) {

            switch (currentAnimation) {
                case "afk":
                    CurrentAnimation = AfkAnimation;
                    break;
                case "walk":
                    CurrentAnimation = WalkAnimation;
                    break;
                case "attack":
                    CurrentAnimation = AttackAnimation;
                    break;
                case "defence":
                    CurrentAnimation = DefenceAnimation;
                    break;
            }
        }

        public void SetNewDirection(Direction newDirection) {

            CurrentAnimation.SetDirection(newDirection);

        }

        public void Update() {
            CurrentAnimation.Play();
        }

        public void SetNewRectangle(Rectangle newRectangle) {

            CurrentAnimation.SetNewRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
 
        }

        public void Draw() {
            CurrentAnimation.Draw();
        }
    }
}
