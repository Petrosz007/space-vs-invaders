using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    /// <summary>
    /// Pause scene
    /// </summary>
    public class PauseScene : Scene
    {
        public event EventHandler Resume;
        public event EventHandler Exit;
        public event EventHandler ExitToMainMenu;
        private List<Component> components;
        private Texture2D background;

        private bool prevEscapeState;

        /// <summary>
        /// Constructor of <c>PauseScene</c>
        /// </summary>
        /// <param name="width">Window Width</param>
        /// <param name="height">Window Height</param>
        public PauseScene(int width, int height)
            : base(width, height)
        {

        }

        /// <summary>
        /// Loads content, runs once per program run
        /// </summary>
        public override void LoadContent()
        {
            prevEscapeState = true;

            int btnWidth = 400;
            int btnHeight = 100;
            int btnMargin = 50;

            var resumeButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100), btnHeight, btnWidth, "Resume");
            resumeButton.LeftClicked += new EventHandler((o, e) => Resume?.Invoke(o, e));

            var exitToMainMenuButton = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + btnHeight + btnMargin), btnHeight, btnWidth, "Exit to Main Menu");
            exitToMainMenuButton.LeftClicked += new EventHandler((o, e) => ExitToMainMenu?.Invoke(o, e));

            var exitToDesktop = new Button(new Vector2((Width - btnWidth)/2, Height * 45 / 100 + (btnHeight + btnMargin)*2), btnHeight, btnWidth, "Exit to Desktop");
            exitToDesktop.LeftClicked += new EventHandler((o, e) => Exit?.Invoke(o, e));

            components = new List<Component>
            {
                resumeButton,
                exitToMainMenuButton,
                exitToDesktop,
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) != prevEscapeState && !prevEscapeState)
            {
                Resume?.Invoke(this, new EventArgs());
            }
            prevEscapeState = Keyboard.GetState().IsKeyDown(Keys.Escape);
            
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