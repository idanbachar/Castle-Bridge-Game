using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Camera Camera;
        private Map Map;

        private bool IsPressedSpaceKey;

        public GameScreen(Viewport viewPort) : base(viewPort) {
            Init(viewPort);
        }

        private void Init(Viewport viewPort) {

            InitHUD();
            InitMap();
            InitPlayer();
            Camera = new Camera(viewPort);
        }

        private void CheckMovement() {



            if (Player.GetState() != PlayerState.Attack) {

                if (Keyboard.GetState().GetPressedKeys().Length == 0) {

                    Player.SetState(PlayerState.Afk);
                }


                if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                    Player.SetDirection(Direction.Right);
                    Player.SetState(PlayerState.Walk);
                    Player.Move(Direction.Right);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                    Player.SetDirection(Direction.Left);
                    Player.SetState(PlayerState.Walk);
                    Player.Move(Direction.Left);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                    if (!Map.IsOnTopMap(Player))
                        Player.Move(Direction.Up);

                    Player.SetState(PlayerState.Walk);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                    Player.Move(Direction.Down);
                    Player.SetState(PlayerState.Walk);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Player.GetState() != PlayerState.Attack) {
                Player.SetState(PlayerState.Attack);
            }

            if (Player.GetState() == PlayerState.Attack && Player.CurrentCharacter.AttackAnimation.IsFinished) {
                Player.CurrentCharacter.AttackAnimation.Reset();
                Player.SetState(PlayerState.Afk);
            }

        }

        private void InitPlayer() {

            Player = new Player(CharacterName.Archer, "Idan", Map.Grass.GetRectangle().X + 25, Map.Grass.GetRectangle().Top - 75, 125, 175);
        }

        private void InitMap() {
            Map = new Map();
        }

        private void InitHUD() {

            HUD = new HUD();
            HUD.AddLabel(new Text(FontType.Default, "Idan", new Vector2(100, 100), Color.White, true, Color.Black));
        }

        public override void Update() {
            CheckMovement();
            Player.Update();
            Map.Update();
            Camera.Focus(new Vector2(Player.GetRectangle().X, Player.GetRectangle().Y), Map.WIDTH, Map.HEIGHT);

            HUD.GetLabels() [0].SetText("(" + Player.GetRectangle().X + "," + Player.GetRectangle().Y + ")");
        }

        public override void Draw() {

            Game1.SpriteBatch.Begin();

            Map.DrawStuck();

            Game1.SpriteBatch.End();

            Game1.SpriteBatch.Begin(SpriteSortMode.Deferred,
                            BlendState.AlphaBlend,
                            null,
                            null,
                            null,
                            null,
                            Camera.Transform
                            );

            Map.DrawTile();
            Player.Draw();

            Game1.SpriteBatch.End();

            Game1.SpriteBatch.Begin();

            HUD.Draw();
            Game1.SpriteBatch.End();

        }
    }
}
