using System;

namespace SpaceVsInvaders.Model.Towers
{
    public class SVsITower
    {
        public int Health { get; private set; }
        public int Cost { get; private set; }
        public int Level { get; private set; }
        public int TickTime { get; private set; }
        public int CoolDown { get; private set; }
        public int Range { get; private set; }
        
        public  SVsITower() 
        { 
            
        }
    }
}