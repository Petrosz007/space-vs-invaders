using System;
using System.Timers;
using SpaceVsInvaders.Model;

namespace SpaceVsInvaders
{


    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();

        }
    }
}