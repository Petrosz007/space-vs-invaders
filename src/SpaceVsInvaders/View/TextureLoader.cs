using System.Runtime.CompilerServices;
using System;
using System.Net.Mime;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SpaceVsInvaders.View
{
    public static class TextureLoader
    {
        private static GraphicsDevice graphicsDevice;
        private static Dictionary<string, Texture2D> textures;
        private static readonly string[] texturePaths = {
            "SvsI_SPrites/big_enemy2",
            "SvsI_SPrites/enemybase",
            "SvsI_SPrites/fast_enemy",
            "SvsI_SPrites/normal_enemy",
            "Buttons/clicked",
            "Buttons/notClicked",
            "SvsI_SPrites/shooter-tower",
            "SvsI_SPrites/gold-tower",
            "SvsI_SPrites/heal-tower",
            "Backgrounds/background",
            "Pixels/transparent-pixel"
        };

        public static void AttachGraphicsDevice(GraphicsDevice gd)
        {
            graphicsDevice = gd;
        }

        public static void LoadTextures(ContentManager content)
        {
            textures = new Dictionary<string, Texture2D>();
            foreach (string path in texturePaths)
            {
                textures.Add(path, content.Load<Texture2D>(path));
            }
        }

        public static Texture2D GetTexture(string path)
        {
            Texture2D texture;
            if (textures.TryGetValue(path, out texture))
            {
                return texture;
            }
            else
            {
                throw new ArgumentException("Could not find texture: " + path);
            }
        }

        public static Texture2D GetTexture(this TileType tile)
        {
            switch (tile)
            {
                case TileType.BuffEnemy: return GetTexture("SvsI_SPrites/big_enemy2");
                case TileType.SpeedyEnemy: return GetTexture("SvsI_SPrites/fast_enemy");
                case TileType.NormalEnemy: return GetTexture("SvsI_SPrites/normal_enemy");
                case TileType.DamageTower: return GetTexture("SvsI_SPrites/shooter-tower");
                case TileType.GoldTower: return GetTexture("SvsI_SPrites/gold-tower");
                case TileType.HealTower: return GetTexture("SvsI_SPrites/heal-tower");
                case TileType.Empty: return GetTexture("SvsI_SPrites/heal-tower");
                default:
                    throw new ArgumentException("No texture for tile: " + tile);
            }
        }

        public static Texture2D CreateSolidtexture(Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }
    }
}