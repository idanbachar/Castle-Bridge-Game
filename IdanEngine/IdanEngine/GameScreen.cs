using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class GameScreen: Screen {

        private List<Text> Labels;

        public GameScreen(): base() {
            Labels = new List<Text>();
            Labels.Add(new Text(FontType.Default, "Hello", new Vector2(100, 200), Color.White, true, Color.Red));
        }

        private void DrawLabels() {

            foreach (Text gameLabel in Labels)
                gameLabel.Draw();
        }



        public override void Update() {
 
        }

        public override void Draw() {
            DrawLabels();
        }
    }
}
