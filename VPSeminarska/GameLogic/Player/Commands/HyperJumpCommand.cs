using System;
using System.Drawing;
using System.Media;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Properties;

// this @Command object enables its parent @GameObject object to move upwards 
// (to jump if it stands on a line (floor)) with increased power

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class HyperJumpCommand : Command
    {
        // execution
        public bool canBeUsed = false;
        public double duration;
        private double counter;

        // auxiliary
        private bool[] keys;
        private bool playSoundOnce;

        // this command execution is based on user input, that's why the event are bind
        public HyperJumpCommand()
            : base()
        {
            keys = new bool[256];
            BindEvents();
        }

        // initialization of variables
        public override void init()
        {
            duration = 7; // the meter fills for 7 seconds
            counter = duration;
            canBeUsed = true;
            playSoundOnce = false;

            base.init();
        }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            // render GUI elements to indicate the duration of hyper-jump time needed to fill the power-up
            g.DrawString("Hyper-Jump: ", new Font("Ariel", 14), Brushes.Black, new Point(350, 10));
            // draw the meter
            g.DrawRectangle(Pens.Black, 470, 19, 100, 10);
            // draw the meter fill
            g.FillRectangle(Brushes.Black, 470, 19, (int)((counter / duration) * 100), 10);

            // when @HyperJump is used @counter = 0
            // the full meter sound can be played as soon as this condition is not true
            if (counter < duration)
            {
                canBeUsed = false;
                playSoundOnce = true;
            }
            // this executes when the meter is full
            else
            {
                // play the full meter sound only once per fill-up
                if (playSoundOnce)
                {
                    playSoundOnce = false;
                    new SoundPlayer(Resources.fullHyperjump).Play();
                }
                // set flag that indicates that the @HyperJump can be used
                canBeUsed = true;
            }

            // if the @player is not in the air and the meter is full and the user pressed 'x' or 'X'
            // execute @HyperJump
            if (!Player.InAir && (keys['x'] || keys['X']) && canBeUsed)
            {
                new SoundPlayer(Resources.hyperjump).Play();
                execute();
            }
            else
            {
                undo();
            }
        }

        // execute @HyperJump
        public override void execute()
        {
            // set meter fill to 0
            // jump 2.2 times more than a normal jump
            // set @Player.InAir flag to true
            counter = 0;
            Player.MoveDirection.Y = -Player.Speed.Y * 2.2;
            Player.InAir = true;
        }

        // execute when the @HyperJump is not executed
        public override void undo()
        {
            // fill the meter as time passes
            counter = Math.Min(duration, counter + Time.deltaTime);
        }

        public override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keys[e.KeyValue] = true;
        }

        public override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            keys[e.KeyValue] = false;
        }
    }
}