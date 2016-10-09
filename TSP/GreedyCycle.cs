using System.Collections.Generic;

namespace TSP
{
    class GreedyCycle : Algorithm
    {
        public GreedyCycle(List<Node> nodes) : base(nodes) { }

        public void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < OutputNodesLimit; i++)
            {
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                actualNode = FindMinimalPath(actualNode, OutputNodes[0]);
            }

            Distance += CalculateDistance(OutputNodes[OutputNodes.Count - 1],
                OutputNodes[0]);
        }

        private Node FindMinimalPath(Node sourceNode, Node firstNode)
        {
            var minimalPath = int.MaxValue;
            Node bestFoundNode = null;

            foreach (var node in InputNodes)
            {
                // Calculating whole closed path
                var path = CalculatePath(Distance, sourceNode, node, firstNode);
                if (minimalPath <= path) continue;
                minimalPath = path;
                bestFoundNode = node;
            }

            // Deleting last connection from Distance (for first node algorithm double
            // first disance and then subtracts it)
            Distance = minimalPath - CalculateDistance(firstNode, bestFoundNode);

            return bestFoundNode;
        }
    }
}
