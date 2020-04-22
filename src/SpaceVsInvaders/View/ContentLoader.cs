using System.Runtime.CompilerServices;
using System;
using System.Net.Mime;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using SpaceVsInvaders.Model;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SpaceVsInvaders.View
{
    public static class ContentLoader
    {
        private static GraphicsDevice graphicsDevice;
        private static Dictionary<string, Texture2D> textures;
        private static readonly string[] texturePaths = {
            "SvsI_SPrites/big_enemy2",
            "SvsI_SPrites/enemybase",
            "SvsI_SPrites/fast_enemy",
            "SvsI_SPrites/normal_enemy",
            "SvsI_SPrites/green-cross-png",
            "Buttons/clicked",
            "Buttons/notClicked",
            "SvsI_SPrites/shooter-tower",
            "SvsI_SPrites/gold-tower",
            "SvsI_SPrites/heal-tower",
            "Backgrounds/background",
            "Pixels/transparent-pixel",
            "Backgrounds/panel1",
            "Buttons/button-edge",
            "Buttons/button-middle",
            "Backgrounds/pause-background",
            "Cursors/normal",
            "Cursors/normal2",
            "SvsI_SPrites/logo",
            "Backgrounds/end-tile",
        };

        private static Dictionary<string, SpriteFont> fonts;

        private static readonly string[] fontsPath = {
            "Fonts/EpicFont",
            "Fonts/NumberFont",
            "Fonts/ButtonFont",
            "Fonts/InfoFont",
            "Fonts/TitleFont",
        };

        private static Dictionary<string, Song> songs;

        private static readonly string[] songsPath = {
            "Sounds/AllMusic",
            "Sounds/MenuMusic",
            "Sounds/GameMusic",
        };

        private static Dictionary<string, SoundEffect> soundEffects;

        private static readonly string[] soundEffectsPath = {
            "Sounds/btn_click",
            "Sounds/btn_hover",
            "Sounds/laser",
            "Sounds/error",
        };

        public static void AttachGraphicsDevice(GraphicsDevice gd)
        {
            graphicsDevice = gd;
        }

        public static void LoadContent(ContentManager content)
        {
            textures = new Dictionary<string, Texture2D>();
            foreach (string path in texturePaths)
            {
                textures.Add(path, content.Load<Texture2D>(path));
            }

            fonts = new Dictionary<string, SpriteFont>();
            foreach(string path in fontsPath)
            {
                fonts.Add(path, content.Load<SpriteFont>(path));
            }

            songs = new Dictionary<string, Song>();
            foreach(string path in songsPath)
            {
                songs.Add(path, content.Load<Song>(path));
            }

            soundEffects = new Dictionary<string, SoundEffect>();
            foreach(string path in soundEffectsPath)
            {
                soundEffects.Add(path, content.Load<SoundEffect>(path));
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
        public static SpriteFont GetFont(string path)
        {
            SpriteFont font;
            if (fonts.TryGetValue(path, out font))
            {
                return font;
            }
            else
            {
                throw new ArgumentException("Could not find font: " + path);
            }
        }

        public static Song GetSong(string path)
        {
            Song sound;
            if (songs.TryGetValue(path, out sound))
            {
                return sound;
            }
            else
            {
                throw new ArgumentException("Could not find song: " + path);
            }
        }

        public static SoundEffect GetSoundEffect(string path)
        {
            SoundEffect sound;
            if (soundEffects.TryGetValue(path, out sound))
            {
                return sound;
            }
            else
            {
                throw new ArgumentException("Could not find soundeffect: " + path);
            }
        }

        public static Texture2D GetTexture(this TowerType tile) =>  tile switch
        {
            TowerType.Heal   => GetTexture("SvsI_SPrites/heal-tower"),
            TowerType.Damage => GetTexture("SvsI_SPrites/shooter-tower"),
            TowerType.Gold   => GetTexture("SvsI_SPrites/gold-tower")
        };

        public static Texture2D GetTexture(this EnemyType tile) =>  tile switch
        {
            EnemyType.Buff   => GetTexture("SvsI_SPrites/big_enemy2"),
            EnemyType.Normal => GetTexture("SvsI_SPrites/normal_enemy"),
            EnemyType.Speedy => GetTexture("SvsI_SPrites/fast_enemy")
        };

        public static Texture2D CreateSolidtexture(Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }
    }
}