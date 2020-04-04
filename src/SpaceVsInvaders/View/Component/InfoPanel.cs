using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    public class InfoPanel : Component
    {
        private SVsIModel model;
        private SpriteFont font;
        private int secondsElapsed;

        public Button UpgradeCastleButton { get; private set; }

        public InfoPanel(Vector2 position, int height, int width, SVsIModel model)
            : base(position, height, width)
        {
            this.model = model;
            font = ContentLoader.GetFont("Fonts/EpicFont");

            UpgradeCastleButton = new Button(new Vector2(position.X + width - 100, position.Y + 100), 50, 100);
        }

        public override void Update(GameTime gameTime)
        {
            secondsElapsed = (int)gameTime.TotalGameTime.TotalSeconds;

            UpgradeCastleButton.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"Time: {secondsElapsed}", new Vector2(position.X, position.Y), Color.White);
            spriteBatch.DrawString(font, $"Money: {model.Money}", new Vector2(position.X, position.Y + 30), Color.White);
            spriteBatch.DrawString(font, $"Castle Level: {model.Castle.Level}", new Vector2(position.X, position.Y + 60), Color.White);
            spriteBatch.DrawString(font, $"Castle Health: {model.Castle.Health}", new Vector2(position.X, position.Y + 90), Color.White);

            UpgradeCastleButton.Draw(spriteBatch);
        }
    }
}