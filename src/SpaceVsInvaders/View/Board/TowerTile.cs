using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    public class TowerTile : Tile
    {
        private TowerType tower;
        private Texture2D texture;

        private int currHealth;
        private int maxHealth;
        private SpriteFont font;


        public TowerTile(Vector2 position, int height, int width, int row, int col, StateManager stateManager, TowerType tower, int currHealth, int maxHealth)
            : base(position, height, width, row, col, stateManager)
        {
            this.tower = tower;
            this.texture = tower.GetTexture();

            Row = row;
            Col = col;

            this.currHealth = currHealth;
            this.maxHealth = maxHealth;

            font = ContentLoader.GetFont("Fonts/NumberFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int size = (width > height) ? height : width;


            var rect = new Rectangle((int)position.X + (width - size)/2, (int)position.Y + (height - size)/2, size, size);
            spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            // spriteBatch.DrawString(ContentLoader.GetFont("Fonts/NumberFont"), $"{currHealth}/{maxHealth}", new Vector2(position.X + 4, position.Y + height - 20), Color.White);
            DrawHpBar(spriteBatch);
           
           base.Draw(spriteBatch);
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
            Color fgColor = Color.MediumSeaGreen;

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(bgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5, 
                    (int)position.Y + height - hpBarHeight, 
                    hpBarWidth, 
                    hpBarHeight), 
                Color.White);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(fgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5 + borderSize, 
                    (int)position.Y + height - hpBarHeight + borderSize, 
                    (hpBarWidth - 5 - borderSize) * currHealth / maxHealth, 
                    hpBarHeight - borderSize*2), 
                Color.White);
            
            var measure = font.MeasureString(currHealth.ToString()) + new Vector2(2,2);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(bgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5,
                    (int)position.Y + height - (int)measure.Y - borderSize*2,
                    (int)measure.X + borderSize,
                    (int)measure.Y + borderSize*2),
                Color.White);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(bgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5 + borderSize + (int)measure.X,
                    (int)position.Y + height - (int)measure.Y - borderSize*2,
                    borderSize,
                    (int)measure.Y - hpBarHeight + borderSize*2), 
                Color.White);

            spriteBatch.Draw(ContentLoader.CreateSolidtexture(fgColor), 
                new Rectangle(
                    (int)position.X + (width - hpBarWidth)/2 + 5 + borderSize,
                    (int)position.Y + height - (int)measure.Y - borderSize,
                    (int)measure.X,
                    (int)measure.Y),
                Color.White);

            
            DrawOutlinedString(spriteBatch, font, currHealth.ToString(), 
                new Vector2(
                    position.X + (width - hpBarWidth)/2 + 5 + borderSize + 1,
                    position.Y + height - (int)measure.Y - 1),
                Color.White);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}