using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIGoldTower : SVsITower
    {
        public SVsIGoldTower() 
            : base(Config.GetValue<TowerConfig>("GoldTower"))
        { 
        }

        public int Gold() =>
            Level * 10;
    }
}