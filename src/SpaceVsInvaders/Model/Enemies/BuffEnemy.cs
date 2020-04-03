using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsIBuffEnemy : SVsIEnemy
    {
        public  SVsIBuffEnemy() 
        { 
            EnemyConfig conf = Config.GetValue<EnemyConfig>("BuffEnemy");
            Health = conf.Health;
            Movement = conf.Movement;
            Damage = conf.Damage;
            TickTime = conf.TickTime;
            CoolDown = 0;
        }

    }
}