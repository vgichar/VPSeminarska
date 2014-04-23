using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.GameLogic.Player.Commands;
using VPSeminarska.GameLogic.SceneItems;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic
{
    class MenuScreen : Scene
    {
        private int W;
        private int H;
        private MainWindow mw;

        private Point MousePosition;
        private bool IsMousePressed;

        private Font _font;

        private Rectangle r1;
        private Rectangle r2;
        private Rectangle r3;

        private Point p1;
        private Point p2;
        private Point p3;

        public MenuScreen(Form f)
            : base(f)
        {
            W = Screen.PrimaryScreen.WorkingArea.Width;
            H = Screen.PrimaryScreen.WorkingArea.Height;
            MousePosition = new Point(0, 0);
            IsMousePressed = false;
            mw = f as MainWindow;
            _font = new Font("Arial", 26);
            r1 = new Rectangle(new Point(W / 2 - 45 - 10, 400 - 10), new Size(2 * (45 + 10), 55));
            r2 = new Rectangle(new Point(W / 2 - 53 - 10, 470 - 10), new Size(2 * (53 + 10), 55));
            r3 = new Rectangle(new Point(W / 2 - 37 - 10, 540 - 10), new Size(2 * (37 + 10), 55));
            p1 = new Point(W / 2 - 45, 400);
            p2 = new Point(W / 2 - 53, 470);
            p3 = new Point(W / 2 - 37, 540);

            PlayerGameObject Player = new PlayerGameObject(f, this);

            Player.Circle.Center = new PointF(W / 2, 0);
            Player.Commands.Add(new MovementAICommand(f, Player));
            Player.Commands.Add(new ColisionDetectionCommand(f, Player));
            this.GameObjects.Add(Player);

            LineGameObject l1 = new LineGameObject(f, this, new Line2D(new PointF(0, 260), new PointF(W, 260)));
            this.GameObjects.Add(l1);
        }

        public override void Paint(Graphics g, Form f)
        {
            base.Paint(g, f);

            g.DrawString("Start", _font, Brushes.Black, p1);
            g.DrawString("Editor", _font, Brushes.Black, p2);
            g.DrawString("Exit", _font, Brushes.Black, p3);

            if (IsPointInRectangle(r1, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r1);
                if (IsMousePressed)
                {
                    mw.CurrentScene = mw.Scenes.ElementAt(1);
                }
            }
            if (IsPointInRectangle(r2, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r2);
                if (IsMousePressed)
                {

                }
            }
            if (IsPointInRectangle(r3, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r3);
                if (IsMousePressed)
                {
                    Application.Exit();
                }
            }

            IsMousePressed = false;
        }

        private bool IsPointInRectangle(Rectangle r, Point p)
        {
            if (p.X > r.X && p.X < r.Width + r.X && p.Y > r.Y && p.Y < r.Y + r.Height)
            {
                return true;
            }
            return false;
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
            IsMousePressed = true;
        }

        public override void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public override void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        }

        public override void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MousePosition = e.Location;
        }
    }
}
