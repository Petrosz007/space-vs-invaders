using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    public class InfoPanel : BasePanel
    {
        private SVsIModel model;
        private StateManager stateManager;
        private SpriteFont font;
        private double secondsElapsed;
        private int seconds;
        private int minutes;

        public Button UpgradeCastleButton { get; private set; }

        public InfoPanel(Vector2 position, int height, int width, SVsIModel model, StateManager stateManager)
            : base(position, height, width)
        {
            this.model = model;
            this.stateManager = stateManager;
            font = ContentLoader.GetFont("Fonts/InfoFont");

            UpgradeCastleButton = new Button(new Vector2(PanelX, PanelY + 140), 50, PanelWidth, "Upgrade Castle $0");

            secondsElapsed = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if(stateManager.GameOver) return;
            
            base.Update(gameTime);

            secondsElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            int remainingSeconds = Config.GetValue<int>("RoundTime") - (int) secondsElapsed;

            seconds = remainingSeconds % 60;
            minutes = (remainingSeconds / 60) % 60;

            UpgradeCastleButton.UpdateText($"Upgrade Castle ${model.Castle.CurrentUpgradeCost}");
            UpgradeCastleButton.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, $"Time: {minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}", 
                new Vector2(PanelX, PanelY), Color.White);
            spriteBatch.DrawString(font, $"Money: {model.Money}", new Vector2(PanelX, PanelY + 30), Color.White);
            spriteBatch.DrawString(font, $"Castle Level: {model.Castle.Level}", new Vector2(PanelX, PanelY + 60), Color.White);
            spriteBatch.DrawString(font, $"Castle Health: {model.Castle.Health}", new Vector2(PanelX, PanelY + 90), Color.White);

            UpgradeCastleButton.Draw(spriteBatch);
        }
    }
}