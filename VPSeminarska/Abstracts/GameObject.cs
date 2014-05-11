using System.Drawing;
using System.Windows.Forms;
using VPSeminarska.Abstracts.Interfaces;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.Libraries;

// every @Scene object has a lot of @GameObject objects to display
// each @GameObject object has a lot of @Command objects that represent and execute separate functionalities 

namespace VPSeminarska.Abstracts
{
    public abstract class GameObject : BaseObject
    {
        // each @GameObject object object belongs to a specific @Scene object
        // the @GameObject object has the control, and the @GameObject object is a part of the parent @Scene
        // this type of architecture enables IoC (Inversion of Control) - the child has the control on the parent
        public Scene Scene;

        // We need @ObservableList in order to dynamically assign the parent (this) to every child @Command object
        public ObservableList<Command> Commands;

        // initialization
        public GameObject()
        {
            this.Commands = new ObservableList<Command>();
            this.Commands.OnAdd += Commands_AddingNew;
        }

        // when a @Command object is added the the list of @Command objects
        // the parent variable is set to this @GameObject object - obviously
        private void Commands_AddingNew(object sender, ObservableListOnAddEventArgs e)
        {
            Command c = e.Item as Command;
            c.GameObject = this;
        }

        // when this object is initialized each @Command child object is initialized too
        // if @Command objects are added as children to this @GameObject object in the @init() method of the @Scene object,
        // and they usually are, the @base.init() (the method below is the @base.init() method because this is an abstract class)
        // must be invoked at the end of @this.init() method in order for each child @Command objects @init() method to be invoked
        public virtual new void init()
        {
            foreach (Command c in Commands)
            {
                c.init();
            }
        }

        // used to dislocate objects
        // every time a @Scene object is initialized the @Command objects are added to the corresponding @GameObject object
        // so every time a @Scene object is destroyed, each child @Command object must be destroyed
        public virtual new void destroy()
        {
            this.Commands.Clear();
        }

        // at each iteration of the game loop, each @GameObject object is redrawn
        // each @GameObject object redraws his own child @Command objects
        public virtual new void Paint(Graphics g)
        {
            foreach (Command c in Commands)
            {
                c.Paint(g);
            }
        }
    }
}
