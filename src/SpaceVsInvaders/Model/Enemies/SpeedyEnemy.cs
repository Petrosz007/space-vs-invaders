using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsISpeedyEnemy : SVsIEnemy
    {
        public  SVsISpeedyEnemy() 
        { 
            EnemyConfig conf = Config.GetValue<EnemyConfig>("SpeedyEnemy");
            Health = conf.Health;
            Movement = conf.Movement;
            Damage = conf.Damage;
            TickTime = conf.TickTime;
            CoolDown = 0;
        }
        
    }
}