using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View
{
    public class Button
    {
        private Texture2D stillTexture;
        private Texture2D clickedTexture;
        private int height;
        private int width;
        private Vector2 position;

        private Rectangle area;

        public bool Clicked { get; set; }

        public Button(Texture2D stillTexture, Texture2D clickedTexture, Vector2 position, int height, int width)
        {
            this.stillTexture = stillTexture;
            this.clickedTexture = clickedTexture;
            this.height = height;
            this.width = width;
            this.position = position;

            area = new Rectangle(0, 0, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Clicked ? clickedTexture : stillTexture, position, area, Color.Pink);
        }

        public bool isMouseOver(Point mousePosition)
        {
            return new Rectangle((int) position.X, (int) position.Y, width, height).Contains(mousePosition);
        }
    }
}