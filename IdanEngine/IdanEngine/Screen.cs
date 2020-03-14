using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public abstract class Screen {

        public Screen() {

        }

        public abstract void Update();

        public abstract void Draw();
    }
}
