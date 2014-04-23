using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.Player;

namespace VPSeminarska.GameLogic
{
    public class Level : Scene
    {
        public Level(Form f):base(f) {
        }

        public override void Paint(Graphics g, Form f)
        {
            base.Paint(g, f);
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
