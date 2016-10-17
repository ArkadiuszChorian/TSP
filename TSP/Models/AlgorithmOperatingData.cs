using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Models
{
    class AlgorithmOperatingData
    {
        //public IList<Node> ClonedNodes { get; set; }
        public IList<Node> UnusedNodes { get; set; }
        public IList<Node> PathNodes { get; set; }
        public int Distance { get; set; }

        //public AlgorithmOperatingData(IList<Node> unusedNodes, IList<Node> pathNodes, int distance)
        //{
        //    UnusedNodes = unusedNodes;
        //    PathNodes = pathNodes;
        //    Distance = distance;
        //}
    }   
}
