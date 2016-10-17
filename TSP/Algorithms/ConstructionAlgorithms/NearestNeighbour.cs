using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class NearestNeighbour : ConstructionAlgorithm
    {
        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                OperatingData.PathNodes.Add(actualNode);
                OperatingData.UnusedNodes.Remove(actualNode);
                actualNode = FindNearestNeighbour(actualNode);
            }
            OperatingData.Distance += CalculateDistance(OperatingData.PathNodes.Last(), OperatingData.PathNodes.First());
        }

        protected virtual Node FindNearestNeighbour(Node sourceNode)
        {
            var minimalDistance = int.MaxValue;
            Node nearestNode = null;

            foreach (var node in OperatingData.UnusedNodes)
            {
                var distance = CalculateDistance(sourceNode, node);
                if (minimalDistance <= distance) continue;
                minimalDistance = distance;
                nearestNode = node;
            }

            OperatingData.Distance += minimalDistance;

            return nearestNode;
        }      
    }
}
