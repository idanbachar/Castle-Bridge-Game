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
        private int GenerateXpTimer;
        private bool IsPressedD1;
        private bool IsPressedD2;

        public GameScreen(Viewport viewPort) : base(viewPort) {
            Init(viewPort);
        }

        private void Init(Viewport viewPort) {

            InitMap();
            InitPlayer();
            InitHUD();
            Camera = new Camera(viewPort);
        }

        private void GenerateXp() {
            
            if(GenerateXpTimer < 1000) {
                GenerateXpTimer++;
            }
            else {
                GenerateXpTimer = 0;
                Player.CurrentCharacter.AddXp(1);
                HUD.AddPopup(new Popup("+1xp", HUD.GetPlayerLevelBar().GetRectangle().Left + 3, HUD.GetPlayerLevelBar().GetRectangle().Top, Color.White, Color.Green), false);
                HUD.SetPlayerLevel(1);
            }
        }

        private void CheckMovement() {

            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                Player.SetDirection(Direction.Right);

                if (!Player.IsOnRightMap()) {
                    Player.SetState(PlayerState.Walk);
                    Player.Move(Direction.Right);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                Player.SetDirection(Direction.Left);

                if (!Player.IsOnLeftMap()) {
                    Player.SetState(PlayerState.Walk);
                    Player.Move(Direction.Left);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                if (!Player.IsOnTopMap(Map)) {
                    Player.Move(Direction.Up);
                    Player.SetState(PlayerState.Walk);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                if (!Player.IsOnBottomMap()) {
                    Player.Move(Direction.Down);
                    Player.SetState(PlayerState.Walk);
                }
            }

            if(Keyboard.GetState().IsKeyDown(Keys.D1) && !IsPressedD1) {
                IsPressedD1 = true;
                if (Player.CurrentCharacter.GetName() != CharacterName.Archer) {
                    Player.ChangeCharacter(CharacterName.Archer);
                    UpdateHud();
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.D1))
                IsPressedD1 = false;

            if (Keyboard.GetState().IsKeyUp(Keys.D2))
                IsPressedD2 = false;


            if (Keyboard.GetState().IsKeyDown(Keys.D2) && !IsPressedD2) {
                IsPressedD2 = true;
                if (Player.CurrentCharacter.GetName() != CharacterName.Knight) {
                    Player.ChangeCharacter(CharacterName.Knight);
                    UpdateHud();
                }
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
                        HUD.AddPopup(new Popup("No arrows!", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.Red, Color.Black), true);
                    }
                    HUD.SetPlayerWeaponAmmo(archer.CurrentArrows + "/" + archer.MaxArrows);
      
                }


                Player.SetState(PlayerState.Afk);
            }
        }

        private void CheckLoot() {

            if (Keyboard.GetState().IsKeyDown(Keys.E) && Player.GetState() != PlayerState.Loot) {
            
                for(int i = 0; i < Map.WorldEntities.Count; i++) {

                    MapEntity currentEntity = Map.WorldEntities [i];

                    if (Player.IsTouchWorldEntity(currentEntity)) {
                        Player.SetState(PlayerState.Loot);

                        switch (currentEntity.Name) {
                            case MapEntityName.Red_Flower:
                                Player.CurrentCharacter.IncreaseHp(15);
                                HUD.SetPlayerHealth(15);
                                HUD.AddPopup(new Popup("+15hp", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White, Color.Red), true);
                                HUD.AddPopup(new Popup("+15", HUD.GetPlayerHealthBar().GetRectangle().Left + 3, HUD.GetPlayerHealthBar().GetRectangle().Top, Color.White, Color.Red), false);
                                break;
                            case MapEntityName.Stone:
                                Player.AddStones(1);
                                HUD.AddPopup(new Popup("+1 Stone", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White, Color.Black), true);
                                Player.CurrentCharacter.AddXp(3);
                                HUD.AddPopup(new Popup("+3xp", HUD.GetPlayerLevelBar().GetRectangle().Left + 3, HUD.GetPlayerLevelBar().GetRectangle().Top, Color.White, Color.Green), false);
                                break;
                            case MapEntityName.Tree:
                                Player.SetWoods(5);
                                HUD.AddPopup(new Popup("+5 Woods", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White, Color.Black), true);
                                Player.CurrentCharacter.AddXp(20);
                                HUD.AddPopup(new Popup("+20xp", HUD.GetPlayerLevelBar().GetRectangle().Left + 3, HUD.GetPlayerLevelBar().GetRectangle().Top, Color.White, Color.Green), false);
                                break;
                        }

                        Map.WorldEntities.RemoveAt(i);
                        break;
                    }
                }

                for(int i = 0; i < Map.FallenArrows.Count; i++) {

                    Arrow currentArrow = Map.FallenArrows [i];

                    if (Player.IsTouchFallenArrow(currentArrow)) {

                        if (Player.CurrentCharacter is Archer) {

                            Archer archer = Player.CurrentCharacter as Archer;

                            Player.SetState(PlayerState.Loot);
                            HUD.AddPopup(new Popup("+1 Arrow", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White, Color.Black), true);
                            HUD.AddPopup(new Popup("+1", (int)HUD.GetPlayerWeaponAmmo().GetPosition().X, (int)HUD.GetPlayerWeaponAmmo().GetPosition().Y - 10, Color.White, Color.Black), false);
                            Map.FallenArrows.RemoveAt(i);
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

            Player = new Player(CharacterName.Knight, Team.Red, "Idan", Map.Grass.GetRectangle().X + 25, Map.Grass.GetRectangle().Top - 75, 125, 175);
        }

        private void InitMap() {
            Map = new Map();
        }

        private void InitHUD() {

            HUD = new HUD();
            UpdateHud();
        }

        private void UpdateHud() {
            HUD.SetPlayerAvatar(Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeam());

            if (Player.CurrentCharacter is Archer) {
                HUD.SetPlayerWeapon(Weapon.Bow, Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeam());
                HUD.SetPlayerWeaponAmmo(((Archer)Player.CurrentCharacter).CurrentArrows + "/" + ((Archer)Player.CurrentCharacter).MaxArrows);
            }
            else if (Player.CurrentCharacter is Knight) {
                HUD.SetPlayerWeapon(Weapon.Sword, Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeam());
                HUD.SetPlayerWeaponAmmo(string.Empty);
            }
        }

        public override void Update() {
            CheckKeyboard();
            Player.Update();
            GenerateXp();

            if (Player.CurrentCharacter is Archer) {

                Archer archer = Player.CurrentCharacter as Archer;

                for (int i = 0; i < archer.GetArrows().Count; i++) {
                    if (!archer.GetArrows() [i].IsFinished)
                        archer.GetArrows() [i].Move();
                    else {
                        Map.FallenArrows.Add(archer.GetArrows() [i]);
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

            Map.DrawCastles();

            for (int i = Map.Grass.GetRectangle().Top; i < Map.Grass.GetRectangle().Bottom; i++) {
                Map.DrawTile(i);
                Player.Draw(i);

                if (Player.CurrentCharacter is Archer) {
                    Archer archer = Player.CurrentCharacter as Archer;
                    archer.DrawArrows(i);
                }

                foreach (Arrow arrow in Map.FallenArrows)
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
