using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class GreedyCycleGrasp : GreedyCycle
    {
        public override Node FindMinimalPath(Node sourceNode, Node firstNode)
        {
            var minimalDistanceList = new Dictionary<Node, int>();

            foreach ( var node in InputNodes )
            {
                var path = CalculatePath(Distance, sourceNode, node, firstNode);
                minimalDistanceList.Add(node, path);
            }
            
            var kvp = minimalDistanceList.OrderBy(i => i.Value).ElementAt(RandomObject.Next(0,2));
            var bestFoundNode = kvp.Key;
            var minimalPath = kvp.Value;

            Distance = minimalPath - CalculateDistance(firstNode, bestFoundNode);

            return bestFoundNode;
        }
    }
}
