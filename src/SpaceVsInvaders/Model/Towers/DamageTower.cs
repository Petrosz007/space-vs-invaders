using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIDamageTower : SVsITower
        {

            public  SVsIDamageTower() 
            { 
                var conf = Config.GetValue<DamageTowerConfig>("DamageTower");
                Health = conf.Health;
                Cost = conf.Cost;
                Level = 1;
                TickTime = conf.TickTime;
                CoolDown = 0; 
                Range = conf.Range;
            }

            public override int Damage()
            {
                return Level * 5;
            }
        }
}
