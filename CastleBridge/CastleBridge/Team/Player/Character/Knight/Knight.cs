﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Knight: Character {

        private int AttackDamage;
  
        public Knight(CharacterName name, TeamName teamName, int x, int y, int width, int height) : base(name, teamName, x, y, width, height) {
 
            Health = 100;
            AttackDamage = 15;
        }

        public int GetAttackDamage() {
            return AttackDamage;
        }
 
    }
}
