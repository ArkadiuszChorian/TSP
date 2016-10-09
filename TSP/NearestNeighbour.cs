using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class NearestNeighbour : Algorithm
    {
        //private List<Node> clonedList;
        //private Node actualNode;
        //private Node nextNode;

        public NearestNeighbour(List<Node> nodes)
        {
            var temporaryArray = new Node[nodes.Count];     
            nodes.CopyTo(temporaryArray);
            ClonedNodes = temporaryArray.ToList();
            InputNodes = temporaryArray.ToList();
            OutputNodes = new List<Node>();
            Distance = 0;
        }

        public void ResetAlgorithm()
        {
            var temporaryArray = new Node[ClonedNodes.Count];
            ClonedNodes.CopyTo(temporaryArray);
            InputNodes = temporaryArray.ToList();
            OutputNodes.Clear();
            Distance = 0;
        }

        public void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (int i = 0; i < OutputNodesLimit; i++)
            {
                //Console.WriteLine(actualNode.X + " " + actualNode.Y);
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                //Console.WriteLine(InputNodes.Count);
                actualNode = FindNearestNeighbour(actualNode);
                //Console.ReadKey();
            }        
        }

        private Node FindNearestNeighbour(Node sourceNode)
        {
            var minimalDistance = int.MaxValue;
            Node nearestNode = null;

            foreach (var node in InputNodes)
            {
                var distance = CalculateDistance(sourceNode, node);
                if (minimalDistance > distance)
                {
                    minimalDistance = distance;
                    nearestNode = node;
                }
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
