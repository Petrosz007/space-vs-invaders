using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Tile component used in the Game Board
    /// </summary>
    public class Tile : Clickable
    {
        /// <summary>
        /// Row of the tile
        /// </summary>
        /// <value>Row of the tile</value>
        public int Row { get; set; }

        /// <summary>
        /// Column of the tile
        /// </summary>
        /// <value>Column of the tile</value>
        public int Col { get; set; }

        private StateManager stateManager;

        /// <summary>
        /// Constructor of <c>Tile</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height</param>
        /// <param name="width">Width</param>
        /// <param name="row">Row of the tile</param>
        /// <param name="col">Column of the tile</param>
        /// <param name="stateManager">State manager to get the state from</param>
        public Tile(Vector2 position, int height, int width, int row, int col, StateManager stateManager)
            : base(position, height, width)
        {
            Row = row;
            Col = col;
            this.stateManager = stateManager;
        }

        /// <summary>
        /// Draws the tile to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
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

        /// <summary>
        /// Draws an outlined string to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        /// <param name="font">Font</param>
        /// <param name="text">Text</param>
        /// <param name="pos">Position</param>
        /// <param name="color">Color</param>
        protected void DrawOutlinedString(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 pos, Color color)
        {
                spriteBatch.DrawString(font, text, pos + new Vector2(1,1), Color.Black);
                spriteBatch.DrawString(font, text, pos + new Vector2(1,-1), Color.Black);
                spriteBatch.DrawString(font, text, pos + new Vector2(-1,1), Color.Black);
                spriteBatch.DrawString(font, text, pos + new Vector2(-1,-1), Color.Black);
                spriteBatch.DrawString(font, text, pos, Color.White);
        }
    }
}