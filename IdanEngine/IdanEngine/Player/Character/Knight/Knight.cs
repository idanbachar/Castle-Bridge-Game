using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Knight: Character {

  
        public Knight(CharacterName name, Team team, int x, int y, int width, int height) : base(name, team, x, y, width, height) {
 
            Health = 110;
        }
 
    }
}
