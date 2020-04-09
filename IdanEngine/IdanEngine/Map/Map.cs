using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Map {

        private MapName Name;
        public Image Grass;
        public Image Sun;
        public Image Sky;
        private List<Cloud> Clouds;
        public static int WIDTH;
        public static int HEIGHT;
        public List<MapEntity> WorldEntities;

        public Map() {

            Name = MapName.Forest;
            WIDTH = 10000;
            HEIGHT = 2000;

            Init();
        }

        private void Init() {

            InitGrass();
            InitClouds();
            InitWorldEntities();
        }

        public void Update() {

            foreach (Cloud cloud in Clouds)
                cloud.Update();
        }

        private void InitGrass() {
            Grass = new Image("map/" + Name, "grass", 0, HEIGHT / 5, WIDTH, HEIGHT, Color.White);
        }

        private void InitClouds() {

            Clouds = new List<Cloud>();
 
            for(int i = 0; i < 100; i++) {

                GenerateCloud();
            }
        }

        private void InitWorldEntities() {

            int grassX = Grass.GetRectangle().X;
            int grassY = Grass.GetRectangle().Y;

            WorldEntities = new List<MapEntity>();
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 635, 1257, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 1435, 509, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 1827, 845, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 809, 785, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, -43, 561, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 1329, 541, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 393, 529, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 993, 785, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 1717, 613, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 13, 1157, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 2133, 909, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 3513, 809, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 4257, 1161, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 5429, 881, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 1373, 713, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 2181, 1329, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 2425, 1429, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 3413, 785, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 4869, 661, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 5477, 1245, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 6661, 541, 50, 50));


            for (int i = 0; i < 60; i++) {
                WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, (i * 200) + 25, grassY - 250, 200, 250));
            }
            for (int i = 0; i < 150; i++) {
                WorldEntities.Add(new MapEntity(MapEntityName.Ground_Leaves, MapName.Forest, (i * 65), grassY - 60, 75, 75));
            }


            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 6989, 1257, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 5042, 509, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 7532, 845, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 6433, 785, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Bush, MapName.Forest, 9012, 561, 100, 45));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 8432, 541, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 5674, 529, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 5532, 785, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 8832, 613, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Red_Flower, MapName.Forest, 6643, 1157, 36, 36));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 5019, 909, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 9942, 809, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 8532, 1161, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, 8001, 881, 400, 500));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 7543, 713, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 6942, 1329, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 9931, 1429, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 8821, 785, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 6664, 661, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 7777, 1245, 50, 50));
            WorldEntities.Add(new MapEntity(MapEntityName.Stone, MapName.Forest, 4523, 541, 50, 50));

            Sun = new Image("map/sun", "sun_0", Game1.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);
            Sky = new Image("map/sky", "sky", 0, 0, Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight, Color.White);
        }

        private void GenerateCloud() {
            Random rnd = new Random();

            Cloud cloud = new Cloud(rnd.Next(0, WIDTH * 2), rnd.Next(0, 200), 125, 75);
            Clouds.Add(cloud);
        }

        public bool IsOnTopMap(Player player) {
            if (player.GetRectangle().Bottom - player.GetRectangle().Height / 2 < Grass.GetRectangle().Top)
                return true;

            return false;
        }

        public void DrawTile() {

            Grass.Draw();

            foreach (Cloud cloud in Clouds)
                cloud.Draw();

            foreach (MapEntity mapEntity in WorldEntities)
                mapEntity.Draw();
        }

        public void DrawStuck() {
            Sky.Draw();
            Sun.Draw();
        }
    
    }
}
