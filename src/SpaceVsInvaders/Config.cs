using System;
using System.IO;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace SpaceVsInvaders
{
    public class EnemyConfig
    {
        public int Health { get; set; }
        public int Movement { get; set; }
        public int TickTime { get; set; }
    }
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
}