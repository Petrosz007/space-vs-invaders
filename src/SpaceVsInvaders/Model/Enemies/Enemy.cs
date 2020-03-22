using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsIEnemy
    {
        public int Health { get; protected set; }
        public int Movement { get; protected set; }
        public int Damage { get; protected set; }
        public int CoolDown { get; protected set; }

        public SVsIEnemy()
        {
            
        }
    }
}