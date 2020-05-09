using System;

namespace SpaceVsInvaders.Model.Towers
{
    /// <summary>
    /// Derived class of tower that can damage the enemy.
    /// </summary>
    public class SVsIDamageTower : SVsITower
    {
        /// <summary>
        /// SVsIDamageTower constructor.
        /// </summary>
        public SVsIDamageTower()
            : base(Config.GetValue<TowerConfig>("DamageTower")) 
        { 
        }

        /// <summary>
        /// Defines how much damage it causes.
        /// </summary>
        /// <returns> Its damage </returns>
        public int Damage() =>
            Level * 15;
    }
}
