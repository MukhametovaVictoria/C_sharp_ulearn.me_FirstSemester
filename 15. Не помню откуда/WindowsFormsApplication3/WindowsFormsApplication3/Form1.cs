using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        Graphics g;
        bool IsDrawLine = false;

        List<Point> Points = new List<Point>();
        Dictionary<Point, Point> Lines = new Dictionary<Point, Point>();

        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var point in Points)
            {
                g.FillEllipse(new SolidBrush(Color.Black), point.X - 2, point.Y - 2, 4, 4);
            }

            if (IsDrawLine)
            {
                foreach (var line in Lines)
                {
                    g.DrawLine(new Pen(Color.Black), line.Key.X, line.Key.Y, line.Value.X, line.Value.Y);
                }

            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Points.Add(new Point(e.X, e.Y));
            lbPoints.Items.Add($"{e.X}:{e.Y}");
            Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Points.Clear();
            lbPoints.Items.Clear();

            IsDrawLine = false;
            Lines.Clear();

            Invalidate();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            lbPoints.Items.Clear();
            var localPoints = Points.OrderBy(s => s.X).ToList();
            for (int h = 0; h < localPoints.Count; h++)
            {
                var x = localPoints[h].X;
                var y = localPoints[h].Y;
                int firstIndex = h, finalIndex = localPoints.FindLastIndex(z => z.X == x);
                if (firstIndex != finalIndex)
                    localPoints = Sort(firstIndex, finalIndex - firstIndex + 1, localPoints);
                h = finalIndex;
            }
                for (int j = 0; j < localPoints.Count; j++)
            {
                var x = localPoints[j].X;
                var y = localPoints[j].Y;
                int firstIndex = j, finalIndex = localPoints.FindLastIndex(z => z.X == x);
                int predY = 0;
                if (j != 0 && firstIndex != finalIndex)
                {
                    predY = localPoints[j - 1].Y;
                    if (Math.Abs(predY - y) > Math.Abs(predY - localPoints[finalIndex].Y)) 
                        localPoints = SortReverse(firstIndex, finalIndex - firstIndex + 1, localPoints);
                }
                j = finalIndex;
            }

            for (int i = 0; i < localPoints.Count - 1; i++)
            {
                if(!Lines.ContainsKey(localPoints[i])) Lines.Add(localPoints[i], localPoints[i + 1]);
                lbPoints.Items.Add($"{localPoints[i].X}:{localPoints[i].Y}");
            }
            lbPoints.Items.Add($"{localPoints[localPoints.Count - 1].X}:{localPoints[localPoints.Count - 1].Y}");
            IsDrawLine = true;
            Invalidate();
        }

        private static List<Point> Sort(int firstIndex, int count, List<Point> pointsss)
        {
            var points = pointsss;
            var range = points.GetRange(firstIndex, count).OrderBy(d => d.Y);
            points.RemoveRange(firstIndex, count);
            points.InsertRange(firstIndex, range);
            return points;
        }
            private static List<Point> SortReverse(int firstIndex, int count, List<Point> pointsss)
        {
            var points = pointsss;
            var range = points.GetRange(firstIndex, count).ToArray();
            for (int i = 0; i < range.Length; i++)
            {
                var point = points.ElementAt(firstIndex + i);
                points.Remove(point);
                points.Insert(firstIndex + i, range[range.Length - 1 - i]);
            }
            return points;
        }

        private void lbPoints_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
