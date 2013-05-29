using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EatingPies
{
    [Serializable]
    public class Pie
    {
        //координати на центарот на питата
        public int X { get; set; }
        public int Y { get; set; }
        //радиус на питата
        public int Radius { get; set; }
        //агол на завртување
        public int Angle { get; set; }
        //боја на обојување
        public Color PaintColor { get; set; }
        //
        public bool flag;

        public Pie(int x, int y, int radius, int angle, Color paintColor)
        {
            X = x;
            Y = y;
            Radius = radius;
            Angle = angle;
            PaintColor = paintColor;
            flag = false;
        }

        public void Draw(Graphics g)
        {
            g.FillPie(new SolidBrush(PaintColor), X - Radius, Y - Radius, 2 * Radius, 2 * Radius, 0, Angle);
        }

        public void changeAngle()
        {
            Angle = Angle - 90;
            if (Angle == 0)
            {
                flag = true;
            }
        }
    }
}
