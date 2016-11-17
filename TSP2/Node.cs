using System;

namespace TSP2
{
    public class Node : ICloneable, IEquatable<Node>
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
            return nodeToCompare != null && Id == nodeToCompare.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
