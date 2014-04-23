﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic.Player
{
    public class PlayerGameObject : GameObject
    {
        public const double Gravity = 2000;
        public Size Size { get; set; }
        public Circle Circle { get; set; }
        public bool InAir { get; set; }
        public bool UseGravity { get; set; }
        public double Mass { get; set; }

        public Vector2D Speed { get; set; }
        public Vector2D MoveDirection { get; set; }

        public PlayerGameObject(Form f, Scene Scene)
            : base(f, Scene)
        {
            Circle = new Circle(new Point(180, 0), 25);
            Speed = new Vector2D(300, 500);
            MoveDirection = new Vector2D(0, 0);
            InAir = true;
            Size = new Size((int)Circle.Radius * 2, (int)Circle.Radius * 2);
        }

        public override void Paint(Graphics g, System.Windows.Forms.Form f)
        {
            base.Paint(g, f);

            Circle.Center = ((MoveDirection * Time.deltaTime) + Circle.Center).GetPointF();

            Point PositionCenter = new Point((int)Circle.Center.X - Size.Width / 2, (int)Circle.Center.Y - Size.Height / 2);

            g.DrawEllipse(new Pen(Brushes.Black, 1), new Rectangle(PositionCenter, Size));
        }

        public override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        public override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        public override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
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
