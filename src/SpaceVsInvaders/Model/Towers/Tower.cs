using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsITower
    {
        public int Health { get; protected set; }
        public int Cost { get; protected set; }
        public int Level { get; protected set; }
        public int TickTime { get; protected set; }
        public int CoolDown { get; protected set; }
        public int Range { get; protected set; }
        
        public  SVsITower() 
        { 
            
        }
    }
}