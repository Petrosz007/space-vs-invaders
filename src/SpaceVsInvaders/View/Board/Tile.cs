using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Board
{
    public class Tile : Clickable
    {
        public int Row { get; set; }
        public int Col { get; set; }

        private StateManager stateManager;
        public Tile(Vector2 position, int height, int width, int row, int col, StateManager stateManager)
            : base(position, height, width)
        {
            Row = row;
            Col = col;
            this.stateManager = stateManager;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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

            if(stateManager.SelectedPos == (Row, Col))
            {
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), new Rectangle(
                    (int)position.X + 2, (int)position.Y + height - 3, width / 3, 3), Color.DarkRed);
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), new Rectangle(
                    (int)position.X + 2, (int)(position.Y + height * 2/3), 3 , height / 3), Color.DarkRed);

                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), 
                            new Rectangle(
                                (int)position.X + width - 3, 
                                (int)position.Y + 2,
                                3, 
                                height / 3), 
                        Color.Red);
                spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.White), 
                            new Rectangle(
                                (int)(position.X + width * 2/3),
                                (int)position.Y + 2, 
                                width / 3, 
                                3), 
                        Color.Red);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}