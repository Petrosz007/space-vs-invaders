using System.Reflection;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.View.Components;

namespace SpaceVsInvaders.View.Board
{
    public class UnderCursorTower : Component
    {
        private TileType tile;
        private StateManager stateManager;

        public UnderCursorTower(Vector2 position, int height, int width, StateManager stateManager)
            : base(position, height, width)
        {
            this.stateManager = stateManager;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            TileType tile = TileType.Empty;
            if(stateManager.TowerPlacingType == TowerType.Damage) tile = TileType.DamageTower;
            else if(stateManager.TowerPlacingType == TowerType.Heal) tile = TileType.HealTower;
            else if(stateManager.TowerPlacingType == TowerType.Gold) tile = TileType.GoldTower;

            Texture2D texture = tile.GetTexture();

            if(stateManager.PlacingTower)
            {
                Point cursor = Mouse.GetState().Position;
                position = new Vector2(cursor.X, cursor.Y);
                spriteBatch.Draw(texture, area, new Rectangle(0, 0, texture.Width, texture.Height), Color.Pink);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}