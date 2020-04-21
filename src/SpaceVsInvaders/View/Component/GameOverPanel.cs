using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceVsInvaders.View.Components
{
    public class GameOverPanel : Component
    {
        private StateManager stateManager;
        private SpriteFont font;
        private Texture2D background;
        public Button MainMenuButton { get; private set; }
        public GameOverPanel(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;

            font = ContentLoader.GetFont("Fonts/InfoFont");
            background = ContentLoader.GetTexture("Backgrounds/end-tile");

            const int btnHeight = 75;
            const int btnWidth = 200;
            MainMenuButton = new Button(position + new Vector2((width - btnWidth)/2, height - btnHeight - 50), btnHeight, btnWidth, "Main Menu");
        }

        public override void Update(GameTime gameTime)
        {
            if(!stateManager.GameOver) return;
            
            MainMenuButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!stateManager.GameOver) return;
            spriteBatch.Draw(background, 
                new Rectangle((int)position.X, (int)position.Y, width, height), 
                new Rectangle(0,0, background.Width, background.Height),
                Color.White);

            var text = stateManager.Victory ? "Victory!" : "Defeat.";
            var measure = font.MeasureString(text);
            spriteBatch.DrawString(font, text, 
                position + new Vector2((width - measure.X)/2, 100), Color.White);
        
            MainMenuButton.Draw(spriteBatch);
        }
    }
}