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
        private Image DaySky;
        private Image NightSky;
        private Image CurrentPlanet;
        private Image CurrentSky;
        private List<Cloud> Clouds;
        private Random Rnd;
        private bool IsRain;
        private int ScreenWidth;
        private int NumberClouds;
        public Weather(TimeType timeType, bool isRain, int screenWidth, int numberClouds) {

            ScreenWidth = screenWidth;
            NumberClouds = numberClouds;
            IsRain = isRain;
            Init();
            SetTime(timeType);
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

            for (int i = 0; i < NumberClouds; i++)
                Clouds.Add(new Cloud(Rnd.Next(0, ScreenWidth), Rnd.Next(0, 200), 125, 75, IsRain, ScreenWidth));
        }

        private void InitPlanets() {

            Sun = new Image("map/sun", "sun_0", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);
            Moon = new Image("map/moon", "moon_0", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);

            DaySky = new Image("map/sky", "day", 0, 0, CastleBridge.Graphics.PreferredBackBufferWidth, CastleBridge.Graphics.PreferredBackBufferHeight, Color.White);
            NightSky = new Image("map/sky", "night", 0, 0, CastleBridge.Graphics.PreferredBackBufferWidth, CastleBridge.Graphics.PreferredBackBufferHeight, Color.White);
        }

        public void SetTime(TimeType timeType) {

            TimeType = timeType;

            switch (TimeType) {
                case TimeType.Day:
                    CurrentPlanet = Sun;
                    CurrentSky = DaySky;
                    break;
                case TimeType.Night:
                    CurrentPlanet = Moon;
                    CurrentSky = NightSky;
                    break;
            }
        }


        public List<Cloud> GetClouds() {
            return Clouds;
        }
        public Image GetSky() {
            return CurrentSky;
        }
        public Image GetSun() {
            return Sun;
        }

        public void DrawClouds() {

            foreach (Cloud cloud in Clouds)
                cloud.Draw();
        }
        public void DrawStuck() {
            CurrentSky.Draw();
            CurrentPlanet.Draw();
        }
    }
}
