using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    /// <summary>
    /// Buy panel with buttons to buy towers
    /// </summary>
    public class BuyPanel : BasePanel
    {
        private StateManager stateManager;
        private SpriteFont font;

        /// <summary>
        /// Damage tower buy button
        /// </summary>
        /// <value>Damage tower buy button</value>
        public Button DamageTowerButton { get; private set; }

        /// <summary>
        /// Gold tower buy button
        /// </summary>
        /// <value>Gold tower buy button</value>
        public Button GoldTowerButton { get; private set; }

        /// <summary>
        /// Heal tower buy button
        /// </summary>
        /// <value>Heal tower buy button</value>
        public Button HealTowerButton { get; private set; }

        /// <summary>
        /// Constructor of <c>BuyPanel</c>
        /// </summary>
        /// <param name="position">Position of the panel</param>
        /// <param name="height">Height of the panel</param>
        /// <param name="width">Width of the panel</param>
        /// <param name="stateManager">State manager to get the state from</param>
        public BuyPanel(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;

            font = ContentLoader.GetFont("Fonts/InfoFont");

            DamageTowerButton = new Button(new Vector2(PanelX + 60, PanelY), 50, PanelWidth - 60, $"Damage ${Config.GetValue<TowerConfig>("DamageTower").Cost}");
            GoldTowerButton = new Button(new Vector2(PanelX + 60, PanelY + 60), 50, PanelWidth - 60, $"Gold ${Config.GetValue<TowerConfig>("GoldTower").Cost}");
            HealTowerButton = new Button(new Vector2(PanelX + 60, PanelY + 120), 50, PanelWidth - 60, $"Heal ${Config.GetValue<TowerConfig>("HealTower").Cost}");
        }

        /// <summary>
        /// Update the buttons of the buy panel
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public override void Update(GameTime gameTime)
        {
            if(stateManager.GameOver) return;

            // base.Update(gameTime);

            DamageTowerButton.Update(gameTime);
            GoldTowerButton.Update(gameTime);
            HealTowerButton.Update(gameTime);
        }

        /// <summary>
        /// Draw the buy panel
        /// </summary>
        /// <param name="spriteBatch">Sprite batch to draw to</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            DamageTowerButton.Draw(spriteBatch);
            GoldTowerButton.Draw(spriteBatch);
            HealTowerButton.Draw(spriteBatch);

            spriteBatch.Draw(TowerType.Damage.GetTexture(), new Rectangle(PanelX, PanelY, 50, 50), 
                new Rectangle(0,0,TowerType.Damage.GetTexture().Width,TowerType.Damage.GetTexture().Height), Color.White);

            spriteBatch.Draw(TowerType.Gold.GetTexture(), new Rectangle(PanelX, PanelY + 60, 50, 50), 
                new Rectangle(0,0,TowerType.Gold.GetTexture().Width,TowerType.Gold.GetTexture().Height), Color.White);

            spriteBatch.Draw(TowerType.Heal.GetTexture(), new Rectangle(PanelX, PanelY + 120, 50, 50), 
                new Rectangle(0,0,TowerType.Heal.GetTexture().Width,TowerType.Heal.GetTexture().Height), Color.White);
        }
    }
}