using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;

namespace VPSeminarska.GameLogic.Player
{
    public class PlayerGameObject : GameObject
    {
        public Point Position { get; set; }
        public Size Size { get; set; }

        public double Speed;
        public Vector2D MoveDirection;

        public PlayerGameObject(Form f)
            : base(f)
        {
            Position = new Point(100, 100);
            Size = new Size(30, 30);
            Speed = 20;
            MoveDirection = new Vector2D(0, 0);
        }

        public override void Paint(Graphics g, System.Windows.Forms.Form f)
        {
            base.Paint(g, f);

            Position = (MoveDirection * Speed + Position).ToPoint();
            Point PositionCenter = Position;
            PositionCenter.X -= Size.Width;
            PositionCenter.Y -= Size.Height;

            g.FillEllipse(Brushes.Purple, new Rectangle(PositionCenter, Size));
        }

        public override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 'W' || e.KeyValue == 'w')
            {
                MoveDirection.j = -1;
            }

            if (e.KeyValue == 'S' || e.KeyValue == 's')
            {
                MoveDirection.j = 1;
            }

            if (e.KeyValue == 'A' || e.KeyValue == 'a')
            {
                MoveDirection.i = -1;
            }

            if (e.KeyValue == 'D' || e.KeyValue == 'd')
            {
                MoveDirection.i = 1;
            }
        }

        public override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        public override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 'W' || e.KeyValue == 'w' || e.KeyValue == 'S' || e.KeyValue == 's')
            {
                MoveDirection.j = 0;
            }

            if (e.KeyValue == 'A' || e.KeyValue == 'a' || e.KeyValue == 'D' || e.KeyValue == 'd')
            {
                MoveDirection.i = 0;
            }
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
