using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP
{
    class GraspNn : NearestNeighbour
    {
        public GraspNn( List<Node> nodes ) : base(nodes) { }

        public override Node FindNearestNeighbour(Node sourceNode)
        {
            var minimalDistanceList = new SortedDictionary< Node, int>();
            var minimalDistance = int.MaxValue;
            Node nearestNode = null;

            foreach ( var node in InputNodes )
            {
                var distance = CalculateDistance(sourceNode, node);
                minimalDistanceList.Add(node, distance);
            }

            var kvp = minimalDistanceList.OrderBy(i => i.Value);
            var random = new Random();
            var val = kvp.ElementAt(random.Next(0, 2));
            nearestNode = val.Key;
            minimalDistance = val.Value;

            Distance += minimalDistance;

            return nearestNode;
        }
    }
}
