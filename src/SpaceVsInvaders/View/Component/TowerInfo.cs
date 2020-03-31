using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;

namespace SpaceVsInvaders.View.Components
{
    public class TowerInfo : Component
    {
        StateManager stateManager;
        SpriteFont font;
        // int secondsElapsed;
        public TowerInfo(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;
            font = ContentLoader.GetFont("Fonts/TowerInfoFont");
        }

        public override void Update(GameTime gameTime)
        {
            // secondsElapsed = (int)gameTime.TotalGameTime.TotalSeconds;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            SVsITower tower = stateManager.SelectedTower;

            // TODO: panel keret

            if(tower != null)
            {
                spriteBatch.DrawString(font, $"Health: {tower.Health}", new Vector2(position.X, position.Y), Color.White);
                spriteBatch.DrawString(font, $"Cost: {tower.Cost}", new Vector2(position.X, position.Y + 15), Color.White);
                spriteBatch.DrawString(font, $"Level: {tower.Level}", new Vector2(position.X, position.Y + 30), Color.White);
                spriteBatch.DrawString(font, $"Range: {tower.Range}", new Vector2(position.X, position.Y + 45), Color.White);
                spriteBatch.DrawString(font, $"Cooldown: {tower.CoolDown}", new Vector2(position.X, position.Y + 60), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, "No tower selected", new Vector2(position.X, position.Y), Color.White);
            }
        }
    }
}