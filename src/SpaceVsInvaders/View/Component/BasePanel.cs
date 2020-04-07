using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    public class BasePanel : Component
    {
        private Texture2D backgroundPanel;
        protected int PanelX { get; private set; }
        protected int PanelY { get; private set; }
        protected int PanelWidth { get; private set; }
        protected int PanelHeight { get; private set; }

        public BasePanel(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            backgroundPanel = ContentLoader.GetTexture("Backgrounds/panel1");


            int xOffset = width / 5;
            int yOffset = height / 5;

            PanelX = (int) position.X + xOffset;
            PanelY = (int) position.Y + yOffset;

            PanelWidth = width - xOffset * 2;
            PanelHeight = height - yOffset * 2;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundPanel, new Rectangle((int)position.X, (int)position.Y, width, height), 
                new Rectangle(0,0,backgroundPanel.Width, backgroundPanel.Height), Color.White);
        }
    }
}