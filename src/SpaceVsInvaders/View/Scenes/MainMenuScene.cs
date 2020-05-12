using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    /// <summary>
    /// Main menu scene
    /// </summary>
    public class MainMenuScene : Scene
    {
        /// <summary>
        /// New game should be started with the difficulty
        /// </summary>
        public event EventHandler<Difficulty> NewGame;

        /// <summary>
        /// The program should exit event
        /// </summary>
        public event EventHandler Exit;
        private List<Component> components;
        private Texture2D background;

        /// <summary>
        /// Constructor of <c>MainMenuScene</c>
        /// </summary>
        /// <param name="width">Window Width</param>
        /// <param name="height">Window Height</param>
        public MainMenuScene(int width, int height)
            : base(width, height)
        {

        }

        /// <summary>
        /// Loads content, runs once per program run
        /// </summary>
        public override void LoadContent()
        {
            int btnWidth = 400;
            int btnHeight = 100;
            int btnMargin = 50;

            var normalGameButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100), btnHeight, btnWidth, "Normal Game");
            normalGameButton.LeftClicked += new EventHandler((o, e) => NewGame?.Invoke(o, Difficulty.Normal));

            var hardGameButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + (btnHeight + btnMargin)), btnHeight, btnWidth, "Hard Game");
            hardGameButton.LeftClicked += new EventHandler((o, e) => NewGame?.Invoke(o, Difficulty.Hard));

            var exitButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + (btnHeight + btnMargin)*2), btnHeight, btnWidth, "Exit");
            exitButton.LeftClicked += new EventHandler((o, e) => Exit?.Invoke(o, e));

            components = new List<Component>
            {
                normalGameButton,
                hardGameButton,
                exitButton,
            };

            background = ContentLoader.GetTexture("Backgrounds/pause-background");
        }

        /// <summary>
        /// Handles the unloading of content
        /// </summary>
        public override void UnloadContent()
        {

        }

        /// <summary>
        /// Updates the scene
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {           
            foreach(var component in components)
            {
                component.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the scene to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0,0,Width,Height), new Rectangle(0,0,background.Width,background.Height), Color.White);

            foreach(var component in components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}