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

        //! remove this
        Texture2D background;

        double prevSecond;
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

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
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

            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.AttachGraphicsDevice(GraphicsDevice);
            ContentLoader.LoadContent(Content);


            ErrorDisplay Err = new ErrorDisplay(new Vector2(width/2-200, 100),300,500);

            stateManager = new StateManager(model, Err);

            background = ContentLoader.GetTexture("Backgrounds/background");


            Button btn_heal = new Button(new Vector2(width - 110,5), 50, 100);
            btn_heal.LeftClicked += new EventHandler((o, e) => stateManager.HandleNewTowerType( TowerType.Heal));
            Button btn_gold = new Button(new Vector2(width - 220,5), 50, 100);
            btn_gold.LeftClicked += new EventHandler((o, e) => stateManager.HandleNewTowerType( TowerType.Gold));
            Button btn_damage = new Button(new Vector2(width-330,5), 50, 100);
            btn_damage.LeftClicked += new EventHandler((o, e) => stateManager.HandleNewTowerType( TowerType.Damage));

            
            board = new Board(new Vector2(0, 0), height, height, model);
            board.TileClicked += new EventHandler<(int, int)>(stateManager.HandleTileClicked);

            InfoPanel infoPanel = new InfoPanel(new Vector2(width - 400, height - 400), 400, 400, model);
            infoPanel.UpgradeCastleButton.LeftClicked += new EventHandler(stateManager.HandleCastleUpgradeClicked);

            TowerInfo towerInfo = new TowerInfo(new Vector2(width - 400, 200), 400, 400, stateManager, model);
            towerInfo.UpgradeButton.LeftClicked += new EventHandler(stateManager.HandleTowerUpgradeClicked);
            towerInfo.SellButton.LeftClicked += new EventHandler(stateManager.HandleTowerSellClicked);

            UnderCursorTower underCursorTower = new UnderCursorTower(new Vector2(0,0), 50, 50, stateManager);

            components = new List<Component>
            {
                board,
                btn_damage,
                btn_heal,
                btn_gold,
                Err,
                infoPanel,
                towerInfo,
                underCursorTower
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
                new Rectangle(0,0, Window.ClientBounds.Width, Window.ClientBounds.Height),
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
