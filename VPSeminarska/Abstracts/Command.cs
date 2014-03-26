using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPSeminarska.Abstracts
{
    public abstract class Command
    {
        public Graphics Graphics { get; set; }
        public Form Form { get; set; }
        public GameObject GameObject { get; set; }

        public Command(Form Form, GameObject gameObject) {
            this.Form = Form;
            GameObject = gameObject;
            BindEvents();
        }

        public abstract void execute();

        public abstract void undo();

        public virtual void Paint(Graphics g, Form f) {
            Graphics = g;
            Form = f;
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
