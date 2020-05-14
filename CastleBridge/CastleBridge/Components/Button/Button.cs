using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class Button {

        private Image DefaultImage; //Button's default image
        private Image OverImage; //Button's over image
        private Image CurrentImage; //Button's current image
        private Text Text; //Button's text
        public bool IsMouseOver; //Button's is mouse over indication
        public bool IsClicked; //Button's is clicked indication
        private bool IsPressedLeftButton; //Button's is pressed left button

        /// <summary>
        /// Receives default image, over image, text, text color
        /// and creates button
        /// </summary>
        /// <param name="defaultImage"></param>
        /// <param name="overImage"></param>
        /// <param name="text"></param>
        /// <param name="textColor"></param>
        public Button(Image defaultImage, Image overImage, string text, Color textColor) {

            DefaultImage = defaultImage;
            OverImage = overImage;
            CurrentImage = defaultImage;
            Text = new Text(FontType.Default, text, new Vector2(CurrentImage.GetRectangle().Left + 5, CurrentImage.GetRectangle().Top + 5), textColor, false, Color.Black);
            IsMouseOver = false;
            IsClicked = false;
            IsPressedLeftButton = false;
        }

        /// <summary>
        /// Update stuff
        /// </summary>
        public void Update() {

            //Check if mouse is over button:
            CheckMouseOver();
        }

        /// <summary>
        /// Checks mouse over button
        /// </summary>
        private void CheckMouseOver() {

            Rectangle mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10);

            //Checks if mouse is touching button
            if (mouseRectangle.Intersects(CurrentImage.GetRectangle())) {
                IsMouseOver = true;

                //Replace current button's image to over image:
                CurrentImage = OverImage;

                //Sets current button's color:
                CurrentImage.SetColor(Color.WhiteSmoke);

            }
            else { //else 
                IsMouseOver = false;

                //Checks if not clicked on button:
                if (!IsClicked) {

                    //Replace current button's image to default:
                    CurrentImage = DefaultImage;

                    //Sets current button's color:
                    CurrentImage.SetColor(Color.White);
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
                IsPressedLeftButton = false;
        }

        /// <summary>
        /// Checks if clicking on button
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Reset button's stuff
        /// </summary>
        public void Reset() {

            IsClicked = false;
            CurrentImage = DefaultImage;
        }

        /// <summary>
        /// Get current button's image
        /// </summary>
        /// <returns></returns>
        public Image GetCurrentImage() {
            return CurrentImage;
        }

        /// <summary>
        /// Draw button
        /// </summary>
        public void Draw() {

            //Draw current button's image:
            CurrentImage.Draw();

            //Draw text:
            Text.Draw();
        }
    }
}
