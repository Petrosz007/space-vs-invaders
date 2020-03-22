using System;
using System.Net.Mime;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceVsInvaders.View
{
    public static class TextureLoader
    {
        private static Dictionary<string, Texture2D> textures;
        private static readonly string[] texturePaths = {
            "SvsI_SPrites/big_enemy2",
            "SvsI_SPrites/enemybase",
            "SvsI_SPrites/fast_enemy",
            "SvsI_SPrites/normal_enemy",
            "Buttons/clicked",
            "Buttons/notClicked",
        };

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
                default:
                    throw new ArgumentException("No texture for tile: " + tile);
            }
        }
    }
}