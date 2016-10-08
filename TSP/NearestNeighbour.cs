using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class NearestNeighbour : Algorithm
    {
        private List<Node> clonedList;
        private Node actualNode;
        private Node nextNode;

        public NearestNeighbour(List<Node> list)
        {
            clonedList = list;
        }

        public int calculateNearestNeighbour(Node startNode)
        {
            int min = 0;
            actualNode = startNode;
            clonedList.Remove(startNode);
            for (int i = 0; i < clonedList.Count; i++)
            {
                int distance = Distance(actualNode, clonedList[i]);
                if (distance < min || min == 0)
                {
                    min = distance;
                    nextNode = clonedList[i];
                }
            }

            return min;
        }

        public int CalculateRoute(Node node)
        {
            int distance;
            distance = calculateNearestNeighbour(node);
            for (int i = 0; i < 49; i++)
            {
                if (nextNode != null)
                    distance += calculateNearestNeighbour(nextNode);
            }

            return distance += Distance(nextNode, node);
        }
    }
}
