﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge.Client {
    public class Popup {

        public Text Text; //Popup's text
        private int ShowTimer; //Popup's show timer
        public bool IsFinished; //Popup's is finished indication

        /// <summary>
        /// Receives text, coordinates, text color, background color
        /// and creates popup
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="textColor"></param>
        /// <param name="backgroundColor"></param>
        public Popup(string text, int x, int y, Color textColor, Color backgroundColor) {
            Text = new Text(FontType.Default, text, new Vector2(x, y), textColor, true, backgroundColor);
            ShowTimer = 0;
            IsFinished = false;
        }

        /// <summary>
        /// Update stuff
        /// </summary>
        public void Update() {

            //Checks if popup didn't finish to move -> so move:
            if (!IsFinished) {
                if (ShowTimer < 35) {
                    ShowTimer++;
                    Text.SetPosition(new Vector2(Text.GetPosition().X, Text.GetPosition().Y - 3));
                } //else reset:
                else {
                    ShowTimer = 0;
                    IsFinished = true;
                }
            }
        }

        /// <summary>
        /// Draw popup
        /// </summary>
        public void Draw() {

            //Draw text:
            Text.Draw();
        }
    
    }
}
