using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class MapEntity {

        private MapEntityName Name;
        public Animation Animation;
        public Text DisplayedText;
        public bool IsTouchable;

        public MapEntity(MapEntityName entityName, MapName mapName, int x, int y, int width, int height, bool isTouchable) {
            Name = entityName;

            IsTouchable = isTouchable;
            Animation = new Animation(new Image("map/" + mapName + "/" + entityName, entityName + "_", x, y, width, height, Color.White), 0, 0, 1, 5, true, true);
            Animation.Start();

            DisplayedText = new Text(FontType.Default, string.Empty, new Vector2(x, y - 25), Color.Blue, true, Color.Green);
            DisplayedText.SetVisible(false);

            switch (entityName) {
                case MapEntityName.Red_Flower:
                    DisplayedText.SetText("Press 'E' to eat" +
                        "\n" +
                        "(+15hp)");
                    break;
                case MapEntityName.Stone:
                    DisplayedText.SetText("Press 'E' to take" +
                        "\n" +
                        "(+1 stone)");
                    break;
                case MapEntityName.Tree:
                    DisplayedText.SetText("Press 'E' to cut" +
                        "\n" +
                        "(+5 woods)");
                    break;
            }

        }

        public void Update() {
            Animation.Play();
        }

        public void Draw() {

            Animation.Draw();

            DisplayedText.Draw();
        }
    }
}
