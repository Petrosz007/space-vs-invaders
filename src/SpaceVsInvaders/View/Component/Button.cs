using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View.Components
{
    public class Button : Clickable
    {
        private Texture2D stillTexture;
        private Texture2D clickedTexture;

        public Button(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            this.stillTexture = TextureLoader.GetTexture("Buttons/clicked");
            this.clickedTexture = TextureLoader.GetTexture("Buttons/notClicked");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentlyClicked ? clickedTexture : stillTexture, area, new Rectangle(0,0,stillTexture.Width,stillTexture.Height), Color.Pink);
        }
    }
}