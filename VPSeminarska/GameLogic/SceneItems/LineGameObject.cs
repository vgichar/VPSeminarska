using System.Drawing;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

// a class used to store information and render the game object that represents the floor that the Player jumps from

namespace VPSeminarska.GameLogic.SceneItems
{
    class LineGameObject : GameObject
    {
        // auxiliary constant variables
        private Pen _pen;

        // rendering info
        public Line2D Line;

        // movement info
        public Vector2D MoveDirection;

        // initialize variables
        public LineGameObject(Line2D Line)
            : base()
        {
            this.Line = Line;
            this.MoveDirection = new Vector2D(0, 0);
            _pen = new Pen(Brushes.Black, 2);
        }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            // store the updated position based on movement
            Line.Start = new PointF((float)(Line.Start.X + (MoveDirection.X * Time.deltaTime)), (float)(Line.Start.Y + (MoveDirection.Y * Time.deltaTime)));
            Line.End = new PointF((float)(Line.End.X + (MoveDirection.X * Time.deltaTime)), (float)(Line.End.Y + (MoveDirection.Y * Time.deltaTime)));

            // render the line
            g.DrawLine(_pen, Line.Start, Line.End);
        }
    }
}
