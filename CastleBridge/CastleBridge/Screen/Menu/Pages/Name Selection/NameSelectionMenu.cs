using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class NameSelectionMenu : Menu {

        private Image RedCastle;
        private Image YellowCastle;
        private Image CurrentCastle;
        private List<Character> RedCharacters;
        private List<Character> YellowCharacters;
        private Character CurrentCharacter;

        private Keys[] lastPressedKeys = new Keys[1];
        public Text Text;
        private string Name;
        private Image InputText;
        private Button OkButton;
        private Button BackButton;
        public bool IsSelected;
        private bool caps;

        public NameSelectionMenu(string title): base(title) {

            RedCastle = new Image("map/castles/teams/red/outside", "castle", 0, 100, 1400, 431, Color.White);
            YellowCastle = new Image("map/castles/teams/yellow/outside", "castle", 0, 100, 1400, 431, Color.White);
            CurrentCastle = RedCastle;

            InputText = new Image("menu/button backgrounds/empty", CastleBridge.Graphics.PreferredBackBufferWidth / 2 - 100, CastleBridge.Graphics.PreferredBackBufferHeight / 2, 200, 25);
            OkButton = new Button(new Image("menu/button backgrounds/empty", InputText.GetRectangle().Left, InputText.GetRectangle().Bottom + 10, 100, 35), new Image("menu/button backgrounds", "empty", InputText.GetRectangle().Left, InputText.GetRectangle().Bottom + 10, 100, 35, Color.Red), "Ok", Color.Black);
            BackButton = new Button(new Image("menu/button backgrounds/empty", CastleBridge.Graphics.PreferredBackBufferWidth - 100, 20, 100, 35), new Image("menu/button backgrounds", "empty", CastleBridge.Graphics.PreferredBackBufferWidth - 100, 20, 100, 35, Color.Red), "Back", Color.Black);
            Name = string.Empty;
            Text = new Text(FontType.Default, Name, new Vector2(InputText.GetRectangle().X, InputText.GetRectangle().Y), Color.Black, false, Color.Red);
            IsSelected = false;
            SelectedTeam = TeamName.None;

            InitCharacters();
        }

        private void InitCharacters() {
            RedCharacters = new List<Character>();
            YellowCharacters = new List<Character>();
 

            RedCharacters.Add(new Archer(CharacterName.Archer, TeamName.Red, 100, 250, 250, 400));
            RedCharacters.Add(new Knight(CharacterName.Knight, TeamName.Red, 100, 250, 250, 400));
            RedCharacters.Add(new Mage(CharacterName.Mage, TeamName.Red, 100, 250, 250, 400));

            YellowCharacters.Add(new Archer(CharacterName.Archer, TeamName.Yellow, 100, 250, 250, 400));
            YellowCharacters.Add(new Knight(CharacterName.Knight, TeamName.Yellow, 100, 250, 250, 400));
            YellowCharacters.Add(new Mage(CharacterName.Mage, TeamName.Yellow, 100, 250, 250, 400));

            CurrentCharacter = RedCharacters[0];
        }

        public void SelectTeamAndCharacter(CharacterName selectedCharacter, TeamName selectedTeam) {

            switch (selectedTeam) {
                case TeamName.Red:
                    CurrentCastle = RedCastle;
                    break;
                case TeamName.Yellow:
                    CurrentCastle = YellowCastle;
                    break;
            }

            CurrentCharacter = GetSelectedCharacterByTeam(selectedCharacter ,selectedTeam);
        }

        private Character GetSelectedCharacterByTeam(CharacterName targetCharacter, TeamName team) {

            if (team == TeamName.Red) {

                foreach (Character character in RedCharacters) {
                    if (character.GetName() == targetCharacter) {
                        return character;
                    }
                }
            }
            else if (team == TeamName.Yellow) {

                foreach (Character character in YellowCharacters) {
                    if (character.GetName() == targetCharacter) {
                        return character;
                    }
                }
            }

            return null;
        }

        private void GetKeys() {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            //check if any of the previous update's keys are no longer pressed
            foreach (Keys key in lastPressedKeys) {
                if (!pressedKeys.Contains(key))
                    OnKeyUp(key);
            }

            //check if the currently pressed keys were already pressed
            foreach (Keys key in pressedKeys) {
                if (!lastPressedKeys.Contains(key))
                    OnKeyDown(key);
            }
            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
        }

        private void OnKeyDown(Keys key) {
            //do stuff
            if (key == Keys.Back && Name.Length > 0) //Removes a letter from the name if there is a letter to remove
            {
                Name = Name.Remove(Name.Length - 1);
                Text.ChangeText(Name);
            }
            else if (key == Keys.Back && Name.Length == 0) {
                Name = string.Empty;
                Text.ChangeText(Name);
            }
            else if (key == Keys.CapsLock) {
                if (caps)
                    caps = false;
                else
                    caps = true;
            }
            else if (key == Keys.LeftShift || key == Keys.RightShift)//Sets caps to true if a shift key is pressed
            {
                caps = true;
            }
            else if (!caps && Name.Length < 16) //If the name isn't too long, and !caps the letter will be added without caps
            {
                if (key == Keys.Space) {
                    Name += " ";
                    Text.ChangeText(Name);
                }
                else {
                    Name += key.ToString().ToLower();
                    Text.ChangeText(Name);
                }
            }
            else if (Name.Length < 16) //Adds the letter to the name in CAPS
            {
                Name += key.ToString();
                Text.ChangeText(Name);
            }
        }

        private void OnKeyUp(Keys key) {
            //Sets caps to false if one of the shift keys goes up
            if (key == Keys.LeftShift || key == Keys.RightShift) {
                caps = false;
            }
        }

        public override void Update() {

            GetKeys();
            Weather.Update();
            CurrentCharacter.Update();

            if (Name.Length > 2)
                IsSelected = true;
            else
                IsSelected = false;

            BackButton.Update();
            OkButton.Update();
        }

        public Button GetBackButton() {
            return BackButton;
        }

        public string GetSelectedName() {
            return Name;
        }

        public Button GetOkButton() {
            return OkButton;
        }

        public override void Draw() {
            base.Draw();

            CurrentCastle.Draw();
            CurrentCharacter.Draw();

            InputText.Draw();
            Text.Draw();

            BackButton.Draw();

            if (IsSelected)
                OkButton.Draw();
        }
    }
}
