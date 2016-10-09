using System;

namespace TSP
{
    class Node : IComparable
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

        public int CompareTo(object obj)
        {
            Node node = obj as Node;
            if (node != null) return Id.CompareTo(node.Id);
            throw new ArgumentException("Argument is not of type Node");
        }
    }
}
