using System;
using System.Collections;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    public class SVsIEventArgs : EventArgs
    {
        public Coordinate From { get; private set; }
        public Coordinate To { get; private set; }
        public Coordinate Where { get; private set; }
        public bool GameOver { get; private set; }

        public EnemyType Type { get; private set; }
        
        public SVsIEventArgs(int fromX, int fromY, int toX, int toY)
        {
            From = new Coordinate(fromX, fromY);
            To = new Coordinate(toX, toY);
        }

        public SVsIEventArgs(int whereX, int whereY)
        {
            Where = new Coordinate(whereX, whereY);
        }
         public SVsIEventArgs(bool gameover)
         {
            GameOver = true;
         }

        public SVsIEventArgs(int whereX, int whereY, EnemyType type)
        {
            Where = new Coordinate(whereX, whereY);
            Type = type;
        }
    }
}