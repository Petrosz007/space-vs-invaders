using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Board
{
    public class Tile : Clickable
    {
        private TileType tile;
        private Texture2D texture;
        private float scale;
        public int X { get; private set; }
        public int Y { get; private set; }


        public Tile(Vector2 position, int height, int width, TileType tile, int x, int y)
            : base(position, height, width)
        {
            this.tile = tile;
            this.texture = tile.GetTexture();

            X = x;
            Y = y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, area, new Rectangle(0, 0, texture.Width, texture.Height), Color.Pink);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}