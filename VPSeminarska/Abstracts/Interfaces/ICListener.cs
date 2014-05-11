using System.Windows.Forms;

// this interface contains all event handlers used throughout this application

namespace VPSeminarska.Abstracts.Interfaces
{
    interface IEventHandlers
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
