using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceVsInvaders.Model;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;
using SpaceVsInvaders.View.Components;
using System.Collections.Generic;

namespace SpaceVsInvaders.View.Boards
{
    /// <summary>
    /// Game board component
    /// </summary>
    public class Board : Component
    {
        private SVsIModel model;
        private StateManager stateManager;

        /// <summary>
        /// Shot animator of the board
        /// </summary>
        /// <value>Shot animator of the board</value>
        public ShotAnimator ShotAnimator { get; set; }

        /// <summary>
        /// CatastropheAnimator of the board
        /// </summary>
        /// <value>CatastropheAnimator of the board</value>
        public CatastropheAnimator CatastropheAnimator { get; set; }

        private int colWidth;
        private int rowHeight;
        private Tile[,] tiles;
        private Texture2D divTexture;
        private int divWidth;
        private Color divColor;

        /// <summary>
        /// Board tile has been clicked event, the tuple is the row and column of the tile
        /// </summary>
        public event EventHandler<(int, int)> TileClicked;

        /// <summary>
        /// Constructor of <c>Board</c>
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="height">Height of the board</param>
        /// <param name="width">Width of the board</param>
        /// <param name="model">Model to get the data from</param>
        /// <param name="stateManager">State manager to get the state from</param>
        public Board(Vector2 position, int height, int width, SVsIModel model, StateManager stateManager)
            : base(position, height, width)
        {
            this.model = model;
            this.stateManager = stateManager;

            divColor = Color.White;
            divTexture = ContentLoader.CreateSolidtexture(Color.White);
            divWidth = 2;

            colWidth = width / model.Cols;
            rowHeight = height / model.Rows;
            tiles = new Tile[model.Rows, model.Cols];

            ShotAnimator = new ShotAnimator(position, height, width, colWidth, rowHeight);
            CatastropheAnimator = new CatastropheAnimator(position, height, width, colWidth, rowHeight);
        }

        /// <summary>
        /// Handles when one tile has been clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args (not used)</param>
        private void HandleTileClick(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            TileClicked?.Invoke(this, (tile.Row, tile.Col));
        }

        /// <summary>
        /// Draws the board to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw to</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile?.Draw(spriteBatch);
            }

            if(stateManager.PlacingTower)
            {
                for (int i = 0; i < model.Cols + 1; ++i)
                {
                    Rectangle divRect = new Rectangle(
                        (int)position.X + i * colWidth,
                        (int)position.Y,
                        divWidth,
                        height
                    );

                    spriteBatch.Draw(divTexture, divRect, divColor * 0.5f);
                }

                for (int i = 0; i < model.Rows + 1; ++i)
                {
                    Rectangle divRect = new Rectangle(
                        (int)position.X,
                        (int)position.Y + i * rowHeight,
                        width,
                        divWidth
                    );

                    spriteBatch.Draw(divTexture, divRect, divColor * 0.5f);
                }
            }

            ShotAnimator.Draw(spriteBatch);
            CatastropheAnimator.Draw(spriteBatch);
        }

        /// <summary>
        /// Updates the board
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public override void Update(GameTime gameTime)
        {
            if(stateManager.GameOver) return;
            
            ShotAnimator.Update(gameTime);
            CatastropheAnimator.Update(gameTime);

            for (int i = 0; i < model.Rows; ++i)
            {
                for (int j = 0; j < model.Cols; ++j)
                {
                    int currHealth = 0;
                    int maxHealth = 0;
                    if (model.Towers[i, j] != null)
                    {
                        var tile = model.Towers[i, j] switch
                        {
                            SVsIDamageTower _ => TowerType.Damage,
                            SVsIGoldTower   _ => TowerType.Gold,
                            SVsIHealTower   _ => TowerType.Heal,
                        };
                        currHealth = model.Towers[i, j].Health;
                        maxHealth = model.Towers[i, j].MaxHealth;

                        tiles[i, j] = new TowerTile(
                            new Vector2(position.X + colWidth * j, position.Y + rowHeight * i),
                            rowHeight,
                            colWidth,
                            i,
                            j,
                            stateManager,
                            tile,
                            currHealth,
                            maxHealth
                        );
                    }
                    else if(model.Enemies[i, j].Count > 0)
                    {
                        var enemies = new List<(EnemyType, int)>();
                        foreach(var enemy in model.Enemies[i, j])
                        {
                            var enemyType = enemy switch {
                                SVsIBuffEnemy   _ => EnemyType.Buff,
                                SVsINormalEnemy _ => EnemyType.Normal,
                                SVsISpeedyEnemy _ => EnemyType.Speedy,
                            };
                            if(enemies.Any(e => e.Item1 == enemyType))
                                enemies = enemies.Select(e => (e.Item1, e.Item2 + ((e.Item1 == enemyType) ? 1 : 0))).ToList();
                            else
                                enemies.Add((enemyType, 1));
                        }

                        currHealth  = model.Enemies[i, j][0].Health;
                        maxHealth = model.Enemies[i, j][0].MaxHealth;

                        tiles[i, j] = new EnemyTile(
                            new Vector2(position.X + colWidth * j, position.Y + rowHeight * i),
                            rowHeight,
                            colWidth,
                            i,
                            j,
                            stateManager,
                            enemies,
                            currHealth,
                            maxHealth
                        );
                    }
                    else
                    {
                        tiles[i, j] = new Tile(
                            new Vector2(position.X + colWidth * j, position.Y + rowHeight * i),
                            rowHeight,
                            colWidth,
                            i,
                            j,
                            stateManager
                        );
                    }

                    tiles[i, j].LeftClicked += new EventHandler(HandleTileClick);
                }
            }

            foreach (var tile in tiles)
            {
                tile.Update(gameTime);
            }
        }
    }
}