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
        private enum EnemyPlace { LEFT, CENTER, RIGHT };
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
            DrawHpBar(spriteBatch);

            if(enemies.Count >= 3)
            {
                DrawEnemy(spriteBatch, enemies[2], EnemyPlace.LEFT);
            }
            if(enemies.Count >= 2)
            {
                DrawEnemy(spriteBatch, enemies[1], EnemyPlace.RIGHT);
            }
            if(enemies.Count >= 1)
            {
                DrawEnemy(spriteBatch, enemies[0], EnemyPlace.CENTER);
            }

            base.Draw(spriteBatch);
        }

        private void DrawEnemy(SpriteBatch spriteBatch, (EnemyType, int) enemy, EnemyPlace enemyPlace)
        {  
            int originalSize = (width > height) ? height : width;

            (int size, int offsetAmount) = enemyPlace switch {
                EnemyPlace.LEFT   => (originalSize * 3/5, -30),
                EnemyPlace.CENTER => (originalSize,         0),
                EnemyPlace.RIGHT  => (originalSize * 3/5,  30),
            };


            var texture = enemy.Item1.GetTexture();
            var rect = new Rectangle((int)position.X + (width - size)/2 + offsetAmount, (int)position.Y + (height - size)/2, size, size);
            spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            if(enemy.Item2 > 1)
            {
                string count = enemy.Item2.ToString();
                var countMeasure = font.MeasureString(count);
                var textX = enemyPlace switch {
                    EnemyPlace.LEFT   => 4 + width/4 - countMeasure.X/2,
                    EnemyPlace.CENTER => (width - countMeasure.X)/2,
                    EnemyPlace.RIGHT  => width - width/4 - countMeasure.X/2 - 4,
                };

                var pos = new Vector2(position.X + textX, position.Y + height - 4 - countMeasure.Y);
                DrawOutlinedString(spriteBatch, font, count, pos, Color.White);
            }
        }

        private void DrawOutlinedString(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 pos, Color color)
        {
                spriteBatch.DrawString(font, text, pos + new Vector2(1,1), Color.Black);
                spriteBatch.DrawString(font, text, pos + new Vector2(1,-1), Color.Black);
                spriteBatch.DrawString(font, text, pos + new Vector2(-1,1), Color.Black);
                spriteBatch.DrawString(font, text, pos + new Vector2(-1,-1), Color.Black);
                spriteBatch.DrawString(font, text, pos, Color.White);
        }

        private void DrawHpBar(SpriteBatch spriteBatch)
        {
            int hpBarWidth = width * 2 / 3;
            int hpBarHeight = 10;
            int borderSize = 3;
            Color bgColor = Color.Black;
            Color fgColor = Color.Red;

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(bgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5,
                    (int)position.Y, hpBarWidth, hpBarHeight), 
                Color.White);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(fgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5 + borderSize,
                    (int)position.Y + borderSize,
                    (hpBarWidth - 5 - borderSize) * currHealth / maxHealth,
                    hpBarHeight - borderSize*2),
                Color.White);
            
            var measure = font.MeasureString(currHealth.ToString()) + new Vector2(2,2);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(bgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5,
                    (int)position.Y,
                    (int)measure.X + borderSize,
                    (int)measure.Y + borderSize*2),
                Color.White);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(bgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5 + (int)measure.X + borderSize,
                    (int)position.Y + hpBarHeight,
                    borderSize, 
                    (int)measure.Y - hpBarHeight + borderSize*2), 
                Color.White);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(fgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5 + borderSize,
                    (int)position.Y + borderSize, 
                    (int)measure.X,
                    (int)measure.Y),
                Color.White);

            
            DrawOutlinedString(spriteBatch, font, currHealth.ToString(), 
                new Vector2(
                    position.X + (width - hpBarWidth)/2 + 5 + borderSize + 1,
                    position.Y + borderSize + 1),
                Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}