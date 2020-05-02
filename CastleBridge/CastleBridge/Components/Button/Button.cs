using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Button {

        private Image DefaultImage;
        private Image OverImage;
        private Image CurrentImage;
        private Text Text;
        public bool IsMouseOver;
        public bool IsClicked;
        private bool IsPressedLeftButton;

        public Button(Image defaultImage, Image overImage, string text, Color textColor) {

            DefaultImage = defaultImage;
            OverImage = overImage;
            CurrentImage = defaultImage;
            Text = new Text(FontType.Default, text, new Vector2(CurrentImage.GetRectangle().Left + 5, CurrentImage.GetRectangle().Top + 5), textColor, false, Color.Black);
            IsMouseOver = false;
            IsClicked = false;
            IsPressedLeftButton = false;
        }

        public void Update() {

            CheckMouseOver();
        }

        private void CheckMouseOver() {

            Rectangle mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);

            if (mouseRectangle.Intersects(CurrentImage.GetRectangle())) {
                IsMouseOver = true;

                CurrentImage = OverImage;
                CurrentImage.SetColor(Color.WhiteSmoke);

            }
            else {
                IsMouseOver = false;

                if (!IsClicked) {
                    CurrentImage = DefaultImage;
                    CurrentImage.SetColor(Color.White);
                }
            }


            if (Mouse.GetState().LeftButton == ButtonState.Released)
                IsPressedLeftButton = false;
        }

        public bool IsClicking() {

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !IsPressedLeftButton) {

                IsPressedLeftButton = true;

                if (IsMouseOver) {
                    IsClicked = !IsClicked;
                    return IsClicked;
                }
            }
            return false;
        }

        public void Reset() {

            IsClicked = false;
            CurrentImage = DefaultImage;
            //CurrentImage.SetColor(Color.White);
        }

        public Image GetCurrentImage() {
            return CurrentImage;
        }

        public void Draw() {
            CurrentImage.Draw();
            Text.Draw();
        }
    }
}
