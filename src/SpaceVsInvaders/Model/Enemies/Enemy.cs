using System;

namespace SpaceVsInvaders.Enemies
{
    public class SVsIEnemy
    {
        public int Health { get; private set; }
        public int Movement { get; private set; }
        public int Damage { get; private set; }
        public int CoolDown { get; private set; }

        public SVsIEnemy()
        {
            
        }
    }
}