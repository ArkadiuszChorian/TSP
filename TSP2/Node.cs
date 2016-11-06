using System;

namespace TSP.Models
{
    public class Node : ICloneable
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Node(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Node nodeToCompare)
        {
            return Id == nodeToCompare.Id;
        }
    }
}
