using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Diamond {

        private Image Image; //Diamond's image
        private TeamName TeamName; //Diamon'ds team
        private Player Owner; //Diamond's player owner
        private Location CurrentLocation; //Diamond's current location

        /// <summary>
        /// Receives team, coordinates and location
        /// and creates a diamond
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="location"></param>
        public Diamond(TeamName teamName, int x, int y, Location location) {
            TeamName = teamName;
            Image = new Image("map/castles/teams/" + TeamName + "/inside/diamond/diamond", x, y, 75, 75);
            Owner = null;
            CurrentLocation = location;
        }

        /// <summary>
        /// Get team
        /// </summary>
        /// <returns></returns>
        public TeamName GetTeam() {
            return TeamName;
        }

        /// <summary>
        /// Receives a team and applies it
        /// </summary>
        /// <param name="team"></param>
        public void SetTeam(TeamName team) {
            TeamName = team;
        }

        /// <summary>
        /// Receives a new rectangle and applies it
        /// </summary>
        /// <param name="newRectangle"></param>
        public void SetRectangle(Rectangle newRectangle) {
            Image.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
        }


        /// <summary>
        /// Receives a new location and applies it
        /// </summary>
        /// <param name="newLocation"></param>
        public void ChangeLocationTo(Location newLocation) {
            CurrentLocation = newLocation;
        }

        /// <summary>
        /// Get player owner
        /// </summary>
        /// <returns></returns>
        public Player GetOwner() {
            return Owner;
        }

        /// <summary>
        /// Receives a player and applies it
        /// </summary>
        /// <param name="player"></param>
        public void SetOwner(Player player) {
            Owner = player;
        }

        /// <summary>
        /// Removes player owner (sets owner to null)
        /// </summary>
        public void RemoveOwner() {
            Owner = null;
        }

        /// <summary>
        /// Get image
        /// </summary>
        /// <returns></returns>
        public Image GetImage() {
            return Image;
        }

        /// <summary>
        /// Get current location
        /// </summary>
        /// <returns></returns>
        public Location GetCurrentLocation() {
            return CurrentLocation;
        }

        /// <summary>
        /// Draw diamond
        /// </summary>
        public void Draw() {

            //Draw diamond only if there is no owner:
            if (Owner == null)
                Image.Draw();
        }

    }
}
