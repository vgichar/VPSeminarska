using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;
using VPSeminarska.Properties;

// this @Command object enables its parent @GameObject object to move according to user input

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class MovementCommand : Command
    {
        // auxiliary variables
        // store info for pressed keys;
        private bool[] keys;
        private bool kDown;
        private bool kUp;
        private bool kLeft;
        private bool kRight;

        public MovementCommand()
            : base()
        {
            keys = new bool[255];
            BindEvents();
        }

        // call execution
        public override void Paint(Graphics g)
        {
            base.Paint(g);
            this.execute();
        }

        // execute business logic
        public override void execute()
        {
            // if the @Player is in the air - he moves along the X-axis by inertia of the jump
            // the power of moving along the X-axis in mid air is reduced by the amount @Player.Speed.X/100 per frame
            Player.MoveDirection.X = (Math.Abs(Player.MoveDirection.X) == 0 || !Player.InAir) ? 0 : Player.MoveDirection.X - Player.Speed.X / 100 * (Player.MoveDirection.X / Math.Abs(Player.MoveDirection.X));
            // Force downwards movement because of gravity
            Player.MoveDirection.Y += PlayerGameObject.Gravity * Time.deltaTime;


            // if @Player is not in the air and user presses some keys, the @Player jumps
            if (!Player.InAir && (keys['w'] || keys['W'] || keys[32] || kUp))
            {
                // play jumping sound
                new SoundPlayer(Resources.jump).Play();

                // set movement speed upwards
                Player.MoveDirection.Y = -Player.Speed.Y;

                // set flag that @Player is in the air
                Player.InAir = true;
            }

            // if pressed 'a' or 'A' or leftArrow the @Player moves leftwards
            if (keys['a'] || keys['A'] || kLeft)
            {
                Player.MoveDirection.X = -Player.Speed.X;
            }

            // if pressed 'd' or 'D' or rightArrow the @Player moves rightwards
            if (keys['d'] || keys['D'] || kRight)
            {
                Player.MoveDirection.X = Player.Speed.X;
            }

            // if pressed 's' or 'S' or downArrow the @Player falls down from a line (floor)
            if (keys['s'] || keys['S'] || kDown)
            {
                Player.Circle.Center = new System.Drawing.PointF(Player.Circle.Center.X, Player.Circle.Center.Y + 1);
            }
        }

        // event handlers
        public override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keys[e.KeyValue] = true;
            if (Keys.Down == e.KeyCode)
            {
                kDown = true;
            }
            if (Keys.Up == e.KeyCode)
            {
                kUp = true;
            }
            if (Keys.Left == e.KeyCode)
            {
                kLeft = true;
            }
            if (Keys.Right == e.KeyCode)
            {
                kRight = true;
            }
        }

        public override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keys[e.KeyValue] = false;
            if (Keys.Down == e.KeyCode)
            {
                kDown = false;
            }
            if (Keys.Up == e.KeyCode)
            {
                kUp = false;
            }
            if (Keys.Left == e.KeyCode)
            {
                kLeft = false;
            }
            if (Keys.Right == e.KeyCode)
            {
                kRight = false;
            }
        }
    }
}
