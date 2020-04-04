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
        public int Row { get; private set; }
        public int Col { get; private set; }

        private int currHealth;
        private int maxHealth;


        public Tile(Vector2 position, int height, int width, TileType tile, int row, int col, int currHealth, int maxHealth)
            : base(position, height, width)
        {
            this.tile = tile;
            this.texture = tile.GetTexture();

            Row = row;
            Col = col;

            this.currHealth = currHealth;
            this.maxHealth = maxHealth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(tile != TileType.Empty)
            {
                spriteBatch.Draw(texture, area, new Rectangle(0, 0, texture.Width, texture.Height), Color.Pink);
                spriteBatch.DrawString(ContentLoader.GetFont("Fonts/TowerInfoFont"), $"{currHealth}/{maxHealth}", new Vector2(position.X + 4, position.Y + height - 20), Color.White);
            }

            if(isMouseOver())
            {
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), new Rectangle((int)position.X + 2, (int)position.Y + 2, width / 3, 3), Color.LimeGreen);
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), new Rectangle((int)position.X + 2, (int)position.Y + 2, 3 , height / 3), Color.LimeGreen);

                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), 
                            new Rectangle(
                                (int)position.X + width - 3, 
                                (int)(position.Y + height * 2/3),
                                3, 
                                height / 3), 
                        Color.Green);
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), 
                            new Rectangle(
                                (int)(position.X + width * 2/3),
                                (int)position.Y + height - 3, 
                                width / 3, 
                                3), 
                        Color.Green);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}