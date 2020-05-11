using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Team {

        private TeamName Name;
        private Castle Castle;
        private Dictionary<string, Player> Players;
        private Rectangle MapDimensionRectangle;
        private int MaxPlayers;
        private int MinPlayers;
        private Horse Horse;
        private Random Rnd;

        public Team(TeamName teamName, Rectangle mapDimensionRectangle) {
            Name = teamName;
            Players = new Dictionary<string, Player>();
            MaxPlayers = 6;
            MinPlayers = 2;
            MapDimensionRectangle = mapDimensionRectangle;
            InitCastles(mapDimensionRectangle);
            Rnd = new Random();
            InitHorses();
        }

        private void InitHorses() {

            switch (Name) {

                case TeamName.Red:
                    Horse = new Horse(Name, 600, 600, 400, 300);
                    Horse.SetDirection(Direction.Right);
                    break;
                case TeamName.Yellow:
                    Horse = new Horse(Name, MapDimensionRectangle.Width - 1200, 600, 400, 300);
                    Horse.SetDirection(Direction.Left);
                    break;
            }
        }

        private void InitCastles(Rectangle mapDimensionRectangle) {

            switch (Name) {
                case TeamName.Red:
                    Castle = new Castle(Name, 300, mapDimensionRectangle.Top - 400);
                    break;
                case TeamName.Yellow:
                    Castle = new Castle(Name, mapDimensionRectangle.Width - 1700, mapDimensionRectangle.Top - 400);
                    break;
            }
        }

        public void AddPlayer(CharacterName character, TeamName team, string name) {

            int x = 0;
            int y = 0;
            int width = 125;
            int height = 175;
            Direction direction = Direction.Right;

            switch (team) {
                case TeamName.Red:
                    x = Rnd.Next(MapDimensionRectangle.Left + 25, MapDimensionRectangle.Left + 500);
                    y = Rnd.Next(MapDimensionRectangle.Top - 75, MapDimensionRectangle.Top + 300);
                    direction = Direction.Right;
                    break;
                case TeamName.Yellow:
                    x = Rnd.Next(MapDimensionRectangle.Right - 500, MapDimensionRectangle.Right - 100);
                    y = Rnd.Next(MapDimensionRectangle.Top - 75, MapDimensionRectangle.Top + 300);
                    direction = Direction.Left;
                    break;
            }

            Player player = new Player(character, team, name, x, y, width, height);
            player.SetDirection(direction);

            lock (Players) {
                Players.Add(name, player);
            }
        }

        public Dictionary<string, Player> GetPlayers() {
            return Players;
        }

        public TeamName GetName() {
            return Name;
        }

        public Castle GetCastle() {
            return Castle;
        }

        public int GetMaxPlayers() {
            return MaxPlayers;
        }

        public int GetMinPlayers() {
            return MinPlayers;
        }

        public Horse GetHorse() {
            return Horse;
        }
    }
}
