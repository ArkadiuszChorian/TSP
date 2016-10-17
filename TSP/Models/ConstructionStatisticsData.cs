using System.Collections.Generic;

namespace TSP.Models
{
    class ConstructionStatisticsData
    {
        public int AccumulatedDistance { get; set; } = 0;
        public int MaximumDistance { get; set; } = 0;
        public int MinimumDistance { get; set; } = int.MaxValue;
        public IList<Node> BestRoute { get; set; } = new List<Node>();
    }
}