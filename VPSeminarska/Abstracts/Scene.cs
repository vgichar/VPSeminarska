using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPSeminarska.Abstracts
{
    public abstract class Scene : ICListener
    {
        public Graphics Graphics { get; set; }
        public Form Form { get; set; }
        public List<GameObject> GameObjects { get; set; }

        public Scene(Form Form) {
            this.Form = Form;
            GameObjects = new List<GameObject>();
            BindEvents();
        }

        public void Paint(Graphics g, Form f)
        {
            Graphics = g;
            Form = f;
            foreach(GameObject go in GameObjects){
                go.Paint(g, f);
            }
        }

        public void BindEvents()
        {
            Form.KeyDown += OnKeyDown;
            Form.KeyUp += OnKeyUp;
            Form.KeyPress += OnKeyPress;
            Form.MouseClick += OnClick;
            Form.MouseDown += OnMouseDown;
            Form.MouseUp += OnMouseUp;
            Form.MouseMove += OnMouseMove;
        }

        public abstract void OnKeyDown(object sender, KeyEventArgs e);
        public abstract void OnKeyPress(object sender, KeyPressEventArgs e);
        public abstract void OnKeyUp(object sender, KeyEventArgs e);
        public abstract void OnClick(object sender, MouseEventArgs e);
        public abstract void OnMouseDown(object sender, MouseEventArgs e);
        public abstract void OnMouseUp(object sender, MouseEventArgs e);
        public abstract void OnMouseMove(object sender, MouseEventArgs e);
    }
}
