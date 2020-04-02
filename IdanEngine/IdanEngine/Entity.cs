using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public abstract class Entity {

        public bool IsColide;
        public bool IsGravity;
        public Animation Animation;
        public Rectangle Rectangle;
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
            Animation.Play();
        }

        public Rectangle GetRectangle() {
            return Rectangle;
        }

        public abstract void Draw();
    }
}
