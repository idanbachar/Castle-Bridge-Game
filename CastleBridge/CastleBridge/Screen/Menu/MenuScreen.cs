using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class MenuScreen: Screen {

        private MenuPage CurrentPage;
        private Dictionary<MenuPage, Menu> Menus;
        private TeamName SelectedTeam;
        private CharacterName SelectedCharacter;
        private bool IsPressedLeftButton;

        public delegate void StartGame(TeamName team, CharacterName characterName);
        public event StartGame OnStartGame;

        public MenuScreen(Viewport viewPort) : base(viewPort) {
            Menus = new Dictionary<MenuPage, Menu>();
            SelectedTeam = TeamName.None;
            SelectedCharacter = CharacterName.None;
            IsPressedLeftButton = false;
            Init();
        }

        private void Init() {

            Menus.Add(MenuPage.MainMenu, new MainMenu("Main Menu"));
            Menus.Add(MenuPage.TeamSelection, new TeamSelectionMenu("Select team by pressing castle sides"));
            Menus.Add(MenuPage.CharacterSelection, new CharacterSelectionMenu("Select a character to play with"));
            CurrentPage = MenuPage.TeamSelection;
        }
 

        public override void Update() {

            Menus[CurrentPage].Update();

            switch (CurrentPage) {

                case MenuPage.TeamSelection:
                    TeamSelectionMenu teamSelectionMenu = Menus[CurrentPage] as TeamSelectionMenu;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && teamSelectionMenu.GetOkButton().IsMouseOver) {
                        if (teamSelectionMenu.IsSelected) {
                            SelectedTeam = teamSelectionMenu.GetSelectedTeam();
                            GoToPage(MenuPage.CharacterSelection);
                            ((CharacterSelectionMenu)Menus[CurrentPage]).SelectCastleByTeam(SelectedTeam);
                        }
                    }
                    break;
                case MenuPage.CharacterSelection:
                    CharacterSelectionMenu characterSelectionMenu = Menus[CurrentPage] as CharacterSelectionMenu;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && characterSelectionMenu.GetBackButton().IsMouseOver) {
                        GoToPage(MenuPage.TeamSelection);
                    }
                    foreach (Character character in characterSelectionMenu.GetCurrentCharacters()) {

                        if (character.IsMouseOver()) {
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !IsPressedLeftButton) {
                                IsPressedLeftButton = true;
                                SelectedCharacter = character.GetName();
                                characterSelectionMenu.SelectCharacter(character);
                                break;
                            }
                        }
                    }

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && characterSelectionMenu.GetOkButton().IsMouseOver) {
                        if (SelectedCharacter != CharacterName.None)
                            OnStartGame(SelectedTeam, SelectedCharacter);
                    }
                    break;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
                IsPressedLeftButton = false;
        }

        private void GoToPage(MenuPage page) {
            CurrentPage = page;
        }

        public override void Draw() {

            CastleBridge.SpriteBatch.Begin();

            Menus[CurrentPage].Draw();

            CastleBridge.SpriteBatch.End();
        }
    }
}
