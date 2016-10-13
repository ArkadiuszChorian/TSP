﻿using System.Linq;
using TSP.Models;

namespace TSP.Algorithms.ConstructionAlgorithms
{
    class NearestNeighbour : ConstructionAlgorithm
    {
        public override void FindRoute(Node startNode)
        {
            var actualNode = startNode;

            for (var i = 0; i < ResultNodesLimit; i++)
            {
                OutputNodes.Add(actualNode);
                InputNodes.Remove(actualNode);
                actualNode = FindNearestNeighbour(actualNode);
            }
            Distance += CalculateDistance(OutputNodes.Last(), OutputNodes.First());
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