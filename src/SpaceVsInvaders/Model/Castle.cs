using System;

namespace SpaceVsInvaders.Castle
{
    public class SVsICastle
    {
        public int Health { get; private set; }

        public int Level { get; private set; }
        public SVsICastle()
        {
            Health = 10; // csak peldak, ezt ki kell majd pontosan szamolni
            Level = 1;
        }
    }
}