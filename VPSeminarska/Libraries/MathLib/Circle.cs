using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPSeminarska.Libraries.MathLib
{
    public class Circle
    {
        public PointF Center { get; set; }
        public double Radius { get; set; }

        public Circle(PointF Center, double Radius) {
            this.Center = Center;
            this.Radius = Radius;
        }

        public Circle Clone() {
            return new Circle(new PointF(Center.X, Center.Y), Radius);
        }
    }
}
