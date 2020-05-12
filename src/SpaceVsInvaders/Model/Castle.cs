using System;

namespace SpaceVsInvaders.Model
{
    /// <summary>
    /// Castle's class.
    /// </summary>
    public class SVsICastle
    {
        /// <summary>
        /// Castle's health
        /// </summary>
        /// <value> Health </value>
        public int Health { get; set; }

        /// <summary>
        /// Castle's original upgrade cost
        /// </summary>
        /// <value> UpgradeCost </value>
        public int UpgradeCost { get; set; }

        /// <summary>
        /// Castle's current level
        /// </summary>
        /// <value> Level </value>
        public int Level { get; set; }

        /// <summary>
        /// Calculates the current upgrade cost of the castle.
        /// </summary>
        /// <value> CurrentUpgradeCost </value>
        public int CurrentUpgradeCost 
        {
            get => UpgradeCost * Level;
        }

        /// <summary>
        /// Castle constructor.
        /// </summary>
        public SVsICastle()
        {
            var conf = Config.GetValue<CastleConfig>("Castle");
            Health = conf.Health; // csak peldak, ezt ki kell majd pontosan szamolni
            UpgradeCost = conf.UpgradeCost;
            Level = 1;
        }

        /// <summary>
        /// Upgrade castle
        /// </summary>
        public void Upgrade()
        {
            Level++;
            Health += Level * 10;
        }
    }
}