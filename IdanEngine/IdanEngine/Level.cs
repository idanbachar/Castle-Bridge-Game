using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Level {

        private const int OBJECT_WIDTH = 100;
        private const int OBJECT_HEIGHT = 100;

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

            for (int y = 0; y < Objects.GetLength(1); y++) {
                for (int x = 0; x < Objects.GetLength(0); x++) {
                    Objects [x, y] = new Animation(new Image("map/" + MapName + "/level_" + LevelIndex + "/" + (LevelEntity)LevelMatrix [x, y],
                                        ((LevelEntity)LevelMatrix [x, y]).ToString() + "_", y * OBJECT_WIDTH, x * OBJECT_HEIGHT, OBJECT_WIDTH, OBJECT_HEIGHT, Color.White), 0, 0, 1);


                    if ((LevelEntity)LevelMatrix [x, y] == LevelEntity.None) {
                        Objects [x, y].SetVisible(false);
                    }
                }
            }
        }
 
        public int GetHeight() {
            return Objects.GetLength(0) * OBJECT_HEIGHT;
        }

        public int GetWidth() {
            return Objects.GetLength(1) * OBJECT_WIDTH;
        }

        public void Draw() {

            foreach (Animation levelObject in Objects)
                levelObject.Draw();

        }
    }
}
