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

        public Archer(CharacterName name, Team team, int x, int y, int width, int height) : base(name, team, x, y, width, height) {

            Arrows = new List<Arrow>();
            MaxArrows = 7;
            CurrentArrows = MaxArrows;
            Health = 110;
        }

        public void ShootArrow(Direction shootDirection) {

            Arrows.Add(new Arrow(CurrentAnimation.GetCurrentSprite().GetRectangle().Left + CurrentAnimation.GetCurrentSprite().GetRectangle().Width / 2,
                                    CurrentAnimation.GetCurrentSprite().GetRectangle().Top + CurrentAnimation.GetCurrentSprite().GetRectangle().Height / 2, Direction, shootDirection));

            CurrentArrows--;
        }

        public bool IsCanShoot() {
            return CurrentArrows > 0;
        }

        public void AddArrow() {
            CurrentArrows++;
        }
 
        public void DrawArrows(int i) {

            foreach (Arrow arrow in Arrows)
                if (arrow.Animation.GetCurrentSprite().GetRectangle().Bottom == i)
                    arrow.Draw();
        }
 

        public List<Arrow> GetArrows() {
            return Arrows;
        }
 
    }
}
