using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWalls
{
    class Water
    {
        public Wall Up { get; set; }
        public Wall Down { get; set; }
        public Wall Left { get; set; }
        public Wall Right { get; set; }
        
        public Water(Wall up, Wall down, Wall left, Wall right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public Water()
        {
            Up = null;
            Down = null;
            Left = null;
            Right = null;
        }

        public void SearchAndDestroy(List<Wall> walls)
        {
            foreach (var wall in walls)
            {
                if ((wall.Compare(Up)) || (wall.Compare(Down)) || (wall.Compare(Left)) || (wall.Compare(Right))) walls.Remove(wall);
            }
        }
    }
}
