using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace CastleBridge
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CastleBridge : Game
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;

        public Dictionary<ScreenType, Screen> Screens;
        public ScreenType CurrentScreen;

        public static ContentManager PublicContent;


 
        public CastleBridge()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 1400;// GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = 728;// GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
 

            Content.RootDirectory = "Content";
            PublicContent = Content;
            
            IsMouseVisible = true;

            Screens = new Dictionary<ScreenType, Screen>();
            CurrentScreen = ScreenType.Menu;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);



            Screens.Add(ScreenType.Menu, new MenuScreen(GraphicsDevice.Viewport));
            Screens.Add(ScreenType.Game, new GameScreen(GraphicsDevice.Viewport));
            ((MenuScreen)Screens[ScreenType.Menu]).OnStartGame += StartJoinSession;
            ((GameScreen)Screens[ScreenType.Game]).OnStartGameAfterLoading += StartGame;
            ((GameScreen)Screens[ScreenType.Game]).GetGameClient().OnUpdateLoadingPercent += ((LoadingMenu)((MenuScreen)Screens[ScreenType.Menu]).GetMenu(MenuPage.Loading)).UpdateText;


            // TODO: use this.Content to load your game content here
        }

        private void StartJoinSession(CharacterName characterName, TeamName team, string name) {

            ((MenuScreen)Screens[ScreenType.Menu]).GoToPage(MenuPage.Loading);
            ((GameScreen)Screens[ScreenType.Game]).JoinGame(characterName, team, name);
            ((GameScreen)Screens[ScreenType.Game]).UpdateHud();
        }

        private void StartGame() {
            CurrentScreen = ScreenType.Game;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Environment.Exit(Environment.ExitCode);

            // TODO: Add your update logic here

            Screens [CurrentScreen].Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(81, 234, 255));

            // TODO: Add your drawing code here

 

            Screens [CurrentScreen].Draw();

 

            base.Draw(gameTime);
        }
    }
}
