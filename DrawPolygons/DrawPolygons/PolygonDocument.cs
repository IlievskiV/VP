using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawPolygons
{
    [Serializable]
    public class PolygonDocument
    {
        public List<Polygon> Polygons { get; set; }

        public PolygonDocument()
        {
            Polygons = new List<Polygon>();
        }

        public void Draw(Graphics g)
        {
            foreach (Polygon polygon in Polygons)
            {
                polygon.Draw(g);
            }
        }

        public void Move(int dx, int dy)
        {
            foreach (Polygon polygon in Polygons)
            {
                polygon.Move(dx,dy);
            }
        }
    }
}
