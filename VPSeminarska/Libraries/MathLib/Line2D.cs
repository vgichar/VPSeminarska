using System;
using System.Drawing;

namespace VPSeminarska.Libraries.MathLib
{
    public class Line2D
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }
        public double Length { 
            get {
                PointF normalizedEnd = new PointF(End.X - Start.X, End.Y - Start.Y);
                return Math.Sqrt(Math.Pow(normalizedEnd.X, 2) + Math.Pow(normalizedEnd.Y, 2));
            }
            set
            {
                float xAsc = (float)(Math.Cos(AngleRadian) * value);
                float yAsc = (float)(Math.Sin(AngleRadian) * value);

                End = new PointF(Start.X + xAsc, Start.X + yAsc);
            }
        }
        public double AngleDegrees
        { 
            get 
            {
                return Math.Atan2(Start.Y - End.Y, Start.X - End.X) * (180 / Math.PI);
            } 
            set 
            {
                double radi = value * Math.PI / 180;
                End = new PointF((float)(Start.X + Length * Math.Cos(radi)), (float)(Start.Y + Length * Math.Sin(radi)));
            } 
        }
        public double AngleRadian
        {
            get
            {
                return Math.Atan2(Start.Y - End.Y, Start.X - End.X);
            }
            set
            {
                double radi = value;
                End = new PointF((float)(Start.X + Length * Math.Cos(radi)), (float)(Start.Y + Length * Math.Sin(radi)));
            }
        }

        public Line2D(PointF Start, PointF End)
        {
            this.Start = Start;
            this.End = End;
            
            if (Start.X > End.X) {
                this.Start = End;
                this.End = Start;
            }
        }

        public Line2D(Point Start, Point End)
        {
            this.Start = Start;
            this.End = End;

            if (Start.X > End.X)
            {
                this.Start = End;
                this.End = Start;
            }
        }

        public void Normalize() {
            End = new PointF(End.X - Start.X, End.Y - Start.Y);
            Start = new PointF(0, 0);
        }

        public static double AngleBetweenDegrees(Line2D l1, Line2D l2) {
            return Math.Abs(l1.AngleDegrees - l2.AngleDegrees);
        }

        public static double AngleBetweenRadians(Line2D l1, Line2D l2)
        {
            return Math.Abs(l1.AngleRadian - l2.AngleRadian);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", Start, End);
        }

        public Line2D Clone() {
            return new Line2D(new PointF(Start.X, Start.Y), new PointF(End.X, End.Y)); 
        }
    }
}
