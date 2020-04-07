using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceVsInvaders.View.Components
{
    public class Cursor : Component
    {
        public Texture2D texture;
        public int rows;
        public int cols;
        public int elems;
        private int currElem;
        private double prevSecond;
        private int[] miliSecPerElem = { 83, 83, 83, 83, 2000 };
        public Cursor(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            texture = ContentLoader.GetTexture("Cursors/normal2");
            rows = 2;
            cols = 3;
            elems = 5;
            currElem = 4;
        }

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