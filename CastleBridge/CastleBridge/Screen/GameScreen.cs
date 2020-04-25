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
        private bool IsPressedD3;
        private bool IsPressedE;
        private bool IsPressedF;

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

                if(Player.GetCurrentHorse() != null) {
                    if (!Player.GetCurrentHorse().IsOnRightMap()) {
                        Player.GetCurrentHorse().SetDirection(Direction.Right);
                        Player.GetCurrentHorse().SetState(Horsestate.Walk);
                        Player.GetCurrentHorse().Move(Direction.Right);
                        Player.SetState(PlayerState.Walk);
                        Player.Move(Direction.Right);
                    }
                }
                else {
                    if (!Player.IsOnRightMap()) {
                        Player.SetState(PlayerState.Walk);
                        Player.Move(Direction.Right);
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                Player.SetDirection(Direction.Left);

                if (Player.GetCurrentHorse() != null) {
                    if (!Player.GetCurrentHorse().IsOnLeftMap()) {
                        Player.GetCurrentHorse().SetDirection(Direction.Left);
                        Player.GetCurrentHorse().SetState(Horsestate.Walk);
                        Player.GetCurrentHorse().Move(Direction.Left);
                        Player.SetState(PlayerState.Walk);
                        Player.Move(Direction.Left);
                    }
                }
                else {

                    if (!Player.IsOnLeftMap()) {
                        Player.SetState(PlayerState.Walk);
                        Player.Move(Direction.Left);
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {

                if (Player.GetCurrentHorse() != null) {
                    if (!Player.GetCurrentHorse().IsOnTopMap(Map)) {
                        Player.GetCurrentHorse().SetState(Horsestate.Walk);
                        Player.GetCurrentHorse().Move(Direction.Up);
                        Player.Move(Direction.Up);
                        Player.SetState(PlayerState.Walk);
                    }
                }
                else {
                    if (!Player.IsOnTopMap(Map)) {
                        Player.Move(Direction.Up);
                        Player.SetState(PlayerState.Walk);
                    }

                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {

                if (Player.GetCurrentHorse() != null) {
                    if (!Player.GetCurrentHorse().IsOnBottomMap()) {
                        Player.GetCurrentHorse().SetState(Horsestate.Walk);
                        Player.GetCurrentHorse().Move(Direction.Down);
                        Player.Move(Direction.Down);
                        Player.SetState(PlayerState.Walk);
                    }
                }
                else {
                    if (!Player.IsOnBottomMap()) {
                        Player.Move(Direction.Down);
                        Player.SetState(PlayerState.Walk);
                    }

                }
            }
        }

        private void CheckChangeCharacter() {

            if (Keyboard.GetState().IsKeyDown(Keys.D1) && !IsPressedD1) {
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

            if (Keyboard.GetState().IsKeyUp(Keys.D3))
                IsPressedD3 = false;


            if (Keyboard.GetState().IsKeyDown(Keys.D2) && !IsPressedD2) {
                IsPressedD2 = true;
                if (Player.CurrentCharacter.GetName() != CharacterName.Knight) {
                    Player.ChangeCharacter(CharacterName.Knight);
                    UpdateHud();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3) && !IsPressedD3) {
                IsPressedD3 = true;
                if (Player.CurrentCharacter.GetName() != CharacterName.Mage) {
                    Player.ChangeCharacter(CharacterName.Mage);
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
                        archer.ShootArrow(shootDirection, Player.GetCurrentLocation());
                    }
                    else {
                        archer.GetCurrentAnimation().SetReverse(true);
                        HUD.AddPopup(new Popup("No arrows!", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.Red, Color.Black), true);
                    }
                    HUD.SetPlayerWeaponAmmo(archer.CurrentArrows + "/" + archer.MaxArrows);
      
                }
                else if (Player.CurrentCharacter is Mage) {

                    Direction shootDirection = Direction.Down;
                    Mage mage = Player.CurrentCharacter as Mage;

                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                        shootDirection = Direction.Up;
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                        shootDirection = Direction.Down;

                    if (mage.IsCanShoot()) {
                        mage.GetCurrentAnimation().SetReverse(false);
                        mage.ShootSpell(shootDirection, Player.GetCurrentLocation());
                    }
                    else {
                        mage.GetCurrentAnimation().SetReverse(true);
                        HUD.AddPopup(new Popup("No spells!", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.Red, Color.Black), true);
                    }
                    HUD.SetPlayerWeaponAmmo(mage.CurrentSpells + "/" + mage.MaxSpells);

                }


                Player.SetState(PlayerState.Afk);
            }
        }

        private void CheckMountHorse() {
 
            foreach (KeyValuePair<TeamName, Team> team in Map.GetTeams()) {
                if (Player.IsTouchHorse(team.Value.GetHorse()) && !team.Value.GetHorse().IsHasOwner()) {
                    team.Value.GetHorse().GetTooltip().SetVisible(true);
                    if (Keyboard.GetState().IsKeyDown(Keys.E) && !IsPressedE) {
                        IsPressedE = true;
                        team.Value.GetHorse().SetOwner(Player);
                        Player.MountHorse(team.Value.GetHorse());
                        HUD.GetHorseAvatar().SetVisible(true);
                        break;
                    }
                }
                else
                    team.Value.GetHorse().GetTooltip().SetVisible(false);
            }

            if (Player.GetCurrentHorse() != null)
                Player.GetCurrentHorse().GetTooltip().SetVisible(true);

            if (Keyboard.GetState().IsKeyUp(Keys.E)) {
                IsPressedE = false;
            }

            if (Player.GetCurrentHorse() != null) {

                Player.GetCurrentHorse().GetTooltip().SetPosition(new Vector2(Player.GetCurrentHorse().GetRectangle().X + 50, Player.GetCurrentHorse().GetRectangle().Bottom - 20));

                if (Keyboard.GetState().IsKeyDown(Keys.F) && !IsPressedF) {

                    IsPressedF = true;
                    Player.DismountHorse();
                    HUD.GetHorseAvatar().SetVisible(false);
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.F)) {
                IsPressedF = false;
            }


        }

        private void CheckDefence() {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && Player.GetState() != PlayerState.Defence) {
                Player.SetState(PlayerState.Defence);
            }

            if (Player.GetState() == PlayerState.Defence && Player.CurrentCharacter.DefenceAnimation.IsFinished) {
                Player.CurrentCharacter.DefenceAnimation.Reset();

                Player.SetState(PlayerState.Afk);
            }
        }

        private void CheckLoot() {

            for (int i = 0; i < Map.GetWorldEntities().Count; i++) {

                MapEntity currentEntity = Map.GetWorldEntities() [i];

                if (Player.IsTouchWorldEntity(currentEntity)) {

                    if (Keyboard.GetState().IsKeyDown(Keys.E) && Player.GetState() != PlayerState.Loot) {
                        Player.SetState(PlayerState.Loot);

                        switch (currentEntity.GetName()) {
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
                            case MapEntityName.Arrow:

                                if (Player.CurrentCharacter is Archer) {
                                    Archer archer = Player.CurrentCharacter as Archer;
                                    HUD.AddPopup(new Popup("+1 Arrow", Player.GetRectangle().X, Player.GetRectangle().Y - 30, Color.White, Color.Black), true);
                                    HUD.AddPopup(new Popup("+1", (int)HUD.GetPlayerWeaponAmmo().GetPosition().X, (int)HUD.GetPlayerWeaponAmmo().GetPosition().Y - 10, Color.White, Color.Black), false);
                                    archer.AddArrow();
                                    HUD.SetPlayerWeaponAmmo(archer.CurrentArrows + "/" + archer.MaxArrows);
                                }
                                break;
                        }

                        Map.GetWorldEntities().RemoveAt(i);
                        break;
                    }
                }

            }

            if (Player.GetState() == PlayerState.Loot && Player.CurrentCharacter.LootAnimation.IsFinished) {
                Player.CurrentCharacter.LootAnimation.Reset();
                Player.SetState(PlayerState.Afk);
            }
        }

        private void CheckEnterExitCastleDoors() {

            foreach (KeyValuePair<TeamName, Team> team in Map.GetTeams()) {
                if (Player.IsTouchCastleDoor(team.Value.GetCastle().GetOutsideDoor())) {
                    if (Keyboard.GetState().IsKeyDown(Keys.E) && !IsPressedE) {
                        IsPressedE = true;
                        switch (team.Value.GetName()) {
                            case TeamName.Red:
                                Player.ChangeLocationTo(Location.Inside_Red_Castle);
                                Map.UpdateLocationsTo(Location.Inside_Red_Castle);
                                Player.SetRectangle(new Rectangle(team.Value.GetCastle().GetInsideDoor().GetImage().GetRectangle().Left,
                                                                  team.Value.GetCastle().GetInsideDoor().GetImage().GetRectangle().Bottom - 50,
                                                                  Player.GetRectangle().Width,
                                                                  Player.GetRectangle().Height));
                                break;
                            case TeamName.Yellow:
                                Player.ChangeLocationTo(Location.Inside_Yellow_Castle);
                                Map.UpdateLocationsTo(Location.Inside_Yellow_Castle);
                                Player.SetRectangle(new Rectangle(team.Value.GetCastle().GetInsideDoor().GetImage().GetRectangle().Left,
                                                                  team.Value.GetCastle().GetInsideDoor().GetImage().GetRectangle().Bottom - 50,
                                                                  Player.GetRectangle().Width,
                                                                  Player.GetRectangle().Height));
                                break;
                        }
                        break;
                    }
                }
                else if (Player.IsTouchCastleDoor(team.Value.GetCastle().GetInsideDoor())) {
                    if (Keyboard.GetState().IsKeyDown(Keys.E) && !IsPressedE) {
                        IsPressedE = true;
                        switch (team.Value.GetName()) {
                            case TeamName.Red:
                            case TeamName.Yellow:
                                Player.ChangeLocationTo(Location.Outside);
                                Map.UpdateLocationsTo(Location.Outside);
                                Player.SetRectangle(new Rectangle(team.Value.GetCastle().GetOutsideDoor().GetImage().GetRectangle().Left,
                                                                  team.Value.GetCastle().GetOutsideDoor().GetImage().GetRectangle().Bottom - 50,
                                                                  Player.GetRectangle().Width,
                                                                  Player.GetRectangle().Height));
                                break;
                        }
                        break;
                    }
                }
            }
        }

        private void CheckKeyboard() {
 
            if (Player.GetState() != PlayerState.Attack && Player.GetState() != PlayerState.Defence && Player.GetState() != PlayerState.Loot) {

                if (Keyboard.GetState().GetPressedKeys().Length == 0) {
                    Player.SetState(PlayerState.Afk);
                    if(Player.GetCurrentHorse() != null) {
                        Player.GetCurrentHorse().SetState(Horsestate.Afk);
                    }
                }

                CheckMovement();
            }

            CheckChangeCharacter();
            CheckAttack();
            CheckDefence();
            CheckLoot();
            CheckMountHorse();
            CheckEnterExitCastleDoors();

        }

        private void InitPlayer() {

            Player = new Player(CharacterName.Knight, TeamName.Red, "Idan", Map.GetGrass().GetRectangle().X + 25, Map.GetGrass().GetRectangle().Top - 75, 125, 175);
        }

        private void InitMap() {
            Map = new Map();
        }

        private void InitHUD() {

            HUD = new HUD();
            UpdateHud();
        }

        private void UpdateHud() {
            HUD.SetPlayerAvatar(Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeamName());

            if (Player.CurrentCharacter is Archer) {
                HUD.SetPlayerWeapon(Weapon.Bow, Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeamName());
                HUD.SetPlayerWeaponAmmo(((Archer)Player.CurrentCharacter).CurrentArrows + "/" + ((Archer)Player.CurrentCharacter).MaxArrows);
            }
            else if (Player.CurrentCharacter is Knight) {
                HUD.SetPlayerWeapon(Weapon.Sword, Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeamName());
                HUD.SetPlayerWeaponAmmo(string.Empty);
            }
            else if (Player.CurrentCharacter is Mage) {
                HUD.SetPlayerWeapon(Weapon.Wand, Player.CurrentCharacter.GetName(), Player.CurrentCharacter.GetTeamName());
                HUD.SetPlayerWeaponAmmo(((Mage)Player.CurrentCharacter).CurrentSpells + "/" + ((Mage)Player.CurrentCharacter).MaxSpells);
            }
        }

        private void UpdatePlayer() {
            Player.Update();

            if (Player.CurrentCharacter is Archer) {

                Archer archer = Player.CurrentCharacter as Archer;

                for (int i = 0; i < archer.GetArrows().Count; i++) {
                    if (!archer.GetArrows() [i].IsFinished)
                        archer.GetArrows() [i].Move();
                    else {
                        Map.AddEntity(MapEntityName.Arrow,
                                      archer.GetArrows() [i].GetAnimation().GetCurrentSpriteImage().GetRectangle().X,
                                      archer.GetArrows() [i].GetAnimation().GetCurrentSpriteImage().GetRectangle().Y,
                                      archer.GetArrows() [i].GetDirection(), archer.GetArrows() [i].GetDirection() == Direction.Right ? 0.7f : -0.7f, Player.GetCurrentLocation());
                        archer.GetArrows().RemoveAt(i);
                    }
                }
            }
            else if (Player.CurrentCharacter is Mage) {

                Mage mage = Player.CurrentCharacter as Mage;

                for (int i = 0; i < mage.GetSpells().Count; i++) {
                    if (!mage.GetSpells() [i].IsFinished)
                        mage.GetSpells() [i].Move();
                    else {
                        mage.GetSpells().RemoveAt(i);
                    }
                }
            }
        }

        private void UpdatePlayers() {

            foreach (KeyValuePair<TeamName, Team> team in Map.GetTeams()) {

                foreach (KeyValuePair<string, Player> onlinePlayer in team.Value.GetPlayers()) {
                    onlinePlayer.Value.Update();

                    if (onlinePlayer.Value.CurrentCharacter is Archer) {

                        Archer archer = onlinePlayer.Value.CurrentCharacter as Archer;

                        for (int i = 0; i < archer.GetArrows().Count; i++) {
                            if (!archer.GetArrows() [i].IsFinished)
                                archer.GetArrows() [i].Move();
                            else {
                                Map.AddEntity(MapEntityName.Arrow,
                                              archer.GetArrows() [i].GetAnimation().GetCurrentSpriteImage().GetRectangle().X,
                                              archer.GetArrows() [i].GetAnimation().GetCurrentSpriteImage().GetRectangle().Y,
                                              archer.GetArrows() [i].GetDirection(), archer.GetArrows() [i].GetDirection() == Direction.Right ? 0.7f : -0.7f, Player.GetCurrentLocation());
                                archer.GetArrows().RemoveAt(i);
                            }
                        }
                    }
                    else if (onlinePlayer.Value.CurrentCharacter is Mage) {

                        Mage mage = onlinePlayer.Value.CurrentCharacter as Mage;

                        for (int i = 0; i < mage.GetSpells().Count; i++) {
                            if (!mage.GetSpells() [i].IsFinished)
                                mage.GetSpells() [i].Move();
                            else {
                                mage.GetSpells().RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public override void Update() {
            CheckKeyboard();
            UpdatePlayer();
            UpdatePlayers();
            GenerateXp();
            Map.Update();
            Camera.Focus(new Vector2(Player.GetRectangle().X, Player.GetRectangle().Y), Map.WIDTH, Map.HEIGHT);
            HUD.Update();
        }

        public override void Draw() {

            CastleBridge.SpriteBatch.Begin();
            Map.GetWeather().DrawStuck();
            CastleBridge.SpriteBatch.End();

            CastleBridge.SpriteBatch.Begin(SpriteSortMode.Deferred,
                            BlendState.AlphaBlend,
                            null,
                            null,
                            null,
                            null,
                            Camera.Transform
                            );

            Map.GetGrass().Draw();
            Map.GetWeather().DrawClouds();

            Map.DrawCastles(Player.GetCurrentLocation());

            for (int i = Map.GetGrass().GetRectangle().Top; i < Map.GetGrass().GetRectangle().Bottom; i++) {

                Map.DrawTile(i, Player.GetCurrentLocation());

                if (Player.GetCurrentAnimation().GetCurrentSpriteImage().GetRectangle().Bottom - 10 == i)
                    Player.Draw();

                if (Player.CurrentCharacter is Archer) {
                    Archer archer = Player.CurrentCharacter as Archer;
                    foreach (Arrow arrow in archer.GetArrows()) {
                        if (arrow.GetAnimation().GetCurrentSpriteImage().GetRectangle().Bottom == i)
                            arrow.Draw();
                    }
                }
                else if (Player.CurrentCharacter is Mage) {
                    Mage mage = Player.CurrentCharacter as Mage;
                    foreach (EnergyBall energyBall in mage.GetSpells()) {
                        if (energyBall.GetAnimation().GetCurrentSpriteImage().GetRectangle().Bottom == i)
                            energyBall.Draw();
                    }
                }


            }

            HUD.DrawTile();

            foreach(Cloud cloud in Map.GetWeather().GetClouds()) {
                foreach(RainDrop rainDrop in cloud.GetRainDrops()) {
                    if (cloud.IsRain) {
                        rainDrop.Draw();
                    }
                }
            }

            CastleBridge.SpriteBatch.End();



            CastleBridge.SpriteBatch.Begin();
            HUD.DrawStuck();
            CastleBridge.SpriteBatch.End();

        }
    }
}
