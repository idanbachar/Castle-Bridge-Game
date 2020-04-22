using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Weather {

        private TimeType TimeType;
        private Image Sun;
        private Image Moon;
        private Image CurrentPlanet;
        private Image Sky;
        private List<Cloud> Clouds;
        private Random Rnd;
        private bool IsRain;
        public Weather(TimeType timeType, bool isRain) {

            TimeType = timeType;
            IsRain = isRain;
            Init();
        }

        public void Update() {

            foreach (Cloud cloud in Clouds)
                cloud.Update();
        }

        private void Init() {

            Rnd = new Random();
            InitClouds();
            InitPlanets();
        }

        private void InitClouds() {

            Clouds = new List<Cloud>();

            for (int i = 0; i < 50; i++)
                Clouds.Add(new Cloud(Rnd.Next(0, Map.WIDTH), Rnd.Next(0, 200), 125, 75, IsRain));
        }

        private void InitPlanets() {

            Sun = new Image("map/sun", "sun_0", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);
            Moon = new Image("map/moon", "moon_0", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);

            switch (TimeType) {
                case TimeType.Day:
                    CurrentPlanet = Sun;
                    break;
                case TimeType.Night:
                    CurrentPlanet = Moon;
                    break;
            }

            Sky = new Image("map/sky", "sky", 0, 0, CastleBridge.Graphics.PreferredBackBufferWidth, CastleBridge.Graphics.PreferredBackBufferHeight, Color.White);
        }


        public List<Cloud> GetClouds() {
            return Clouds;
        }
        public Image GetSky() {
            return Sky;
        }
        public Image GetSun() {
            return Sun;
        }

        public void DrawClouds() {

            foreach (Cloud cloud in Clouds)
                cloud.Draw();
        }
        public void DrawStuck() {
            Sky.Draw();
            CurrentPlanet.Draw();
        }
    }
}
