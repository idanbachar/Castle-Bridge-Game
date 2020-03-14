using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public abstract class Entity {

        protected bool IsColide;
        protected bool IsGravity;
        protected Animation Animation;
        protected Rectangle Rectangle;
        protected int Speed;

        public Entity(Animation animation, bool isColide, bool isGravity) {
            IsColide = isColide;
            IsGravity = isGravity;
            Animation = animation;
            Rectangle = Animation.GetCurrentSprite().GetRectangle();
            Speed = 3;
        }

        public virtual void Update() {
            Animation.SetNewRectangle(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
        }

        public abstract void Draw();
    }
}
