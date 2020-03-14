using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Camera {

        public Matrix Transform; 
        public Vector2 Position;  
        private Vector2 Center;  
        private Viewport Viewport;

        public Camera(Viewport viewport) {

            Viewport = viewport;
            Position = new Vector2();
 
        }
 
        public void FocusPosition(int width, int height) {
            Focus(Position, width, height);
        }
 
        public void Focus(Vector2 position, int xOffset, int yOffset) {
            if (position.X < Viewport.Width / 2)
                Center.X = Viewport.Width / 2;
            else if (position.X > xOffset - (Viewport.Width / 2))
                Center.X = xOffset - (Viewport.Width / 2);
            else
                Center.X = position.X;

            if (position.Y < Viewport.Height / 2)
                Center.Y = Viewport.Height / 2;
            else if (position.Y > yOffset - (Viewport.Height / 2))
                Center.Y = yOffset - (Viewport.Height / 2);
            else
                Center.Y = position.Y;

            Center = new Vector2(Position.X, Position.Y);
            Transform = Matrix.CreateTranslation(new Vector3(-Center.X, -Center.Y, 0)) *
                    Matrix.CreateTranslation(new Vector3(Viewport.Width / 2, Viewport.Height / 2, 0));
        }
 
    }
}
