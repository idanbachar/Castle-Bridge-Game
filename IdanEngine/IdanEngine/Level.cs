using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Level {

        private MapName MapName;
        private int LevelIndex;

        private int [,] LevelMatrix;
        private Animation [,] Objects;
        public Level(int [,] levelMatrix, MapName mapName, int levelIndex) {

            MapName = mapName;
            LevelIndex = levelIndex;
            Generate(levelMatrix);
        }

        public void Generate(int [,] levelMatrix) {
            LevelMatrix = levelMatrix;
            Objects = new Animation [levelMatrix.GetLength(0), levelMatrix.GetLength(1)];

            int objectWidth = 100;
            int objectHeight = 100;

            for (int y = 0; y < Objects.GetLength(1); y++) {
                for (int x = 0; x < Objects.GetLength(0); x++) {
                    Objects [x, y] = new Animation(new Image("map/" + MapName + "/level_" + LevelIndex + "/grass",
                                        ((LevelEntity)LevelMatrix [x, y]).ToString() + "_", 250 + y * objectWidth, 250 + x * objectHeight, objectWidth, objectHeight, Color.White), 0, 0, 1);
                }
            }
        }
 

        public void Draw() {

            foreach (Animation levelObject in Objects)
                levelObject.Draw();

        }
    }
}
