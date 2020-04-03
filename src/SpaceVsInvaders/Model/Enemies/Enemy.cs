using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsIEnemy
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        /// <param name="Movement">Azt írja le, hogy hány másodpercenként lép előre az ellenség.</param>
        public int Movement { get; set; }
        public int Damage { get; set; }
        public int TickTime { get; set; }
        public int CoolDown { get; set; }


        public SVsIEnemy()
        {

        }

        protected SVsIEnemy(EnemyConfig conf)
        {
            Health    = conf.Health;
            Movement  = conf.Movement;
            Damage    = conf.Damage;
            TickTime  = conf.TickTime;

            CoolDown  = 0;
            MaxHealth = Health;
        }
    }
}