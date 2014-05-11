using System.Drawing;
using System.Windows.Forms;
using VPSeminarska.GameLogic.Player;

// if some @GameObject needs a bunch of specific functionalities they can be programmed separately and in a modularly fashion

namespace VPSeminarska.Abstracts
{
    public abstract class Command : BaseObject
    {
        // each @Command object belongs to a specific @GameObject
        // the @Command object has the control, and the @Command object is a part of the parent @GameObject object
        // this type of architecture enables IoC (Inversion of Control) - the child has the control on the parent
        public GameObject GameObject;

        // login to be executed onto the parent
        public virtual void execute()
        {
        }

        // login to be undo onto the parent
        public virtual void undo()
        {
        }
    }
}
