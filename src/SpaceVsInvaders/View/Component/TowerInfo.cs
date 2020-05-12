using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Currently selected Tower information display panel
    /// </summary>
    public class TowerInfo : BasePanel
    {
        private StateManager stateManager;
        private SVsIModel model;
        private SVsITower tower;
        private SpriteFont font;
        /// <summary>
        /// Upgrade Button
        /// </summary>
        /// <value></value>
        public Button UpgradeButton { get; private set; }
        /// <summary>
        /// Sell button
        /// </summary>
        /// <value>Sell button</value>
        public Button SellButton { get; private set; }

        /// <summary>
        /// Constructor of <c>TowerInfo</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the panel</param>
        /// <param name="width">Width of the panel</param>
        /// <param name="stateManager">State manager to get the state from</param>
        /// <param name="model">Model to get the current tower from</param>
        public TowerInfo(Vector2 position, int height, int width, StateManager stateManager, SVsIModel model)
            : base(position, height, width)
        {
            this.stateManager = stateManager;
            this.model = model;
            font = ContentLoader.GetFont("Fonts/InfoFont");

            UpgradeButton = new Button(new Vector2(PanelX, PanelY + 100), 50, PanelWidth, "Upgrade $0");
            SellButton = new Button(new Vector2(PanelX, PanelY + 160), 50, PanelWidth, "Sell $0");
        }

        /// <summary>
        /// Updates the info based on the currently selected tower
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if(stateManager.GameOver) return;
            
            // base.Update(gameTime);

            tower = model.Towers[stateManager.SelectedPos.Item1, stateManager.SelectedPos.Item2];
            if(tower != null)
            {
                UpgradeButton.UpdateText($"Upgrade ${tower.UpgradeCost}");
                SellButton.UpdateText($"Sell ${tower.SellCost}");

                UpgradeButton.Update(gameTime);
                SellButton.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the tower info panel
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw to</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (tower != null)
            {
                spriteBatch.DrawString(font, $"Level: {tower.Level}", new Vector2(PanelX, PanelY), Color.White);
                spriteBatch.DrawString(font, $"Health: {tower.Health}", new Vector2(PanelX, PanelY + 15), Color.White);
                spriteBatch.DrawString(font, $"Upgrade Cost: {tower.UpgradeCost}", new Vector2(PanelX, PanelY + 30), Color.White);
                
                UpgradeButton.Draw(spriteBatch);
                SellButton.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(font, "No tower selected", new Vector2(PanelX, PanelY), Color.White);
            }
        }
    }
}