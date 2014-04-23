using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class MovementCommand : Command
    {
        private PlayerGameObject Player { get; set; }

        private bool[] keys;

        public MovementCommand(Form Form, GameObject gameObject):base(Form, gameObject)
        {
            Player = gameObject as PlayerGameObject;
            keys = new bool[255];
        }

        public override void Paint(System.Drawing.Graphics g, Form f)
        {
            base.Paint(g, f);
            this.execute();
        }

        public override void execute()
        {
            Player.MoveDirection.X = (Math.Abs(Player.MoveDirection.X) == 0 || !Player.InAir) ? 0 : Player.MoveDirection.X - Player.Speed.X / 100 * (Player.MoveDirection.X / Math.Abs(Player.MoveDirection.X));
            Player.MoveDirection.Y += PlayerGameObject.Gravity * Time.deltaTime;

            if (!Player.InAir && (keys['w'] || keys['W'] || keys[32]))
            {
                Player.MoveDirection.Y -= Player.Speed.Y;
                Player.InAir = true;
            }

            if (keys['a'] || keys['A'])
            {
                Player.MoveDirection.X = -Player.Speed.X;
            }

            if (keys['d'] || keys['D'])
            {
                Player.MoveDirection.X = Player.Speed.X;
            }

            if (keys['s'] || keys['S'])
            {
                Player.Circle.Center = new System.Drawing.PointF(Player.Circle.Center.X, Player.Circle.Center.Y + 1);
            }
        }

        public override void undo()
        {
            Player.MoveDirection = new Vector2D(0, 0);
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
