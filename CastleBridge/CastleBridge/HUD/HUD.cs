﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class HUD {

        private List<Text> Labels;
        private Image PlayerAvatar;
        private Image HorseAvatar;
        private Image PlayerWeapon;
        private Image PlayerHealthBar;
        private Image PlayerLevelBar;
        private Text PlayerWeaponAmmo;
        private Text PlayerHealth;
        private Text PlayerLevel;
        private List<Popup> TilePopups;
        private List<Popup> StuckPopups;
        public HUD() {

            Labels = new List<Text>();
            TilePopups = new List<Popup>();
            StuckPopups = new List<Popup>();
            PlayerAvatar = new Image(string.Empty, string.Empty, 30, CastleBridge.Graphics.PreferredBackBufferHeight - 135, 100, 100, Color.White);
            PlayerWeapon = new Image(string.Empty, string.Empty, PlayerAvatar.GetRectangle().Right, PlayerAvatar.GetRectangle().Top, 50, 50, Color.White);
            PlayerHealthBar = new Image("player/health", "health_bar", PlayerAvatar.GetRectangle().Left, PlayerAvatar.GetRectangle().Top - 50, 100, 25, Color.White);
            HorseAvatar = new Image("horse/teams/red/avatar", "horse_avatar", PlayerHealthBar.GetRectangle().Left, PlayerHealthBar.GetRectangle().Top - 100, 100, 100, Color.White);
            HorseAvatar.SetVisible(false);
            PlayerLevelBar = new Image("player/level", "level_bar", PlayerAvatar.GetRectangle().Left, PlayerAvatar.GetRectangle().Bottom, 0, 25, Color.White);
            PlayerWeaponAmmo = new Text(FontType.Default, "0", new Vector2(PlayerWeapon.GetRectangle().Left, PlayerWeapon.GetRectangle().Bottom + 5), Color.White, true, Color.Black);
            PlayerHealth = new Text(FontType.Default, "100/100", new Vector2(PlayerHealthBar.GetRectangle().Left + PlayerHealthBar.GetRectangle().Width / 2, PlayerHealthBar.GetRectangle().Top), Color.White, false, Color.Black);
            PlayerLevel = new Text(FontType.Default, "0/100", new Vector2(PlayerLevelBar.GetRectangle().Left + PlayerLevelBar.GetRectangle().Width / 2, PlayerLevelBar.GetRectangle().Top), Color.White, false, Color.Black);
        }

        public void AddLabel(Text text) {
            Labels.Add(text);
        }

        public void AddPopup(Popup popup, bool isTile) {

            if(isTile)
                TilePopups.Add(popup);
            else {
                StuckPopups.Add(popup);
            }
        }

        public void SetPlayerAvatar(CharacterName name, TeamName teamName) {
            PlayerAvatar.ChangeImage("player/characters/teams/" + teamName + "/" + name + "/avatar/" + name + "_avatar");
        }

        public void SetPlayerWeapon(Weapon weapon, CharacterName name, TeamName teamName) {
            PlayerWeapon.ChangeImage("player/characters/teams/" + teamName + "/" + name + "/weapons/" + weapon + "/" + name + "_" + weapon + "_avatar");
        }

        public void SetPlayerWeaponAmmo(string ammo) {
            PlayerWeaponAmmo.ChangeText(ammo);
        }

        public void SetHorseAvatar(TeamName teamName) {
            HorseAvatar.ChangeImage("horse/teams/" + teamName + "/avatar/horse_avatar");
        }

        public void AddPlayerHealth(int health, int maxHealth) {
            if (PlayerHealthBar.GetRectangle().Width <= maxHealth) {
                PlayerHealthBar.SetRectangle(PlayerHealthBar.GetRectangle().X, PlayerHealthBar.GetRectangle().Y, PlayerHealthBar.GetRectangle().Width + health, PlayerHealthBar.GetRectangle().Height);
                PlayerHealth.ChangeText(PlayerHealthBar.GetRectangle().Width.ToString() + "/" + maxHealth + "hp");
                PlayerHealth.SetPosition(new Vector2(PlayerHealthBar.GetRectangle().Left + PlayerHealthBar.GetRectangle().Width / 2, PlayerHealthBar.GetRectangle().Top));
            }
        }

        private void SetPlayerXp(int levelXp, int maxLevelXp) {
            PlayerLevelBar.SetRectangle(PlayerLevelBar.GetRectangle().X, PlayerLevelBar.GetRectangle().Y, levelXp, PlayerLevelBar.GetRectangle().Height);
            PlayerLevel.ChangeText(PlayerLevelBar.GetRectangle().Width.ToString() + "/" + maxLevelXp + "xp");
        }

        public void AddPlayerXp(int levelXp, int maxLevelXp) {
            if (PlayerLevelBar.GetRectangle().Width + levelXp < maxLevelXp) {
                PlayerLevelBar.SetRectangle(PlayerLevelBar.GetRectangle().X, PlayerLevelBar.GetRectangle().Y, PlayerLevelBar.GetRectangle().Width + levelXp, PlayerLevelBar.GetRectangle().Height);
                PlayerLevel.ChangeText(PlayerLevelBar.GetRectangle().Width.ToString() + "/" + maxLevelXp + "xp");
            }
            else {
                SetPlayerXp(0, maxLevelXp + 100);
            }
        }

        private void UpdateTilePopups() {

            for (int i = 0; i < TilePopups.Count; i++) {
                if (!TilePopups [i].IsFinished)
                    TilePopups [i].Update();
                else {
                    TilePopups.RemoveAt(i);
                }
            }
        }
        private void UpdateStuckPopups() {

            for (int i = 0; i < StuckPopups.Count; i++) {
                if (!StuckPopups [i].IsFinished)
                    StuckPopups [i].Update();
                else {
                    StuckPopups.RemoveAt(i);
                }
            }
        }

        public void Update() {

            UpdateTilePopups();
            UpdateStuckPopups();
        }

        public List<Text> GetLabels() {
            return Labels;
        }

        public List<Popup> GetPopups() {
            return TilePopups;
        }

        public void DrawTile() {

            foreach (Popup popup in TilePopups)
                popup.Draw();
        }

        public Image GetPlayerAvatar() {
            return PlayerAvatar;
        }

        public Image GetHorseAvatar() {
            return HorseAvatar;
        }

        public Image GetPlayerHealthBar() {
            return PlayerHealthBar;
        }

        public Image GetPlayerLevelBar() {
            return PlayerLevelBar;
        }

        public Text GetPlayerWeaponAmmo() {
            return PlayerWeaponAmmo;
        }

        public void DrawStuck() {

            foreach (Text label in Labels)
                label.Draw();

            PlayerAvatar.Draw();
            PlayerWeapon.Draw();
            PlayerWeaponAmmo.Draw();
            PlayerHealthBar.Draw();
            PlayerHealth.Draw();
            PlayerLevelBar.Draw();
            PlayerLevel.Draw();
            HorseAvatar.Draw();

            foreach (Popup popup in StuckPopups)
                popup.Draw();
        }
    
    }
}
