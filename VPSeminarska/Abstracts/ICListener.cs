using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPSeminarska.Abstracts
{
    interface ICListener
    {
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnKeyPress(object sender, KeyPressEventArgs e);
        void OnKeyUp(object sender, KeyEventArgs e);
        void OnClick(object sender, MouseEventArgs e);
        void OnMouseDown(object sender, MouseEventArgs e);
        void OnMouseUp(object sender, MouseEventArgs e);
        void OnMouseMove(object sender, MouseEventArgs e);
    }
}
