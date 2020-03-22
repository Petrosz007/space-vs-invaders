using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsISpeedyEnemy : SVsIEnemy
    {
        public  SVsISpeedyEnemy() 
        { 
            Health = 75;
            Movement = 1;
            Damage = 5;
            CoolDown = 0;
        }
        
    }
}