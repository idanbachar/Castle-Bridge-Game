using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdanEngine {
    public class GameScreen: Screen {

        private HUD HUD;
        private Player Player;

        public GameScreen(): base() {
            Init();
        }

        private void Init() {
            InitHUD();
            InitPlayer();
        }

        private void CheckMovement() {

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Player.MoveRight();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Player.MoveLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Player.MoveUp();
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Player.MoveDown();
        }

        private void InitPlayer() {

            Player = new Player(new Animation(new Image("player/walk", "Run_", 100, 100, 100, 100, Color.White), 0, 7, 7), false, false, "Idan");
        }

        private void InitHUD() {

            HUD = new HUD();
            HUD.AddLabel(new Text(FontType.Default, "Idan", new Vector2(100, 100), Color.White, true, Color.Black));
        }

        public override void Update() {
            CheckMovement();
            Player.Update();
        }

        public override void Draw() {
            Player.Draw();
            HUD.Draw();
        }
    }
}
