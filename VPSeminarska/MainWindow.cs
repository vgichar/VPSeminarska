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
using VPSeminarska.Libraries;

namespace VPSeminarska
{
    public partial class MainWindow : Form
    {
        public List<Scene> Scenes { get; set; }
        public Scene CurrentScene { get; set; }
 
        public MainWindow()
        {
            Scenes = new List<Scene>();

            InitializeComponent();

            Scene1 sc1 = new Scene1(this);
            sc1.GameObjects.Add(new PlayerGameObject(this));
            Scenes.Add(sc1);

            CurrentScene = sc1;

            BindEvents();

            this.Invalidate();
            this.DoubleBuffered = true;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Time.startTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
            CurrentScene.Paint(g, this);

            Time.endTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            System.Threading.Thread.Sleep((int)(16 - ((Time.endTime - Time.startTime) * 1000)));
            Time.deltaTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds - Time.startTime;

            this.Invalidate();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
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
