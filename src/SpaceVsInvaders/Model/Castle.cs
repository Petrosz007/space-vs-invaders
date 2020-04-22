using System;

namespace SpaceVsInvaders.Model
{
    public class SVsICastle
    {
        public int Health { get; set; }
        public int UpgradeCost { get; set; }
        public int Level { get; set; }

        public int CurrentUpgradeCost 
        {
            get => UpgradeCost * Level;
        }
        public SVsICastle()
        {
            var conf = Config.GetValue<CastleConfig>("Castle");
            Health = conf.Health; // csak peldak, ezt ki kell majd pontosan szamolni
            UpgradeCost = conf.UpgradeCost;
            Level = 1;
        }

        public void Upgrade()
        {
            Level++;
            Health += Level * 10;
        }
    }
}