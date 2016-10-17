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

        public AlgorithmOperatingData CloneData()
        {
            //var clonedOperatingData = new AlgorithmOperatingData();
            //clonedOperatingData.UnusedNodes = UnusedNodes.CloneList();
            //clonedOperatingData.PathNodes = PathNodes.CloneList();
            //clonedOperatingData.Distance = Distance;

            return new AlgorithmOperatingData
            {
                UnusedNodes = UnusedNodes.CloneList(),
                PathNodes = PathNodes.CloneList(),
                Distance = Distance
            };
        }

        //public AlgorithmOperatingData(IList<Node> unusedNodes, IList<Node> pathNodes, int distance)
        //{
        //    UnusedNodes = unusedNodes;
        //    PathNodes = pathNodes;
        //    Distance = distance;
        //}
    }   
}
