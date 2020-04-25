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
        private List<Diamond> Diamonds;
        private Door OutsideDoor;
        private Door InsideDoor;
        private Location CurrentLocation;
        private Image InsideWall;
        private Image InsideFloor;

        public Castle(TeamName teamName, int x, int y) {
            TeamName = teamName;
            Image = new Image("map/castles/teams/" + teamName + "/outside", "castle", x, y, 1400, 431, Color.White);
            OutsideDoor = new Door(x + 639, y + 288, 120, 120, teamName, Location.Outside);
            InsideWall = new Image("map/castles/teams/" + teamName + "/inside/castle_wall", 0, y, 1400, 431);
            InsideDoor = new Door(614, 303, 188, 107, teamName, teamName == TeamName.Red ? Location.Inside_Red_Castle : Location.Inside_Yellow_Castle);
            InsideFloor = new Image("map/castles/teams/" + teamName + "/inside/floor/castle_floor", 0, CastleBridge.Graphics.PreferredBackBufferHeight / 2, CastleBridge.Graphics.PreferredBackBufferWidth, CastleBridge.Graphics.PreferredBackBufferHeight);
            InitDiamonds();
            CurrentLocation = Location.Outside;
        }

        private void AddDiamond(int x, int y) {
            switch (TeamName) {
                case TeamName.Red:
                    Diamonds.Add(new Diamond(TeamName, x, y, Location.Inside_Red_Castle));
                    break;
                case TeamName.Yellow:
                    Diamonds.Add(new Diamond(TeamName, x, y, Location.Inside_Yellow_Castle));
                    break;
            }
        }

        private void InitDiamonds() {

            Diamonds = new List<Diamond>();
            for(int i = 1; i <= 3; i++)
                AddDiamond(100, InsideFloor.GetRectangle().Top - 10 + (i * 100));
        }

        public TeamName GetTeam() {
            return TeamName;
        }

        public List<Diamond> GetDiamonds() {
            return Diamonds;
        }

        public Door GetOutsideDoor() {
            return OutsideDoor;
        }

        public Door GetInsideDoor() {
            return InsideDoor;
        }

        public Location GetCurrentLocation() {
            return CurrentLocation;
        }

        public void ChangeLocationTo(Location newLocation) {
            CurrentLocation = newLocation;
        }

        public void DrawOutside() {
            Image.Draw();
            OutsideDoor.Draw();
        }
 
        public void DrawInside() {
            InsideFloor.Draw();
            InsideWall.Draw();
            InsideDoor.Draw();
        }
    }
}
