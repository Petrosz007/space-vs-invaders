using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Board
{
    public class Board : Component
    {
        private SVsIModel model;

        private int colWidth;
        private int rowHeight;
        private Tile[,] tiles;
        private Texture2D divTexture;
        private int divWidth;
        public event EventHandler<Tuple<int, int>> TileClicked;
        public Board(Vector2 position, int height, int width, SVsIModel model)
            : base(position, height, width)
        {
            this.model = model;

            divTexture = ContentLoader.CreateSolidtexture(Color.Pink);
            divWidth = 2;

            colWidth = width / model.Cols;
            rowHeight = height / model.Rows;
            tiles = new Tile[model.Rows, model.Cols];
        }

        private void HandleTileClick(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;

            Console.WriteLine("Clicked Row={0} Col={1}", tile.Row, tile.Col);
            TileClicked?.Invoke(this, new Tuple<int, int>(tile.Row, tile.Col));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }

            for (int i = 0; i < model.Cols + 1; ++i)
            {
                Rectangle divRect = new Rectangle(
                    (int)position.X + i * colWidth,
                    (int)position.Y,
                    divWidth,
                    height
                );

                spriteBatch.Draw(divTexture, divRect, Color.Pink); 
            }

            for (int i = 0; i < model.Rows + 1; ++i)
            {
                Rectangle divRect = new Rectangle(
                    (int)position.X,
                    (int)position.Y + i * rowHeight,
                    width,
                    divWidth
                );

                spriteBatch.Draw(divTexture, divRect, Color.Pink); 
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < model.Rows; ++i)
            {
                for (int j = 0; j < model.Cols; ++j)
                {
                    TileType tile = TileType.Empty;
                    if (model.Towers[i, j] != null)
                    {
                        if (model.Towers[i, j] is SVsIDamageTower) tile = TileType.DamageTower;
                        else if (model.Towers[i, j] is SVsIGoldTower) tile = TileType.GoldTower;
                        else if (model.Towers[i, j] is SVsIHealTower) tile = TileType.HealTower;
                    }
                    else
                    {
                        if (model.Enemies[i, j].Count > 0)
                        {
                            if (model.Enemies[i, j][0] is SVsINormalEnemy) tile = TileType.NormalEnemy;
                            else if (model.Enemies[i, j][0] is SVsIBuffEnemy) tile = TileType.BuffEnemy;
                            else if (model.Enemies[i, j][0] is SVsISpeedyEnemy) tile = TileType.SpeedyEnemy;
                        }
                    }

                    tiles[i, j] = new Tile(
                        new Vector2(position.X + colWidth * j, position.Y + rowHeight * i),
                        rowHeight,
                        colWidth,
                        tile,
                        i,
                        j);
                    tiles[i, j].LeftClicked += new EventHandler(HandleTileClick);
                }
            }

            foreach (var tile in tiles)
            {
                tile.Update(gameTime);
            }
        }
    }
}