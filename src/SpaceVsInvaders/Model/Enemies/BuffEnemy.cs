using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsIBuffEnemy : SVsIEnemy
    {
        public  SVsIBuffEnemy() 
        { 
            Health = 50;
            Movement = 3;
            Damage = 20;
            TickTime = 7;
            CoolDown = 0;
        }

    }
}