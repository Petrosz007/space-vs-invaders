using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Animated mothership component
    /// </summary>
    public class Mothership : Component
    {
        private StateManager stateManager;
        private Texture2D texture;
        private double currXOffset;
        private bool goingRight;
        private int scaledWidth;

        /// <summary>
        /// Constructor of <c>Mothership</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the area to patrol</param>
        /// <param name="width"></param>
        /// <param name="stateManager"></param>
        public Mothership(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;

            texture = ContentLoader.GetTexture("SvsI_SPrites/enemybase");
            scaledWidth = (int) (texture.Width * (height / (double) texture.Height));
        }

        /// <summary>
        /// Updates the position of the mothership
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public override void Update(GameTime gameTime)
        {
            if(stateManager.GameOver) return;
            
            const int pixelPerSec = 400;
            double currMove = pixelPerSec * gameTime.ElapsedGameTime.TotalSeconds;
            if(!goingRight) currMove = -1 * currMove;

            if(currXOffset + currMove + scaledWidth > width)
            {
                goingRight = false;
                currXOffset = width - scaledWidth;
            }
            else if(currXOffset + currMove < 0)
            {
                goingRight = true;
                currXOffset = 0;
            }


            currXOffset += currMove;
        }

        /// <summary>
        /// Draws the mothership to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch</param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int) (position.X + currXOffset), (int) position.Y, scaledWidth, height), 
                new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
        }
    }
}