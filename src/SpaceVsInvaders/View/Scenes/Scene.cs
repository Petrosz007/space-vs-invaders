using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    /// <summary>
    /// Base scene class
    /// </summary>
    public abstract class Scene
    {
        /// <summary>
        /// Width of the scene
        /// </summary>
        /// <value>Width of the scene</value>
        protected int Width { get; private set; }

        /// <summary>
        /// Height of the scene
        /// </summary>
        /// <value>Height of the scene</value>
        protected int Height { get; private set; }

        /// <summary>
        /// Constructor of <c>Scene</c>
        /// </summary>
        /// <param name="width">Width of the scene</param>
        /// <param name="height">Height of the scene</param>
        /// <returns>Scene</returns>
        public Scene(int width, int height) =>
            (Width, Height) = (width, height);

        /// <summary>
        /// Loads content, runs once per program run
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Handles the unloading of content
        /// </summary>
        public abstract void UnloadContent();

        /// <summary>
        /// Updates the scene
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the scene to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}