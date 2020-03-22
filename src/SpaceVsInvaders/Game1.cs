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
        private const double TickTime = 0.1;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Component> components;
        Board board;

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureLoader.LoadTextures(Content);

            // Tile enemyTile = new Tile(new Vector2(100,100), 250, 50, TileType.SpeedyEnemy);
            // Button myButton = new Button(new Vector2(50, 50), 50, 100);
            // components = new List<Component>
            // {
            //     // myButton,
            //     // enemyTile
            // };

            // enemyTile.LeftClicked += new EventHandler(EnemyTileClicked);
            // myButton.LeftClicked += new EventHandler(MyButtonClicked);

            board = new Board(new Vector2(100,100), 300, 300);
            components = new List<Component>
            {
                board
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
                prevSecond = currentSeconds;
            }
        }
    }
}
