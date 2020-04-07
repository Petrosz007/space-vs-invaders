using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View;
using SpaceVsInvaders.View.Board;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly double TickTime = Config.GetValue<double>("TickTime");
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SVsIModel model;
        StateManager stateManager;
        List<Component> components;
        Board board;

        Texture2D background;

        double prevSecond;
        int boardWidth;
        int panelsWidth;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            prevSecond = 0;

            this.IsMouseVisible = true;
            this.Window.Title = "Space Vs Invaders";
            this.Window.IsBorderless = true;

            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            model = new SVsIModel();
            model.NewGame(Config.GetValue<int>("Rows"), Config.GetValue<int>("Cols"));
            
            model.Money = Config.GetValue<int>("StartingMoney");

            int width = Window.ClientBounds.Width;
            int height = Window.ClientBounds.Height;

            boardWidth = width * 80 / 100;
            panelsWidth = width * 20 / 100;
            int spawnHeight = 100;

            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.AttachGraphicsDevice(GraphicsDevice);
            ContentLoader.LoadContent(Content);


            ErrorDisplay Err = new ErrorDisplay(new Vector2(width/2-200, 100),300,500);

            stateManager = new StateManager(model, Err);

            background = ContentLoader.GetTexture("Backgrounds/background");


            BuyPanel buyPanel = new BuyPanel(new Vector2(width - panelsWidth, 0), height / 3, panelsWidth);
            buyPanel.DamageTowerButton.LeftClicked += new EventHandler((o, e) => stateManager.HandleNewTowerType(TowerType.Damage));
            buyPanel.GoldTowerButton.LeftClicked   += new EventHandler((o, e) => stateManager.HandleNewTowerType(TowerType.Gold));
            buyPanel.HealTowerButton.LeftClicked   += new EventHandler((o, e) => stateManager.HandleNewTowerType(TowerType.Heal));

            
            board = new Board(new Vector2(0, spawnHeight), height - spawnHeight * 2, boardWidth, model, stateManager);
            board.TileClicked += new EventHandler<(int, int)>(stateManager.HandleTileClicked);
            model.TowerHasAttacked += new EventHandler<SVsIEventArgs>(board.ShotAnimator.HandleNewShot);

            InfoPanel infoPanel = new InfoPanel(new Vector2(width - panelsWidth, height * 2/3), height / 3, panelsWidth, model);
            infoPanel.UpgradeCastleButton.LeftClicked += new EventHandler(stateManager.HandleCastleUpgradeClicked);

            TowerInfo towerInfo = new TowerInfo(new Vector2(width - panelsWidth, height * 1/3), height / 3, panelsWidth, stateManager, model);
            towerInfo.UpgradeButton.LeftClicked += new EventHandler(stateManager.HandleTowerUpgradeClicked);
            towerInfo.SellButton.LeftClicked += new EventHandler(stateManager.HandleTowerSellClicked);

            UnderCursorTower underCursorTower = new UnderCursorTower(new Vector2(0,0), 50, 50, stateManager);

            Mothership mothership = new Mothership(new Vector2(0,0), spawnHeight, boardWidth);

            components = new List<Component>
            {
                board,
                Err,
                infoPanel,
                towerInfo,
                mothership,
                buyPanel,
                underCursorTower,
            };            
        }

        private void MyButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button tile clicked");
        }

        private void EnemyTileClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Enemy tile clicked");
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
                Exit();

            HandleTick(gameTime.TotalGameTime.TotalSeconds);

            foreach(var component in components)
            {
                component.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background,
                new Rectangle(-panelsWidth, 0, Window.ClientBounds.Width + panelsWidth, Window.ClientBounds.Height),
                new Rectangle(0,0, background.Width, background.Height), Color.Pink);

            foreach(var component in components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void HandleTick(double currentSeconds)
        {
            if (currentSeconds > prevSecond + TickTime)
            {
                // TODO: call this when it isn't buggy
                model.HandleTick();
                prevSecond = currentSeconds;
            }
        }
    }
}
