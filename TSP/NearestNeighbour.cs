using System.Collections.Generic;

namespace TSP
{
    class NearestNeighbour : Algorithm
    {
        public NearestNeighbour(List<Node> nodes) : base(nodes) { }

        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < OutputNodesLimit; i++)
            {
                //Console.WriteLine(actualNode.X + " " + actualNode.Y);
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                //Console.WriteLine(InputNodes.Count);
                actualNode = FindNearestNeighbour(actualNode);
                //Console.ReadKey();
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

        //public int calculateNearestNeighbour(Node startNode)
        //{
        //    int min = 0;
        //    actualNode = startNode;
        //    clonedList.Remove(startNode);
        //    for (int i = 0; i < clonedList.Count; i++)
        //    {
        //        int distance = CalculateDistance(actualNode, clonedList[i]);
        //        if (distance < min || min == 0)
        //        {
        //            min = distance;
        //            nextNode = clonedList[i];
        //        }
        //    }

        //    return min;
        //}

        //public int CalculateRoute(Node node)
        //{
        //    int distance;
        //    distance = calculateNearestNeighbour(node);
        //    for (int i = 0; i < 49; i++)
        //    {
        //        if (nextNode != null)
        //            distance += calculateNearestNeighbour(nextNode);
        //    }

        //    return distance += CalculateDistance(nextNode, node);
        //}
    }
}
