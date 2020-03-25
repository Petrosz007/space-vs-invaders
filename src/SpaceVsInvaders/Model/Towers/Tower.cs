using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsITower
    {
        public int Health { get; set; }
        public int Cost { get; protected set; }
        public int Level { get; set; }
        public int TickTime { get; set; }
        public int CoolDown { get; set; }
        public int Range { get; set; }
        
        public  SVsITower() 
        { 
            
        }

    
        public virtual int Damage() {return 0;}
        public virtual int Gold() {return 0;}
        public virtual int Heal() {return 0;}
    }
}