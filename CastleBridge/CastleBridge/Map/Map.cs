using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Map {

        private MapName Name;
        public static int WIDTH;
        public static int HEIGHT;
        private Image Grass;
        private Image Sun;
        private Image Sky;
        private List<Cloud> Clouds;
        private List<MapEntity> WorldEntities;
        private Dictionary<TeamName, Team> Teams;
        private Random Rnd;
        public Map() {

            Name = MapName.Forest;
            WIDTH = 10000;
            HEIGHT = 2000;
            Rnd = new Random();
            Init();
        }

        private void Init() {

            InitGrass();
            InitClouds();
            InitWorldEntities();
            InitTeams();
        }

        public void Update() {

            foreach (Cloud cloud in Clouds)
                cloud.Update();

            foreach (KeyValuePair<TeamName, Team> team in Teams)
                team.Value.GetHorse().Update();
        }

        private void InitTeams() {

            Teams = new Dictionary<TeamName, Team>();
            Teams.Add(TeamName.Red, new Team(TeamName.Red, Grass.GetRectangle()));
            Teams.Add(TeamName.Yellow, new Team(TeamName.Yellow, Grass.GetRectangle()));
        }

        private void InitGrass() {
            Grass = new Image("map/" + Name, "grass", 0, HEIGHT / 5, WIDTH, HEIGHT, Color.White);
        }

        private void InitClouds() {

            Clouds = new List<Cloud>();

            for (int i = 0; i < 50; i++)
                GenerateCloud();
        }

        private void InitWorldEntities() {

            WorldEntities = new List<MapEntity>();
            
            Random rnd = new Random();
            for (int i = 1; i < 100; i++) {
                GenerateWorldEntity();
            }
 
            for (int i = 0; i < 60; i++)
                WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, (i * 200) + 25, Grass.GetRectangle().Top - 250, 200, 250, false, Direction.Left, 0f));

            for (int i = 0; i < 156; i++)
                WorldEntities.Add(new MapEntity(MapEntityName.Ground_Leaves, MapName.Forest, (i * 65), Grass.GetRectangle().Top - 60, 75, 75, false, Direction.Left, 0f));   

            Sun = new Image("map/sun", "sun_0", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 75, 0, 150, 150, Color.White);
            Sky = new Image("map/sky", "sky", 0, 0, CastleBridge.Graphics.PreferredBackBufferWidth, CastleBridge.Graphics.PreferredBackBufferHeight, Color.White);
        }

        private void GenerateCloud() {

            Clouds.Add(new Cloud(Rnd.Next(0, WIDTH), Rnd.Next(0, 200), 125, 75));
        }

        private void GenerateWorldEntity() {

            int grassX = Grass.GetRectangle().Left;
            int grassY = Grass.GetRectangle().Top;

            int x = Rnd.Next(grassX, WIDTH);
            int y = Rnd.Next(grassY, HEIGHT);

            MapEntityName entity = (MapEntityName)Rnd.Next(0, 5);

            AddEntity(entity, x, y, Direction.Left, 0f);
        }
 
        public void AddEntity(MapEntityName entityName, int x, int y, Direction direction, float rotation) {

            int width = 0;
            int height = 0;

            bool isTouchable = false;

            if (direction != Direction.Left && direction != Direction.Right)
                direction = Direction.Left;


            switch (entityName) {
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
                case MapEntityName.Arrow:
                    width = 44;
                    height = 21;
                    isTouchable = true;
                    break;
            }

            WorldEntities.Add(new MapEntity(entityName, Name, x , y + height, width, height, isTouchable, direction, rotation));
        }

        public Image GetSun() {
            return Sun;
        }

        public Image GetGrass() {
            return Grass;
        }

        public Image GetSky() {
            return Sky;
        }

        public List<MapEntity> GetWorldEntities() {
            return WorldEntities;
        }

        public List<Cloud> GetClouds() {
            return Clouds;
        }

        public Dictionary<TeamName, Team> GetTeams() {
            return Teams;
        }

        public void DrawTile(int i) {

            foreach (MapEntity mapEntity in WorldEntities)
                if (mapEntity.GetAnimation().GetCurrentSpriteImage().GetRectangle().Bottom == i)
                    mapEntity.Draw();
        }

        public void DrawTeamsPlayers(int i) {

            foreach (KeyValuePair<TeamName, Team> team in Teams)
                team.Value.DrawPlayers(i);
        }

        public void DrawTeamsCastles() {

            foreach (KeyValuePair<TeamName, Team> team in Teams)
                team.Value.DrawCastle();
        }

        public void DrawTeamsHorses(int i) {

            foreach (KeyValuePair<TeamName, Team> team in Teams)
                team.Value.DrawHorses(i);
        }

        public void DrawClouds() {
            foreach (Cloud cloud in Clouds)
                cloud.Draw();
        }

        public void DrawStuck() {
            Sky.Draw();
            Sun.Draw();
        }
    }
}
