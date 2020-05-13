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
        private Weather Weather;
        private List<MapEntity> WorldEntities;
        private Dictionary<TeamName, Team> Teams;
        private Random Rnd;

        public Map() {
            Rnd = new Random();
            Name = MapName.Forest;
            WIDTH = 10000;
            HEIGHT = 2000;
            WorldEntities = new List<MapEntity>();
            Init();
        }

        private void Init() {

            InitGrass();
            InitWeather();
            InitBackgroundWorldEntities();
            InitTeams();
        }

        public void Update() {

            Weather.Update();

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

        private void InitWeather() {

            Weather = new Weather(TimeType.Day, true, WIDTH, 50);
        }
 

        private void InitBackgroundWorldEntities() {

            for (int i = 0; i < 60; i++)
                WorldEntities.Add(new MapEntity(MapEntityName.Tree, MapName.Forest, (i * 200) + 25, Grass.GetRectangle().Top - 250, 200, 250, false, Direction.Left, 0f, Location.Outside));

            for (int i = 0; i < 156; i++)
                WorldEntities.Add(new MapEntity(MapEntityName.Ground_Leaves, MapName.Forest, (i * 65), Grass.GetRectangle().Top - 60, 75, 75, false, Direction.Left, 0f, Location.Outside));

        }

        public void AddPlayer(CharacterName character, TeamName team, string name) {
            Teams[team].AddPlayer(character, team, name);
        }

        private void GenerateWorldEntity() {

            int grassX = Grass.GetRectangle().Left;
            int grassY = Grass.GetRectangle().Top;

            int x = Rnd.Next(grassX, WIDTH);
            int y = Rnd.Next(grassY, HEIGHT);

            MapEntityName entity = (MapEntityName)Rnd.Next(0, 5);

            AddEntity(entity, x, y, Direction.Left, 0f, Location.Outside);
        }

        public void AddEntity(MapEntityName entityName, int x, int y, Direction direction, float rotation, Location location) {

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

            lock (WorldEntities) {
                WorldEntities.Add(new MapEntity(entityName, Name, x, y + height, width, height, isTouchable, direction, rotation, location));
            }
        }

        public void UpdateLocationsTo(Location newLocation) {

            foreach (KeyValuePair<TeamName, Team> team in Teams)
                team.Value.GetCastle().ChangeLocationTo(newLocation);

            switch (newLocation) {
                case Location.Outside:
                    WIDTH = 10000;
                    HEIGHT = 2000;
                    break;
                case Location.Inside_Red_Castle:
                case Location.Inside_Yellow_Castle:
                    WIDTH = CastleBridge.Graphics.PreferredBackBufferWidth;
                    HEIGHT = CastleBridge.Graphics.PreferredBackBufferHeight;
                    break;
            }
        }

        public Image GetGrass() {
            return Grass;
        }

        public List<MapEntity> GetWorldEntities() {
            return WorldEntities;
        }

        public Dictionary<TeamName, Team> GetTeams() {
            return Teams;
        }

        public Weather GetWeather() {
            return Weather;
        }

        public void DrawCastles(Location playerLocation) {
            foreach (KeyValuePair<TeamName, Team> team in Teams)
                if (team.Value.GetCastle().GetCurrentLocation() == playerLocation || team.Value.GetCastle().GetCurrentLocation() == Location.All) {
                    switch (team.Value.GetName()) {
                        case TeamName.Red:
                            if (playerLocation == Location.Inside_Red_Castle)
                                team.Value.GetCastle().DrawInside();
                            else if (playerLocation == Location.Outside)
                                team.Value.GetCastle().DrawOutside();
                            break;
                        case TeamName.Yellow:
                            if (playerLocation == Location.Inside_Yellow_Castle)
                                team.Value.GetCastle().DrawInside();
                            else if (playerLocation == Location.Outside)
                                team.Value.GetCastle().DrawOutside();
                            break;
                    }
                }
        }

        public void DrawTile(int i, Location playerLocation) {



            foreach (MapEntity mapEntity in WorldEntities) {
                if (mapEntity.GetAnimation().GetCurrentSpriteImage().GetRectangle().Bottom == i)
                    if (mapEntity.GetCurrentLocation() == playerLocation || mapEntity.GetCurrentLocation() == Location.All)
                        mapEntity.Draw();
            }

            foreach (KeyValuePair<TeamName, Team> team in Teams) {

                foreach (KeyValuePair<string, Player> player in team.Value.GetPlayers()) {
                    if (player.Value.GetCurrentAnimation().GetCurrentSpriteImage().GetRectangle().Bottom - 10 == i)
                        if (player.Value.GetCurrentLocation() == playerLocation || player.Value.GetCurrentLocation() == Location.All)
                            player.Value.Draw();

                    if (player.Value.CurrentCharacter is Archer) {
                        Archer archer = player.Value.CurrentCharacter as Archer;
                        foreach (Arrow arrow in archer.GetArrows()) {
                            if (arrow.GetAnimation().GetCurrentSpriteImage().GetRectangle().Bottom == i)
                                if (arrow.GetCurrentLocation() == playerLocation || arrow.GetCurrentLocation() == Location.All)
                                    arrow.Draw();
                        }
                    }
                    else if (player.Value.CurrentCharacter is Mage) {
                        Mage mage = player.Value.CurrentCharacter as Mage;
                        foreach (EnergyBall energyBall in mage.GetSpells()) {
                            if (energyBall.GetAnimation().GetCurrentSpriteImage().GetRectangle().Bottom == i)
                                if (energyBall.GetCurrentLocation() == playerLocation || energyBall.GetCurrentLocation() == Location.All)
                                    energyBall.Draw();
                        }
                    }
                }

                foreach (Diamond diamond in team.Value.GetCastle().GetDiamonds()) {
                    if (diamond.GetImage().GetRectangle().Bottom == i)
                        if (diamond.GetCurrentLocation() == playerLocation || diamond.GetCurrentLocation() == Location.All)
                            diamond.Draw();
                }



                if (team.Value.GetHorse().GetCurrentAnimation().GetCurrentSpriteImage().GetRectangle().Bottom - 10 == i) {
                    if (team.Value.GetHorse().GetCurrentLocation() == playerLocation || team.Value.GetHorse().GetCurrentLocation() == Location.All)
                        team.Value.GetHorse().Draw();
                }
                
            }
        }
    }
}
