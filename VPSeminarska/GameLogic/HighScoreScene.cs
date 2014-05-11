using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.Data;
using VPSeminarska.GameLogic.Data.Entities;
using VPSeminarska.GameLogic.Player;

namespace VPSeminarska.GameLogic
{
    public class HighScoreScene : Scene
    {
        // auxiliary variables
        private List<Score> HighScores;

        private int W;
        private int H;

        private Point MousePosition;
        private bool IsMousePressed;

        // disposable static auxiliary variables
        private Font _font;
        private Rectangle r1;
        private Point p1;
        private Point p2;

        public HighScoreScene()
            : base()
        {
            W = Screen.PrimaryScreen.Bounds.Width;
            H = Screen.PrimaryScreen.Bounds.Height;
            MousePosition = new Point(0, 0);
            IsMousePressed = false;
            _font = new Font("Arial", 24);
            
            // point of drawing text and on-hover rectangle for the back button
            p1 = new Point(W - 100, H - 70);
            r1 = new Rectangle(new Point(p1.X - 10, p1.Y - 10), new Size(2 * (40 + 10), 55));

            // point of drawing text for the "High Scores" title text
            p2 = new Point(W / 2 - 80, 70);

            BindEvents();
        }

        public override void init()
        {
            // get serialized @HighScores
            HighScores = ScoreSerializer.HighScores;

            // no need of @Player
            Player = new PlayerGameObject();
            Player.Circle.Center = new PointF(-200, -200);

            base.init();
        }

        public override void Paint(Graphics g)
        {
            base.Paint(g);


            // draw "High Scores" title
            g.DrawString("High Scores", _font, Brushes.Black, p2);

            // draw each @HighScore
            int i = 0;
            foreach (Score sc in HighScores)
            {
                g.DrawString(string.Format("{0}.", i + 1), _font, Brushes.Black, new Point(W / 2 - 250, 150 + i * 50));
                g.DrawString(sc.Name, _font, Brushes.Black, new Point(W / 2 - 200, 150 + i * 50));
                g.DrawString(sc.Points.ToString(), _font, Brushes.Black, new Point(W / 2 + 200, 150 + i * 50));
                i++;
            }

            // draw @Back button
            g.DrawString("Back", _font, Brushes.Black, p1);

            // on hover and on click handler for the back cutton
            if (IsPointInRectangle(r1, MousePosition))
            {
                g.DrawRectangle(Pens.Black, r1);
                if (IsMousePressed)
                {
                    MainWindow.CurrentScene = MainWindow.Scenes[0];
                }
            }

            IsMousePressed = false;
        }

        // calculate if a point is inside a rectangle
        // used to calculate is the mouse is in the on-hover rectangle for the back button
        private bool IsPointInRectangle(Rectangle r, Point p)
        {
            if (p.X > r.X && p.X < r.Width + r.X && p.Y > r.Y && p.Y < r.Y + r.Height)
            {
                return true;
            }
            return false;
        }

        // event handlers
        public override void OnClick(object sender, MouseEventArgs e)
        {
            IsMousePressed = true;
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            MousePosition = e.Location;
        }
    }
}
