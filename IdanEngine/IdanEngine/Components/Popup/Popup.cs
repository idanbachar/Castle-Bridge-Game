using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Popup {

        public Text Text;
        private int ShowTimer;
        public bool IsFinished;

        public Popup(string text, int x, int y, Color textColor, Color backgroundColor) {
            Text = new Text(FontType.Default, text, new Vector2(x, y), textColor, true, backgroundColor);
            ShowTimer = 0;
            IsFinished = false;
        }

        public void Update() {

            if (!IsFinished) {
                if (ShowTimer < 35) {
                    ShowTimer++;
                    Text.SetPosition(new Vector2(Text.GetPosition().X, Text.GetPosition().Y - 3));
                }
                else {
                    ShowTimer = 0;
                    IsFinished = true;
                }
            }
        }

        public void Draw() {
            Text.Draw();
        }
    
    }
}
