using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class GameScreen: Screen {

        private HUD HUD;
        private Player Player;
        private Camera Camera;
        private Map Map;

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

        private void CheckAttack() {

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Player.GetState() != PlayerState.Attack) {
                Player.SetState(PlayerState.Attack);
            }

            if (Player.GetState() == PlayerState.Attack && Player.CurrentCharacter.AttackAnimation.IsFinished) {
                Player.CurrentCharacter.AttackAnimation.Reset();

                if (Player.CurrentCharacter is Archer) {

                    Direction shootDirection = Direction.Down;

                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                        shootDirection = Direction.Up;
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                        shootDirection = Direction.Down;

                    ((Archer)Player.CurrentCharacter).ShootArrow(shootDirection);
                }


                Player.SetState(PlayerState.Afk);
            }
        }

        private void CheckLoot() {

            if (Keyboard.GetState().IsKeyDown(Keys.E) && Player.GetState() != PlayerState.Loot) {

                for(int i = 0; i < Map.WorldEntities.Count; i++) {
                    if (Player.IsTouchWorldEntity(Map.WorldEntities[i])) {
                        Player.SetState(PlayerState.Loot);
                        HUD.AddPopup(new Popup("+1 " + Map.WorldEntities [i].Name.ToString().Replace("_", " "), Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White));
                        Map.WorldEntities.RemoveAt(i);
                        break;
                    }
                }
 
            }

            if (Player.GetState() == PlayerState.Loot && Player.CurrentCharacter.LootAnimation.IsFinished) {
                Player.CurrentCharacter.LootAnimation.Reset();
                Player.SetState(PlayerState.Afk);
            }
        }

        private void CheckKeyboard() {
 
            if (Player.GetState() != PlayerState.Attack && Player.GetState() != PlayerState.Loot) {

                if (Keyboard.GetState().GetPressedKeys().Length == 0)
                    Player.SetState(PlayerState.Afk);

                CheckMovement();
            }

            CheckAttack();
            CheckLoot();

        }

        private void InitPlayer() {

            Player = new Player(CharacterName.Archer, "Idan", Map.Grass.GetRectangle().X + 25, Map.Grass.GetRectangle().Top - 75, 125, 175);
            HUD.SetPlayerAvatar(Player.CurrentCharacter.GetName());
            HUD.SetPlayerWeapon(Weapon.Bow,Player.CurrentCharacter.GetName());
        }

        private void InitMap() {
            Map = new Map();
        }

        private void InitHUD() {

            HUD = new HUD();
            HUD.AddLabel(new Text(FontType.Default, "Idan", new Vector2(100, 100), Color.White, true, Color.Black));
        }

        public override void Update() {
            CheckKeyboard();
            Player.Update();
            Map.Update(Player);

            Camera.Focus(new Vector2(Player.GetRectangle().X, Player.GetRectangle().Y), Map.WIDTH, Map.HEIGHT);

            HUD.Update();
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
            HUD.DrawTile();

            Game1.SpriteBatch.End();

            Game1.SpriteBatch.Begin();

            HUD.DrawStuck();
            Game1.SpriteBatch.End();

        }
    }
}
