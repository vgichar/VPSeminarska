using System;
using System.Runtime.Serialization;

// basic serializable high score class
// contains @Name of player and scored @Points

namespace VPSeminarska.GameLogic.Data.Entities
{
    [Serializable()]
    public class Score : ISerializable
    {
        public string Name;
        public int Points;

        public Score(string Name, int Points)
        {
            this.Name = Name;
            this.Points = Points;
        }

        // constructor for deserialization purposes
        public Score(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("#Name", typeof(string));
            this.Points = (int)info.GetValue("#Points", typeof(int));
        }

        // method for serialization purposes
        // abstract method from @ISerializable interface, needed to serialize the object
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("#Name", Name);
            info.AddValue("#Points", Points);
        }
    }
}
