using System;

namespace SpaceVsInvaders.Model
{
    public class SVsICastle
    {
        public int Health { get; set; }
        public int UpgradeCost { get; set; }
        public int Level { get; set; }
        public SVsICastle()
        {
            Health = 10; // csak peldak, ezt ki kell majd pontosan szamolni
            UpgradeCost = 100;
            Level = 1;
        }
    }
}