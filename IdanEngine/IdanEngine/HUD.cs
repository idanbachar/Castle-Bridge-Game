using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class HUD {

        private List<Text> Labels;
        private Image PlayerAvatar;
        public HUD() {

            Labels = new List<Text>();
            PlayerAvatar = new Image(string.Empty, string.Empty, 0, 0, 100, 100, Color.White);
        }

        public void AddLabel(Text text) {
            Labels.Add(text);
        }

        public void SetPlayerAvatar(CharacterName name) {
            PlayerAvatar.SetNewImage("player/characters/" + name + "/avatar/" + name + "_avatar");
        }

        public List<Text> GetLabels() {
            return Labels;
        }

        public void Draw() {

            foreach (Text label in Labels)
                label.Draw();


            PlayerAvatar.Draw();
        }
    
    }
}
