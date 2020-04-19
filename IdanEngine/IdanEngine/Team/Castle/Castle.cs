using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Castle {

        private Image Image;
        private TeamName TeamName;
        private List<Chest> Chests;

        public Castle(TeamName teamName, int x, int y) {
            TeamName = teamName;
            Image = new Image("map/castles/teams/" + teamName + "/outside", "castle", x, y, 1400, 600, Color.White);
            Chests = new List<Chest>();
        }

        public TeamName GetTeam() {
            return TeamName;
        }

        public List<Chest> GetChests() {
            return Chests;
        }

        public void Draw() {
            Image.Draw();
        }
    }
}
