using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.Data;
using VPSeminarska.GameLogic.Data.Entities;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.GameLogic.Player.Commands;
using VPSeminarska.GameLogic.SceneItems;
using VPSeminarska.GameLogic.SceneItems.SceneItemsCommands;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic
{
    public class LevelScene : Scene
    {
        // information holders and constraints
        private bool StartState;
        private double Score;

        private int MinLineSize = 150;
        private int MaxLineSize = 350;

        // auxiliary variables
        private int w;
        private int h;

        private bool[] keys;
        private bool kEnter;
        private bool IsCursorVisible;

        private double Height;

        private Random rand;
        private TextBox tb;

        public LevelScene()
            : base()
        {
            w = Screen.PrimaryScreen.Bounds.Width;
            h = Screen.PrimaryScreen.Bounds.Height;

            rand = new Random();
            keys = new bool[256];
            kEnter = false;
            IsCursorVisible = true;

            BindEvents();
        }

        // initialize variables and generate lines
        public override void init()
        {
            if (IsCursorVisible)
            {
                Cursor.Hide();
                IsCursorVisible = false;
            }

            StartState = false;
            Score = 0;

            tb = new TextBox();
            tb.Size = new Size(450, 1);
            tb.Font = new Font("Arial", 36);
            tb.Location = new Point(w / 2 - tb.Size.Width / 2, 200);
            tb.BackColor = MainWindow.BackColor;
            tb.BorderStyle = BorderStyle.None;
            tb.MaxLength = 18;
            tb.TextAlign = HorizontalAlignment.Center;
            tb.KeyDown += OnKeyDown;

            Player = new PlayerGameObject();
            Player.Commands.Add(new MovementCommand());
            Player.Commands.Add(new CollisionDetectionCommand());
            Player.Commands.Add(new TimeLapseCommand());
            Player.Commands.Add(new HyperJumpCommand());

            Player.Circle.Center = new PointF(w / 2, h - 100);

            GameObjects.Add(new LineGameObject(new Line2D(new Point(100, 0), new Point(100, h))));
            GameObjects.Add(new LineGameObject(new Line2D(new Point(w - 100, 0), new Point(w - 100, h))));

            GameObjects.Add(new LineGameObject(new Line2D(new Point(100, h - 50), new Point(w - 100, h - 50))));

            Height = h - 50;

            generateLines(50, false); // generate 50 static lines

            base.init();
        }

        // destroy TextBox, Show cursor
        public override void destroy()
        {
            MainWindow.Controls.Remove(tb);
            tb.Dispose();
            if (!IsCursorVisible)
            {
                IsCursorVisible = true;
                Cursor.Show();
            }
            base.destroy();
        }

        // generate lines
        private void generateLines(int numLonesToGenerate, bool move)
        {
            for (int i = 0; i < numLonesToGenerate; i++)
            {
                // assume that the line to be generated is not overlapping existing line
                bool acceptable = true;

                // generate string and end point x-axis coordinates
                int start = rand.Next(w - 200 - MinLineSize) + 100;
                int end = start + MinLineSize + rand.Next(MaxLineSize - MinLineSize);
                end = Math.Min(end, w - 100);

                // each 3rd line should be on the same level as the previous line in average
                if (rand.Next(3) != 0)
                {
                    Height -= 100;
                }
                else
                {
                    // check if the generated line overlaps some previous generated line
                    foreach (GameObject go in GameObjects)
                    {
                        LineGameObject lgo1 = go as LineGameObject;
                        if (Math.Abs(lgo1.Line.Start.Y - Height) < 10
                            && (
                                (lgo1.Line.Start.X < start && lgo1.Line.End.X > start)
                                || (lgo1.Line.Start.X < end && lgo1.Line.End.X > end)
                                || (lgo1.Line.Start.X < start && lgo1.Line.End.X > end)
                                || (lgo1.Line.Start.X > start && lgo1.Line.End.X < end)
                                )
                            )
                        {
                            i--;
                            acceptable = false;
                            break;
                        }
                    }
                    if (!acceptable)
                    {
                        continue;
                    }
                }

                // if the new line passes all tests - it gets to be generated
                LineGameObject lgo = new LineGameObject(new Line2D(new Point(start, (int)Height), new Point(end, (int)Height)));
                if (move) // if the line should be moving downwards - add @Command accordingly
                {
                    lgo.Commands.Add(new AILineMoveDownCommand());
                }
                GameObjects.Add(lgo);
            }
        }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            // refresh all WPF controls
            foreach (Control c in MainWindow.Controls)
            {
                c.Refresh();
            }

            // display score
            g.DrawString("Score: " + (int)Score, new Font("Arial", 20), Brushes.Black, new PointF(w - 280, 20));

            // if @Player lost
            if (Player.Circle.Center.Y > h + 100)
            {
                // lock @PLayer position - no need to fall forever (might overflow if it does)
                Player.Circle.Center.Y = h + 200;
                
                // display cursor and TextBox to enter User name
                if (!IsCursorVisible)
                {
                    IsCursorVisible = true;
                    Cursor.Show();
                    MainWindow.Controls.Add(tb);
                    tb.Focus();
                }

                // Graphical makeup
                new LineGameObject(new Line2D(new Point(w / 2 - tb.Size.Width / 2, 256), new Point(w / 2 + tb.Size.Width / 2, 256))).Paint(g);
                g.DrawString("Your name:", new Font("Arial", 24), Brushes.Black, new Point(w / 2 - 90, 150));

                // if User hits the @Escape button - don't save his score and reload @LevelScene
                if (keys[27])
                {
                    MainWindow.CurrentScene = MainWindow.Scenes[1];
                }

                // if User hits the @Enter button - save his score and reload @LevelScene
                if (kEnter)
                {
                    ScoreSerializer.HighScores.Add(new Score(tb.Text, (int)Score));
                    MainWindow.CurrentScene = MainWindow.Scenes[1];
                }
                return; // don't render anything else if the @Player lost
            }

            // if player is not on initial bottom line, start moving lines downwards
            if (!StartState && (Player.Circle.Center.Y < h - 200 || (!Player.InAir && Player.Circle.Center.Y < h - 100)))
            {
                StartState = true; // start the game

                // add movement @Command to existing lines
                foreach (GameObject go in GameObjects)
                {
                    LineGameObject lgo = go as LineGameObject;
                    if(lgo != null && lgo.Line.AngleDegrees % 180 == 0)
                    {
                        go.Commands.Add(new AILineMoveDownCommand());
                    }
                }

                //Player.Commands.Add(new AILineMoveDownCommand());
            }

            // increase difficulty based on score
            AILineMoveDownCommand.SpeedUpTimes = Math.Min((int)(Score / 100), 10);

            // if the game is started
            if (StartState)
            {
                // score is based on time passed, not on height climbed
                Score += Time.deltaTime * 10;

                // update @Height for generating new lines (y-axis coordinate)
                Height += AILineMoveDownCommand.RealSpeed * Time.deltaTime;

                // generate new lines if the last line is almost visible on the screen
                LineGameObject lgo = GameObjects.ElementAt(GameObjects.Count - 1) as LineGameObject;
                if (lgo.Line.Start.Y > -100)
                {
                    generateLines(10, true);
                }

                // destroy passed objects to free up memory space
                while (true && GameObjects.Count > 2)
                {
                    bool Continue = false;

                    /* LineGameObject */
                    lgo = GameObjects.ElementAt(2) as LineGameObject;
                    if (lgo != null && lgo.Line.Start.Y > h + 30)
                    {
                        Continue = true;
                    }

                    if (!Continue) 
                    { 
                        break;
                    }
                    else
                    {
                        GameObjects.RemoveAt(2);
                    }
                }
            }
        }


        // event handlers
        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            // suppress beep sound when @Enter or @Escape key is pressed while the TextBox is focused
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Escape))
            {
                e.Handled = e.SuppressKeyPress = true;
            }

            keys[e.KeyValue] = true;
            if (e.KeyCode == Keys.Enter)
            {
                kEnter = true;
            }
        }

        public override void OnKeyUp(object sender, KeyEventArgs e)
        {
            keys[e.KeyValue] = false;
            if (e.KeyCode == Keys.Enter)
            {
                kEnter = false;
            }
        }
    }
}
