using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Button component
    /// </summary>
    public class Button : Clickable
    {
        private Texture2D edgeTexture;
        private Texture2D middleTexture;
        private SpriteFont font;
        private string text;
        private SoundEffectInstance hoverSoundInstance;
        private SoundEffectInstance clickSoundInstance;

        /// <summary>
        /// Constructor of <c>Button</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the button</param>
        /// <param name="width">Width of the button</param>
        /// <param name="text">Text of the button</param>
        public Button(Vector2 position, int height, int width, string text = "NO TEXT")
            : base(position, height, width)
        {
            this.text = text;
            this.edgeTexture = ContentLoader.GetTexture("Buttons/button-edge");
            this.middleTexture = ContentLoader.GetTexture("Buttons/button-middle");

            this.font = ContentLoader.GetFont("Fonts/ButtonFont");

            this.hoverSoundInstance = ContentLoader.GetSoundEffect("Sounds/btn_hover").CreateInstance();
            this.clickSoundInstance = ContentLoader.GetSoundEffect("Sounds/btn_click").CreateInstance();

            // this.MouseEnter += new EventHandler((o, e) => hoverSoundInstance.Play());
            this.LeftClicked += new EventHandler((o, e) => clickSoundInstance.Play());
            this.RightClicked += new EventHandler((o, e) => clickSoundInstance.Play());
        }

        /// <summary>
        /// Draws the button to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw to</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            double scale = height / (double) edgeTexture.Height;
            //Console.WriteLine($"{Math.Floor(edgeTexture.Width * scale)} {height}");
            spriteBatch.Draw(edgeTexture, new Rectangle((int)position.X, (int)position.Y, (int) Math.Floor(edgeTexture.Width * scale), height), 
                new Rectangle(0,0,edgeTexture.Width, edgeTexture.Height), Color.White);

            spriteBatch.Draw(edgeTexture, new Rectangle((int)position.X + width - (int) Math.Floor(edgeTexture.Width * scale), (int)position.Y, (int) Math.Floor(edgeTexture.Width * scale), height), 
                new Rectangle(0,0,edgeTexture.Width, edgeTexture.Height), Color.White, 0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0f);

            spriteBatch.Draw(middleTexture, new Rectangle((int) (position.X + Math.Floor(edgeTexture.Width * scale)), (int) position.Y, 
                width - 2*(int)Math.Floor(edgeTexture.Width * scale), height), new Rectangle(0,0, middleTexture.Width, middleTexture.Height), Color.White);

            var measure = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2(position.X + (width - measure.X)/2, position.Y + (height - measure.Y)/2), Color.Green);
        }

        /// <summary>
        /// Replaces the text of the button with a new text
        /// </summary>
        /// <param name="newText">New text</param>
        public void UpdateText(string newText) => 
            text = newText;
    }
}