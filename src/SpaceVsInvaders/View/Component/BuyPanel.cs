using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    public class BuyPanel : BasePanel
    {
        private SpriteFont font;
        private int minutesElapsed;
        private int secondsElapsed;

        public Button DamageTowerButton { get; private set; }
        public Button GoldTowerButton { get; private set; }
        public Button HealTowerButton { get; private set; }

        public BuyPanel(Vector2 position, int height, int width)
            : base(position, height, width)
        {
            font = ContentLoader.GetFont("Fonts/InfoFont");

            DamageTowerButton = new Button(new Vector2(PanelX + 60, PanelY), 50, PanelWidth - 60, $"Damage ${Config.GetValue<TowerConfig>("DamageTower").Cost}");
            GoldTowerButton = new Button(new Vector2(PanelX + 60, PanelY + 60), 50, PanelWidth - 60, $"Gold ${Config.GetValue<TowerConfig>("GoldTower").Cost}");
            HealTowerButton = new Button(new Vector2(PanelX + 60, PanelY + 120), 50, PanelWidth - 60, $"Heal ${Config.GetValue<TowerConfig>("HealTower").Cost}");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DamageTowerButton.Update(gameTime);
            GoldTowerButton.Update(gameTime);
            HealTowerButton.Update(gameTime);
        }
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