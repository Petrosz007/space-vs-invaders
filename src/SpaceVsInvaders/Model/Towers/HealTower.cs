using System;

namespace SpaceVsInvaders.Model.Towers
{
    /// <summary>
    /// Derived class of tower that can heal other towers.
    /// </summary>
    public class SVsIHealTower : SVsITower
    {
        /// <summary>
        /// SVsIHealTower constructor.
        /// </summary>
        public SVsIHealTower()
            : base(Config.GetValue<TowerConfig>("HealTower"))
        {
        }

        /// <summary>
        /// Defines how much the other towers' healthpoints are increased by.
        /// </summary>
        /// <returns> Its healing. </returns>
        public int Heal() =>
            Level * 5;
    }
}