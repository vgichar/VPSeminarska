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
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska
{
    public partial class MainWindow : Form
    {
        public List<Scene> Scenes { get; set; }
        public Scene CurrentScene { get; set; }
        public bool[] keys;
 
        public MainWindow()
        {
            Scenes = new List<Scene>();
            keys = new bool[255];

            InitializeComponent();

            Scene1 sc1 = new Scene1(this);

            PlayerGameObject Player = new PlayerGameObject(this, sc1);
            Player.Commands.Add(new MovementCommand(this, Player));
            Player.Commands.Add(new ColisionDetectionCommand(this, Player));

            sc1.GameObjects.Add(Player);
            sc1.GameObjects.Add(new LineGameObject(this, sc1, new Line2D(new PointF(100, 300), new PointF(300, 300))));
            
            Scenes.Add(sc1);
            CurrentScene = sc1;

            BindEvents();

            this.DoubleBuffered = true;
            this.Invalidate();
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

        public void MoveKeys() {
            if (keys['t'] || keys['T']) {
                Time.deltaTime /= 10;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Time.startTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            MoveKeys();
            CurrentScene.Paint(g, this);

            System.Threading.Thread.Sleep(5);

            Time.endTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            Time.deltaTime = Time.endTime - Time.startTime;

            this.Invalidate();
        }


        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            keys[e.KeyValue] = true;

            if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
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
