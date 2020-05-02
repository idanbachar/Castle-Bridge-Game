using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class TeamSelectionMenu : Menu {

        private Button RedCastle;
        private Button YellowCastle;
        private Button OkButton;
        public bool IsSelected;

        public TeamSelectionMenu(string title): base(title) {

            RedCastle = new Button(new Image("menu/castles selection/castle_left_side_noteam", 0, 100, 700, 431), new Image("menu/castles selection/castle_left_side_redteam", 0, 100, 700, 431), string.Empty, Color.White);
            YellowCastle = new Button(new Image("menu/castles selection/castle_right_side_noteam", RedCastle.GetCurrentImage().GetRectangle().Right, 100, 700, 431), new Image("menu/castles selection/castle_right_side_yellowteam", RedCastle.GetCurrentImage().GetRectangle().Right, 100, 700, 431), string.Empty, Color.White);
            OkButton = new Button(new Image("menu/button backgrounds/empty", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 100 , CastleBridge.Graphics.PreferredBackBufferHeight - 100, 100, 35), new Image("menu/button backgrounds" ,"empty", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 100, CastleBridge.Graphics.PreferredBackBufferHeight - 100, 100, 35, Color.Red), "Next", Color.Black);

            IsSelected = false;
            SelectedTeam = TeamName.None;
        }

        public override void Update() {

            RedCastle.Update();
            YellowCastle.Update();

            if (RedCastle.IsClicking()) {
                IsSelected = true;
                SelectedTeam = TeamName.Red;
                YellowCastle.Reset();
            }
            if (YellowCastle.IsClicking()) {
                IsSelected = true;
                SelectedTeam = TeamName.Yellow;
                RedCastle.Reset();
            }
            if(!RedCastle.IsClicked && !YellowCastle.IsClicked) {
                IsSelected = false;
                SelectedTeam = TeamName.None;
            }

            Weather.Update();

            OkButton.Update();
        }

        public Button GetRedCastle() {
            return RedCastle;
        }

        public Button GetYellowCastle() {
            return YellowCastle;
        }

        public Button GetOkButton() {
            return OkButton;
        }
 
        public override void Draw() {
            base.Draw();

            RedCastle.Draw();
            YellowCastle.Draw();

            if (IsSelected)
                OkButton.Draw();
        }
    }
}
