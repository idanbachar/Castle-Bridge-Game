using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public abstract class Menu {

        private Text Title;
        
        public Menu(string title) {
            Title = new Text(FontType.Default, title, new Vector2(100, 100), Color.Black, true, Color.White);
        }

        public abstract void Update();

        public virtual void Draw() {
            Title.Draw();
        }

    }
}
