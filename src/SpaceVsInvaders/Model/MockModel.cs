using System;
using Microsoft.Xna.Framework;

namespace SpaceVsInvaders.Model
{
    public class MockModel
    {
        public Position Player { get; set; }
        private int Ticks { get; set; }

        public MockModel()
        {
            Player = new Position{ X = 0, Y = 2 };
            Ticks = 0;
        }

        public void HandleTick()
        {
            Ticks++;

            if(Ticks % 10 == 0) {
                Player.X -= 10;
                Player.Y += 10;
            } else {
                Player.X += 2;
            }
        }
    }
}