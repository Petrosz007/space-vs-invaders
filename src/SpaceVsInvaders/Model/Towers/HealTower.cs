using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIHealTower : SVsITower
    { 
        public  SVsIHealTower() 
        { 
            Health = 50;
            Cost = 100;
            Level = 1;
            TickTime = 3;
            CoolDown = 0; 
            // Range = 3;
        }


        public override int Heal()
        {
           return Level * 5;
        }

    }
}