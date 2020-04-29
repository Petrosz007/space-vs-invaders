using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    /// <summary>
    /// Logo scene displayed in the beginning
    /// </summary>
    public class LogoScene : Scene
    {
        public event EventHandler End;
        private double secondsElapsed;
        private Texture2D logo;
        private float logoOpacity;
        private float fontOpacity;
        private float backOpacity;
        private const double DisplayForSeconds = 8.55;
        private const double LogoRevealTime = 2.5;
        private const double FontRevealTime = 2.5;
        private const double FadeOutTime = 0.4;

        /// <summary>
        /// Constructor of <c>LogoScene</c>
        /// </summary>
        /// <param name="width">Window width</param>
        /// <param name="height">Window Height</param>
        public LogoScene(int width, int height)
            : base(width, height)
        {
            secondsElapsed = 0;
            logoOpacity = 0f;
            fontOpacity = 0f;
            backOpacity = 1f;

            MediaPlayer .Play(ContentLoader.GetSong("Sounds/MenuMusic"));
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Loads content, runs once per program run
        /// </summary>
        public override void LoadContent()
        {
            logo = ContentLoader.GetTexture("SvsI_SPrites/logo");
        }

        /// <summary>
        /// Handles the unloading of content
        /// </summary>
        public override void UnloadContent()
        {

        }

        /// <summary>
        /// Updates the logo animation
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            secondsElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if(secondsElapsed >= DisplayForSeconds) {
                End?.Invoke(this, null);
            }

            if(secondsElapsed >= 1 && logoOpacity <= 1) {
                logoOpacity += (float) ((1 / LogoRevealTime) * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if(secondsElapsed >= 3 && fontOpacity <= 1) {
                fontOpacity += (float) ((1 / FontRevealTime) * gameTime.ElapsedGameTime.TotalSeconds);
            }

            if(secondsElapsed >= DisplayForSeconds - FadeOutTime) {
                float change = (float) ((1/FadeOutTime) * gameTime.ElapsedGameTime.TotalSeconds);
                logoOpacity = Math.Max(0, fontOpacity - change);
                fontOpacity = Math.Max(0, fontOpacity - change);
                backOpacity = Math.Max(0, fontOpacity - change);
            }
        }

        /// <summary>
        /// Draws the logo scene to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentLoader.GetTexture("Backgrounds/pause-background"), new Rectangle(0,0, Width, Height), Color.White);
            spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), new Rectangle(0,0, Width, Height), Color.White * backOpacity);

            int logoWidth = 400;
            int logoHeight = 400;
            spriteBatch.Draw(logo, new Rectangle((Width - logoWidth)/2, (Height - logoHeight)/2, logoWidth, logoHeight),
                new Rectangle(0, 0, logo.Width, logo.Height), Color.White * logoOpacity);

            var measure = ContentLoader.GetFont("Fonts/TitleFont").MeasureString("Andikvarium");
            spriteBatch.DrawString(ContentLoader.GetFont("Fonts/TitleFont"), "Andikvarium", 
                new Vector2((Width - measure.X)/2, (Height - logoHeight)/2 + logoHeight + 10), Color.Black * fontOpacity);
        }
    }
}