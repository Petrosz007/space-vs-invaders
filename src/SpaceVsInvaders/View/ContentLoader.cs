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
    /// <summary>
    /// Singleton that can load textures, fonts, sounds or music
    /// </summary>
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

        private static Dictionary<Color, Texture2D> solidTextureCache = 
            new Dictionary<Color, Texture2D>();

        /// <summary>
        /// Attach the Graphics Device to the ContentLoader, to allow the creating of textures
        /// </summary>
        /// <param name="gd">Main Graphics Device</param>
        public static void AttachGraphicsDevice(GraphicsDevice gd)
        {
            graphicsDevice = gd;
        }

        /// <summary>
        /// Loads content into the object using the ContentManger
        /// </summary>
        /// <remarks>Should be used during the initializaton of the main game</remarks>
        /// <param name="content">ContentManager to load the content from</param>
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

        /// <summary>
        /// Fetches the texture of the given path
        /// </summary>
        /// <param name="path">The texture's path</param>
        /// <returns>The requested texture</returns>
        /// <exception cref="ArgumentException">Invalid path</exception>
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

        /// <summary>
        /// Fetches the font of the given path
        /// </summary>
        /// <param name="path">The font's path</param>
        /// <returns>The requested font</returns>
        /// <exception cref="ArgumentException">Invalid path</exception>

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

        /// <summary>
        /// Fetches the song of the given path
        /// </summary>
        /// <param name="path">The song's path</param>
        /// <returns>The requested song</returns>
        /// <exception cref="ArgumentException">Invalid path</exception>

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

        /// <summary>
        /// Fetches the sound effect of the given path
        /// </summary>
        /// <param name="path">The sound effect's path</param>
        /// <returns>The requested sound effect</returns>
        /// <exception cref="ArgumentException">Invalid path</exception>

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

        /// <summary>
        /// Fetches the tower's texture
        /// </summary>
        /// <param name="tile">The tower's type</param>
        /// <returns>The tower's texture</returns>
        public static Texture2D GetTexture(this TowerType tile) =>  tile switch
        {
            TowerType.Heal   => GetTexture("SvsI_SPrites/heal-tower"),
            TowerType.Damage => GetTexture("SvsI_SPrites/shooter-tower"),
            TowerType.Gold   => GetTexture("SvsI_SPrites/gold-tower")
        };

        /// <summary>
        /// Fetches the enemy's texture
        /// </summary>
        /// <param name="tile">The enemy's type</param>
        /// <returns>The enemy's texture</returns>
        public static Texture2D GetTexture(this EnemyType tile) =>  tile switch
        {
            EnemyType.Buff   => GetTexture("SvsI_SPrites/big_enemy2"),
            EnemyType.Normal => GetTexture("SvsI_SPrites/normal_enemy"),
            EnemyType.Speedy => GetTexture("SvsI_SPrites/fast_enemy")
        };

        /// <summary>
        /// Creates a solid 1px by 1px texture
        /// </summary>
        /// <param name="color">Color of the texture</param>
        /// <returns>The created texture</returns>
        /// <example>
        /// spriteBatch.Draw(ContentLoader.CreateSolidtexture(Color.Green), new Rectangle(0,0, Width, Height), Color.White * backOpacity);
        /// </example>
        public static Texture2D CreateSolidtexture(Color color)
        {
            Texture2D texture;
            if (solidTextureCache.TryGetValue(color, out texture))
            {
                return texture;
            }

            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { color });

            solidTextureCache.Add(color, texture);

            return texture;
        }
    }
}