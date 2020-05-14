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

        public Archer(CharacterName name, TeamName teamName, int x, int y, int width, int height) : base(name, teamName, x, y, width, height) {

            Arrows = new List<Arrow>();
            MaxArrows = 999;
            CurrentArrows = MaxArrows;
            Health = 110;
        }

        public void ShootArrow(Direction shootDirection, Location currentLocation, Player owner) {

            Arrows.Add(new Arrow(CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Left + CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Width / 2,
                                    CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Top + CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Height / 2, Direction, shootDirection, currentLocation, owner));

            CurrentArrows--;
        }

        public bool IsCanShoot() {
            return CurrentArrows > 0;
        }

        public void AddArrow() {
            CurrentArrows++;
        }

        public List<Arrow> GetArrows() {
            return Arrows;
        }
 
    }
}
