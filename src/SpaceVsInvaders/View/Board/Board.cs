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
    public class Board : Component
    {
        private SVsIModel model;
        private StateManager stateManager;
        public ShotAnimator ShotAnimator { get; set; }
        public CatastropheAnimator CatastropheAnimator { get; set; }

        private int colWidth;
        private int rowHeight;
        private Tile[,] tiles;
        private Texture2D divTexture;
        private int divWidth;
        private Color divColor;
        public event EventHandler<(int, int)> TileClicked;
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

        private void HandleTileClick(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;

            // Console.WriteLine("Clicked Row={0} Col={1}", tile.Row, tile.Col);
            TileClicked?.Invoke(this, (tile.Row, tile.Col));
        }

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