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
    public class MovementAICommand : Command
    {
        private PlayerGameObject Player { get; set; }

        private bool[] keys;

        public MovementAICommand(Form Form, GameObject gameObject)
            : base(Form, gameObject)
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

            if (!Player.InAir)
            {
                Player.MoveDirection.Y -= Player.Speed.Y;
                Player.InAir = true;
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
