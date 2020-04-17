﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Castle {

        private Image Image;
        private Team Team;
        private List<Chest> Chests;

        public Castle(Team team, int x, int y) {
            Team = team;
            Chests = new List<Chest>();
            Image = new Image("map/castles/teams/" + team + "/outside", "castle", x, y, 1400, 600, Color.White);
        }

        public Team GetTeam() {
            return Team;
        }

        public List<Chest> GetChests() {
            return Chests;
        }

        public void Draw() {
            Image.Draw();
        }
    }
}