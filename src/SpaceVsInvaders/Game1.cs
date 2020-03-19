using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View;

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
        Dictionary<string, Texture2D> sprites;

        Button button;

        double prevSecond;
        bool prevLeftClickState;

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
            sprites = new Dictionary<string, Texture2D>();
            prevSecond = 0;
            prevLeftClickState = false;

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

            sprites.Add("lul", Content.Load<Texture2D>("GameSprites/LUL"));

            button = new Button(Content.Load<Texture2D>("Buttons/notClicked"), Content.Load<Texture2D>("Buttons/clicked"), new Vector2(50, 50), 50, 100);
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


            MouseState mouseState = Mouse.GetState();
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            // Console.WriteLine(button.isMouseOver(mousePosition) + " " + (mouseState.LeftButton == ButtonState.Pressed).ToString());
            button.Clicked = button.isMouseOver(mousePosition) && mouseState.LeftButton == ButtonState.Pressed;

            if(button.isMouseOver(mousePosition) && prevLeftClickState == true && mouseState.LeftButton != ButtonState.Pressed) {
                Console.WriteLine("Click received!");
                // model.Player.X += 100;
            }


            prevLeftClickState = (mouseState.LeftButton == ButtonState.Pressed);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            button.Draw(spriteBatch);

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
