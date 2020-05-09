using System;

namespace SpaceVsInvaders.Model.Towers
{
    /// <summary>
    /// Base class of towers.
    /// </summary>
    public abstract class SVsITower
    {
        /// <summary>
        /// The tower's current health.
        /// </summary>
        /// <value> Health </value>
        public int Health { get; set; }

        /// <summary>
        /// The maximum value of the tower's health.
        /// </summary>
        /// <value> MaxHealth </value>  
        public int MaxHealth { get; set; }
        /// <summary>
        /// The tower's cost.
        /// </summary>
        /// <value> Cost </value>
        public int Cost { get; protected set; }

        /// <summary>
        /// The current level of the tower.
        /// </summary>
        /// <value> Level </value>
        public int Level { get; set; }

        /// <summary>
        /// How often the tower's specific action repeats. 
        /// </summary>
        /// <value> TickTime </value>
        public int TickTime { get; set; }

        /// <summary>
        /// How many ticks are left until the next action.
        /// </summary>
        /// <value> CoolDown </value>
        public int CoolDown { get; set; }

        /// <summary>
        /// The range within the specific action is made.
        /// </summary>
        /// <value> Range </value>
        public int Range { get; set; }

        /// <summary>
        /// The cost of the tower's upgrade.
        /// </summary>
        /// <value></value>
        public int UpgradeCost {
            get => Cost + Level * 50;
        }

        /// <summary>
        /// The amount of money the player receives when the tower is sold.
        /// </summary>
        /// <value> SellCost </value>
        public int SellCost {
            get => Cost / 2;
        }
        
        /// <summary>
        /// SVsITower constructor - public.
        /// </summary>
        public SVsITower() { }

        /// <summary>
        /// SVsITower constructor - protected.
        /// </summary>
        protected SVsITower(TowerConfig conf) 
        {
            Health = conf.Health;
            MaxHealth = conf.MaxHealth;
            Cost = conf.Cost;
            TickTime = conf.TickTime;
            Range = conf.Range;

            Level = 1;
            CoolDown = 0;
            //MaxHealth = Health;
        }

        /// <summary>
        /// Upgrades the tower.
        /// </summary>
        public void Upgrade()
        {
            Health += 50;
            MaxHealth += 100;
            Level++;
        }
    }
}