using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class RainDrop {

        private Animation Animation;
        private int Speed;
        public bool IsFinished;

        public RainDrop(int x, int y) {

            Animation = new Animation("map/clouds/rain drop/drop_", new Rectangle(x, y, 5, 10), 0, 0, 1, 1, false, false);
            Animation.Start();
            Speed = 15;
            IsFinished = false;
        }

        public void Fall() {

            Animation.SetRectangle(Animation.GetCurrentSpriteImage().GetRectangle().X,
                                   Animation.GetCurrentSpriteImage().GetRectangle().Y + Speed,
                                   Animation.GetCurrentSpriteImage().GetRectangle().Width,
                                   Animation.GetCurrentSpriteImage().GetRectangle().Height);


            if (Animation.GetCurrentSpriteImage().GetRectangle().Bottom >= Map.HEIGHT)
                IsFinished = true;

        }

        public void Draw() {
            Animation.Draw();
        }
    }
}
