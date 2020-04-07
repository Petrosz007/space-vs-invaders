using System.Reflection;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    public class UnderCursorTower : Component
    {
        private StateManager stateManager;

        public UnderCursorTower(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(stateManager.PlacingTower)
            {
                Point cursor = Mouse.GetState().Position;
                position = new Vector2(cursor.X, cursor.Y);
                var texture = stateManager.TowerPlacingType.GetTexture();
                spriteBatch.Draw(texture, area, new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}