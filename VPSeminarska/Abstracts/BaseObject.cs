using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VPSeminarska.Abstracts.Interfaces;
using VPSeminarska.GameLogic.Player;

// contains basic info that every object should have for this kind of architecture design
// each of this classes is constructed by the Template Pattern
// the execution is as follows:
//
// Constructor
// init
// OnPaint - loop
// destroy
//
// the Constructor usually initializes variables
// init usually executes initial logic and adds Components
// OnPaint represents the game loop for the object
// destroy usually dislocates resources 

namespace VPSeminarska.Abstracts
{
    public abstract class BaseObject : IEventHandlers
    {
        // enables easy access to the @MainWindowform
        public MainWindow MainWindow
        {
            get
            {
                return MainWindow.Instance;
            }
        }

        // enables easy access to the @Player
        // the @Player is a singleton and its global, so this enables access to the global variable
        public PlayerGameObject Player
        {
            get
            {
                return MainWindow.Player;
            }
            set
            {
                MainWindow.Player = value;
            }
        }

        // init usually executes initial logic and adds Components
        public virtual void init()
        {
        }

        // destroy usually dislocates resources 
        public virtual void destroy()
        {
        }

        // OnPaint represents the game loop for the object
        public virtual void Paint(Graphics g)
        {
        }

        // sometimes the classes need to have access to the events fired by the user
        // if this method is called in the @Constructor all the events are bind to the according event handler
        public void BindEvents()
        {
            MainWindow.KeyDown += OnKeyDown;
            MainWindow.KeyUp += OnKeyUp;
            MainWindow.KeyPress += OnKeyPress;
            MainWindow.MouseClick += OnClick;
            MainWindow.MouseDown += OnMouseDown;
            MainWindow.MouseUp += OnMouseUp;
            MainWindow.MouseMove += OnMouseMove;
        }

        // event handlers
        public virtual void OnKeyDown(object sender, KeyEventArgs e) { }
        public virtual void OnKeyPress(object sender, KeyPressEventArgs e) { }
        public virtual void OnKeyUp(object sender, KeyEventArgs e) { }
        public virtual void OnClick(object sender, MouseEventArgs e) { }
        public virtual void OnMouseDown(object sender, MouseEventArgs e) { }
        public virtual void OnMouseUp(object sender, MouseEventArgs e) { }
        public virtual void OnMouseMove(object sender, MouseEventArgs e) { }
    }
}
