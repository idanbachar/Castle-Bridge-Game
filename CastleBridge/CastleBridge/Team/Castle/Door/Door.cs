using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Door {

        private Image Image;
        private Text Tooltip;
        private Location CurrentLocation;
        public Door(int x, int y, int width, int height, TeamName teamName, Location location) {
            Image = new Image("map/castles/teams/" + teamName + "/outside/door", "door", x, y, width, height, Color.White);
            Tooltip = new Text(FontType.Default, string.Empty, new Vector2(x , y - 170), Color.Black, true, Color.Gold);
            CurrentLocation = location;

            switch (CurrentLocation) {
                case Location.Inside_Red_Castle:
                case Location.Inside_Yellow_Castle:
                    Tooltip.ChangeText("Press 'E' to exit castle.");
                    break;
                case Location.Outside:
                    Tooltip.ChangeText("Press 'E' to enter castle.");
                    break;
            }
            
            Tooltip.SetVisible(false);
        }
        public Text GetTooltip() {
            return Tooltip;
        }

        public Image GetImage() {
            return Image;
        }

        public Location GetCurrentLocation() {
            return CurrentLocation;
        }

        public void Draw() {
            Image.Draw();
            Tooltip.Draw();
        }
    
    }
}
