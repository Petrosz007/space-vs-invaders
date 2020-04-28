using System.Reflection;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Displays the current tower to be bought under the cursor
    /// </summary>
    public class UnderCursorTower : Component
    {
        private StateManager stateManager;

        /// <summary>
        /// Constructor of <c>UnderCursorTower</c>
        /// </summary>
        /// <param name="position">Position (not used)</param>
        /// <param name="height">Height of the component</param>
        /// <param name="width">Width of the component</param>
        /// <param name="stateManager">State manager to get the curent tower to be bought</param>
        public UnderCursorTower(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;
        }

        /// <summary>
        /// Draws the tower to the mouse position to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw to</param>
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

        /// <summary>
        /// Updates the UnderCursorTower, currently does nothing as there is nothing to update
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public override void Update(GameTime gameTime)
        {

        }
    }
}