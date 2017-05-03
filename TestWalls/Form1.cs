using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWalls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] input;
        Field field;

        private void button1_Click(object sender, EventArgs e)
        {
            input = richTextBox1.Lines;
            List<Point> points = new List<Point>();
            List<Wall> walls = new List<Wall>();
            GetPointsFromRich(points);
            GetWallsFromRich(walls, points);
            int sizeN = 0, sizeM = 0;
            foreach (var point in points)
            {
                if (sizeN < point.X) sizeN = point.X;
                if (sizeM < point.Y) sizeM = point.Y;
            }
            field = new Field(sizeN + 1, sizeM + 1, walls);
            field.Fill();
            
        }

        void GetPointsFromRich(List<Point> points)
        {
            for (int i = 1; i <= Int32.Parse(input[0]); i++)
            {
                points.Add(new Point(Int32.Parse(input[i].Split(' ')[0]), Int32.Parse(input[i].Split(' ')[1])));
            }
        }

        void GetWallsFromRich(List<Wall> walls, List<Point> points)
        {
            for (int i = points.Count + 2; i < input.Length; i++)
            {
                walls.Add(new Wall(points.ElementAt(Int32.Parse(input[i].Split(' ')[0]) - 1), points.ElementAt(Int32.Parse(input[i].Split(' ')[1]) - 1)));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            field.Fill();
            field.SkipHour();
            foreach (Wall wall in field.saved) label1.Text += wall.begin + " " + wall.end + "\n";
        }
    }
}
