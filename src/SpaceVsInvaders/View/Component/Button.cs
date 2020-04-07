using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View.Components
{
    public class Button : Clickable
    {
        private Texture2D edgeTexture;
        private Texture2D middleTexture;
        private SpriteFont font;
        private string text;

        public Button(Vector2 position, int height, int width, string text = "NO TEXT")
            : base(position, height, width)
        {
            this.text = text;
            this.edgeTexture = ContentLoader.GetTexture("Buttons/button-edge");
            this.middleTexture = ContentLoader.GetTexture("Buttons/button-middle");

            this.font = ContentLoader.GetFont("Fonts/ButtonFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            double scale = height / (double) edgeTexture.Height;
            Console.WriteLine($"{Math.Floor(edgeTexture.Width * scale)} {height}");
            spriteBatch.Draw(edgeTexture, new Rectangle((int)position.X, (int)position.Y, (int) Math.Floor(edgeTexture.Width * scale), height), 
                new Rectangle(0,0,edgeTexture.Width, edgeTexture.Height), Color.Pink);

            spriteBatch.Draw(edgeTexture, new Rectangle((int)position.X + width - (int) Math.Floor(edgeTexture.Width * scale), (int)position.Y, (int) Math.Floor(edgeTexture.Width * scale), height), 
                new Rectangle(0,0,edgeTexture.Width, edgeTexture.Height), Color.Pink, 0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0f);

            spriteBatch.Draw(middleTexture, new Rectangle((int) (position.X + Math.Floor(edgeTexture.Width * scale)), (int) position.Y, 
                width - 2*(int)Math.Floor(edgeTexture.Width * scale), height), new Rectangle(0,0, middleTexture.Width, middleTexture.Height), Color.Pink);

            var measure = font.MeasureString(text);
            spriteBatch.DrawString(font, text, new Vector2(position.X + (width - measure.X)/2, position.Y + (height - measure.Y)/2), Color.Green);
        }
    }
}