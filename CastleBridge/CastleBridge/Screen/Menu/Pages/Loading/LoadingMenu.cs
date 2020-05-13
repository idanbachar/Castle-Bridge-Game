using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class LoadingMenu : Menu {

        public Text Text;

        public LoadingMenu(string title): base(title) {

            Text = new Text(FontType.Default, "Loading Game", new Vector2(200, 200), Color.Black, false, Color.Red);

        }
 
        public override void Update() {

 
        }
 

        public override void Draw() {
            base.Draw();

            Text.Draw();
        }

    }
}
