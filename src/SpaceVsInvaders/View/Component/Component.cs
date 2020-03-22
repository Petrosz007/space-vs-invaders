using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View.Components
{
    public abstract class Component
    {
        protected int height;
        protected int width;
        protected Vector2 position;
        protected Rectangle area {
            get => new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public Component(Vector2 position, int height, int width)
        {
            this.height = height;
            this.width = width;
            this.position = position;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}