using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EatingPies
{
    [Serializable]
    public class PieDocument
    {
        public List<Pie> Pies { get; set; }

        public PieDocument()
        {
            Pies = new List<Pie>();
        }

        public void Draw(Graphics g)
        {
            foreach (Pie pie in Pies)
            {
                pie.Draw(g);
            }
        }

        public void ChangePiesAngle()
        {
            foreach (Pie pie in Pies)
            {
                pie.changeAngle();
            }
        }
    }
}
