using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View;
using SpaceVsInvaders.View.Boards;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameScene : Scene
    {
        public event EventHandler OpenPauseMenu;
        private readonly double TickTime = Config.GetValue<double>("TickTime");
        private SVsIModel model;
        private StateManager stateManager;
        private List<Component> components;
        private Board board;

        private Texture2D background;

        private double prevSecond;
        private bool prevEscapeState;
        private int boardWidth;
        private int panelsWidth;
        public GameScene(int width, int height)
            : base(width, height)
        {
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public override void LoadContent()
        {
            prevSecond = 0;
            prevEscapeState = false;

            model = new SVsIModel();
            model.NewGame(Config.GetValue<int>("Rows"), Config.GetValue<int>("Cols"));
            
            model.Money = Config.GetValue<int>("StartingMoney");

            boardWidth = Width * 80 / 100;
            panelsWidth = Width * 20 / 100;
            int spawnHeight = 100;


            ErrorDisplay Err = new ErrorDisplay(new Vector2(Width/2-200, 100),300,500);

            stateManager = new StateManager(model, Err);
            stateManager.OpenPauseMenu += new EventHandler((o, e) => OpenPauseMenu?.Invoke(o, e));

            background = ContentLoader.GetTexture("Backgrounds/background");


            BuyPanel buyPanel = new BuyPanel(new Vector2(Width - panelsWidth, 0), Height / 3, panelsWidth);
            buyPanel.DamageTowerButton.LeftClicked += new EventHandler((o, e) => stateManager.HandleNewTowerType(TowerType.Damage));
            buyPanel.GoldTowerButton.LeftClicked   += new EventHandler((o, e) => stateManager.HandleNewTowerType(TowerType.Gold));
            buyPanel.HealTowerButton.LeftClicked   += new EventHandler((o, e) => stateManager.HandleNewTowerType(TowerType.Heal));

            
            board = new Board(new Vector2(0, spawnHeight), Height - spawnHeight * 2, boardWidth, model, stateManager);
            board.TileClicked += new EventHandler<(int, int)>(stateManager.HandleTileClicked);
            model.TowerHasAttacked += new EventHandler<SVsIEventArgs>(board.ShotAnimator.HandleNewShot);
            model.AsteroidCatastrophe += new EventHandler<SVsIEventArgs>(board.CatastropheAnimator.HandleAsteroids);
            model.HealingCatastrophe += new EventHandler<SVsIEventArgs>(board.CatastropheAnimator.HandleHealing);

            InfoPanel infoPanel = new InfoPanel(new Vector2(Width - panelsWidth, Height * 2/3), Height / 3, panelsWidth, model);
            infoPanel.UpgradeCastleButton.LeftClicked += new EventHandler(stateManager.HandleCastleUpgradeClicked);

            TowerInfo towerInfo = new TowerInfo(new Vector2(Width - panelsWidth, Height * 1/3), Height / 3, panelsWidth, stateManager, model);
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

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) != prevEscapeState && !prevEscapeState)
            {
                stateManager.HandleEscapePressed(this, new EventArgs());
            }
            prevEscapeState = Keyboard.GetState().IsKeyDown(Keys.Escape);


            HandleTick(gameTime.TotalGameTime.TotalSeconds);

            foreach(var component in components)
            {
                component.Update(gameTime);
            }

            // base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background,
                new Rectangle(-panelsWidth, 0, Width + panelsWidth, Height),
                new Rectangle(0,0, background.Width, background.Height), Color.White);

            foreach(var component in components)
            {
                component.Draw(spriteBatch);
            }

            // base.Draw(spriteBatch);
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
