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

        public MenuScreen(Viewport viewPort) : base(viewPort) {
            Menus = new Dictionary<MenuPage, Menu>();
            SelectedTeam = TeamName.None;
            Init();
        }

        private void Init() {

            Menus.Add(MenuPage.MainMenu, new MainMenu("Main Menu"));
            Menus.Add(MenuPage.TeamSelection, new TeamSelectionMenu("Team Selection"));
            Menus.Add(MenuPage.CharacterSelection, new CharacterSelectionMenu("Character Selection"));
            CurrentPage = MenuPage.TeamSelection;
        }

        public override void Update() {

            Menus[CurrentPage].Update();

            switch (CurrentPage) {

                case MenuPage.TeamSelection:
                    TeamSelectionMenu currentMenu = Menus[CurrentPage] as TeamSelectionMenu;
                    if(Mouse.GetState().LeftButton == ButtonState.Pressed && currentMenu.GetOkButton().IsMouseOver) {
                        if (currentMenu.IsSelected)
                            GoToPage(MenuPage.CharacterSelection);
                    }
                    break;



            }

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
