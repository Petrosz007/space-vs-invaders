using System;
using System.Timers;
using SpaceVsInvaders.Model;
using Microsoft.Extensions.Configuration;

namespace SpaceVsInvaders
{
    /// <summary>
    /// Main program of Space Vs Invaders
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Config.Initiate("normal.json");

            using (var game = new MainGame())
                game.Run();

        }
    }
}