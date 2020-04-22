using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Image {

        private Texture2D Texture;
        private Rectangle Rectangle;
        private Color Color;
        private bool Visible;
        private string FullPath;
        private Direction Direction;
        private float Rotation;

        public Image(string imageFolderPath, string imageName, int x, int y, int width, int height, Color imageColor) {

            FullPath = imageFolderPath.Length == 0 ? imageName : (imageFolderPath.Replace("_", " ") + "/" + imageName);
            LoadImage(FullPath);
            Rectangle = new Rectangle(x, y, width, height);
            Color = imageColor;
            Direction = Direction.Left;
            Visible = true;
            Rotation = 0f;
        }

        public Image(string imageFolderFullPath, int x, int y, int width, int height) {

            LoadImage(imageFolderFullPath);
            Rectangle = new Rectangle(x, y, width, height);
            Color = Color.White;
            Direction = Direction.Left;
            Visible = true;
            Rotation = 0f;
        }

        private void LoadImage(string fullPath) {

            try {
                Texture = CastleBridge.PublicContent.Load<Texture2D>("images/" + fullPath);
            }catch(Exception e) {
                Console.WriteLine(e.Message);
                Texture = CastleBridge.PublicContent.Load<Texture2D>("images/undefined");
            }
        }

        public void ChangeImage(string fullPath) {

            LoadImage(fullPath);
        }

        public void SetDirection(Direction newDirection) {
            Direction = newDirection;
        }

        public Direction GetDirection() {
            return Direction;
        }

        public void SetRectangle(int x, int y, int width, int height) {

            Rectangle.X = x;
            Rectangle.Y = y;
            Rectangle.Width = width;
            Rectangle.Height = height;
        }

        public Rectangle GetRectangle() {
            return Rectangle;
        }

        public void SetVisible(bool value) {
            Visible = value;
        }

        public void SetRotation(float rotation) {
            Rotation = rotation;
        }

        public float GetRotation() {
            return Rotation;
        }

        public string GetFullPath() {
            return FullPath;
        }

        public void Draw() {

            if (Visible) {

                switch (Direction) {
                    case Direction.Right:
                        CastleBridge.SpriteBatch.Draw(Texture, Rectangle, null, Color, Rotation, new Vector2(), SpriteEffects.FlipHorizontally, 1);
                        break;
                    case Direction.Left:
                        CastleBridge.SpriteBatch.Draw(Texture, Rectangle, null, Color, Rotation, new Vector2(), SpriteEffects.None, 1);
                        break;
                }
                
            }
        }
    }
}
