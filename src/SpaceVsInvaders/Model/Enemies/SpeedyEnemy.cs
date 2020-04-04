using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsISpeedyEnemy : SVsIEnemy
    {
        public  SVsISpeedyEnemy() 
            : base(Config.GetValue<EnemyConfig>("SpeedyEnemy"))
        { 
        }
        
    }
}