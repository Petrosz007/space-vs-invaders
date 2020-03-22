using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Board
{
    public class Board : Component
    {
        private SVsIModel model;

        private int colWidth;
        private int rowHeight;
        private int n;
        private Tile[,] tiles;
        public Board(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            n = 5;
            colWidth = width / n;
            rowHeight = height / n;
            tiles = new Tile[n,n];

            for(int i = 0; i < n; ++i)
            {
                for(int j = 0; j < n; ++j)
                {
                    tiles[i,j] = new Tile(
                        new Vector2(position.X + colWidth * i, position.Y + rowHeight * j),
                        rowHeight,
                        colWidth,
                        i < 3 ? TileType.BuffEnemy : TileType.NormalEnemy,
                        i,
                        j);
                    tiles[i,j].LeftClicked += new EventHandler(HandleTileClick);
                }
            }
        }

        private void HandleTileClick(object sender, EventArgs e)
        {
            Tile tile = (Tile) sender;

            Console.WriteLine("Clicked x={0} y={1}", tile.X, tile.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var tile in tiles)
            {
                tile.Update(gameTime);
            }
        }
    }
}