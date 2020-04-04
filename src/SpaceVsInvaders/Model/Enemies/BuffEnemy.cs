using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsIBuffEnemy : SVsIEnemy
    {
        public  SVsIBuffEnemy() 
            : base(Config.GetValue<EnemyConfig>("BuffEnemy"))
        { 
        }

    }
}