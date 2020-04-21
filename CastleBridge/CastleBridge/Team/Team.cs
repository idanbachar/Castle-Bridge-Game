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

        public Team(TeamName teamName, Rectangle mapDimensionRectangle) {
            Name = teamName;
            Players = new Dictionary<string, Player>();
            MaxPlayers = 6;
            MinPlayers = 2;
            MapDimensionRectangle = mapDimensionRectangle;
            InitCastles(mapDimensionRectangle);

            AddPlayer(CharacterName.Archer, teamName, "gleb");
            AddPlayer(CharacterName.Knight, teamName, "alon");

            InitHorses();
        }

        private void InitHorses() {

            switch (Name) {

                case TeamName.Red:
                    Horse = new Horse(Name, 600, 600, 400, 300);
                    break;
                case TeamName.Yellow:
                    Horse = new Horse(Name, MapDimensionRectangle.Width - 1200, 600, 400, 300);
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

            Random rnd = new Random();
            int x = 0;
            int y = 0;
            int width = 125;
            int height = 175;

            switch (team) {
                case TeamName.Red:
                    x = rnd.Next(MapDimensionRectangle.Left + 25, MapDimensionRectangle.Left + 500);
                    y = rnd.Next(MapDimensionRectangle.Top - 75, MapDimensionRectangle.Top + 170);
                    break;
                case TeamName.Yellow:
                    x = rnd.Next(MapDimensionRectangle.Right - 500, MapDimensionRectangle.Right - 100);
                    y = rnd.Next(MapDimensionRectangle.Top - 75, MapDimensionRectangle.Top + 170);
                    break;
            }

            Players.Add(name, new Player(character, team, name, x, y, width, height));
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

        public void DrawCastle() {
            Castle.Draw();
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

        public void DrawPlayers(int i) {

            foreach (KeyValuePair<string, Player> player in Players) {
                player.Value.Draw(i);

                if (player.Value.CurrentCharacter is Archer) {
                    Archer archer = player.Value.CurrentCharacter as Archer;
                    archer.DrawArrows(i);
                }
                else if (player.Value.CurrentCharacter is Mage) {
                    Mage mage = player.Value.CurrentCharacter as Mage;
                    mage.DrawSpells(i);
                }
            }
        }

        public void DrawHorses(int i) {
            Horse.Draw(i);
        }
    }
}
