using System;

namespace SpaceVsInvaders.Model.Towers
{
    /// <summary>
    /// Derived class of tower that can produce money for the player.
    /// </summary>
    public class SVsIGoldTower : SVsITower
    {
        /// <summary>
        /// SVsIGoldTower constructor. 
        /// </summary>
        public SVsIGoldTower() 
            : base(Config.GetValue<TowerConfig>("GoldTower"))
        { 
        }

        /// <summary>
        /// Defines how much money it produces.
        /// </summary>
        /// <returns> Its production. </returns>
        public int Gold() =>
            Level * 10;
    }
}