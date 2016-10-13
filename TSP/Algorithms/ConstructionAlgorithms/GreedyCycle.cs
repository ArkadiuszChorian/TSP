using System.Collections.Generic;
using System.Linq;

namespace TSP
{
    class GreedyCycle : ConstructionAlgorithm
    {
        //public GreedyCycle(IList<Node> nodes) : base(nodes) { }

        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                actualNode = FindMinimalPath(actualNode, OutputNodes.First());
            }

            Distance += CalculateDistance(OutputNodes.Last(), OutputNodes.First());
        }

        public virtual Node FindMinimalPath(Node sourceNode, Node firstNode)
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
