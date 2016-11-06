using System;
using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP2
{
    public class RandomSolution : Algorithm
    {
        public OperatingData FindRoute(Node startNode, OperatingData operatingData)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                operatingData.PathNodes.Add(actualNode);
                operatingData.UnusedNodes.Remove(actualNode);
                actualNode = FindRandomNeighbour(actualNode, operatingData);
            }
            operatingData.Distance += CalculateDistance(operatingData.PathNodes.Last(), operatingData.PathNodes.First());

            return operatingData;
        }

        public OperatingData FindRouteFromRandomStart(OperatingData operatingData)
        {
            var actualNode = operatingData.UnusedNodes[RandomGenerator.Next(0, operatingData.UnusedNodes.Count-1)];

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                operatingData.PathNodes.Add(actualNode);
                operatingData.UnusedNodes.Remove(actualNode);
                actualNode = FindRandomNeighbour(actualNode, operatingData);
            }
            operatingData.Distance += CalculateDistance(operatingData.PathNodes.Last(), operatingData.PathNodes.First());

            return operatingData;
        }

        public Node FindRandomNeighbour(Node sourceNode, OperatingData operatingData)       
        {
            var randomNode = operatingData.UnusedNodes[RandomGenerator.Next(0, operatingData.UnusedNodes.Count - 1)];
            operatingData.Distance += CalculateDistance(sourceNode, randomNode);

            return randomNode;
        }       

        public static readonly int ResultNodesLimit = 50;
    }
}
