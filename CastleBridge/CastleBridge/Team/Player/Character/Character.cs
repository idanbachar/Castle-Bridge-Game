using Microsoft.Xna.Framework;
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
        public Character(CharacterName name, TeamName teamName, int x, int y, int width, int height) {

            Name = name;
            TeamName = teamName;
            AfkAnimation = new Animation("player/characters/teams/" + teamName + "/" + name + "/afk/" + name + "_afk_", new Rectangle(x, y, width, height), 0, 6, 6, 5, true, true);
            WalkAnimation = new Animation("player/characters/teams/" + teamName + "/" + name + "/walk/" + name + "_walk_", new Rectangle(x, y, width, height), 0, 4, 4, 3, true, true);
            AttackAnimation = new Animation("player/characters/teams/" + teamName + "/" + name + "/attack/" + name + "_attack_", new Rectangle(x, y, width, height), 0, 7, 7, 4, false, false);
            DefenceAnimation = new Animation("player/characters/teams/" + teamName + "/" + name + "/defence/" + name + "_defence_", new Rectangle(x, y, width, height), 0, 5, 5, 3, true, false);
            LootAnimation = new Animation("player/characters/teams/" + teamName + "/" + name + "/loot/" + name + "_loot_", new Rectangle(x, y, width, height), 0, 5, 5, 2, true, false);
            Health = 100;
            MaxHealth = 100;
            State = PlayerState.Afk;
            SetCurrentAnimation(State);
            CurrentAnimation.Start();
            Level = 0;
            Xp = 0;
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
            CurrentAnimation.Start();
        }

        public void SetDirection(Direction newDirection) {

            Direction = newDirection;

            AfkAnimation.SetDirection(newDirection);
            WalkAnimation.SetDirection(newDirection);
            AttackAnimation.SetDirection(newDirection);
            LootAnimation.SetDirection(newDirection);
            DefenceAnimation.SetDirection(newDirection);

        }

        public Animation GetCurrentAnimation() {
            return CurrentAnimation;
        }

        public virtual void Update() {
            CurrentAnimation.Play();
        }

        public void SetHealth(int hp) {
            Health = hp;
        }

        public void IncreaseHp(int hp) {
            Health += Health < MaxHealth ? hp : 0;
        }

        public void DecreaseHp(int hp) {
            Health -= (Health - hp) > 0 ? hp : 0;
        }

        public int GetHealth() {
            return Health;
        }

        public int GetLevel() {
            return Level;
        }

        public void LevelUp() {
            Level++;
            Xp = 0;
        }

        public void AddXp(int xp) {

            Xp += xp;
            if (Xp >= 100) {
                LevelUp();
            }
        }

        public CharacterName GetName() {
            return Name;
        }

        public TeamName GetTeamName() {
            return TeamName;
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
