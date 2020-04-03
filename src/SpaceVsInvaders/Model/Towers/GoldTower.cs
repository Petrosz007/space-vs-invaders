using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIGoldTower : SVsITower
    {
        
        public  SVsIGoldTower() 
        { 
            var conf = Config.GetValue<TowerConfig>("GoldTower");
            Health = conf.Health;
            Cost = conf.Cost;
            Level = 1;
            TickTime = conf.TickTime;
            CoolDown = 0; 
           // Range = 10; 
        }

        public override int Gold()
        {
            return Level * 10;
        }
    }
}