using System;
using System.Collections.Generic;
using TSP.Models;

namespace TSP2
{
    public class LocalSearch : Algorithm
    {
        public int SwapPathsFirstIndex { get; set; }
        public int SwapPathsSecondIndex { get; set; }
        public int SwapVerticesPathNodeIndex { get; set; }
        public int SwapVerticesUnusedNodeIndex { get; set; }
        public int BestSwapPathsDistance { get; set; } = int.MaxValue;
        public int BestSwapVerticesDistance { get; set; } = int.MaxValue;
        public bool PathsChangeMade { get; set; } = true;
        public bool VerticesChangeMade { get; set; } = true;

        protected void CheckSwapPaths(int firstIndex, int secondIndex, OperatingData operatingData)
        {
            var totalDistance = operatingData.Distance;

            var oldDistance1 = CalculateDistance(operatingData.PathNodes[firstIndex], operatingData.PathNodes[firstIndex + 1]);
            var oldDistance2 = CalculateDistance(operatingData.PathNodes[secondIndex],
                operatingData.PathNodes[secondIndex >= operatingData.PathNodes.Count - 1 ? 0 : secondIndex + 1]);
            totalDistance -= ( oldDistance1 + oldDistance2 );

            var newDistance1 = CalculateDistance(operatingData.PathNodes[firstIndex], operatingData.PathNodes[secondIndex]);
            var newDistance2 = CalculateDistance(operatingData.PathNodes[firstIndex + 1],
                operatingData.PathNodes[secondIndex >= operatingData.PathNodes.Count - 1 ? 0 : secondIndex + 1]);
            totalDistance += ( newDistance1 + newDistance2 );

            if ( totalDistance >= operatingData.Distance || totalDistance >= BestSwapPathsDistance ) return;
            BestSwapPathsDistance = totalDistance;
            SwapPathsFirstIndex = firstIndex;
            SwapPathsSecondIndex = secondIndex;
            PathsChangeMade = true;
        }

        protected void FindBestSwapPaths(OperatingData operatingData)
        {
            for ( var i = 0; i < operatingData.PathNodes.Count - 2; i++ )
            {
                for ( var j = i + 2; j < operatingData.PathNodes.Count; j++ )
                {
                    if ( i == 0 && j == operatingData.PathNodes.Count - 1 ) continue;
                    CheckSwapPaths(i, j, operatingData);
                }
            }
        }

        protected void CheckSwapVertices(int pathNodeIndex, int unusedNodeIndex, OperatingData operatingData)
        {
            var totalDistance = operatingData.Distance;
            var previousPathNodeIndex = pathNodeIndex - 1 < 0 ? operatingData.PathNodes.Count - 1 : pathNodeIndex - 1;
            var nextPathNodeIndex = pathNodeIndex + 1 > operatingData.PathNodes.Count - 1 ? 0 : pathNodeIndex + 1;

            totalDistance -=
                CalculateDistance(operatingData.PathNodes[previousPathNodeIndex], operatingData.PathNodes[pathNodeIndex]) +
                CalculateDistance(operatingData.PathNodes[pathNodeIndex], operatingData.PathNodes[nextPathNodeIndex]);

            totalDistance +=
                CalculateDistance(operatingData.PathNodes[previousPathNodeIndex], operatingData.UnusedNodes[unusedNodeIndex]) +
                CalculateDistance(operatingData.UnusedNodes[unusedNodeIndex], operatingData.PathNodes[nextPathNodeIndex]);

            if ( totalDistance >= operatingData.Distance || totalDistance >= BestSwapVerticesDistance ) return;
            BestSwapVerticesDistance = totalDistance;
            SwapVerticesPathNodeIndex = pathNodeIndex;
            SwapVerticesUnusedNodeIndex = unusedNodeIndex;
            VerticesChangeMade = true;
        }

        protected void FindBestSwapVertices(OperatingData operatingData)
        {
            for ( var i = 0; i < operatingData.PathNodes.Count; i++ )
            {
                for ( var j = 0; j < operatingData.UnusedNodes.Count; j++ )
                {
                    CheckSwapVertices(i, j, operatingData);
                }
            }
        }

        protected void SwapPaths(OperatingData operatingData)
        {
            var newPath = (List<Node>)operatingData.PathNodes;
            newPath.Reverse(SwapPathsFirstIndex + 1, Math.Abs(SwapPathsSecondIndex - SwapPathsFirstIndex));
            operatingData.Distance = BestSwapPathsDistance;
        }

        protected void SwapVertices(OperatingData operatingData)
        {
            var newNode = operatingData.UnusedNodes[SwapVerticesUnusedNodeIndex];
            var oldNode = operatingData.PathNodes[SwapVerticesPathNodeIndex];
            operatingData.PathNodes[SwapVerticesPathNodeIndex] = newNode;
            operatingData.UnusedNodes[SwapVerticesUnusedNodeIndex] = oldNode;
            operatingData.Distance = BestSwapVerticesDistance;
        }

        public void ResetAlgorithm()
        {
            VerticesChangeMade = true;
            PathsChangeMade = true;
            BestSwapPathsDistance = int.MaxValue;
            BestSwapVerticesDistance = int.MaxValue;
        }

        public OperatingData Optimize(OperatingData operatingData)
        {
            while ( VerticesChangeMade || PathsChangeMade )
            {
                if ( PathsChangeMade )
                {
                    PathsChangeMade = false;
                    FindBestSwapPaths(operatingData);
                }
                if ( VerticesChangeMade )
                {
                    VerticesChangeMade = false;
                    FindBestSwapVertices(operatingData);
                }

                if ( !VerticesChangeMade && !PathsChangeMade ) break;
                if ( BestSwapPathsDistance < BestSwapVerticesDistance )
                    SwapPaths(operatingData);
                else
                    SwapVertices(operatingData);
            }

            //ResetAlgorithm();
            operatingData.Distance = BestSwapPathsDistance < BestSwapVerticesDistance
                ? BestSwapPathsDistance
                : BestSwapVerticesDistance;

            ResetAlgorithm();

            return operatingData;
        }
    }
}
