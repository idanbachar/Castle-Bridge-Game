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
        private List<Cloud> Clouds;
        public static int WIDTH;
        public static int HEIGHT;

        public Map() {

            Name = MapName.Forest;
            WIDTH = 2000;
            HEIGHT = 1000;

            Init();
        }

        private void Init() {

            InitGrass();
            InitClouds();
        }

        public void Update() {

            foreach (Cloud cloud in Clouds)
                cloud.Update();
        }

        private void InitGrass() {
            Grass = new Image("map/" + Name, "grass", 0, Game1.Graphics.PreferredBackBufferHeight / 2, Game1.Graphics.PreferredBackBufferWidth * 2, Game1.Graphics.PreferredBackBufferHeight, Color.White);
        }

        private void InitClouds() {

            Clouds = new List<Cloud>();
 
            for(int i = 0; i < 25; i++) {

                GenerateCloud();
            }
        }

        private void GenerateCloud() {
            Random rnd = new Random();

            Cloud cloud = new Cloud(rnd.Next(0, WIDTH * 2), rnd.Next(0, 200), 125, 75);
            Clouds.Add(cloud);
        }

        public void Draw() {
            Grass.Draw();

            foreach (Cloud cloud in Clouds)
                cloud.Draw();
        }
    
    }
}
