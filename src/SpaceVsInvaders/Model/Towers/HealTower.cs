using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIHealTower : SVsITower
    { 
        public  SVsIHealTower() 
        { 
            var conf = Config.GetValue<TowerConfig>("HealTower");
            Health = conf.Health;
            Cost = conf.Cost;
            Level = 1;
            TickTime = conf.TickTime;
            CoolDown = 0; 
            // Range = 3;
        }


        public override int Heal()
        {
           return Level * 5;
        }

    }
}