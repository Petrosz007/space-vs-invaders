using System;
using System.Timers;
using SpaceVsInvaders.Model;
using Microsoft.Extensions.Configuration;

namespace SpaceVsInvaders
{


    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Config.Initiate("normal.json");


            EnemyConfig enemy = Config.GetValue<EnemyConfig>("BuffEnemy");
            Console.WriteLine("Buff Enemy: {0} {1} {2}", enemy.Health, enemy.Movement, enemy.TickTime);

            int speedyMovement = Config.GetValue<int>("SpeedyEnemy:Movement");
            Console.WriteLine("SpeedyEnemy Movement: {0}", speedyMovement);

            using (var game = new Game1())
                game.Run();

        }
    }
}