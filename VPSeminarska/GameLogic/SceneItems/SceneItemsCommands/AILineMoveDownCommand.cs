using System.Drawing;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries.MathLib;

// this @Command object enables its parent @GameObject object to move downwards

namespace VPSeminarska.GameLogic.SceneItems.SceneItemsCommands
{
    class AILineMoveDownCommand : Command
    {
        // all lines have the same speed, so these variables are static

        // stores the initial speed of the lines
        public static double InitialSpeed = 60;

        // stores the real speed of the lines - @RealSpeed = @Speed + (@SpeedUp * @SpeedUpTimes)
        public static double RealSpeed = 60;

        // stores the current speed of the lines
        public static double Speed = 60;

        // stores the speedup of the lines which is based on game info and user info
        public static double SpeedUp = 10;

        // stores the speedup multiplier; how much times to apply the @SpeedUp value
        public static double SpeedUpTimes = 0;

        public override void Paint(Graphics g)
        {
            base.Paint(g);
            this.execute();
        }

        // executes the command logic
        public override void execute()
        {
            // if the player exceeds a certain @TOLERANCE height speed up the movement of the lines
            double playerPosY = Player.Circle.Center.Y;
            double addSpeed = 0;

            double TOLERANCE = 170;
            if (playerPosY < TOLERANCE)
            {
                addSpeed = TOLERANCE - playerPosY;
            }

            // calculate the real speed of the lines based of the Players position (@addSpeed),
            // the games chronology (add speed based on time played, e.g. every 10 seconds add 10 more pixels per second to the speed)
            RealSpeed = Speed + addSpeed + (SpeedUp * SpeedUpTimes);

            // set the speed of the line
            // the lines only move downwards
            LineGameObject lgo = GameObject as LineGameObject;
            if (lgo != null)
            {
                lgo.MoveDirection = new Vector2D(0, RealSpeed);
            }
        }
    }
}
