using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsINormalEnemy : SVsIEnemy
    {
        public  SVsINormalEnemy() 
        { 
            EnemyConfig conf = Config.GetValue<EnemyConfig>("NormalEnemy");
            Health = conf.Health;
            Movement = conf.Movement;
            Damage = conf.Damage;
            TickTime = conf.TickTime;
            CoolDown = 0;
        }   
    }
}