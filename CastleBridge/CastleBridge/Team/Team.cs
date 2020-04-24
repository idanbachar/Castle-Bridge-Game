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

            Random rnd = new Random();
            for (int i = 1; i < 3; i++)
                AddPlayer((CharacterName)rnd.Next(0, 3), teamName, "Bot_" + i);

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
                    y = rnd.Next(MapDimensionRectangle.Top - 75, MapDimensionRectangle.Top + 300);
                    break;
                case TeamName.Yellow:
                    x = rnd.Next(MapDimensionRectangle.Right - 500, MapDimensionRectangle.Right - 100);
                    y = rnd.Next(MapDimensionRectangle.Top - 75, MapDimensionRectangle.Top + 300);
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

        public void DrawCastle(Location playerLocation) {
            if (Castle.GetCurrentLocation() == playerLocation || Castle.GetCurrentLocation() == Location.All)
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

        public void DrawPlayers(int i, Location playerLocation) {

            foreach (KeyValuePair<string, Player> player in Players) {

                if (player.Value.GetCurrentLocation() == playerLocation || playerLocation == Location.All)
                    player.Value.Draw(i);

                if (player.Value.CurrentCharacter is Archer) {
                    Archer archer = player.Value.CurrentCharacter as Archer;
                    archer.DrawArrows(i, playerLocation);
                }
                else if (player.Value.CurrentCharacter is Mage) {
                    Mage mage = player.Value.CurrentCharacter as Mage;
                    mage.DrawSpells(i, playerLocation);
                }
            }
        }

        public void DrawHorses(int i, Location playerLocation) {
            if (Horse.GetCurrentLocation() == playerLocation || Horse.GetCurrentLocation() == Location.All)
                Horse.Draw(i);
        }
    }
}
