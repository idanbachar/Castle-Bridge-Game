using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Mage: Character {

        private List<EnergyBall> Spells;
        public int MaxSpells;
        public int CurrentSpells;

        public Mage(CharacterName name, TeamName teamName, int x, int y, int width, int height) : base(name, teamName, x, y, width, height) {

            Spells = new List<EnergyBall>();
            MaxSpells = 999;
            CurrentSpells = MaxSpells;
            Health = 110;
        }

        public void ShootSpell(Direction shootDirection, Location currentLocation) {

            Spells.Add(new EnergyBall(CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Left + CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Width / 2,
                                    CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Top + CurrentAnimation.GetCurrentSpriteImage().GetRectangle().Height / 2, Direction, shootDirection, currentLocation));

            CurrentSpells--;
        }

        public bool IsCanShoot() {
            return CurrentSpells > 0;
        }

        public void AddSpell() {
            CurrentSpells++;
        }

        public List<EnergyBall> GetSpells() {
            return Spells;
        }

    }
}
