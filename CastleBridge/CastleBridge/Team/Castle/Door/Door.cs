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
        public Door(int x, int y, int width, int height, TeamName teamName) {
            Image = new Image("map/castles/teams/" + teamName + "/outside/door", "door", x, y, width, height, Color.White);
            Tooltip = new Text(FontType.Default, "Press 'E' to enter.", new Vector2(x , y - 170), Color.Black, true, Color.Gold);
            Tooltip.SetVisible(false);

            CurrentLocation = Location.Outside;
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
