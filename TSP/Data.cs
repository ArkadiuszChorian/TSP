using System;
using System.Collections.Generic;

namespace TSP
{
    class Data
    {
        public int AccumulatedDistance{ get; set; }
        public int MaximumDistance { get; set; }
        public int MinimumDistance { get; set; } = int.MaxValue;
        public List<Node> BestRoute { get; set; } = new List<Node>();
    }
}