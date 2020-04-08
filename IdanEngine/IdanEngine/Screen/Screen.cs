using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public abstract class Screen {

        public Screen(Viewport viewPort) {

        }

        public abstract void Update();

        public abstract void Draw();
    }
}
