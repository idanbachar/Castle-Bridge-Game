using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class HUD {

        private List<Text> Labels;
        public HUD() {

            Labels = new List<Text>();
        }

        public void AddLabel(Text text) {
            Labels.Add(text);
        }

        public List<Text> GetLabels() {
            return Labels;
        }

        public void Draw() {

            foreach (Text label in Labels)
                label.Draw();
        }
    
    }
}
