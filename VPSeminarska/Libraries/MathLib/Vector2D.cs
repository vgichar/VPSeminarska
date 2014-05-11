using System;
using System.Drawing;

// basic 2D vector class

namespace VPSeminarska.Libraries.MathLib
{
    public class Vector2D
    {
        public double X;
        public double Y;

        public Vector2D(double i, double j)
        {
            this.X = i;
            this.Y = j;
        }

        public double magnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y));
        }

        // scale the vector to have magnitude 1
        public void normalize()
        {
            double magnitute = this.magnitude();

            if (magnitute <= 0)
                return;

            X /= magnitute;
            Y /= magnitute;
        }

        public static double angleBetweenVectors(Vector2D v1, Vector2D v2)
        {
            return Math.Acos((v1 * v2) / (v1.magnitude() * v2.magnitude()));
        }

        public static double operator *(Vector2D v1, Vector2D v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator *(Vector2D v1, Point v2)
        {
            return new Vector2D(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2D operator +(Vector2D v1, Point v2)
        {
            return new Vector2D(v1.X + v2.Y, v1.X + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Point v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator *(Vector2D v1, PointF v2)
        {
            return new Vector2D(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2D operator +(Vector2D v1, PointF v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, PointF v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            return v1.magnitude() == v2.magnitude();
        }

        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            return v1.magnitude() == v2.magnitude();
        }

        public static bool operator <=(Vector2D v1, Vector2D v2)
        {
            return v1.magnitude() <= v2.magnitude();
        }

        public static bool operator >=(Vector2D v1, Vector2D v2)
        {
            return v1.magnitude() >= v2.magnitude();
        }

        public static bool operator <(Vector2D v1, Vector2D v2)
        {
            return v1.magnitude() < v2.magnitude();
        }

        public static bool operator >(Vector2D v1, Vector2D v2)
        {
            return v1.magnitude() > v2.magnitude();
        }

        public static Vector2D operator *(Vector2D v1, double v)
        {
            return new Vector2D((v1.X * v), (v1.Y * v));
        }

        public static Vector2D operator +(Vector2D v1, double v)
        {
            return new Vector2D(v1.X + v, v1.Y + v);
        }

        public static Vector2D operator -(Vector2D v1, double v)
        {
            return new Vector2D(v1.X - v, v1.Y - v);
        }

        public override bool Equals(object obj)
        {
            Vector2D v2 = obj as Vector2D;
            return (this.X == v2.X && this.Y == v2.Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * (int)(magnitude() * 1000);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        // return vector as Point
        public Point GetPoint()
        {
            return new Point((int)X, (int)Y);
        }

        // return vector as PointF
        public PointF GetPointF()
        {
            return new PointF((float)X, (float)Y);
        }

        public Vector2D Clone()
        {
            return new Vector2D(X, Y);
        }
    }
}