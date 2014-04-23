using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.GameLogic.Player.Commands;
using VPSeminarska.GameLogic.SceneItems;
using VPSeminarska.GameLogic.SceneItems.PowerUps;
using VPSeminarska.GameLogic.SceneItems.SceneItemsCommands;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska
{
    public partial class MainWindow : Form
    {
        public List<Scene> Scenes { get; set; }
        public Scene CurrentScene { get; set; }
        public bool[] keys;
        public PlayerGameObject Player { get; set; }

        public List<Action> beginActions;
        public List<Action> endActions;
 
        public MainWindow()
        {
            InitializeComponent();

            beginActions = new List<Action>();
            endActions = new List<Action>();
            Scenes = new List<Scene>();
            keys = new bool[255];

            MenuScreen sc1 = new MenuScreen(this);
            Scenes.Add(sc1);

            CurrentScene = sc1;

            init();

            BindEvents();

            this.DoubleBuffered = true;
            this.Invalidate();
        }

        public void init(){
            Level sc1 = new Level(this);

            Player = new PlayerGameObject(this, sc1);
            Player.Commands.Add(new TimeLapseCommand(this, Player, new Point(200, 200)));
            Player.Commands.Add(new MovementCommand(this, Player));
            Player.Commands.Add(new ColisionDetectionCommand(this, Player));

            Random rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                LineGameObject Line = new LineGameObject(this, sc1, new Line2D(new PointF(150 + ((rand.Next(2) - 1) * 120), 300 - (i * 50)), new PointF(300 + ((rand.Next(2) - 1) * 120), 300 - (i * 50))));
                Line.Commands.Add(new AILineMoveDownCommand(this, Line));
                sc1.GameObjects.Add(Line);
            }

            sc1.GameObjects.Add(Player);

            Scenes.Add(sc1);
        }

        public void BindEvents()
        {
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.KeyPress += OnKeyPress;
            this.MouseClick += OnClick;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
            this.MouseMove += OnMouseMove;
        }

        public void MoveKeys()
        {
            if (keys[27])
            {
                Application.Exit();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Time.startTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            foreach (Action ac in beginActions)
            {
                ac.Invoke();
            }

            MoveKeys();
            CurrentScene.Paint(g, this);

            System.Threading.Thread.Sleep(10);

            Time.endTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            Time.deltaTime = Time.endTime - Time.startTime;

            foreach (Action ac in endActions) 
            {
                ac.Invoke();
            }

            System.GC.Collect();
            this.Invalidate();
        }


        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            keys[e.KeyValue] = true;
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            keys[e.KeyValue] = false;
        }

        public void OnClick(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
