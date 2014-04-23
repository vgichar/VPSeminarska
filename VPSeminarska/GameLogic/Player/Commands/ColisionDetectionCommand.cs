﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.Abstracts.Interfaces;
using VPSeminarska.GameLogic.SceneItems;
using VPSeminarska.Libraries;
using VPSeminarska.Libraries.MathLib;

namespace VPSeminarska.GameLogic.Player.Commands
{
    public class ColisionDetectionCommand : Command
    {
        private PlayerGameObject Player { get; set; }
        private Scene Scene { get; set; }

        public ColisionDetectionCommand(Form f, GameObject gameObject) : base(f, gameObject) 
        {
            Player = gameObject as PlayerGameObject;
            Scene = Player.Scene as Scene;
        }

        public override void Paint(System.Drawing.Graphics g, System.Windows.Forms.Form f)
        {
            base.Paint(g, f);

            this.execute();
        }

        public override void execute()
        {
            Player.InAir = true;
            
            foreach (var ob in Scene.GameObjects) {
                LineGameObject lgo = ob as LineGameObject;
                if (ob != Player)
                {
                    if (Player.MoveDirection.Y > 0
                        && (Player.Circle.Center.Y + Player.Circle.Radius <= lgo.Line.Start.Y
                        && Player.Circle.Center.Y + (Player.MoveDirection.Y * Time.deltaTime) + Player.Circle.Radius >= lgo.Line.Start.Y)
                        && Player.Circle.Center.X > lgo.Line.Start.X
                        && Player.Circle.Center.X < lgo.Line.End.X
                        )
                    Player.InAir = false;
                }
            }

            if (!Player.InAir)
            {
                Player.MoveDirection.Y = 0;
            }
        }

        public override void undo()
        {
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
