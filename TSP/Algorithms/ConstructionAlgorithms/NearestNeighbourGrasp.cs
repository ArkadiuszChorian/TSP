using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class NearestNeighbourGrasp : NearestNeighbour
    {
        protected override Node FindNearestNeighbour(Node sourceNode)
        {
            var minimalDistanceList = new Dictionary< Node, int>();

            foreach ( var node in OperatingData.UnusedNodes )
            {
                var distance = CalculateDistance(sourceNode, node);
                minimalDistanceList.Add(node, distance);
            }

            var val = minimalDistanceList.OrderBy(i => i.Value).ElementAt(RandomGenerator.Next(0, 2));
            var nearestNode = val.Key;
            var minimalDistance = val.Value;

            OperatingData.Distance += minimalDistance;

            return nearestNode;
        }
    }
}
