using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public abstract class Menu {

        private Text Title;
        protected Image Grass;
        protected Weather Weather;
        protected TeamName SelectedTeam;
        
        public Menu(string title) {
            Title = new Text(FontType.Default, title, new Vector2(0, 0), Color.Gold, true, Color.Black);
            Grass = new Image("map/forest/grass", 0, CastleBridge.Graphics.PreferredBackBufferHeight / 2 + 100, CastleBridge.Graphics.PreferredBackBufferWidth, CastleBridge.Graphics.PreferredBackBufferHeight / 2);
            Weather = new Weather(TimeType.Day, true, CastleBridge.Graphics.PreferredBackBufferWidth, 15);
        }

        public abstract void Update();

        public TeamName GetSelectedTeam() {
            return SelectedTeam;
        }

        public virtual void Draw() {
            Weather.DrawStuck();
            Weather.DrawClouds();
            Grass.Draw();
            Title.Draw();
        }

    }
}
