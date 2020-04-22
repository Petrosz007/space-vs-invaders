using System;
using System.Collections;
using System.Collections.Generic;
using SpaceVsInvaders.Model.Towers;
using SpaceVsInvaders.Model.Enemies;

namespace SpaceVsInvaders.Model
{
    public class Coordinate
    {
        private int x;
        private int y;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override bool Equals(object obj)
        {
            var item = obj as Coordinate;

            if (item == null)
            {
                return false;
            }

            return this.x == item.x && this.y == item.y;
        }
        public override int GetHashCode()
        {
            int tmp = ( this.y +  ((this.x+1)/2));
            return x +  ( tmp * tmp);
        }
    }
}