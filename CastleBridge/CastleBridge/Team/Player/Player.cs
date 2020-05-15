using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastleBridge.OnlineLibraries;

namespace CastleBridge {
    public class Player {

        private Text NameLabel; //Player's name label
        private Dictionary<string, Character> Characters; //Player's characters
        public Character CurrentCharacter; //Player's current playing character
        private Rectangle Rectangle; //Player's rectangle
        private PlayerState State; //Player's state
        private TeamName TeamName; //Player's team
        private int CurrentSpeed; //Player's current speed
        private int DefaultSpeed; //Player's default speed
        private int Stones; //Player's stones
        private int Woods; //Player's woods
        private Horse CurrentHorse; //Player's current horse (null if isn't an owner.)
        private Location CurrentLocation; //Player's current location
        private List<Diamond> CollectedRedDiamonds; //Player's collected red diamonds
        private List<Diamond> CollectedYellowDiamonds; //Player's collected yellow diamonds
        private CharacterName CharacterName; //Player's character name type (Knight/Archer/Mage)
        private string Name; //Player's name's string
        public bool IsDead; //Player's dead indication

        public bool CanStartRespawnTimer; //Player's can start respawn after dies indication

        private Random Rnd;

        //Add health event:
        public delegate void AddHealth(int health, int maxHealth);
        public event AddHealth OnAddHealth;

        //Minus health event:
        public delegate void MinusHealth(int health, int maxHealth);
        public event MinusHealth OnMinusHealth;

        //Change to a new location event:
        public delegate void ChangeLocation(Location newLocation);
        public event ChangeLocation OnChangeLocation;

        //Map's floor's rectangle:
        private Rectangle FloorRectangle;

        /// <summary>
        /// Receives character type's name, team, player's name, and map's floor's rectangle
        /// and creates a player.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="teamName"></param>
        /// <param name="name"></param>
        /// <param name="floorRectangle"></param>
        public Player(CharacterName character, TeamName teamName, string name, Rectangle floorRectangle) {
            TeamName = teamName;
            Name = name;
            DefaultSpeed = 3;
            CurrentSpeed = DefaultSpeed;
            Characters = new Dictionary<string, Character>();
            State = PlayerState.Afk;
            Stones = 0;
            Woods = 0;
            CollectedRedDiamonds = new List<Diamond>();
            CollectedYellowDiamonds = new List<Diamond>();
            CurrentHorse = null;
            CurrentLocation = Location.Outside;
            FloorRectangle = floorRectangle;
            CharacterName = character;
            Rnd = new Random();
            CanStartRespawnTimer = false;
            IsDead = false;
        }

        /// <summary>
        /// Receives a new character type name and changes it to current character.
        /// </summary>
        /// <param name="newCharacter"></param>
        public void ChangeCharacter(CharacterName newCharacter) {
            CharacterName = newCharacter;
            CurrentCharacter = Characters[newCharacter.ToString()];
        }

        /// <summary>
        /// Receives a new team
        /// and changes it to current team
        /// </summary>
        /// <param name="newTeam"></param>
        public void ChangeTeam(TeamName newTeam) {

            TeamName = newTeam;

            //Update all characters's teams:
            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.ChangeTeam(newTeam);

        }

        /// <summary>
        /// Add red diamonds to list
        /// </summary>
        /// <param name="diamond"></param>
        public void AddRedDiamond(Diamond diamond) {
            CollectedRedDiamonds.Add(diamond);
        }

        /// <summary>
        /// Add yellow diamonds to list
        /// </summary>
        /// <param name="diamond"></param>
        public void AddYellowDiamond(Diamond diamond) {
            CollectedYellowDiamonds.Add(diamond);
        }

        /// <summary>
        /// Drops all red diamonds
        /// </summary>
        /// <returns></returns>
        public List<Diamond> DropAllRedDiamonds() {

            Random rnd = new Random();

            List<Diamond> diamondsToDrop = new List<Diamond>();

            foreach (Diamond diamond in CollectedRedDiamonds) {
                diamond.ChangeLocationTo(CurrentLocation);
                diamond.RemoveOwner();
                diamond.SetRectangle(new Rectangle(Rectangle.X + rnd.Next(0, 200),
                                                   Rectangle.Bottom,
                                                   diamond.GetImage().GetRectangle().Width,
                                                   diamond.GetImage().GetRectangle().Height));
                diamondsToDrop.Add(diamond);
            }

            CollectedRedDiamonds.Clear();

            return diamondsToDrop;
        }

