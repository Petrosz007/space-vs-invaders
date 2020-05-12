using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Base panel component
    /// </summary>
    /// <remarks>
    /// PanelX,Y,Width,Height are the usable part of the panel that can be drawn to.
    /// If you use the base values you draw on the borders of the panel texture.
    /// </remarks>
    public abstract class BasePanel : Component
    {
        private Texture2D backgroundPanel;

        /// <summary>
        /// Usable base X position of the panel
        /// </summary>
        /// <value>X position</value>
        protected int PanelX { get; private set; }

        /// <summary>
        /// Usable base Y position of the panel
        /// </summary>
        /// <value>Y position</value>
        protected int PanelY { get; private set; }

        /// <summary>
        /// Usable width of the panel
        /// </summary>
        /// <value>Width</value>
        protected int PanelWidth { get; private set; }

        /// <summary>
        /// Usable height of the panel
        /// </summary>
        /// <value>Height</value>
        protected int PanelHeight { get; private set; }

        /// <summary>
        /// Constructor of <c>BasePanel</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the panel</param>
        /// <param name="width">Width of the panel</param>
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

        /// <summary>
        /// Draws the panel background to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw to</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundPanel, new Rectangle((int)position.X, (int)position.Y, width, height), 
                new Rectangle(0,0,backgroundPanel.Width, backgroundPanel.Height), Color.White);
        }
    }
}