using System.Drawing;
using System.Linq;
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
        // auxiliary variables
        private int W;
        private int H;

        private Point MousePosition;
        private bool IsMousePressed;

        // disposable static auxiliary variables
        private Font _font;

        private Rectangle r1;
        private Rectangle r2;
        private Rectangle r3;

        private Point p1;
        private Point p2;
        private Point p3;

        public MenuScreen()
            : base()
        {
            W = Screen.PrimaryScreen.WorkingArea.Width;
            H = Screen.PrimaryScreen.WorkingArea.Height;

            MousePosition = new Point(0, 0);
            IsMousePressed = false;

            _font = new Font("Arial", 26);

            // on hover rectangles for each menu item
            r1 = new Rectangle(new Point(W / 2 - 45 - 10, 400 - 10), new Size(2 * (45 + 10), 55));
            r2 = new Rectangle(new Point(W / 2 - 103 - 10, 470 - 10), new Size(2 * (103 + 10), 55));
            r3 = new Rectangle(new Point(W / 2 - 37 - 10, 540 - 10), new Size(2 * (37 + 10), 55));

            // point of drawing text string for each menu item
            p1 = new Point(W / 2 - 45, 400);
            p2 = new Point(W / 2 - 103, 470);
            p3 = new Point(W / 2 - 37, 540);

            BindEvents();
        }

        public override void init()
        {

            Player = new PlayerGameObject();

            Player.Circle.Center = new PointF(W / 2, 0);
            Player.Commands.Add(new MovementAICommand()); // @Player jump by himself - no user input
            Player.Commands.Add(new CollisionDetectionCommand());

            // draw one horizontal @Line throughout the entire screen for the @Player to jump on
            LineGameObject l1 = new LineGameObject(new Line2D(new PointF(0, 260), new PointF(W, 260)));
            this.GameObjects.Add(l1);

            base.init();
        }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            // draw every menu item
            g.DrawString("Start", _font, Brushes.Black, p1);
            g.DrawString("High Scores", _font, Brushes.Black, p2);
            g.DrawString("Exit", _font, Brushes.Black, p3);

            // on hover draw rectangle for each menu item that the user hovers on
            // if the mouse is pressed while the user hovers
            // the corresponding business logic will be executed
            if (IsPointInRectangle(r1, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r1);
                if (IsMousePressed)
                {
                    MainWindow.CurrentScene = MainWindow.Scenes[1];
                }
            }
            else if (IsPointInRectangle(r2, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r2);
                if (IsMousePressed)
                {
                    MainWindow.CurrentScene = MainWindow.Scenes[2];
                }
            }
            else if (IsPointInRectangle(r3, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r3);
                if (IsMousePressed)
                {
                    Application.Exit();
                }
            }

            IsMousePressed = false;
        }

        // calculate if a point is inside a rectangle
        // used to calculate is the mouse is in the on-hover rectangle for each menu item
        private bool IsPointInRectangle(Rectangle r, Point p)
        {
            if (p.X > r.X && p.X < r.Width + r.X && p.Y > r.Y && p.Y < r.Y + r.Height)
            {
                return true;
            }
            return false;
        }

        // event handlers
        public override void OnClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            IsMousePressed = true;
        }

        public override void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MousePosition = e.Location;
        }
    }
}
