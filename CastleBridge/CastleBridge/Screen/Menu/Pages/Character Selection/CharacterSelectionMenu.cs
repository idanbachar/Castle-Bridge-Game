using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class CharacterSelectionMenu : Menu {

        private Image RedCastle;
        private Image YellowCastle;
        private Image CurrentCastle;
        private List<Character> RedCharacters;
        private List<Character> YellowCharacters;
        private List<Character> CurrentCharacters;

        private Button BackButton;
        private Button OkButton;

        private Image ViSymbol;

        private CharacterName SelectedCharacter;

        public CharacterSelectionMenu(string title): base(title) {
            RedCastle = new Image("map/castles/teams/red/outside", "castle", 0, 100, 1400, 431, Color.White);
            YellowCastle = new Image("map/castles/teams/yellow/outside", "castle", 0, 100, 1400, 431, Color.White);
            CurrentCastle = RedCastle;

            BackButton = new Button(new Image("menu/button backgrounds/empty", CastleBridge.Graphics.PreferredBackBufferWidth - 100, 20, 100, 35), new Image("menu/button backgrounds", "empty", CastleBridge.Graphics.PreferredBackBufferWidth - 100, 20, 100, 35, Color.Red), "Back", Color.Black);
            OkButton = new Button(new Image("menu/button backgrounds/empty", 0, CastleBridge.Graphics.PreferredBackBufferHeight - 100, 100, 35), new Image("menu/button backgrounds", "empty", 0, CastleBridge.Graphics.PreferredBackBufferHeight - 100, 100, 35, Color.Red), "Next", Color.Black);

            ViSymbol = new Image("menu/symbols/vi", 0, 0, 105, 70);
            SelectedCharacter = CharacterName.None;

            InitCharacters();
        
        }

        public void SelectCharacter(Character selectedCharacter) {

            foreach(Character character in CurrentCharacters) {
                if(character == selectedCharacter) {

                    SelectedCharacter = selectedCharacter.GetName();

                    ViSymbol.SetRectangle(character.GetCurrentAnimation().GetCurrentSpriteImage().GetRectangle().X + character.GetCurrentAnimation().GetCurrentSpriteImage().GetRectangle().Width / 2,
                                          character.GetCurrentAnimation().GetCurrentSpriteImage().GetRectangle().Top - 50,
                                          ViSymbol.GetRectangle().Width,
                                          ViSymbol.GetRectangle().Height);
                }
            }

        }

        private void InitCharacters() {
            RedCharacters = new List<Character>();
            YellowCharacters = new List<Character>();
            CurrentCharacters = new List<Character>();

            RedCharacters.Add(new Archer(CharacterName.Archer, TeamName.Red, 300, 250, 250, 400));
            RedCharacters.Add(new Knight(CharacterName.Knight, TeamName.Red, 600, 250, 250, 400));
            RedCharacters.Add(new Mage(CharacterName.Mage, TeamName.Red, 900, 250, 250, 400));

            YellowCharacters.Add(new Archer(CharacterName.Archer, TeamName.Yellow, 300, 250, 250, 400));
            YellowCharacters.Add(new Knight(CharacterName.Knight, TeamName.Yellow, 600, 250, 250, 400));
            YellowCharacters.Add(new Mage(CharacterName.Mage, TeamName.Yellow, 900, 250, 250, 400));

            CurrentCharacters = RedCharacters;
        }

        public void SelectCastleByTeam(TeamName team) {

            switch (team) {
                case TeamName.Red:
                    CurrentCastle = RedCastle;
                    CurrentCharacters = RedCharacters;
                    break;
                case TeamName.Yellow:
                    CurrentCastle = YellowCastle;
                    CurrentCharacters = YellowCharacters;
                    break;
            }
        }

        public override void Update() {

            Weather.Update();

            BackButton.Update();
            OkButton.Update();

            foreach (Character character in CurrentCharacters)
                character.Update();
        }

        public Button GetBackButton() {
            return BackButton;
        }

        public Button GetOkButton() {
            return OkButton;
        }

        public List<Character> GetCurrentCharacters() {
            return CurrentCharacters;
        }

        public override void Draw() {
            base.Draw();

            CurrentCastle.Draw();

            foreach (Character character in CurrentCharacters)
                character.Draw();

            BackButton.Draw();
            OkButton.Draw();

            if (SelectedCharacter != CharacterName.None)
                ViSymbol.Draw();
        }

    }
}
