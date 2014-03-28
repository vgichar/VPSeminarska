using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPSeminarska.Libraries.MathLib
{
    public static class MathTest
    {
        // <Summary>
        // Colision test for line and circle
        // </Summary>
        public static bool LineCircleColisionTest(Line2D li, Circle ci)
        {
            return true;
        }

        public static bool LineCircleColisionMoveTest(Line2D li, Circle ci, Vector2D limd, Vector2D cimd)
        {
            Line2D li2 = new Line2D(new PointF((float)(Time.deltaTime * limd.X + li.Start.X), (float)(Time.deltaTime * limd.Y + li.Start.Y)), new PointF((float)(Time.deltaTime * limd.X + li.End.X), (float)(Time.deltaTime * limd.Y + li.End.Y)));
            Circle ci2 = new Circle(new PointF((float)(Time.deltaTime * cimd.X + ci.Center.X), (float)(Time.deltaTime * cimd.Y + ci.Center.Y)), ci.Radius);
            return (LineCircleColisionTest(li, ci) || LineCircleColisionTest(li2, ci2));
        }
    }
}
