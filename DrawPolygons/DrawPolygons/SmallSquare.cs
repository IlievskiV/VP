using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawPolygons
{
    [Serializable]
    public class SmallSquare
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Pen DashedPen { get; set; }
        //ознака дали треба квадратчето да се исцрта
        public bool Flag = false;

        public SmallSquare(int width, int x, int y)
        {
            Width = width;
            Height = width;
            DashedPen = new Pen(Color.Black, 1);
            DashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            X = x;
            Y = y;
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(DashedPen, X-Width/2, Y-Height/2, Width, Height);
        }

        //дали е кликнато во внатрешноста на квадратчето
        public bool isHit(Point point)
        {
            return (Math.Abs(X - point.X) <= Width / 2 && Math.Abs(Y - point.Y) <= Height / 2);
        }
    }
}
