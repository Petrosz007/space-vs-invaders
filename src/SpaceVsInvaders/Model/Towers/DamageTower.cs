using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIDamageTower : SVsITower
        {

            public  SVsIDamageTower() 
            { 
                Health = 100;
                Cost = 200;
                Level = 1;
                TickTime = 1;
                CoolDown = 0; 
                Range = 6;
            }

            public void  DamageTower()
            {
                
            }
        }
}
