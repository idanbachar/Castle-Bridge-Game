using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class MapEntity {

        private MapEntityName Name;
        private Animation Animation;

        public MapEntity(MapEntityName entityName, MapName mapName, int x, int y, int width, int height) {
            Name = entityName;
            Animation = new Animation(new Image("map/" + mapName + "/" + entityName, entityName + "_", x, y, width, height, Color.White), 0, 0, 1, 5, true, true);
            Animation.Start();
        }

        public void Update() {
            Animation.Play();
        }

        public void Draw() {
            Animation.Draw();
        }
    }
}
