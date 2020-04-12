using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public class HUD {

        private List<Text> Labels;
        private Image PlayerAvatar;
        private Image PlayerWeapon;
        private Text PlayerWeaponAmmo;
        private List<Popup> Popups;
        public HUD() {

            Labels = new List<Text>();
            Popups = new List<Popup>();
            PlayerAvatar = new Image(string.Empty, string.Empty, 30, Game1.Graphics.PreferredBackBufferHeight - 135, 100, 100, Color.White);
            PlayerWeapon = new Image(string.Empty, string.Empty, PlayerAvatar.GetRectangle().Right, PlayerAvatar.GetRectangle().Top, 50, 50, Color.White);
            PlayerWeaponAmmo = new Text(FontType.Default, "0", new Vector2(PlayerWeapon.GetRectangle().Left, PlayerWeapon.GetRectangle().Bottom + 5), Color.White, true, Color.Black);
        }

        public void AddLabel(Text text) {
            Labels.Add(text);
        }

        public void AddPopup(Popup popup) {
            Popups.Add(popup);
        }

        public void SetPlayerAvatar(CharacterName name) {
            PlayerAvatar.SetNewImage("player/characters/" + name + "/avatar/" + name + "_avatar");
        }

        public void SetPlayerWeapon(Weapon weapon, CharacterName name) {
            PlayerWeapon.SetNewImage("player/characters/" + name + "/weapons/" + weapon + "/" + name + "_" + weapon + "_avatar");
        }

        public void SetPlayerWeaponAmmo(string ammo) {
            PlayerWeaponAmmo.SetText(ammo);
        }

        public void Update() {

            for(int i = 0; i < Popups.Count; i++) {
                if (!Popups [i].IsFinished)
                    Popups [i].Update();
                else {
                    Popups.RemoveAt(i);
                }
            }
 
        }

        public List<Text> GetLabels() {
            return Labels;
        }

        public List<Popup> GetPopups() {
            return Popups;
        }

        public void DrawTile() {

            foreach (Popup popup in Popups)
                popup.Draw();
        }

        public void DrawStuck() {

            foreach (Text label in Labels)
                label.Draw();

            PlayerAvatar.Draw();
            PlayerWeapon.Draw();
            PlayerWeaponAmmo.Draw();
        }
    
    }
}
