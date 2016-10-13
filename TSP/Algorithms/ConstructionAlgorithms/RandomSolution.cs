using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.SolutionConstruction.ConstructionAlgorithms;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class RandomSolution : ConstructionAlgorithm
    {
        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                actualNode = FindRandomNeighbour(actualNode);
            }
            Distance += CalculateDistance(OutputNodes.Last(), OutputNodes.First());
        }

        public Node FindRandomNeighbour(Node sourceNode)
        {
            var randomNode = InputNodes[RandomObject.Next(0, InputNodes.Count - 1)];
            Distance += CalculateDistance(sourceNode, randomNode);

            return randomNode;
        }
    }
}
