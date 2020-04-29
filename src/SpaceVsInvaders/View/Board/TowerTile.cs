using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Tower tile used in the Game Board
    /// </summary>
    public class TowerTile : Tile
    {
        private TowerType tower;
        private Texture2D texture;

        private int currHealth;
        private int maxHealth;
        private SpriteFont font;

        /// <summary>
        /// Constructor of <c>TowerTile</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height</param>
        /// <param name="width">Width</param>
        /// <param name="row">Row of the tile</param>
        /// <param name="col">Column of the tile</param>
        /// <param name="stateManager">State manager to get the state from</param>
        /// <param name="tower">Tower type of the tile</param>
        /// <param name="currHealth">Current health</param>
        /// <param name="maxHealth">Max health</param>
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

        /// <summary>
        /// Draw the tower tile to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            int size = (width > height) ? height : width;


            var rect = new Rectangle((int)position.X + (width - size)/2, (int)position.Y + (height - size)/2, size, size);
            spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            DrawHpBar(spriteBatch);
           
           base.Draw(spriteBatch);
        }
        
        /// <summary>
        /// Draws the healthbar of the tile to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
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
    }
}