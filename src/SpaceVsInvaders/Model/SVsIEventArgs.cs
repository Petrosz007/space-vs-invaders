using System;
using System.Collections;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    /// <summary>
    /// Class to contain event data.
    /// </summary>
    public class SVsIEventArgs : EventArgs
    {
        /// <summary>
        /// 'From' coordinates of an event.
        /// </summary>
        /// <value>From</value>
        public Coordinate From { get; private set; }
        
        /// <summary>
        /// 'To' coordinates of an event.
        /// </summary>
        /// <value>To</value>
        public Coordinate To { get; private set; }
        
        /// <summary>
        /// 'Where' coordinates of an event.
        /// </summary>
        /// <value></value>
        public Coordinate Where { get; private set; }
        
        /// <summary>
        /// Boolean that represents whether the game is over or not.
        /// </summary>
        /// <value> GameOver </value>
        public bool GameOver { get; private set; }

        /// <summary>
        /// Type of the enemy.
        /// </summary>
        /// <value>EnemyType</value>
        public EnemyType Type { get; private set; }
        
        /// <summary>
        /// EventArgs for movement and shot.
        /// </summary>
        /// <param name="fromX"> The starting row of the action. </param>
        /// <param name="fromY"> The starting coloumn of the action.</param>
        /// <param name="toX"> The ending row of the action. </param>
        /// <param name="toY"> The ending coloumn of the action. </param>
        public SVsIEventArgs(int fromX, int fromY, int toX, int toY)
        {
            From = new Coordinate(fromX, fromY);
            To = new Coordinate(toX, toY);
        }

        /// <summary>
        /// EventArgs for catastrophes, tower destroyals, enemy exterminations.
        /// </summary>
        /// <param name="whereX"> The row where the event occurs. </param>
        /// <param name="whereY"> The coloumn where the event occurs. </param>
        public SVsIEventArgs(int whereX, int whereY)
        {
            Where = new Coordinate(whereX, whereY);
        }

        /// <summary>
        /// EventArgs for setting 'GameOver' true.
        /// </summary>
        /// <param name="gameover"></param>
         public SVsIEventArgs(bool gameover)
         {
            GameOver = true;
         }

        /// <summary>
        /// EventArgs for the enemy's entry to the castle.
        /// </summary>
        /// <param name="whereX"> The row where it was before the entry. </param>
        /// <param name="whereY"> The coloumn where it was before the entry. </param>
        /// <param name="type"> The type of the enemy. </param>
        public SVsIEventArgs(int whereX, int whereY, EnemyType type)
        {
            Where = new Coordinate(whereX, whereY);
            Type = type;
        }
    }
}