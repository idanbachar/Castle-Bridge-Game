using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastleBridge.OnlineLibraries;

namespace CastleBridge {
    public class Player {

        private Text NameLabel;
        private Dictionary<string, Character> Characters;
        public Character CurrentCharacter;
        private Rectangle Rectangle;
        private PlayerState State;
        private TeamName TeamName;
        private int CurrentSpeed;
        private int DefaultSpeed;
        private int Stones;
        private int Woods;
        private Horse CurrentHorse;
        private Location CurrentLocation;
        private List<Diamond> CollectedRedDiamonds;
        private List<Diamond> CollectedYellowDiamonds;
        private CharacterName CharacterName;
        private string Name;
        public bool IsDead;

        public bool CanStartRespawnTimer;

        private Random Rnd;

        public delegate void AddHealth(int health, int maxHealth);
        public event AddHealth OnAddHealth;

        public delegate void MinusHealth(int health, int maxHealth);
        public event MinusHealth OnMinusHealth;

        public delegate void ChangeLocation(Location newLocation);
        public event ChangeLocation OnChangeLocation;

        private Rectangle FloorRectangle;

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

        public void ChangeCharacter(CharacterName newCharacter) {
            CharacterName = newCharacter;
            CurrentCharacter = Characters[newCharacter.ToString()];
        }

        public void ChangeTeam(TeamName newTeam) {

            TeamName = newTeam;
          
            foreach(KeyValuePair<string, Character> character in Characters) {
                character.Value.ChangeTeam(newTeam);
            }

        }

        public void AddRedDiamond(Diamond diamond) {
            CollectedRedDiamonds.Add(diamond);
        }

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

        public bool IsTouchArrow(Arrow arrow) {

            if (Rectangle.Intersects(arrow.GetAnimation().GetCurrentSpriteImage().GetRectangle()) && 
                arrow.GetOwner().GetTeamName() != TeamName &&
                (CurrentLocation == arrow.GetCurrentLocation() || arrow.GetCurrentLocation() == Location.All)) {
                return true;
            }

            return false;
        }

        public bool IsTouchSpell(EnergyBall spell) {

            if (Rectangle.Intersects(spell.GetAnimation().GetCurrentSpriteImage().GetRectangle()) &&
                spell.GetOwner().GetTeamName() != TeamName &&
                (CurrentLocation == spell.GetCurrentLocation() || spell.GetCurrentLocation() == Location.All)) {
                return true;
            }

            return false;
        }

        public bool IsTouchCastleDoor(Door door) {

            if (Rectangle.Intersects(door.GetImage().GetRectangle()) && CurrentHorse == null && (CurrentLocation == door.GetCurrentLocation() || door.GetCurrentLocation() == Location.All)) {
                door.GetTooltip().SetVisible(true);
                return true;
            }

            door.GetTooltip().SetVisible(false);
            return false;
        }

        public bool IsTouchDiamond(Diamond diamond) {

            if (Rectangle.Intersects(diamond.GetImage().GetRectangle()) &&
                CurrentHorse == null &&
                diamond.GetOwner() == null &&
                (CurrentLocation == diamond.GetCurrentLocation() || diamond.GetCurrentLocation() == Location.All))
                return true;

            return false;
        }

        public bool IsTouchHorse(Horse horse) {

            if (Rectangle.Intersects(horse.GetRectangle()) &&
                CurrentHorse == null &&
                TeamName == horse.GetTeamName() &&
                (CurrentLocation == horse.GetCurrentLocation() || horse.GetCurrentLocation() == Location.All))
                return true;

            return false;
        }

        public bool IsTouchOnlinePlayer(Player onlinePlayer) {

            if (Rectangle.Intersects(onlinePlayer.GetRectangle()) && CurrentLocation == onlinePlayer.GetCurrentLocation())
                return true;

            return false;
        }

        public void SetState(PlayerState state) {

            State = state;

            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetCurrentAnimation(state);
        }

        public void SetSpeed(int speed) {
            CurrentSpeed = speed;
        }

