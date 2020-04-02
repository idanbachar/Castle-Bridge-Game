using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Block : Entity {

        public Block(Animation animation, bool isColide, bool isGravity) : base(animation, isColide, isGravity) {
 
        }

        public Rectangle GetCurrentAnimationRectangle() {
            return Animation.GetCurrentSprite().GetRectangle();
        }
 
        public override void Update() {

            base.Update();
        }

        public override void Draw() {
            Animation.Draw();
        }
    }
}
