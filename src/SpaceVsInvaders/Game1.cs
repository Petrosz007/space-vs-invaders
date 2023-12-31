﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View;
using SpaceVsInvaders.View.Boards;
using SpaceVsInvaders.View.Components;
using SpaceVsInvaders.View.Scenes;

namespace SpaceVsInvaders
{
    /// <summary>
    /// Scene types
    /// </summary>
    public enum SceneType
    {
        /// <summary>
        /// Game Scene
        /// </summary>
        Game,

        /// <summary>
        /// Pause Scene
        /// </summary>
        Pause,

        /// <summary>
        /// Main Menu Scene
        /// </summary>
        MainMenu,

        /// <summary>
        /// Logo Scene
        /// </summary>
        Logo,
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Dictionary<SceneType, Scene> scenes;
        private SceneType activeScene;
        private Cursor cursor;

        /// <summary>
        /// Constructor of <c>MainGame</c>
        /// </summary>
        public MainGame()
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
            // this.IsMouseVisible = true;
            this.Window.Title = "Space Vs Invaders";
            // this.Window.IsBorderless = true;

            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLoader.AttachGraphicsDevice(GraphicsDevice);
            ContentLoader.LoadContent(Content);

            var mainMenuScene = new MainMenuScene(Window.ClientBounds.Width, Window.ClientBounds.Height);
            mainMenuScene.NewGame += new EventHandler<Difficulty>(HandleNewGame);
            mainMenuScene.Exit += new EventHandler((o, e) => Exit());
            
            var gameScene = new GameScene(Window.ClientBounds.Width, Window.ClientBounds.Height);
            gameScene.OpenPauseMenu += new EventHandler((o, e) => activeScene = SceneType.Pause);
            gameScene.ExitToMainMenu += new EventHandler((o, e) => activeScene = SceneType.MainMenu);
            gameScene.NewGame(Difficulty.Normal);

            var pauseScene = new PauseScene(Window.ClientBounds.Width, Window.ClientBounds.Height);
            pauseScene.Resume += new EventHandler((o, e) => activeScene = SceneType.Game);
            pauseScene.ExitToMainMenu += new EventHandler((o, e) => activeScene = SceneType.MainMenu);
            pauseScene.Exit += new EventHandler((o, e) => Exit());

            var logoScene = new LogoScene(Window.ClientBounds.Width, Window.ClientBounds.Height);
            logoScene.End += new EventHandler((o, e) => activeScene = SceneType.MainMenu);

            scenes = new Dictionary<SceneType, Scene>
            {
                { SceneType.MainMenu, mainMenuScene },
                { SceneType.Game, gameScene },
                { SceneType.Pause, pauseScene },
                { SceneType.Logo, logoScene },
            };

            activeScene = SceneType.Logo;

            cursor = new Cursor(new Vector2(0,0), 35, 35);

            foreach(var scene in scenes.Values)
            {
                scene.LoadContent();
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            foreach(var scene in scenes.Values)
            {
                scene.UnloadContent();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            cursor.Update(gameTime);

            scenes[activeScene].Update(gameTime);

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

            scenes[activeScene].Draw(spriteBatch);

            cursor.Draw(spriteBatch);

            // DisplayFPS(spriteBatch, gameTime);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Handles the new game event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="difficulty">Difficulty of the new game</param>
        private void HandleNewGame(object sender, Difficulty difficulty)
        {
            ((GameScene) scenes[SceneType.Game]).NewGame(difficulty);
            activeScene = SceneType.Game;
        }

        private void DisplayFPS(SpriteBatch spriteBatch, GameTime gameTime)
        {
            double frameRate = (1 / gameTime.ElapsedGameTime.TotalSeconds);

            Console.WriteLine($"Elapsed = {gameTime.ElapsedGameTime.TotalSeconds} FPS = {frameRate}");

            spriteBatch.DrawString(ContentLoader.GetFont("Fonts/NumberFont"), Math.Round(frameRate, 2).ToString(), new Vector2(0,0), Color.LightGreen);
        }
    }
}