        public TeamName GetTeamName() {
            return TeamName;
        }

        public void Update() {

            if (CurrentCharacter != null)
                CurrentCharacter.Update();

            IsDead = CurrentCharacter.IsDead;
            if (IsDead) {
                Dead();
            }
        }

        public void Hit(int damage) {

            CurrentCharacter.DecreaseHp(damage);
            OnMinusHealth(damage, CurrentCharacter.GetMaxHealth());
        }

        public void Dead() {
            CanStartRespawnTimer = true;
        }

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

        public void SetVisible(bool value) {

            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetVisible(value);

            NameLabel.SetVisible(value);
        }

        public Character GetCurrentCharacter() {
            return CurrentCharacter;
        }

        public void SetDirection(Direction newDirection) {

 
            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetDirection(newDirection);

        }

        public void SetRectangle(Rectangle newRectangle) {

            Rectangle.X = newRectangle.X;
            Rectangle.Y = newRectangle.Y;
            Rectangle.Width = newRectangle.Width;
            Rectangle.Height = newRectangle.Height;

            foreach (KeyValuePair<string, Character> character in Characters)
                character.Value.SetRectangle(newRectangle);

            NameLabel.SetPosition(new Vector2(newRectangle.Left + newRectangle.Width / 2 - 5, newRectangle.Bottom + 5));
        }

        public Rectangle GetRectangle() {
            return Rectangle;
        }

        public PlayerState GetState() {
            return State;
        }

        public Direction GetDirection() {
            return CurrentCharacter.GetDirection();
        }

        public bool IsOnTopMap(Map map) {
            if (Rectangle.Bottom - Rectangle.Height / 2 < map.GetGrass().GetRectangle().Top)
                return true;

            return false;
        }

        public bool IsOnLeftMap() {
            if (Rectangle.Left <= 0)
                return true;

            return false;
        }

        public bool IsOnRightMap() {
            if (Rectangle.Right >= Map.WIDTH)
                return true;

            return false;
        }

        public bool IsOnBottomMap() {
            if (Rectangle.Bottom > Map.HEIGHT)
                return true;

            return false;
        }

        public int GetStones() {
            return Stones;
        }

        public Horse GetCurrentHorse() {
            return CurrentHorse;
        }

        public void AddStones(int stones) {
            Stones += stones;
        }

        public int GetWoods() {
            return Woods;
        }

        public Location GetCurrentLocation() {
            return CurrentLocation;
        }

        public void ChangeLocationTo(Location newLocation) {
            CurrentLocation = newLocation;

            if (OnChangeLocation != null)
                OnChangeLocation(newLocation);
        }

        public void SetWoods(int woods) {
            Woods += woods;
        }
        public void MountHorse(Horse horse) {
            CurrentHorse = horse;
            CurrentHorse.SetOwner(this);
            CurrentHorse.GetTooltip().ChangeText("Press 'F' to dismount");
            SetSpeed(horse.GetSpeed());
        }

        public void DismountHorse() {
            if (CurrentHorse != null) {
                CurrentHorse.RemoveOwner();
                CurrentHorse.GetTooltip().SetVisible(false);
                CurrentHorse.GetTooltip().ChangeText("Press 'E' to mount");
                CurrentHorse.GetTooltip().SetPosition(new Vector2(CurrentHorse.GetRectangle().X + 50, CurrentHorse.GetRectangle().Y - 65));
            }
            CurrentHorse = null;
            SetSpeed(DefaultSpeed);
        }

        public Animation GetCurrentAnimation() {
            return CurrentCharacter.GetCurrentAnimation();
        }

        public List<Diamond> GetCollectedRedDiamonds() {
            return CollectedRedDiamonds;
        }

        public List<Diamond> GetCollectedYellowDiamonds() {
            return CollectedYellowDiamonds;
        }

        public string GetName() {
            return NameLabel.GetValue();
        }

        public void Draw() {

            if (!IsDead) {
                NameLabel.Draw();
                CurrentCharacter.Draw();
            }
        }
    }
}
