using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIDamageTower : SVsITower
    {
        public SVsIDamageTower()
            : base(Config.GetValue<TowerConfig>("DamageTower")) 
        { 
        }

        public int Damage() =>
            Level * 5;
    }
}
