using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Abstracts.Interfaces;

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class ColisionDetectionCommand : Command
    {
        private PlayerGameObject Player { get; set; }
        private Scene Scene { get; set; }

        public ColisionDetectionCommand(Form f, GameObject gameObject) : base(f, gameObject) 
        {
            Player = gameObject as PlayerGameObject;
            Scene = Player.Scene as Scene;
        }

        public override void Paint(System.Drawing.Graphics g, System.Windows.Forms.Form f)
        {
            base.Paint(g, f);

            if (Player.Circle.Center.Y >= 350)
            {
                Player.InAir = false;
            }
        }

        public override void execute()
        {
        }

        public override void undo()
        {
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
