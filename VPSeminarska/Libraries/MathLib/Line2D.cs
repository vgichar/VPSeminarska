using System;
using System.Drawing;

// basic 2D line class

namespace VPSeminarska.Libraries.MathLib
{
    public class Line2D
    {
        public PointF Start;
        public PointF End;

        // length of the line
        public double Length
        {
            get
            {
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

        // angle formed between the line and the x axis in degrees
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

        // angle formed between the line and the x axis in radians
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

            // force points to be ordered left to right
            if (Start.X > End.X)
            {
                this.Start = End;
                this.End = Start;
            }
        }

        public Line2D(Point Start, Point End)
        {
            this.Start = Start;
            this.End = End;

            // force points to be ordered left to right
            if (Start.X > End.X)
            {
                this.Start = End;
                this.End = Start;
            }
        }

        // translate line so the starting point to match @Point(0, 0);
        public void Normalize()
        {
            End = new PointF(End.X - Start.X, End.Y - Start.Y);
            Start = new PointF(0, 0);
        }

        // function title says all
        public static double AngleBetweenLines(Line2D l1, Line2D l2)
        {
            return Math.Abs(180 - l1.AngleDegrees - l2.AngleDegrees);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", Start, End);
        }

        public Line2D Clone()
        {
            return new Line2D(new PointF(Start.X, Start.Y), new PointF(End.X, End.Y));
        }
    }
}
