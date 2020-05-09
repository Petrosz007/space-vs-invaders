using System;

namespace SpaceVsInvaders.Model.Enemies
{
    /// <summary>
    /// Derived class of normal enemy.
    /// </summary>
    public class SVsINormalEnemy : SVsIEnemy
    {
        /// <summary>
        /// SVsINormalEnemy constructor
        /// </summary>
        public  SVsINormalEnemy() 
            : base(Config.GetValue<EnemyConfig>("NormalEnemy"))
        { 
        }   
    }
}