using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic.SceneItems.SceneItemsCommands
{
    class AILineMoveDownCommand : Command
    {
        private MainWindow mw;
        private bool[] keys;
        private LineGameObject Line;

        private double speed = 50;

        public AILineMoveDownCommand(Form Form, GameObject gameObject)
            : base(Form, gameObject)
        {
            Line = gameObject as LineGameObject;
            keys = new bool[255];
            mw = this.Form as MainWindow;
        }

        public override void Paint(System.Drawing.Graphics g, Form f)
        {
            base.Paint(g, f);
            this.execute();
        }

        public override void execute()
        {
            double playerPosY = mw.Player.Circle.Center.Y;
            double addSpeed = 0;
            if (playerPosY < 100) {
                addSpeed = 100 - playerPosY;
            }
            Line.MoveDirection = new Vector2D(0, speed + addSpeed);
        }

        public override void undo()
        {
            Line.MoveDirection = new Vector2D(0, 0);
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
