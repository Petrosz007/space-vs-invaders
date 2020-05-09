using System;

namespace SpaceVsInvaders.Model.Enemies
{
    /// <summary>
    /// Derived class of enemy that is slow but harmful.
    /// </summary>
    public class SVsIBuffEnemy : SVsIEnemy
    {
        /// <summary>
        /// SVsIBuffEnemy constructor.
        /// </summary>
        public  SVsIBuffEnemy() 
            : base(Config.GetValue<EnemyConfig>("BuffEnemy"))
        { 
        }

    }
}