using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public abstract class Character {

        protected int Health;
        protected int MaxHealth;
        protected int Level;
        protected int Xp;
        protected int MaxXp;
        protected CharacterName Name;
        protected Animation CurrentAnimation;
        public Animation AfkAnimation;
        public Animation WalkAnimation;
        public Animation AttackAnimation;
        public Animation DefenceAnimation;
        public Animation LootAnimation;
        protected Direction Direction;
        protected PlayerState State;
        protected TeamName TeamName;
        private Rectangle StartingRectangle;

        public bool IsDead;
        public Character(CharacterName name, TeamName teamName, int x, int y, int width, int height) {

            Name = name;
            TeamName = teamName;
            StartingRectangle = new Rectangle(x, y, width, height);
            InitAnimations();
            Health = 100;
            MaxHealth = 100;
            State = PlayerState.Afk;
            SetCurrentAnimation(State);
            Level = 0;
            Xp = 0;
            MaxXp = 100;
            IsDead = false;
        }

        private void InitAnimations() {
            AfkAnimation = new Animation("player/characters/teams/" + TeamName + "/" + Name + "/afk/" + Name + "_afk_", StartingRectangle, 0, 6, 6, 5, true, true);
            WalkAnimation = new Animation("player/characters/teams/" + TeamName + "/" + Name + "/walk/" + Name + "_walk_", StartingRectangle, 0, 4, 4, 3, true, true);
            AttackAnimation = new Animation("player/characters/teams/" + TeamName + "/" + Name + "/attack/" + Name + "_attack_", StartingRectangle, 0, 7, 7, 4, false, false);
            DefenceAnimation = new Animation("player/characters/teams/" + TeamName + "/" + Name + "/defence/" + Name + "_defence_", StartingRectangle, 0, 5, 5, 3, true, false);
            LootAnimation = new Animation("player/characters/teams/" + TeamName + "/" + Name + "/loot/" + Name + "_loot_", StartingRectangle, 0, 5, 5, 2, true, false);
        }

        public void SetCurrentAnimation(PlayerState State) {

            switch (State) {
                case PlayerState.Afk:
                    CurrentAnimation = AfkAnimation;
                    break;
                case PlayerState.Walk:
                    CurrentAnimation = WalkAnimation;
                    break;
                case PlayerState.Attack:
                    CurrentAnimation = AttackAnimation;
                    break;
                case PlayerState.Defence:
                    CurrentAnimation = DefenceAnimation;
                    break;
                case PlayerState.Loot:
                    CurrentAnimation = LootAnimation;
                    break;
            }
        }

        public void SetDirection(Direction newDirection) {

            Direction = newDirection;

            AfkAnimation.SetDirection(newDirection);
            WalkAnimation.SetDirection(newDirection);
            AttackAnimation.SetDirection(newDirection);
            LootAnimation.SetDirection(newDirection);
            DefenceAnimation.SetDirection(newDirection);

        }

        public void SetVisible(bool value) {

            AfkAnimation.SetVisible(value);
            WalkAnimation.SetVisible(value);
            AttackAnimation.SetVisible(value);
            LootAnimation.SetVisible(value);
            DefenceAnimation.SetVisible(value);
        }

        public void SetColor(Color color) {

            AfkAnimation.SetColor(color);
            WalkAnimation.SetColor(color);
            AttackAnimation.SetColor(color);
            LootAnimation.SetColor(color);
            DefenceAnimation.SetColor(color);
        }

        public void ChangeTeam(TeamName team) {
            TeamName = team;
            //InitAnimations();
        }

        public bool IsMouseOver() {

            Rectangle mouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 20, 20);

            if (mouseRectangle.Intersects(CurrentAnimation.GetCurrentSpriteImage().GetRectangle())) {
                SetColor(Color.DarkGray);
                return true;
            }

            SetColor(Color.White);
            return false;
        }

        public Animation GetCurrentAnimation() {
            return CurrentAnimation;
        }

        public virtual void Update() {
            CurrentAnimation.Play();
            CurrentAnimation.Start();
        }

        public void SetHealth(int hp) {
            Health = hp;
            CheckIfDead();
        }

        private void CheckIfDead() {
            if (Health <= 0) IsDead = true;
            else IsDead = false;
        }

        public void IncreaseHp(int hp) {
            Health += hp;
            if (Health >= MaxHealth)
                Health = MaxHealth;

            CheckIfDead();
        }

        public void DecreaseHp(int hp) {
            Health -= hp;
            if (Health <= 0)
                Health = 0;

            CheckIfDead();
        }

        public int GetHealth() {
            return Health;
        }

        public int GetMaxHealth() {
            return MaxHealth;
        }

        public int GetLevel() {
            return Level;
        }

        public int GetXp() {
            return Xp;
        }

        public int GetMaxXp() {
            return MaxXp;
        }

        public void LevelUp() {
            Level++;
            Xp = 0;
            MaxXp += 100;
        }

        public void AddXp(int xp) {

            Xp += xp;
            if (Xp >= MaxXp)
                LevelUp();
        }

        public CharacterName GetName() {
            return Name;
        }

        public TeamName GetTeamName() {
            return TeamName;
        }

        public Direction GetDirection() {
            return Direction;
        }

        public void SetRectangle(Rectangle newRectangle) {

            AfkAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            WalkAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            AttackAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            LootAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            DefenceAnimation.SetRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
    
        }

        public virtual void Draw() {
            CurrentAnimation.Draw();
        }
    }
}
