using System.Drawing;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic.SceneItems;
using VPSeminarska.Libraries;

// this @Command object enables its parent @GameObject object to collide with other objects

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class CollisionDetectionCommand : Command
    {
        // call execution
        public override void Paint(Graphics g)
        {
            base.Paint(g);
            this.execute();
        }

        // execute business logic - if @Player.Circle bottom side collides with a floor - don't pass through it
        public override void execute()
        {
            // lets assume that the @Player is in the air, only if he collides with some floor he wont be
            Player.InAir = true;

            // compare position with every floor for collision
            foreach (var ob in GameObject.Scene.GameObjects)
            {
                LineGameObject lgo = ob as LineGameObject;
                if (lgo != null)
                {
                    // if the line is horizontal
                    // check if the X-axis position of the @Player is between the X-axis position
                    // of the end points of the line (floor)
                    // and check if now @Player's bottom side doesn't intersect with the line
                    // but in the next frame will
                    // that are the conditions for @Player - @Line (floor) collision
                    if (lgo.Line.AngleDegrees == 0 || lgo.Line.AngleDegrees == 180)
                    {
                        if (Player.MoveDirection.Y > 0
                            && (Player.Circle.Center.Y + Player.Circle.Radius <= lgo.Line.Start.Y
                            && Player.Circle.Center.Y + (Player.MoveDirection.Y * Time.deltaTime) + Player.Circle.Radius >= lgo.Line.Start.Y)
                            && Player.Circle.Center.X > lgo.Line.Start.X - 15
                            && Player.Circle.Center.X < lgo.Line.End.X + 15
                            )
                        {
                            Player.InAir = false;
                        }
                    }
                    // if the line is vertical (walls)
                    // get the @Player's movement direction, so we know for which side wall we test for collision
                    // check if now @Player's left and right side 
                    // (depends of which wall) doesn't intersect with the wall
                    // but in the next frame will
                    // that are the conditions for @Player - @Line (wall) collision
                    else
                    {
                        if (
                                (Player.MoveDirection.X > 0
                                && lgo.Line.Start.X > Player.Circle.Center.X
                                && (
                                    lgo.Line.Start.X <= Player.Circle.Center.X + Player.Circle.Radius
                                    || lgo.Line.Start.X <= Player.Circle.Center.X + Player.Circle.Radius + (Player.MoveDirection.X * Time.deltaTime)
                                    )
                                )
                                ||
                                (Player.MoveDirection.X < 0
                                && lgo.Line.Start.X < Player.Circle.Center.X
                                && (
                                    lgo.Line.Start.X >= Player.Circle.Center.X - Player.Circle.Radius
                                    || lgo.Line.Start.X >= Player.Circle.Center.X - Player.Circle.Radius + (Player.MoveDirection.X * Time.deltaTime)
                                    )
                                )
                            )
                        {
                            Player.MoveDirection.X *= -0.9;
                        }
                    }
                }
            }

            if (!Player.InAir)
            {
                Player.MoveDirection.Y = 0;
            }
        }
    }
}
