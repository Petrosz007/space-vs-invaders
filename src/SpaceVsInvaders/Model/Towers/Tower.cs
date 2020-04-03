using System;

namespace SpaceVsInvaders.Model.Towers
{
    public abstract class SVsITower
    {
        public int Health { get; set; }
        public int Cost { get; protected set; }
        public int Level { get; set; }
        public int TickTime { get; set; }
        public int CoolDown { get; set; }
        public int Range { get; set; }
        
        public SVsITower() { }
        protected SVsITower(TowerConfig conf) 
        {
            Health = conf.Health;
            Cost = conf.Cost;
            TickTime = conf.TickTime;

            Level = 1;
            CoolDown = 0;
        }
    }
}