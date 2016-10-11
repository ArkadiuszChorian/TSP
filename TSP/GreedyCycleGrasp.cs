using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class GreedyCycleGrasp : GreedyCycle
    {
        public GreedyCycleGrasp(IList<Node> nodes) : base(nodes) { }

        public override Node FindMinimalPath(Node sourceNode, Node firstNode)
        {
            var minimalDistanceList = new SortedDictionary<Node, int>();

            foreach ( var node in InputNodes )
            {
                var path = CalculatePath(Distance, sourceNode, node, firstNode);
                minimalDistanceList.Add(node, path);
            }

            var kvp = minimalDistanceList.OrderBy(i => i.Value);
            var number = RandomObject.Next(0, 2);
            var val = kvp.ElementAt(number);
            var bestFoundNode = val.Key;
            var minimalPath = val.Value;

            Distance = minimalPath - CalculateDistance(firstNode, bestFoundNode);

            return bestFoundNode;
        }
    }
}
