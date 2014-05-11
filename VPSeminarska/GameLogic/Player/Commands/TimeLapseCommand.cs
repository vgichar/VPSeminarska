using System;
using System.Drawing;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.SceneItems.SceneItemsCommands;
using VPSeminarska.Libraries;

// this @Command object enables its parent @GameObject object to control the speed of all @LineGameObject objects

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class TimeLapseCommand : Command
    {
        // execution variables
        public bool CanBeExecuted;
        public double MaximumDuration;
        public double DurationRemaining;

        // auxiliary variables
        private bool[] keys;
        private double lastUsedCounter;
        private bool hasBeenUsed;

        // this command execution is based on user input, that's why the event are bind
        public TimeLapseCommand()
            : base()
        {
            keys = new bool[256];
            BindEvents();
        }

        // initialization of variables
        public override void init()
        {
            CanBeExecuted = false;
            MaximumDuration = 5; // seconds to use @SlowMotion
            DurationRemaining = MaximumDuration;
            lastUsedCounter = 100;
            hasBeenUsed = false;
            CanBeExecuted = true;

            base.init();
        }

        // render this @Command object
        public override void Paint(Graphics g)
        {
            base.Paint(g);

            // render GUI elements to indicate the quantity of slow-motion duration left
            g.DrawString("Slow-Motion: ", new Font("Ariel", 14), Brushes.Black, new Point(110, 10));
            // render slow-motion meter
            g.DrawRectangle(Pens.Black, 230, 19, 100, 10);
            // render the quantity of slow-motion duration left in the meter
            g.FillRectangle(Brushes.Black, 230, 19, (int)((DurationRemaining / MaximumDuration) * 100), 10);

            // if the slow motion meter indicates 0 or if the user has used this power-up in the last half second
            // it cannot be used
            if (DurationRemaining <= 0 || lastUsedCounter <= 0.5)
            {
                CanBeExecuted = false;
            }
            else
            {
                CanBeExecuted = true;
            }

            // use power-up if it can be used and the key 'z' is pressed
            // indicate that this power-up has been used and execute its logic
            if (CanBeExecuted && (keys['z'] || keys['Z']))
            {
                hasBeenUsed = true;
                execute();
            }
            else
            {
                // if the power-up has been used set the @lastUsedCounter to 0
                // indicate that this power-up has not been used and undo its logic
                if (hasBeenUsed)
                {
                    lastUsedCounter = 0;
                }
                hasBeenUsed = false;
                lastUsedCounter = Math.Min(100, lastUsedCounter + Time.deltaTime);
                undo();
            }
        }

        // change the speed of every @LineGameObject object
        // drain the remaining power-up quantity
        public override void execute()
        {
            DurationRemaining -= Time.deltaTime;
            AILineMoveDownCommand.Speed = AILineMoveDownCommand.InitialSpeed / 2.5;
        }

        // change back the speed of every @LineGameObject object
        // charge the power-ups quantity
        public override void undo()
        {
            DurationRemaining = Math.Min(MaximumDuration, DurationRemaining + Time.deltaTime);
            AILineMoveDownCommand.Speed = AILineMoveDownCommand.InitialSpeed;
        }

        // event handlers

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