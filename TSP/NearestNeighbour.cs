using System.Collections.Generic;

namespace TSP
{
    class NearestNeighbour : Algorithm
    {
        public NearestNeighbour(IList<Node> nodes) : base(nodes) { }

        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < OutputNodesLimit; i++)
            {
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                actualNode = FindNearestNeighbour(actualNode);
            }
            Distance += CalculateDistance(OutputNodes[OutputNodes.Count - 1],
                OutputNodes[0]);
        }

        public virtual Node FindNearestNeighbour(Node sourceNode)
        {
            var minimalDistance = int.MaxValue;
            Node nearestNode = null;

            foreach (var node in InputNodes)
            {
                var distance = CalculateDistance(sourceNode, node);
                if (minimalDistance <= distance) continue;
                minimalDistance = distance;
                nearestNode = node;
            }

            Distance += minimalDistance;

            return nearestNode;
        }      
    }
}
