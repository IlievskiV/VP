using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawPolygons
{
    [Serializable]
    public class Line
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(new Pen(Color.Black, 2), StartPoint, EndPoint);
        }
    }
}
