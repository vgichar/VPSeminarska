using System.Drawing;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

// a class used to store information and render the game object that the user controls
// because the user doesn't store any information for himself except when it scores a high score
// and that data is stored in @Score class in @VPSeminarska.@GameLogic.@Data.@Entities namespace

namespace VPSeminarska.GameLogic.Player
{
    public class PlayerGameObject : GameObject
    {
        public const double Gravity = 2000;

        // rendering info
        public Circle Circle;
        public Size Size;

        // movement info
        public Vector2D Speed;
        public Vector2D MoveDirection;
        public bool InAir;

        // initialize variables
        public PlayerGameObject()
            : base()
        {
            Circle = new Circle(new Point(0, 0), 25);
            Speed = new Vector2D(350, 730);
            MoveDirection = new Vector2D(0, 0);
            Size = new Size((int)Circle.Radius * 2, (int)Circle.Radius * 2);
            InAir = true;
        }

        public override void Paint(Graphics g)
        {
            base.Paint(g);

            // store the updated position based on movement
            // the center is the pivot point for the rendering
            Circle.Center = ((MoveDirection * Time.deltaTime) + Circle.Center).GetPointF();

            // this center is not the center, but the left top corner of the rendering rectangle
            Point PositionCenter = new Point((int)Circle.Center.X - Size.Width / 2, (int)Circle.Center.Y - Size.Height / 2);

            // render the @Circle that represents the @PlayerGameObject
            g.DrawEllipse(new Pen(Brushes.Black, 1), new Rectangle(PositionCenter, Size));
        }
    }
}