        /// <summary>
        /// Drops all yellow diamonds
        /// </summary>
        /// <returns></returns>
        public List<Diamond> DropAllYellowDiamonds() {

            Random rnd = new Random();

            List<Diamond> diamondsToDrop = new List<Diamond>();

            foreach (Diamond diamond in CollectedYellowDiamonds) {
                diamond.ChangeLocationTo(CurrentLocation);
                diamond.RemoveOwner();
                diamond.SetRectangle(new Rectangle(Rectangle.X + rnd.Next(0, 200),
                                                   Rectangle.Bottom,
                                                   diamond.GetImage().GetRectangle().Width,
                                                   diamond.GetImage().GetRectangle().Height));

                diamondsToDrop.Add(diamond);
            }

            CollectedYellowDiamonds.Clear();

            return diamondsToDrop;
        }

        /// <summary>
        /// Adds a new character
        /// </summary>
        /// <param name="name"></param>
        private void AddCharacter(CharacterName name) {

            Character character = null;

            switch (name) {
                case CharacterName.Archer:
                    character = new Archer(name, TeamName, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    break;
                case CharacterName.Knight:
                    character = new Knight(name, TeamName, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    break;
                case CharacterName.Mage:
                    character = new Mage(name, TeamName, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
                    break;
            }

            Characters.Add(name.ToString(), character);
        }

        /// <summary>
        /// Receives a direction
        /// and moves the player to the current direction.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Direction direction) {

            switch (direction) {
                case Direction.Up:
                    SetRectangle(new Rectangle(Rectangle.X, Rectangle.Y - CurrentSpeed, Rectangle.Width, Rectangle.Height));
                    break;
                case Direction.Down:
                    SetRectangle(new Rectangle(Rectangle.X, Rectangle.Y + CurrentSpeed, Rectangle.Width, Rectangle.Height));
                    break;
                case Direction.Right:
                    SetRectangle(new Rectangle(Rectangle.X + CurrentSpeed, Rectangle.Y, Rectangle.Width, Rectangle.Height));
                    break;
                case Direction.Left:
                    SetRectangle(new Rectangle(Rectangle.X - CurrentSpeed, Rectangle.Y, Rectangle.Width, Rectangle.Height));
                    break;
            }
        }

        /// <summary>
        /// Receives an entity and returns true if player touches it else returns false
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsTouchWorldEntity(MapEntity entity) {

            if (Rectangle.Intersects(entity.GetAnimation().GetCurrentSpriteImage().GetRectangle()) &&
                                     entity.IsTouchable &&
                                     CurrentHorse == null &&
                                     (CurrentLocation == entity.GetCurrentLocation() || entity.GetCurrentLocation() == Location.All)) {
                entity.GetTooltip().SetVisible(true);
                return true;
            }

            entity.GetTooltip().SetVisible(false);
            return false;
        }

        /// <summary>
        /// Receives an arrow and returns true if player touches it else returns false
        /// </summary>
        /// <param name="arrow"></param>
        /// <returns></returns>
        public bool IsTouchArrow(Arrow arrow) {

            if (Rectangle.Intersects(arrow.GetAnimation().GetCurrentSpriteImage().GetRectangle()) && 
                arrow.GetOwner().GetTeamName() != TeamName &&
                (CurrentLocation == arrow.GetCurrentLocation() || arrow.GetCurrentLocation() == Location.All)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Receives an energy ball spell and returns true if player touches it else returns false
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public bool IsTouchSpell(EnergyBall spell) {

            if (Rectangle.Intersects(spell.GetAnimation().GetCurrentSpriteImage().GetRectangle()) &&
                spell.GetOwner().GetTeamName() != TeamName &&
                (CurrentLocation == spell.GetCurrentLocation() || spell.GetCurrentLocation() == Location.All)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Receives a door and returns true if player touches it else returns false
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public bool IsTouchCastleDoor(Door door) {

            if (Rectangle.Intersects(door.GetImage().GetRectangle()) && CurrentHorse == null && (CurrentLocation == door.GetCurrentLocation() || door.GetCurrentLocation() == Location.All)) {
                door.GetTooltip().SetVisible(true);
                return true;
            }

            door.GetTooltip().SetVisible(false);
            return false;
        }

        /// <summary>
        /// Receives a diamond and returns true if player touches it else returns false
        /// </summary>
        /// <param name="diamond"></param>
        /// <returns></returns>
        public bool IsTouchDiamond(Diamond diamond) {

            if (Rectangle.Intersects(diamond.GetImage().GetRectangle()) &&
                CurrentHorse == null &&
                diamond.GetOwner() == null &&
                (CurrentLocation == diamond.GetCurrentLocation() || diamond.GetCurrentLocation() == Location.All))
                return true;

            return false;
        }

        /// <summary>
        /// Receives a horse and returns true if player touches it else returns false
        /// </summary>
        /// <param name="horse"></param>
        /// <returns></returns>
        public bool IsTouchHorse(Horse horse) {

            if (Rectangle.Intersects(horse.GetRectangle()) &&
                CurrentHorse == null &&
                TeamName == horse.GetTeamName() &&
                (CurrentLocation == horse.GetCurrentLocation() || horse.GetCurrentLocation() == Location.All))
                return true;

            return false;
        }

        /// <summary>
        /// Receives an online player and returns true if player touches it else returns false
        /// </summary>
        /// <param name="onlinePlayer"></param>
        /// <returns></returns>
        public bool IsTouchOnlinePlayer(Player onlinePlayer) {

            if (Rectangle.Intersects(onlinePlayer.GetRectangle()) && CurrentLocation == onlinePlayer.GetCurrentLocation())
                return true;

            return false;
        }

        /// <summary>
        /// Receives a new player state
        /// and applies it
        /// </summary>
        /// <param name="state"></param>
        public void SetState(PlayerState state) {

            State = state;

            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetCurrentAnimation(state);
        }

        /// <summary>
        /// Receives a new speed and applies it
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(int speed) {
            CurrentSpeed = speed;
        }

        /// <summary>
        /// Get team
        /// </summary>
        /// <returns></returns>
        public TeamName GetTeamName() {
            return TeamName;
        }

        /// <summary>
        /// Update player's stuff like character's updates and check if is dead
        /// </summary>
        public void Update() {

            if (CurrentCharacter != null)
                CurrentCharacter.Update();

            IsDead = CurrentCharacter.IsDead;
            if (IsDead) {
                Dead();
            }
        }

        /// <summary>
        /// Receives a damage and hits player
        /// </summary>
        /// <param name="damage"></param>
        public void Hit(int damage) {

            CurrentCharacter.DecreaseHp(damage);
            OnMinusHealth(damage, CurrentCharacter.GetMaxHealth());
        }

        /// <summary>
        /// Kill current player and start respawn timer
        /// </summary>
        public void Dead() {
            CanStartRespawnTimer = true;
        }

        /// <summary>
        /// Respawn player after dies and respawn timer finished
        /// </summary>
        public void Respawn() {

            int x = 0;
            int y = FloorRectangle.Top - 75 + Rnd.Next(250);
            SetDirection(Direction.Right);

            switch (TeamName) {
                case TeamName.Red:
                    x = FloorRectangle.Left + 150 + Rnd.Next(150);
                    SetDirection(Direction.Right);
                    break;
                case TeamName.Yellow:
                    x = FloorRectangle.Right - 125 - Rnd.Next(150);
                    SetDirection(Direction.Left);
                    break;
            }

            Rectangle = new Rectangle(x, y, 125, 175);
            NameLabel = new Text(FontType.Default, Name, new Vector2(Rectangle.Left + Rectangle.Width / 2 - 5, Rectangle.Bottom + 5), Color.Gold, true, Color.Black);

            Characters.Clear();
            AddCharacter(CharacterName.Archer);
            AddCharacter(CharacterName.Knight);
            AddCharacter(CharacterName.Mage);
            ChangeCharacter(CharacterName);
            CurrentCharacter.SetCurrentAnimation(State);
            ChangeLocationTo(Location.Outside);
            

            //OnAddHealth(CurrentCharacter.GetMaxHealth(), CurrentCharacter.GetMaxHealth());
            IsDead = false;
            CanStartRespawnTimer = false;
        }

        /// <summary>
        /// Receives true/false of visibility and applies it on player's visibility
        /// </summary>
        /// <param name="value"></param>
        public void SetVisible(bool value) {

            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetVisible(value);

            NameLabel.SetVisible(value);
        }

        /// <summary>
        /// Get current character
        /// </summary>
        /// <returns></returns>
        public Character GetCurrentCharacter() {
            return CurrentCharacter;
        }

        /// <summary>
        /// Receives a new direction
        /// and applies it
        /// </summary>
        /// <param name="newDirection"></param>
        public void SetDirection(Direction newDirection) {

            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetDirection(newDirection);
        }

        /// <summary>
        /// Receives a new rectangle
        /// and applies it
        /// </summary>
        /// <param name="newRectangle"></param>
        public void SetRectangle(Rectangle newRectangle) {

            Rectangle.X = newRectangle.X;
            Rectangle.Y = newRectangle.Y;
            Rectangle.Width = newRectangle.Width;
            Rectangle.Height = newRectangle.Height;

            //Applies new rectangle on each character:
            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetRectangle(newRectangle);

            //Updates name label position:
            NameLabel.SetPosition(new Vector2(newRectangle.Left + newRectangle.Width / 2 - 5, newRectangle.Bottom + 5));
        }

        /// <summary>
        /// Get Rectangle
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle() {
            return Rectangle;
        }

        /// <summary>
        /// Get state
        /// </summary>
        /// <returns></returns>
        public PlayerState GetState() {
            return State;
        }

        /// <summary>
        /// Get direction
        /// </summary>
        /// <returns></returns>
        public Direction GetDirection() {
            return CurrentCharacter.GetDirection();
        }

        /// <summary>
        /// Checks if player is on top of map.
        /// Returns true if yes, else returns false.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool IsOnTopMap(Map map) {
            if (Rectangle.Bottom - Rectangle.Height / 2 < map.GetGrass().GetRectangle().Top)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if player is on left of map.
        /// Returns true if yes, else returns false.
        /// </summary>
        /// <returns></returns>
        public bool IsOnLeftMap() {
            if (Rectangle.Left <= 0)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if player is on right of map.
        /// Returns true if yes, else returns false.
        /// </summary>
        /// <returns></returns>
        public bool IsOnRightMap() {
            if (Rectangle.Right >= Map.WIDTH)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if player is on bottom of map.
        /// Returns true if yes, else returns false.
        /// </summary>
        /// <returns></returns>
        public bool IsOnBottomMap() {
            if (Rectangle.Bottom > Map.HEIGHT)
                return true;

            return false;
        }

        /// <summary>
        /// Get current horse (returns null if there is no horse)
        /// </summary>
        /// <returns></returns>
        public Horse GetCurrentHorse() {
            return CurrentHorse;
        }

        /// <summary>
        /// Receives number of stones and adds them to stones capacity
        /// </summary>
        /// <param name="stones"></param>
        public void AddStones(int stones) {
            Stones += stones;
        }

        /// <summary>
        /// Get stones
        /// </summary>
        /// <returns></returns>
        public int GetStones() {
            return Stones;
        }


        /// <summary>
        /// Receives number of woods and adds them to woods capacity
        /// </summary>
        /// <param name="woods"></param>
        public void AddWoods(int woods) {
            Woods += woods;
        }

        /// <summary>
        /// Get woods
        /// </summary>
        /// <returns></returns>
        public int GetWoods() {
            return Woods;
        }


        /// <summary>
        /// Get current location
        /// </summary>
        /// <returns></returns>
        public Location GetCurrentLocation() {
            return CurrentLocation;
        }


        /// <summary>
        /// Receives a new location and applies it
        /// </summary>
        /// <param name="newLocation"></param>
        public void ChangeLocationTo(Location newLocation) {
            CurrentLocation = newLocation;

            //Starts an change location event to applies it on map displayed locations
            if (OnChangeLocation != null)
                OnChangeLocation(newLocation);
        }

        /// <summary>
        /// Receives a horse and start mounting on it
        /// </summary>
        /// <param name="horse"></param>
        public void MountHorse(Horse horse) {
            CurrentHorse = horse;
            CurrentHorse.SetOwner(this);
            CurrentHorse.GetTooltip().ChangeText("Press 'F' to dismount");

            //Change speed to horse's speed:
            SetSpeed(horse.GetSpeed());
        }

        /// <summary>
        /// Dismounts current riding horse
        /// </summary>
        public void DismountHorse() {

            //Only if riding a horse:
            if (CurrentHorse != null) {
                CurrentHorse.RemoveOwner();
                CurrentHorse.GetTooltip().SetVisible(false);
                CurrentHorse.GetTooltip().ChangeText("Press 'E' to mount");
                CurrentHorse.GetTooltip().SetPosition(new Vector2(CurrentHorse.GetRectangle().X + 50, CurrentHorse.GetRectangle().Y - 65));
            }
            CurrentHorse = null;

            //Change speed to default player's speed:
            SetSpeed(DefaultSpeed);
        }

        /// <summary>
        /// Get current character's animation
        /// </summary>
        /// <returns></returns>
        public Animation GetCurrentAnimation() {
            return CurrentCharacter.GetCurrentAnimation();
        }

        /// <summary>
        /// Get collected red diamonds
        /// </summary>
        /// <returns></returns>
        public List<Diamond> GetCollectedRedDiamonds() {
            return CollectedRedDiamonds;
        }

        /// <summary>
        /// Get collected yellow diamonds
        /// </summary>
        /// <returns></returns>
        public List<Diamond> GetCollectedYellowDiamonds() {
            return CollectedYellowDiamonds;
        }

        /// <summary>
        /// Get name label's text
        /// </summary>
        /// <returns></returns>
        public string GetName() {
            return NameLabel.GetValue();
        }

        /// <summary>
        /// Draw player
        /// </summary>
        public void Draw() {

            //Draw only if player is not dead:
            if (!IsDead) {

                //Draw name's label:
                NameLabel.Draw();

                //Draw current character:
                CurrentCharacter.Draw();
            }
        }
    }
}
