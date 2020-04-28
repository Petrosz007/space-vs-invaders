using System;
using System.IO;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace SpaceVsInvaders
{
    /// <summary>
    /// Handles configuration loading
    /// </summary>
    public static class Config
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Uses the given file path as the configuration data
        /// </summary>
        /// <param name="file">Config file name, the path relative to DIR/data/</param>
        public static void Initiate(string file)
        {
            var dataDir = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "data");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(dataDir, file))
                .Build();
        }

        /// <summary>
        /// Retrieves a value from the configuration
        /// </summary>
        /// <param name="key">Key of the config option</param>
        /// <typeparam name="T">Type of value to return</typeparam>
        /// <returns>Config value</returns>
        public static T GetValue<T>(string key) =>
            Configuration.GetSection(key).Get<T>();
    }

    /// <summary>
    /// Enemy configuration layout
    /// </summary>
    public class EnemyConfig
    {
        public int Health { get; set; }
        public int Movement { get; set; }
        public int Damage { get; set; }
        public int TickTime { get; set; }
    }

    /// <summary>
    /// Tower configuration layout
    /// </summary>
    public class TowerConfig
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Cost { get; set; }
        public int TickTime { get; set; }
        public int Range { get; set; }
    }

    /// <summary>
    /// Castle configuration layout
    /// </summary>
    public class CastleConfig
    {
        public int Health { get; set; }
        public int UpgradeCost { get; set; }
    }
}