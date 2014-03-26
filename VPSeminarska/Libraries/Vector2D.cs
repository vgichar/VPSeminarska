using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPSeminarska.Libraries
{
    public class Vector2D
    {
        public double i { get; set; }
        public double j { get; set; }

        public Vector2D(double i, double j) {
            this.i = i;
            this.j = j;
        }

        public double magnitute() {
            return Math.Sqrt((i * i) + (j * j));
        }

        public void normalize() {
            double magnitute = this.magnitute();
            
            if (magnitute <= 0) 
                return;
            
            i /= magnitute;
            j /= magnitute;
        }

        public static double angleBetween(Vector2D v1, Vector2D v2)
        {
            return Math.Acos((v1 * v2)/(v1.magnitute()*v2.magnitute()));
        }

        public static double operator *(Vector2D v1, Vector2D v2)
        {
            return (v1.i * v2.i) + (v1.j * v2.j);
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.i + v2.i, v1.j + v2.j);
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.i - v2.i, v1.j - v2.j);
        }

        public static Vector2D operator *(Vector2D v1, Point v2)
        {
            return new Vector2D(v1.i + v2.X, v1.j + v2.Y);
        }

        public static Vector2D operator +(Vector2D v1, Point v2)
        {
            return new Vector2D(v1.i + v2.X, v1.j + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Point v2)
        {
            return new Vector2D(v1.i - v2.X, v1.j - v2.Y);
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            return v1.magnitute() == v2.magnitute();
        }

        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            return v1.magnitute() == v2.magnitute();
        }

        public static bool operator <=(Vector2D v1, Vector2D v2)
        {
            return v1.magnitute() <= v2.magnitute();
        }

        public static bool operator >=(Vector2D v1, Vector2D v2)
        {
            return v1.magnitute() >= v2.magnitute();
        }

        public static bool operator <(Vector2D v1, Vector2D v2)
        {
            return v1.magnitute() < v2.magnitute();
        }

        public static bool operator >(Vector2D v1, Vector2D v2)
        {
            return v1.magnitute() > v2.magnitute();
        }

        public static Vector2D operator *(Vector2D v1, double v)
        {
            return new Vector2D((v1.i * v), (v1.j * v));
        }

        public static Vector2D operator +(Vector2D v1, double v)
        {
            return new Vector2D(v1.i + v, v1.j + v);
        }

        public static Vector2D operator -(Vector2D v1, double v)
        {
            return new Vector2D(v1.i - v, v1.j - v);
        }

        public override bool Equals(object obj)
        {
            Vector2D v2 = obj as Vector2D;
            return (this.i == v2.i && this.j == v2.j);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * (int)(magnitute() * 1000);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", i, j);
        }

        public Point ToPoint() {
            return new Point((int)i, (int)j);
        }
    }
}
