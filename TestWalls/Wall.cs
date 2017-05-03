using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWalls
{
    class Wall
    {
        public Point begin { get; } = new Point();
        public Point end { get; } = new Point();

        public Wall(Point v1, Point v2)
        {
            begin = v1;
            end = v2;
        }

        public bool Compare(Wall comp)
        {
            if (
                (((comp.begin.X == begin.X) && (comp.begin.Y <= begin.Y)) || ((comp.begin.Y == begin.Y) && (comp.begin.X <= begin.X)))
                &&
                (((comp.end.X == end.X) && (comp.end.Y >= end.Y)) || ((comp.end.Y == end.Y) && (comp.end.X >= end.X)))
                ) return true;
            else return false;
        }
        public bool Compare(Point compA, Point compB)
        {
            if ((begin == compA) && (end == compB)) return true;
            else return false;
        }
    }
}
