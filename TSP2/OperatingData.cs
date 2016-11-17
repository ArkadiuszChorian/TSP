using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP2
{
    public class OperatingData
    {
        public IList<Node> UnusedNodes { get; set; } = new List<Node>();
        public IList<Node> PathNodes { get; set; } = new List<Node>();
        //public Dictionary<Node, Dictionary<Node, bool>> EdgesMatrix { get; set; } = new Dictionary<Node, Dictionary<Node, bool>>();
        public Dictionary<Node, Dictionary<Node, bool>> EdgesMatrix { get; set; } = DAL.Instance.Nodes.CloneList().ToDictionary(node => node, node => DAL.Instance.Nodes.CloneList().ToDictionary(node2 => node2, node2 => false));
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
