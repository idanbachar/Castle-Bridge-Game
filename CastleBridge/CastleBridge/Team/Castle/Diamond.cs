using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Diamond {

        private Image Image;
        private TeamName TeamName;
        private Player Owner;
        private Location CurrentLocation;

        public Diamond(TeamName teamName, int x, int y, Location location) {
            TeamName = teamName;
            Image = new Image("map/castles/teams/" + TeamName + "/inside/diamond/diamond", x, y, 75, 75);
            Owner = null;
            CurrentLocation = location;
        }

        public TeamName GetTeam() {
            return TeamName;
        }

        public void SetTeam(TeamName team) {
            TeamName = team;
        }

        public void SetRectangle(Rectangle newRectangle) {
            Image.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
        }

        public void ChangeLocationTo(Location newLocation) {
            CurrentLocation = newLocation;
        }

        public Player GetOwner() {
            return Owner;
        }

        public void SetOwner(Player player) {
            Owner = player;
        }

        public void RemoveOwner() {
            Owner = null;
        }

        public Image GetImage() {
            return Image;
        }

        public Location GetCurrentLocation() {
            return CurrentLocation;
        }

        public void Draw() {

            if (Owner == null)
                Image.Draw();
        }

    }
}
