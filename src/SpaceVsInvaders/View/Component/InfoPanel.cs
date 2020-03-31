using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders.View.Components
{
    public class InfoPanel : Component
    {
        SVsIModel model;
        SpriteFont font;
        int secondsElapsed;
        public InfoPanel(Vector2 position, int height, int width, SVsIModel model)
            : base(position, height, width)
        {
            this.model = model;
            font = ContentLoader.GetFont("Fonts/EpicFont");
        }

        public override void Update(GameTime gameTime)
        {
            secondsElapsed = (int)gameTime.TotalGameTime.TotalSeconds;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, $"Time: {secondsElapsed}", new Vector2(position.X, position.Y), Color.White);
            spriteBatch.DrawString(font, $"Money: {model.Money}", new Vector2(position.X, position.Y + 30), Color.White);
            spriteBatch.DrawString(font, $"Castle Health: {model.Castle.Health}", new Vector2(position.X, position.Y + 60), Color.White);
        }
    }
}