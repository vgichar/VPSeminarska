using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic.SceneItems
{
    class LineGameObject : GameObject
    {
        private Pen _penCbT2;
        public Line2D Line { get; set; }

        public Vector2D MoveDirection { get; set; }

        public LineGameObject(Form f, Scene s, Line2D Line)
            : base(f, s)
        {
            this.Line = Line;
            this.MoveDirection = new Vector2D(0, 0);
            _penCbT2 = new Pen(Brushes.Black, 2);
        }

        public override void Paint(System.Drawing.Graphics g, Form f)
        {
            base.Paint(g, f);

            Line.Start = new PointF((float)(Line.Start.X + (MoveDirection.X * Time.deltaTime)), (float)(Line.Start.Y + (MoveDirection.Y * Time.deltaTime)));
            Line.End = new PointF((float)(Line.End.X + (MoveDirection.X * Time.deltaTime)), (float)(Line.End.Y + (MoveDirection.Y * Time.deltaTime)));

            g.DrawLine(_penCbT2, Line.Start, Line.End);
        }

        public override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        public override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public override void OnClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public override void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public override void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public override void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }
    }
}
