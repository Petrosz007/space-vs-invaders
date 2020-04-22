using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    public class Mothership : Component
    {
        private StateManager stateManager;
        private Texture2D texture;
        private double currXOffset;
        private bool goingRight;
        private int scaledWidth;
        public Mothership(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;

            texture = ContentLoader.GetTexture("SvsI_SPrites/enemybase");
            scaledWidth = (int) (texture.Width * (height / (double) texture.Height));
        }

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

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int) (position.X + currXOffset), (int) position.Y, scaledWidth, height), 
                new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
        }
    }
}