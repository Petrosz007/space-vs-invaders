using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Board
{
    public class TowerTile : Tile
    {
        private TowerType tower;
        private Texture2D texture;
        public int Row { get; private set; }
        public int Col { get; private set; }

        private int currHealth;
        private int maxHealth;


        public TowerTile(Vector2 position, int height, int width, int row, int col, StateManager stateManager, TowerType tower, int currHealth, int maxHealth)
            : base(position, height, width, row, col, stateManager)
        {
            this.tower = tower;
            this.texture = tower.GetTexture();

            Row = row;
            Col = col;

            this.currHealth = currHealth;
            this.maxHealth = maxHealth;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int size = (width > height) ? height : width;

            var rect = new Rectangle((int)position.X + (width - size)/2, (int)position.Y + (height - size)/2, size, size);
            spriteBatch.Draw(texture, rect, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

            spriteBatch.DrawString(ContentLoader.GetFont("Fonts/NumberFont"), $"{currHealth}/{maxHealth}", new Vector2(position.X + 4, position.Y + height - 20), Color.White);
           
           base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}