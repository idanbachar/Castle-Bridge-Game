using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class Text {

        private SpriteFont Font;
        private Color Color;
        private bool IsBackground;
        private string Value;
        private Vector2 Position;
        private bool Visible;
        private int Width;
        private int Height;
        private Image Background;
        private Color BackgroundColor;

        public Text(FontType fontType, string textValue, Vector2 textPosition, Color fontColor, bool isBackground, Color backgroundColor) {

            LoadFont(fontType);
            Width = 0;
            Height = 0;
            Background = new Image("colors", "empty", 0, 0, 0, 0, backgroundColor);
            Position = new Vector2(textPosition.X, textPosition.Y);
            Color = fontColor;
            IsBackground = isBackground;
            Visible = true;
            BackgroundColor = backgroundColor;
            SetText(textValue);
        }

        private void LoadFont(FontType fontType) {

            try {
                Font = Game1.PublicContent.Load<SpriteFont>("fonts/" + fontType);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Font = Game1.PublicContent.Load<SpriteFont>("fonts/" + FontType.Default);
            }
        }

        public void SetText(string newTextValue) {

            Value = newTextValue;
            Width = (int)Font.MeasureString(Value).X;
            Height = (int)Font.MeasureString(Value).Y;
            Background.SetNewRectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public void Update() {

        }

        public void SetVisible(bool value) {
            Visible = value;
        }

        public void Draw() {

            if (Visible) {

                if (IsBackground)
                    Background.Draw();


                Game1.SpriteBatch.DrawString(Font, Value, Position, Color);
            }
        }
    
    }
}
