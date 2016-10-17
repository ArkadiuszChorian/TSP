using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class RandomSolution : ConstructionAlgorithm
    {
        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                OperatingData.PathNodes.Add(actualNode);
                OperatingData.UnusedNodes.Remove(actualNode);
                actualNode = FindRandomNeighbour(actualNode);
            }
            OperatingData.Distance += CalculateDistance(OperatingData.PathNodes.Last(), OperatingData.PathNodes.First());
        }

        public Node FindRandomNeighbour(Node sourceNode)
        {
            var randomNode = OperatingData.UnusedNodes[RandomGenerator.Next(0, OperatingData.UnusedNodes.Count - 1)];
            OperatingData.Distance += CalculateDistance(sourceNode, randomNode);

            return randomNode;
        }
    }
}
