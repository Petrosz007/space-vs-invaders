using System;

namespace SpaceVsInvaders.Model.Enemies
{
    /// <summary>
    /// Base class of enemies.
    /// </summary>
    public class SVsIEnemy
    {
        /// <summary>
        /// The enemy's current health.
        /// </summary>
        /// <value> Health </value>
        public int Health { get; set; }
       
       /// <summary>
       /// The maximum value of the enemy's health.
       /// </summary>
       /// <value> MaxHealth </value>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Speed of the enemy.
        /// </summary>
        /// <value> Movement </value>
        public int Movement { get; set; }

        /// <summary>
        /// How much damage the enemy causes.
        /// </summary>
        /// <value> Damage </value>
        public int Damage { get; set; }

        /// <summary>
        /// How often the enemy attacks.
        /// </summary>
        /// <value> TickTime </value>
        public int TickTime { get; set; }

        /// <summary>
        /// How many ticks are left until the next attack.
        /// </summary>
        /// <value> CoolDown </value>
        public int CoolDown { get; set; }


        /// <summary>
        /// SVsIEnemy constructor - public.
        /// </summary>
        public SVsIEnemy()
        {

        }

        /// <summary>
        /// SVsIEnemy constructor - protected.
        /// </summary>
        protected SVsIEnemy(EnemyConfig conf)
        {
            Health    = conf.Health;
            Movement  = conf.Movement;
            Damage    = conf.Damage;
            TickTime  = conf.TickTime;

            CoolDown  = 0;
            MaxHealth = Health;
        }
    }
}