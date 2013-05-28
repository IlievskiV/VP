using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawPolygons
{
    [Serializable]
    public class LineDocument
    {
        public List<Line> Lines { get; set; }

        public LineDocument()
        {
            Lines = new List<Line>();
        }

        public void Draw(Graphics g)
        {
            foreach (Line line in Lines)
            {
                line.Draw(g);
            }
        }
    }
}
