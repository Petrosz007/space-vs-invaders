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
        private static IConfiguration Configuration { get; set; }

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
        /// <summary>
        /// Health of the enemy
        /// </summary>
        /// <value>Health of the enemy</value>
        public int Health { get; set; }

        /// <summary>
        /// Ticks it takes the enemy to move
        /// </summary>
        /// <value>Ticks it takes the enemy to move</value>
        public int Movement { get; set; }

        /// <summary>
        /// Damage of the enemy
        /// </summary>
        /// <value>Damage of the enemy</value>
        public int Damage { get; set; }

        /// <summary>
        /// Ticks it takes the enemy to attack
        /// </summary>
        /// <value>Ticks it takes the enemy to attack</value>
        public int TickTime { get; set; }
    }

    /// <summary>
    /// Tower configuration layout
    /// </summary>
    public class TowerConfig
    {
        /// <summary>
        /// Health of the tower
        /// </summary>
        /// <value>Health of the tower</value>
        public int Health { get; set; }

        /// <summary>
        /// Max Health of the tower
        /// </summary>
        /// <value>Max Health of the tower</value>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Cost of the tower
        /// </summary>
        /// <value>Cost of the tower</value>
        public int Cost { get; set; }

        /// <summary>
        /// Ticks it takes the tower to do its action
        /// </summary>
        /// <value>Ticks it takes the tower to do its action</value>
        public int TickTime { get; set; }

        /// <summary>
        /// Attack range of the tower
        /// </summary>
        /// <value>Attack range of the tower</value>
        public int Range { get; set; }
    }

    /// <summary>
    /// Castle configuration layout
    /// </summary>
    public class CastleConfig
    {
        /// <summary>
        /// Health of the castle
        /// </summary>
        /// <value>Health of the castle</value>
        public int Health { get; set; }

        /// <summary>
        /// Upgrade cost of the castle
        /// </summary>
        /// <value>Upgrade cost of the castle</value>
        public int UpgradeCost { get; set; }
    }
}