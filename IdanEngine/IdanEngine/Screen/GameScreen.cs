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
        private List<Arrow> FallenArrows;

        public GameScreen(Viewport viewPort) : base(viewPort) {
            Init(viewPort);
        }

        private void Init(Viewport viewPort) {

            InitMap();
            InitPlayer();
            InitHUD();
            Camera = new Camera(viewPort);

            FallenArrows = new List<Arrow>();
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
                    Archer archer = Player.CurrentCharacter as Archer;

                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                        shootDirection = Direction.Up;
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                        shootDirection = Direction.Down;

                    if (archer.IsCanShoot()) {
                        archer.GetCurrentAnimation().SetReverse(false);
                        archer.ShootArrow(shootDirection);
                    }
                    else {
                        archer.GetCurrentAnimation().SetReverse(true);
                    }
                    HUD.SetPlayerWeaponAmmo(archer.CurrentArrows + "/" + archer.MaxArrows);
      
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

                for(int i = 0; i < FallenArrows.Count; i++) {

                    if (Player.IsTouchFallenArrow(FallenArrows [i])) {

                        if (Player.CurrentCharacter is Archer) {

                            Archer archer = Player.CurrentCharacter as Archer;

                            Player.SetState(PlayerState.Loot);
                            HUD.AddPopup(new Popup("+1 Arrow", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White));
                            FallenArrows.RemoveAt(i);
                            archer.AddArrow();
                            HUD.SetPlayerWeaponAmmo(archer.CurrentArrows + "/" + archer.MaxArrows);
                        }
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
        }

        private void InitMap() {
            Map = new Map();
        }

        private void InitHUD() {

            HUD = new HUD();
            HUD.SetPlayerAvatar(Player.CurrentCharacter.GetName());

            if (Player.CurrentCharacter is Archer) {
                HUD.SetPlayerWeapon(Weapon.Bow, Player.CurrentCharacter.GetName());
                HUD.SetPlayerWeaponAmmo(((Archer)Player.CurrentCharacter).CurrentArrows + "/" + ((Archer)Player.CurrentCharacter).MaxArrows);
            }
        }

        public override void Update() {
            CheckKeyboard();
            Player.Update();

            if (Player.CurrentCharacter is Archer) {

                Archer archer = Player.CurrentCharacter as Archer;

                for (int i = 0; i < archer.GetArrows().Count; i++) {
                    if (!archer.GetArrows() [i].IsFinished)
                        archer.GetArrows() [i].Move();
                    else {
                        FallenArrows.Add(archer.GetArrows() [i]);
                        archer.GetArrows().RemoveAt(i);
                    }
                }
            }

            Map.Update(Player);

            Camera.Focus(new Vector2(Player.GetRectangle().X, Player.GetRectangle().Y), Map.WIDTH, Map.HEIGHT);

            HUD.Update();
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

            Map.Grass.Draw();

            foreach (Cloud cloud in Map.Clouds)
                cloud.Draw();

            for (int i = Map.Grass.GetRectangle().Top; i < Map.Grass.GetRectangle().Bottom; i++) {
                Map.DrawTile(i);
                Player.Draw(i);

                if (Player.CurrentCharacter is Archer) {
                    Archer archer = Player.CurrentCharacter as Archer;
                    archer.DrawArrows(i);
                }

                foreach (Arrow arrow in FallenArrows)
                    if (arrow.Animation.GetCurrentSprite().GetRectangle().Bottom == i)
                        arrow.Draw();
            }
            HUD.DrawTile();

            Game1.SpriteBatch.End();

            Game1.SpriteBatch.Begin();

            HUD.DrawStuck();
            Game1.SpriteBatch.End();

        }
    }
}
