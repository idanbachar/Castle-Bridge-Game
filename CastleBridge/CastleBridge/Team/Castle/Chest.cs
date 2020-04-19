using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Chest {

        private Image Image;
        private TeamName TeamName;
        private Player Owner;

        public Chest(TeamName teamName, int x, int y) {
            TeamName = teamName;
            Image = new Image(string.Empty, string.Empty, x, y, 150, 50, Color.White);
            Owner = null;
        }

        public TeamName GetTeam() {
            return TeamName;
        }

        public void SetTeam(TeamName team) {
            TeamName = team;
        }

        public Player GetOwner() {
            return Owner;
        }

        public void SetOwner(Player player) {
            Owner = player;
        }

        public void Draw() {
            Image.Draw();
        }

    }
}
