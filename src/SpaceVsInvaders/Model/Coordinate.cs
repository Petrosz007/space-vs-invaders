using System;
using System.Collections;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    /// <summary>
    /// Describes a coordinate on the board
    /// </summary>
    public class Coordinate
    {
        private int x;
        private int y;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        /// <summary>
        /// constructor of a coordinate
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// override of the Equal function
        /// </summary>
        /// <param name="obj">the compareable coordinate</param>
        /// <returns>whether the objects are equal</returns>
        public override bool Equals(object obj)
        {
            var item = obj as Coordinate;

            if (item == null)
            {
                return false;
            }

            return this.x == item.x && this.y == item.y;
        }
        /// <summary>
        /// override of the GetHashCode function
        /// </summary>
        /// <returns>a unique hashcode of a coordinate</returns>
        public override int GetHashCode()
        {
            int tmp = ( this.y +  ((this.x+1)/2));
            return x +  ( tmp * tmp);
        }
    }
}