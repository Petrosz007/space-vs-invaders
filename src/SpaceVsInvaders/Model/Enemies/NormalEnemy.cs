using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsINormalEnemy : SVsIEnemy
    {
        public  SVsINormalEnemy() 
        { 
            Health = 100;
            Movement = 3;
            Damage = 10;
            TickTime = 5;
            CoolDown = 0;
        }   
    }
}