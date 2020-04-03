using System;
using System.IO;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace SpaceVsInvaders
{
    public static class Config
    {
        public static IConfiguration Configuration { get; set; }
        public static void Initiate()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "data.json"))
                .Build();
        }
        public static T GetValue<T>(string key) =>
            Configuration.GetSection(key).Get<T>();
    }

    public class EnemyConfig
    {
        public int Health { get; set; }
        public int Movement { get; set; }
        public int Damage { get; set; }
        public int TickTime { get; set; }
    }

    public class TowerConfig
    {
        public int Health { get; set; }
        public int Cost { get; set; }
        public int TickTime { get; set; }
    }

    public class DamageTowerConfig : TowerConfig
    {
        public int Range { get; set; }
    }

    public class CastleConfig
    {
        public int Health { get; set; }
        public int UpgradeCost { get; set; }
    }
}