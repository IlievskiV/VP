using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawPolygons
{
    [Serializable]
    public class Polygon
    {
        public List<Line> Lines { get; set; }
        public Point[] Points { get; set; }

        Color color;


        public Polygon(List<Line> lines, Random r)
        {
            Lines = new List<Line>();
            foreach (Line line in lines)
            {
                Lines.Add(line);
            }
            color = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            Points = getPoints();
        }

        private Point[] getPoints()
        {
            List<Point> pointsList = new List<Point>();
            //ова треба да преставува и почетна и крајна точка
            pointsList.Add(Lines[0].StartPoint);

            for (int i = 0; i < Lines.Count - 1; i++) 
            {
                pointsList.Add(Lines[i].EndPoint);
            }

            Point[] points = new Point[pointsList.Count];
            for (int i = 0; i < pointsList.Count; i++)
            {
                points[i] = new Point(pointsList[i].X, pointsList[i].Y);
            }

            return points;
        }

        public void Draw(Graphics g)
        {
            g.FillPolygon(new SolidBrush(color),Points);
        }

        public void Move(int dx, int dy)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X += dx;
                Points[i].Y += dy;
            }
        }
    }
}
