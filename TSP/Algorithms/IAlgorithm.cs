using System;
using System.Collections.Generic;

namespace TSP.SolutionConstruction.ConstructionAlgorithms
{
    interface IAlgorithm
    {
        void FindRoute(Node node);

        int CalculateDistance(Node node1, Node node2);
        int CalculatePath(int distance, Node node1, Node node2, Node startNode);
        void ResetAlgorithm();

        IList<Node> ClonedNodes { get; set; }
        IList<Node> InputNodes { get; set; }
        IList<Node> OutputNodes { get; set; }
        int Distance { get; set; }
    }
}
