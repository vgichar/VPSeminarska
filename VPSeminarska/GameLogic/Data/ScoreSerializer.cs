using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using VPSeminarska.GameLogic.Data.Entities;

namespace VPSeminarska.GameLogic.Data
{
    public class ScoreSerializer
    {
        private static List<Score> _HighScores;
        public static List<Score> HighScores
        {
            get
            {
                /// order list
                _HighScores = _HighScores.OrderByDescending(x => x.Points).ToList<Score>();

                // get only best 10 high scores
                while (_HighScores.Count > 10)
                {
                    _HighScores.RemoveAt(10);
                }
                return _HighScores;
            }
            set
            {
                _HighScores = value;
            }
        }

        // initialize serializer, initialize List<@Score>
        public static void init()
        {
            _HighScores = new List<Score>();
            // when the Application is closing save the high scores on hard disk
            MainWindow.Instance.FormClosing += Instance_FormClosing;
        }

        // read high scores from hard disk; deserialize
        public static void getData()
        {
            if (File.Exists("icybubble.sav"))
            {
                IFormatter fmt = new BinaryFormatter();
                FileStream fs = new FileStream("icybubble.sav", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                HighScores = (List<Score>)fmt.Deserialize(fs);
                fs.Close();
            }
        }

        // save high scores to hard disk; serialize
        private static void saveData()
        {
            IFormatter fmt = new BinaryFormatter();
            FileStream fs = new FileStream("icybubble.sav", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            fmt.Serialize(fs, HighScores);
            fs.Close();
        }

        private static void Instance_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            saveData();
        }
    }
}
