using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic.SceneItems.PowerUps
{
    public class TimeLapseCommand : Command
    {
        // control
        private PlayerGameObject Player;
        private bool[] keys;
        private MainWindow mw;

        // visual
        private Point center;
        private bool visible;
        private double counter;
        private double rotateSpeed = 1;
        private int size = 40;
        private int num = 8;

        // execution
        private double power = 10;
        private Action applyDelegate;
        private Action removeDelegate;
        private bool shouldDispose;

        public TimeLapseCommand(Form f, GameObject gameObject, Point center)
            : base(f, gameObject)
        {
            this.shouldDispose = false;
            this.Player = gameObject as PlayerGameObject;
            this.keys = new bool[256];
            this.center = center;
            this.counter = 0;
            this.visible = true;
            mw = f as MainWindow;
            applyDelegate = new Action(() =>
            {
                if (!visible)
                {
                    if (keys['t'] || keys['T'])
                    {
                        power -= Time.deltaTime;
                        Time.deltaTime /= 4;
                    }
                }
            });
            removeDelegate = new Action(() =>
            {
                Player.Commands.Remove(this);
                mw.beginActions.Remove(applyDelegate);
            });

            mw.beginActions.Add(applyDelegate);
        }

        public override void Paint(Graphics g, Form f)
        {
            base.Paint(g, f);

            if (this.visible)
            {
                counter += Time.deltaTime;
                if (counter > 1)
                {
                    counter = counter - 1;
                }

                for (int i = 0; i < num; i++)
                {
                    int startSweep = -(int)((i * (360 / num)) + 360 * counter * rotateSpeed);
                    double radStartSweep = (startSweep * Math.PI) / 180;
                    Vector2D startDirection = new Vector2D(Math.Cos(radStartSweep), Math.Sin(radStartSweep));
                    g.DrawArc(Pens.Black, new Rectangle(center.X + (int)(-size / 2 * startDirection.X), center.Y + (int)(-size / 2 * startDirection.Y), size, size), startSweep, 90);
                }
            }
            else
            {
                int dp = (visible) ? 0 : (int)Math.Round(power);
                g.DrawString("Slow-Mo Power: " + dp, new Font("Ariel", 18), Brushes.Black, new Point(0, 0));
            }

            this.g = g;

            this.execute();
        }

        private Graphics g;

        public override void execute()
        {
            if (visible)
            {
                PointF pl = Player.Circle.Center;
                Point pu = new Point(center.X + size / 2, center.Y + size / 2);
                if (Math.Sqrt(Math.Pow(pl.X - pu.X, 2) + Math.Pow(pl.Y - pu.Y, 2)) < Player.Circle.Radius + (size / 2))
                {
                    visible = false;
                }
            }

            if (power <= 0)
            {
                undo();
            }
        }

        public override void undo()
        {
            if (shouldDispose)
            {
                mw.endActions.Remove(removeDelegate);
            }
            else
            {
                mw.endActions.Add(removeDelegate);
                shouldDispose = true;
            }
        }

        public override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keys[e.KeyValue] = true;
        }

        public override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        public override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keys[e.KeyValue] = false;
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
