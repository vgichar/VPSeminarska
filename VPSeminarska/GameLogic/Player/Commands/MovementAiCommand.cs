using System;
using System.Drawing;
using System.Media;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Properties;

// this @Command object enables its parent @GameObject object to move upwards (to jump if it stands on a line (floor))

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class MovementAICommand : Command
    {

        // call execution
        public override void Paint(Graphics g)
        {
            base.Paint(g);
            this.execute();
        }

        // used to simulate the jumping of the @Player in the @MenuScene
        public override void execute()
        {
            // Force moving downwards because of gravity
            Player.MoveDirection.Y += PlayerGameObject.Gravity * Time.deltaTime;

            // if the @Player is not in the air - jump
            if (!Player.InAir)
            {
                // play jumping sound
                new SoundPlayer(Resources.jump).Play();

                // set movement speed upwards
                Player.MoveDirection.Y = -Player.Speed.Y;

                // set flag that @Player is in the air
                Player.InAir = true;
            }
        }
    }
}
