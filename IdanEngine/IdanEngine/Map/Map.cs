using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Map {

        private MapName Name;
        public Image Grass;
        public Image Sun;
        public Image Sky;
        public Image Castle;
        private List<Cloud> Clouds;
        public static int WIDTH;
        public static int HEIGHT;
        public List<MapEntity> WorldEntities;
        public List<Arrow> UsedArrows;

        public Map() {

            Name = MapName.Forest;
            WIDTH = 10000;
            HEIGHT = 2000;
            UsedArrows = new List<Arrow>();
            Init();
        }

        private void Init() {

            InitGrass();
            InitClouds();
            InitWorldEntities();
        }

        public void Update(Player player) {

            foreach (Cloud cloud in Clouds)
                cloud.Update();

            foreach (MapEntity mapEntity in WorldEntities)
                    mapEntity.DisplayedText.SetVisible(player.IsTouchWorldEntity(mapEntity));
        }

        private void InitGrass() {
            Grass = new Image("map/" + Name, "grass", 0, HEIGHT / 5, WIDTH, HEIGHT, Color.White);
        }

        private void InitClouds() {

            Clouds = new List<Cloud>();
 
            for(int i = 0; i < 50; i++) {

                GenerateCloud();
            }
        }

        private void InitWorldEntities() {

            WorldEntities = new List<MapEntity>();
            Random rnd = new Random();
            for (int i = 1; i < 170; i++) {

                MapEntityName entity = (MapEntityName)rnd.Next(0, 5);
                GenerateWorldEntity(entity);
            }
 

            for (int i = 0; i < 60; i++) {
                WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, (i * 200) + 25, Grass.GetRectangle().Top - 250, 200, 250, false));
            }
            for (int i = 0; i < 150; i++) {
                WorldEntities.Add(new MapEntity(MapEntityName.Ground_Leaves, MapName.Forest, (i * 65), Grass.GetRectangle().Top - 60, 75, 75, false));
            }

 

            Sun = new Image("map/sun", "sun_0", Game1.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);
            Sky = new Image("map/sky", "sky", 0, 0, Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight, Color.White);
            Castle = new Image("map/castles/teams/red/outside", "castle", 300, Grass.GetRectangle().Top - 390, 1400, 600, Color.White);
        }

        private void GenerateCloud() {

            Random rnd = new Random();

            Clouds.Add(new Cloud(rnd.Next(0, WIDTH), rnd.Next(0, 200), 125, 75));
        }

        private void GenerateWorldEntity(MapEntityName name) {

            int grassX = Grass.GetRectangle().Left;
            int grassY = Grass.GetRectangle().Top;

            int width = 0;
            int height = 0;

            bool isTouchable = false;

            Random rnd = new Random();
            int x = rnd.Next(grassX, WIDTH);
            int y = rnd.Next(grassY, HEIGHT);

            switch (name) {
                case MapEntityName.Bush:
                    width = 100;
                    height = 45;
                    isTouchable = false;
                    break;
                case MapEntityName.Red_Flower:
                    width = 36;
                    height = 36;
                    isTouchable = true;
                    break;
                case MapEntityName.Tree:
                    width = 400;
                    height = 500;
                    isTouchable = false;
                    break;
                case MapEntityName.Stone:
                    width = 50;
                    height = 50;
                    isTouchable = true;
                    break;
            }

            WorldEntities.Add(new MapEntity(name, Name, x , y + height, width, height, isTouchable));
        }

        public void AddUsedArrows(Arrow arrow) {
            UsedArrows.Add(arrow);
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


            Castle.Draw();
        }

        public void DrawStuck() {
            Sky.Draw();
            Sun.Draw();
        }
    
    }
}
