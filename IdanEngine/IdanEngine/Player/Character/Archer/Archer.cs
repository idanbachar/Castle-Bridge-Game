using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Archer: Character {

        private List<Arrow> Arrows;
        public int MaxArrows;
        public int CurrentArrows;

        public Archer(CharacterName name, int x, int y, int width, int height) : base(name, x, y, width, height) {

            Arrows = new List<Arrow>();
            MaxArrows = 50;
            CurrentArrows = MaxArrows;
            Health = 110;
        }

        public void ShootArrow(Direction shootDirection) {

            if (CurrentArrows > 0) {
                AddArrow(shootDirection);
                CurrentArrows--;
            }
        }

        private void AddArrow(Direction shootDirection) {
 
            Arrows.Add(new Arrow(CurrentAnimation.GetCurrentSprite().GetRectangle().Left + CurrentAnimation.GetCurrentSprite().GetRectangle().Width / 2,
                                    CurrentAnimation.GetCurrentSprite().GetRectangle().Top + CurrentAnimation.GetCurrentSprite().GetRectangle().Height / 2, Direction, shootDirection));
        }

        private void DrawArrows() {

            foreach (Arrow arrow in Arrows)
                arrow.Draw();
        }

        private void UpdateArrows() {

            for (int i = 0; i < Arrows.Count; i++) {
                if (!Arrows [i].IsFinished)
                    Arrows [i].Move();
            }
        }

        public override void Update() {
            base.Update();

            UpdateArrows();
        }

        public override void Draw() {
            base.Draw();

            DrawArrows();
        }
    }
}
