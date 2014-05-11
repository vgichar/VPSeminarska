using System.Drawing;
using System.Windows.Forms;
using VPSeminarska.Abstracts.Interfaces;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.Libraries;

// every @Scene object has a lot of @GameObject objects to display

namespace VPSeminarska.Abstracts
{
    public abstract class Scene : BaseObject
    {
        // We need @ObservableList in order to dynamically assign the parent (this) to every child @GameObject object
        public ObservableList<GameObject> GameObjects;

        // initialization
        public Scene()
        {
            this.GameObjects = new ObservableList<GameObject>();
            this.GameObjects.OnAdd += GameObjects_AddingNew;
        }

        // when a @GameObject object is added the the list of @GameObject objects
        // the parent variable is set to this @Scene object - obviously
        private void GameObjects_AddingNew(object sender, ObservableListOnAddEventArgs e)
        {
            GameObject go = e.Item as GameObject;
            go.Scene = this;
        }

        // when this object is initialized each @GameObject child object is initialized too
        public virtual new void init()
        {
            foreach (GameObject go in GameObjects)
            {
                go.init();
            }
        }

        // used to dislocate objects
        // every time a @Scene object is initialized the @GameObject objects are added to this object
        // so every time a @Scene object is destroyed, each child @GameObject object must be destroyed too
        public virtual new void destroy()
        {
            this.GameObjects.Clear();
        }

        // at each iteration of the game loop, each @GameObject object is redrawn
        // because the @Player is global static resource, he doesn't belong in the @GameObject list,
        // so his @Paint() method must be invoked separately
        public virtual new void Paint(Graphics g)
        {
            Player.Paint(g);
            foreach (GameObject go in GameObjects)
            {
                go.Paint(g);
            }
        }
    }
}
