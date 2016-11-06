using System.Collections.Generic;
using TSP.Models;

namespace TSP2
{
    public class OperatingData
    {
        public IList<Node> UnusedNodes { get; set; } = new List<Node>();
        public IList<Node> PathNodes { get; set; } = new List<Node>();
        public int Distance { get; set; }

        public OperatingData CloneData()
        {
            return new OperatingData
            {
                UnusedNodes = UnusedNodes.CloneList(),
                PathNodes = PathNodes.CloneList(),
                Distance = Distance
            };
        }
    }   
}
