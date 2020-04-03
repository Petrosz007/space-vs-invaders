using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsIHealTower : SVsITower
    {
        public SVsIHealTower()
            : base(Config.GetValue<TowerConfig>("HealTower"))
        {
        }

        public int Heal() =>
            Level * 5;
    }
}