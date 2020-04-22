using System;

namespace SpaceVsInvaders.Model.Towers
{
    public abstract class SVsITower
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Cost { get; protected set; }
        public int Level { get; set; }
        public int TickTime { get; set; }
        public int CoolDown { get; set; }
        public int Range { get; set; }

        public int UpgradeCost {
            get => Cost + Level * 50;
        }

        public int SellCost {
            get => Cost / 2;
        }
        
        public SVsITower() { }
        protected SVsITower(TowerConfig conf) 
        {
            Health = conf.Health;
            MaxHealth = conf.MaxHealth;
            Cost = conf.Cost;
            TickTime = conf.TickTime;
            Range = conf.Range;

            Level = 1;
            CoolDown = 0;
            //MaxHealth = Health;
        }

        public void Upgrade()
        {
            Health += 50;
            MaxHealth += 100;
            Level++;
        }
    }
}