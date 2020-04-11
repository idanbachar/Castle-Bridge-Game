﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleBridge {
    public abstract class Character {

        protected int Health;
        protected int Level;
        protected CharacterName Name;
        protected Image Shadow;
        protected Animation CurrentAnimation;
        public Animation AfkAnimation;
        public Animation WalkAnimation;
        public Animation AttackAnimation;
        public Animation DefenceAnimation;
        public Animation LootAnimation;
        protected Direction Direction;
        protected PlayerState State;
        public Character(CharacterName name, int x, int y, int width, int height) {

            Name = name;
            AfkAnimation = new Animation(new Image("player/characters/" + name + "/afk/", name + "_afk_", x, y, width, height, Color.White), 0, 6, 6, 6, true, true);
            WalkAnimation = new Animation(new Image("player/characters/" + name + "/walk/", name + "_walk_", x, y, width, height, Color.White), 0, 4, 4, 3, true, true);
            AttackAnimation = new Animation(new Image("player/characters/" + name + "/attack/", name + "_attack_", x, y, width, height, Color.White), 0, 6, 7, 4, false, false);
            LootAnimation = new Animation(new Image("player/characters/" + name + "/loot/", name + "_loot_", x, y, width, height, Color.White), 0, 4, 5, 4, true, false);
            
            Health = 100;
            Shadow = new Image("player/characters/" + name + "/shadow/", name + "_shadow", x, y, width / 2, height / 2, Color.White);
            State = PlayerState.Afk;
            SetCurrentAnimation(State);
            CurrentAnimation.Start();
            Level = 0;
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

        public void SetNewDirection(Direction newDirection) {

            Direction = newDirection;

            Shadow.SetNewDirection(newDirection);

            AfkAnimation.SetDirection(newDirection);
            WalkAnimation.SetDirection(newDirection);
            AttackAnimation.SetDirection(newDirection);
            LootAnimation.SetDirection(newDirection);
            //DefenceAnimation.SetDirection(newDirection);

        }

        public virtual void Update() {

            CurrentAnimation.Play();
            Shadow.SetNewRectangle(CurrentAnimation.GetCurrentSprite().GetRectangle().Left, CurrentAnimation.GetCurrentSprite().GetRectangle().Bottom, CurrentAnimation.GetCurrentSprite().GetRectangle().Width, CurrentAnimation.GetCurrentSprite().GetRectangle().Height);
        }

        public void SetHealth(int hp) {
            Health = hp;
        }

        public int GetHealth() {
            return Health;
        }

        public int GetLevel() {
            return Level;
        }

        public void LevelUp() {
            Level++;
        }

        public CharacterName GetName() {
            return Name;
        }

        public void SetNewRectangle(Rectangle newRectangle) {

            AfkAnimation.SetNewRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            WalkAnimation.SetNewRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            AttackAnimation.SetNewRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            LootAnimation.SetNewRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
            //DefenceAnimation.SetNewRectangle(newRectangle.X, newRectangle.Y, newRectangle.Width, newRectangle.Height);
    
        }

        public virtual void Draw() {
            CurrentAnimation.Draw();
            //Shadow.Draw();
        }
    }
}