using System;

namespace SpaceVsInvaders.Model.Enemies
{
    public class SVsINormalEnemy : SVsIEnemy
    {
        public  SVsINormalEnemy() 
            : base(Config.GetValue<EnemyConfig>("NormalEnemy"))
        { 
        }   
    }
}