using System;

namespace SpaceVsInvaders.Model.Enemies
{
    /// <summary>
    /// Derived class of enemy that is speedy but less harmful.
    /// </summary>
    public class SVsISpeedyEnemy : SVsIEnemy
    {
       /// <summary>
       /// SVsISpeedyEnemy constructor
       /// </summary>
        public  SVsISpeedyEnemy() 
            : base(Config.GetValue<EnemyConfig>("SpeedyEnemy"))
        { 
        }
        
    }
}