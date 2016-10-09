namespace TSP
{
    class Node
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
    }
}
