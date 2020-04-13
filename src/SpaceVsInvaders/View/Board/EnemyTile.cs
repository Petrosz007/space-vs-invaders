using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    public class EnemyTile : Tile
    {
        private  List<(EnemyType, int)> enemies;
        private int currHealth;
        private int maxHealth;
        private SpriteFont font;

        public EnemyTile(Vector2 position, int height, int width, int row, int col, StateManager stateManager, List<(EnemyType, int)> enemies, int currHealth, int maxHealth)
            : base(position, height, width, row, col, stateManager)
        {
            this.enemies = enemies;

            Row = row;
            Col = col;

            this.currHealth = currHealth;
            this.maxHealth = maxHealth;

            font = ContentLoader.GetFont("Fonts/NumberFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int originalSize = (width > height) ? height : width;
            // double offsetScale = 3/5;
            int offsetAmount = 30;

            if(enemies.Count >= 3)
            {
                int size = originalSize * 3/5;
                var texture = enemies[2].Item1.GetTexture();
                var rect = new Rectangle((int)position.X + (width - size)/2 - offsetAmount, (int)position.Y + (height - size)/2, size, size);
                spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

                if(enemies[2].Item2 > 1)
                {
                    string count = enemies[2].Item2.ToString();
                    var countMeasure = font.MeasureString(count);
                    spriteBatch.DrawString(font, count, new Vector2(position.X + 4 + 10, position.Y), Color.White);
                }
            }
            if(enemies.Count >= 2)
            {
                int size = originalSize * 3/5;
                var texture = enemies[1].Item1.GetTexture();
                var rect = new Rectangle((int)position.X + (width - size)/2 + offsetAmount, (int)position.Y + (height - size)/2, size, size);
                spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

                if(enemies[1].Item2 > 1)
                {
                    string count = enemies[1].Item2.ToString();
                    var countMeasure = font.MeasureString(count);
                    spriteBatch.DrawString(font, count, new Vector2(position.X + width - countMeasure.Y - 10, position.Y), Color.White);
                }
            }
            if(enemies.Count >= 1)
            {
                int size = originalSize;
                var texture = enemies[0].Item1.GetTexture();
                var rect = new Rectangle((int)position.X + (width - size)/2, (int)position.Y + (height - size)/2, size, size);
                spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

                if(enemies[0].Item2 > 1)
                {
                    string count = enemies[0].Item2.ToString();
                    var countMeasure = font.MeasureString(count);
                    spriteBatch.DrawString(font, count, new Vector2(position.X + 4 + (width - countMeasure.Y)/2, position.Y), Color.White);
                }
            }
            
            spriteBatch.DrawString(font, $"{currHealth}/{maxHealth}", new Vector2(position.X + 4, position.Y + height - 20), Color.White);

            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}