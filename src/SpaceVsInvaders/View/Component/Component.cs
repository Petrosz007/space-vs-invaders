using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Component base class, that can be rendered to the sceen
    /// </summary>
    public abstract class Component
    {
        protected int height;
        protected int width;
        protected Vector2 position;

        /// <summary>
        /// Area of the component
        /// </summary>
        /// <value>Rectangle area of the component</value>
        protected Rectangle area {
            get => new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        /// <summary>
        /// Constructor of <c>Component</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the component</param>
        /// <param name="width">Width of the component</param>
        public Component(Vector2 position, int height, int width)
        {
            this.height = height;
            this.width = width;
            this.position = position;
        }

        /// <summary>
        /// Defines how to component should be drawn on the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Defines how the component should be updated each frame
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
    }
}