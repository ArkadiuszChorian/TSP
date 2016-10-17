using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class GreedyCycle : ConstructionAlgorithm
    {
        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                OperatingData.PathNodes.Add(actualNode);
                OperatingData.UnusedNodes.Remove(actualNode);
                actualNode = FindMinimalPath(actualNode, OperatingData.PathNodes.First());
            }

            OperatingData.Distance += CalculateDistance(OperatingData.PathNodes.Last(), OperatingData.PathNodes.First());
        }

        protected int CalculatePath(int distance, Node node1, Node node2, Node startNode)
        {
            return distance + CalculateDistance(node1, node2) + CalculateDistance(node2, startNode);
        }

        protected virtual Node FindMinimalPath(Node sourceNode, Node firstNode)
        {
            var minimalPath = int.MaxValue;
            Node bestFoundNode = null;

            foreach (var node in OperatingData.UnusedNodes)
            {
                // Calculating whole closed path
                var path = CalculatePath(OperatingData.Distance, sourceNode, node, firstNode);
                if (minimalPath <= path) continue;
                minimalPath = path;
                bestFoundNode = node;
            }

            // Deleting last connection from Distance (for first node algorithm double
            // first disance and then subtracts it)
            OperatingData.Distance = minimalPath - CalculateDistance(firstNode, bestFoundNode);

            return bestFoundNode;
        }
    }
}
