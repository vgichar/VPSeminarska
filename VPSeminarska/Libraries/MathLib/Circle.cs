using System.Drawing;

// basic circle class

namespace VPSeminarska.Libraries.MathLib
{
    public class Circle
    {
        public PointF Center;
        public double Radius;

        public Circle(PointF Center, double Radius)
        {
            this.Center = Center;
            this.Radius = Radius;
        }

        public Circle Clone()
        {
            return new Circle(new PointF(Center.X, Center.Y), Radius);
        }
    }
}
