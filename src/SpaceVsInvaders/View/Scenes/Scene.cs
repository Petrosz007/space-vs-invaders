using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Scenes
{
    public abstract class Scene
    {
        protected int Width { get; private set; }
        protected int Height { get; private set; }
        public Scene(int width, int height) =>
            (Width, Height) = (width, height);
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}