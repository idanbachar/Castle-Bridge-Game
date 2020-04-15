using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Chest {

        private Image Image;
        private Team Team;
        private Player Owner;

        public Chest(Team team, int x, int y) {
            Team = Team;
            Image = new Image(string.Empty, string.Empty, x, y, 150, 50, Color.White);
            Owner = null;
        }

        public Team GetTeam() {
            return Team;
        }

        public void SetTeam(Team team) {
            Team = team;
        }

        public Player GetOwner() {
            return Owner;
        }

        public void SetOwner(Player player) {
            Owner = player;
        }

        public void Draw() {
            Image.Draw();
        }

    }
}
