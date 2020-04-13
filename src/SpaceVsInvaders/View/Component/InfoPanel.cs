using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    public class InfoPanel : BasePanel
    {
        private SVsIModel model;
        private SpriteFont font;
        private int minutesElapsed;
        private int secondsElapsed;

        public Button UpgradeCastleButton { get; private set; }

        public InfoPanel(Vector2 position, int height, int width, SVsIModel model)
            : base(position, height, width)
        {
            this.model = model;
            font = ContentLoader.GetFont("Fonts/InfoFont");

            UpgradeCastleButton = new Button(new Vector2(PanelX, PanelY + 140), 50, PanelWidth, "Upgrade Castle $0");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            minutesElapsed = (int)gameTime.TotalGameTime.Minutes;
            secondsElapsed = (int)gameTime.TotalGameTime.Seconds;

            UpgradeCastleButton.UpdateText($"Upgrade Castle ${model.Castle.CurrentUpgradeCost}");
            UpgradeCastleButton.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, $"Time: {minutesElapsed.ToString().PadLeft(2, '0')}:{secondsElapsed.ToString().PadLeft(2, '0')}", 
                new Vector2(PanelX, PanelY), Color.White);
            spriteBatch.DrawString(font, $"Money: {model.Money}", new Vector2(PanelX, PanelY + 30), Color.White);
            spriteBatch.DrawString(font, $"Castle Level: {model.Castle.Level}", new Vector2(PanelX, PanelY + 60), Color.White);
            spriteBatch.DrawString(font, $"Castle Health: {model.Castle.Health}", new Vector2(PanelX, PanelY + 90), Color.White);

            UpgradeCastleButton.Draw(spriteBatch);
        }
    }
}