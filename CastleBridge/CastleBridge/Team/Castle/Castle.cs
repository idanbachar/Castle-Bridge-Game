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
        private Door Door;
        private Location CurrentLocation;

        public Castle(TeamName teamName, int x, int y) {
            TeamName = teamName;
            Image = new Image("map/castles/teams/" + teamName + "/outside", "castle", x, y, 1400, 431, Color.White);
            Chests = new List<Chest>();
            Door = new Door(x + 639, y + 288, 120, 120, teamName);

            CurrentLocation = Location.Outside;
        }

        public TeamName GetTeam() {
            return TeamName;
        }

        public List<Chest> GetChests() {
            return Chests;
        }

        public Door GetDoor() {
            return Door;
        }

        public Location GetCurrentLocation() {
            return CurrentLocation;
        }

        public void Draw() {
            Image.Draw();
            Door.Draw();
        }
    }
}
