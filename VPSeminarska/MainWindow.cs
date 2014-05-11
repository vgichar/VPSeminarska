using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VPSeminarska.Abstracts;
using VPSeminarska.GameLogic;
using VPSeminarska.GameLogic.Data;
using VPSeminarska.GameLogic.Player;
using VPSeminarska.Libraries;

// There are 4 main types of objects
// the main form - @MainWindow (singleton), @Scene, @GameObject, @Command
//
// there is a hierarchy between them as follows:
// 
// @MainWindow has List<@Scene>
// @Scene has List<@GameObject>
// @GameObject has List<@Command>
//
// in the future text these objects will be mentioned as 
// Parent (e.g. @Scene to List<@GameObject>) and Components (e.g. List<@Command> to @GameObject) accordingly
//
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

namespace VPSeminarska
{
    public partial class MainWindow : Form
    {

        // used to access this MainWindow form anywhere, hence the static
        private static MainWindow _Instance;
        public static MainWindow Instance
        {
            get
            {
                return _Instance;
            }
            set
            {
            }
        }

        // list of all 

        public ObservableList<Scene> Scenes;

        // isolate Player game object for easy access instead to search throughout all the gameObjects
        public PlayerGameObject _Player;
        public PlayerGameObject Player
        {
            get
            {
                // each game object belongs to a specific scene, but because Player is global,
                // it must be set to a specific scene (best and easiest way is to set the scene on getting the Player)
                if (_Player != null)
                {
                    _Player.Scene = CurrentScene;
                }
                return _Player;
            }
            set
            {
                _Player = value;
            }
        }

        // currently active scene
        private Scene _CurrentScene;
        public Scene CurrentScene
        {
            get
            {
                return _CurrentScene;
            }
            set
            {
                // when the current scene is switched it must be destroyed and the newly current scene initialized
                // because the Player is global he doesn't get initialized as the part of the scene,
                // so it must be initialized explicitly
                if (_CurrentScene != null)
                {
                    _CurrentScene.destroy();
                    Player.destroy();
                }
                _CurrentScene = value;
                _CurrentScene.init();
                Player.init();
            }
        }

        // array of booleans; if @key is pressed set @keys[(int)@key] = true; else @keys[(int)@key] = false - (int)@key == ASCII of @key
        private bool[] keys;

        // some objects (Scene and GameObject usually) decide to alter themselves while the execute
        // to preserve object integrity there are actions that execute those changes before or after these objects execute
        public List<Action> beginActions;
        public List<Action> endActions;

        // Initialization of variables
        public MainWindow()
        {
            _Instance = this;
            InitializeComponent();

            beginActions = new List<Action>();
            endActions = new List<Action>();
            Scenes = new ObservableList<Scene>();
            keys = new bool[256];

            this.DoubleBuffered = true;

            init();

            this.Invalidate();
        }

        // execute initial logic
        public void init()
        {
            // bind @MainWindow keyboard and mouse events to corresponding handlers
            BindEvents();

            // initialize the high score serializes
            ScoreSerializer.init();
            // deserialize data
            ScoreSerializer.getData();

            // create needed scenes
            MenuScreen menu = new MenuScreen();
            Scenes.Add(menu);
            LevelScene level = new LevelScene();
            Scenes.Add(level);
            HighScoreScene highscore = new HighScoreScene();
            Scenes.Add(highscore);

            // set menu as current scene
            CurrentScene = menu;
        }

        // game loop
        protected override void OnPaint(PaintEventArgs e)
        {
            // track end time of each iteration through game loop
            // when the loop is called again that mean the previous iteration has ended
            // that is why the end time is measured here
            Time.endTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;

            // calculate total time needed to execute last iteration of the game loop
            Time.deltaTime = Time.endTime - Time.startTime;

            // track start time of each iteration through game loop
            Time.startTime = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            Graphics g = e.Graphics;

            // set quality of rendering
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // execute actions before execution game loop of everything
            foreach (Action ac in beginActions)
            {
                ac.Invoke();
            }

            if (keys[27])
            {
                CurrentScene = Scenes[0];
            }

            // render current scene
            CurrentScene.Paint(g);

            // set maximum frame rate to 60 fps (calculation based on last time needed to execute game loop)
            System.Threading.Thread.Sleep(Math.Max(16 - (int)(Time.deltaTime * 1000), 1));

            // execute actions after execution game loop of everything
            foreach (Action ac in endActions)
            {
                ac.Invoke();
            }

            // force garbage collector to execute
            System.GC.Collect();

            // call next iteration of the game loop
            this.Invalidate();
        }

        // binds @MainWindow keyboard and mouse events to corresponding handlers
        public void BindEvents()
        {
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.KeyPress += OnKeyPress;
            this.MouseClick += OnClick;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
            this.MouseMove += OnMouseMove;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            // if key is pressed set to true
            keys[e.KeyValue] = true;
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            // if key is released set to false
            keys[e.KeyValue] = false;
        }

        public void OnClick(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
