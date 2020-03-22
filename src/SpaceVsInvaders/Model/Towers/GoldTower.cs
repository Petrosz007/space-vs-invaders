using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIGoldTower : SVsITower
    {
        
        public  SVsIGoldTower() 
        { 
            Health = 75;
            Cost = 150;
            Level = 1;
            TickTime = 2;
            CoolDown = 0; 
            Range = 10; 
        }
    }
}