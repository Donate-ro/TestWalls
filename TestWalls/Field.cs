using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWalls
{
    class Field
    {
        int N;
        int M;
        List<Water> flood;
        List<Wall> Walls;
        public List<Wall> saved { get; set; }
        Water tmp, tmp2;
        bool up = false, down = false, left = false, right = false;

        public Field(int n, int m, List<Wall> walls)
        {
            N = n + 1;
            M = m + 1;
            flood = new List<Water>();
            Walls = new List<Wall>();
            foreach (var wall in walls) Walls.Add(wall);
        }

        void CreateBlock(int i, int j)
        {
            tmp = new Water(
                new Wall(new Point(i, j), new Point(i, j + 1)),
                new Wall(new Point(i + 1, j), new Point(i + 1, j + 1)),
                new Wall(new Point(i, j), new Point(i + 1, j)),
                new Wall(new Point(i, j + 1), new Point(i + 1, j + 1))
                );
            bool check = true;
            for (int h = 0; h < flood.Count; h++) if (flood.ElementAt(h).Up.begin == tmp.Up.begin)
                {
                    check = false;
                    break;
                }
            if (check) flood.Add(tmp);
        }


        bool Analyze(Wall navigate)
        {
            bool t = true;
            foreach (var wall in Walls)
                if (navigate.Compare(wall))
                {
                    t = false;
                    break;
                }

            return t;
        }

        bool Check()
        {
            foreach (Water water in flood)
            {
                if (
                       (water.Up.begin.X == 1) || (!Analyze(water.Up))
                       ) up = false;
                else up = true;
                if (
                         (water.Down.begin.X == N - 1) || (!Analyze(water.Down))
                        ) down = false;
                else down = true;
                if (
                         (water.Up.begin.Y == N - 1) || (!Analyze(water.Right))
                        ) right = false;
                else right = true;
                if (
                         (water.Up.begin.Y == 1) || (!Analyze(water.Left))
                        ) left = false;
                else left = true;
                if ((up) || (down) || (left) || (right))
                {
                    foreach (Water wat in flood)
                    {
                        if (water.Up.Compare(wat.Down.begin, wat.Down.end)) up = false;
                        if (water.Down.Compare(wat.Up.begin, wat.Up.end)) down = false;
                        if (water.Right.Compare(wat.Left.begin, wat.Left.end)) right = false;
                        if (water.Left.Compare(wat.Right.begin, wat.Right.end)) left = false;
                        if ((!up) && (!down) && (!left) && (!right)) break;
                    }
                }
                if ((up) || (down) || (left) || (right))
                {
                    tmp2 = water;
                    return false;
                }
            }
            return true;
        }

        public void Fill()
        {
            tmp2 = new Water();
            tmp = new Water();
            CreateBlock(1, M - 3);
            while (true)
            {
                if (Check()) break;
                if (up) CreateBlock(tmp2.Up.begin.X - 1, tmp2.Up.begin.Y);
                if (down) CreateBlock(tmp2.Up.begin.X + 1, tmp2.Up.begin.Y);
                if (left) CreateBlock(tmp2.Up.begin.X, tmp2.Up.begin.Y - 1);
                if (right) CreateBlock(tmp2.Up.begin.X, tmp2.Up.begin.Y + 1);
            }
        }
        public void SkipHour()
        {
            int destroy, i = 0;
            int[] index = new int[Walls.Count];
            for (int j = 0; j < index.Length; j++) index[j] = -1;
            foreach (Wall wall in Walls)
            {
                destroy = 0;
                foreach (Water water in flood)
                {
                    if
                        ((water.Up.Compare(wall)) ||
                        (water.Down.Compare(wall)) ||
                        (water.Left.Compare(wall)) ||
                        (water.Right.Compare(wall))) destroy++;
                }
                if (wall.begin.X == wall.end.X)
                {
                    if (destroy == (wall.end.Y - wall.begin.Y))
                    {
                        index[i] = Walls.IndexOf(wall);
                        i++;
                    }
                }
                else if (destroy == (wall.end.X - wall.begin.X))
                {
                    index[i] = Walls.IndexOf(wall);
                    i++;
                }
            }
            saved = new List<Wall>();
            foreach (var wall in Walls) saved.Add(wall);
            foreach (var wall in Walls)
                for (int j = 0; j < index.Length; j++)
                    if (Walls.IndexOf(wall) == index[j]) saved.Remove(wall);
            Walls = new List<Wall>();
            foreach (var wall in saved) Walls.Add(wall);
        }

    }
}