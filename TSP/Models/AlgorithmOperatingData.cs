using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Models
{
    class AlgorithmOperatingData
    {
        public IList<Node> UnusedNodes { get; set; } = new List<Node>();
        public IList<Node> PathNodes { get; set; } = new List<Node>();
        public int Distance { get; set; }

        public AlgorithmOperatingData CloneData()
        {
            return new AlgorithmOperatingData
            {
                UnusedNodes = UnusedNodes.CloneList(),
                PathNodes = PathNodes.CloneList(),
                Distance = Distance
            };
        }
    }   
}
