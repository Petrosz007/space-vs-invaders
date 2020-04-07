using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;

namespace SpaceVsInvaders.View.Components
{
    public class TowerInfo : BasePanel
    {
        private StateManager stateManager;
        private SVsIModel model;
        private SVsITower tower;
        private SpriteFont font;
        public Button UpgradeButton { get; private set; }
        public Button SellButton { get; private set; }
        public TowerInfo(Vector2 position, int height, int width, StateManager stateManager, SVsIModel model)
            : base(position, height, width)
        {
            this.stateManager = stateManager;
            this.model = model;
            font = ContentLoader.GetFont("Fonts/InfoFont");

            UpgradeButton = new Button(new Vector2(PanelX, PanelY + 100), 50, PanelWidth, $" Upgrade $???");
            SellButton = new Button(new Vector2(PanelX, PanelY + 160), 50, PanelWidth, $"Sell $???");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            tower = model.Towers[stateManager.SelectedPos.Item1, stateManager.SelectedPos.Item2];
            UpgradeButton.Update(gameTime);
            SellButton.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (tower != null)
            {
                spriteBatch.DrawString(font, $"Health: {tower.Health}", new Vector2(PanelX, PanelY), Color.White);
                spriteBatch.DrawString(font, $"Cost: {tower.Cost}", new Vector2(PanelX, PanelY + 15), Color.White);
                spriteBatch.DrawString(font, $"Level: {tower.Level}", new Vector2(PanelX, PanelY + 30), Color.White);
                spriteBatch.DrawString(font, $"Range: {tower.Range}", new Vector2(PanelX, PanelY + 45), Color.White);
                spriteBatch.DrawString(font, $"Cooldown: {tower.CoolDown}", new Vector2(PanelX, PanelY + 60), Color.White);

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