using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Animated cursor component rendered at the current mouse position
    /// </summary>
    public class Cursor : Component
    {
        private Texture2D texture;
        private int rows;
        private int cols;
        private int elems;
        private int currElem;
        private double prevSecond;

        /// <summary>
        /// Timings for each frame of the animation
        /// </summary>
        /// <value>Display time of the frame in ms</value>
        private int[] miliSecPerElem = { 83, 83, 83, 83, 2000 };

        /// <summary>
        /// Constructor of <c>Cursor</c>
        /// </summary>
        /// <param name="position">Position (not used)</param>
        /// <param name="height">Height of the cursor</param>
        /// <param name="width">Width of the cursor</param>
        public Cursor(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            texture = ContentLoader.GetTexture("Cursors/normal2");
            rows = 2;
            cols = 3;
            elems = 5;
            currElem = 4;
        }

        /// <summary>
        /// Updates the currently displaying frame of the animation
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            // const double updateInterval = 100.0 / 60.0;
            if(gameTime.TotalGameTime.TotalSeconds > prevSecond + (double) miliSecPerElem[currElem] / 1000.0)
            {
                prevSecond = gameTime.TotalGameTime.TotalSeconds;
                currElem++;
                if(currElem == elems) currElem = 0;
            }
        }

        /// <summary>
        /// Draws the cursor to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw to</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            int _width = texture.Width / cols;
            int _height = texture.Height / rows;
            int row = (int) ((double) currElem / (double) cols);
            int col = currElem % cols;

            spriteBatch.Draw(texture, new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, width, height),
                new Rectangle(
                    col * _width,
                    row * _height,
                    _width - 1,
                    _height - 1),
                Color.White);
        }
    }
}