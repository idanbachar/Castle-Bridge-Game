using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class LoadingMenu : Menu {

        private Text Text;

        public LoadingMenu(string title): base(title) {

            Text = new Text(FontType.Default, "Loading Game", new Vector2(CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 100, CastleBridge.Graphics.PreferredBackBufferHeight / 2 - 100), Color.Black, false, Color.Red);
        
        }

        public void UpdateText(int current, int max) {

            double percent = ((double)current / (double)max) * 100;

            Text.ChangeText("Downloading map data: (" + percent + "%)");
        }
 
        public override void Update() {

 
        }
 
        public Text GetText() {
            return Text;
        }

        public override void Draw() {
            base.Draw();

            Text.Draw();
        }

    }
}
