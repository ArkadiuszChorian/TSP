using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Models;

namespace TSP.Algorithms.OptimalizationAlgorithms
{
    class LocalSearch : OptimalizationAlgorithm
    {
        public int SwapPathsFirstIndex { get; set; } 
        public int SwapPathsSecondIndex { get; set; } 
        public int SwapVerticesPathNodeIndex { get; set; } 
        public int SwapVerticesUnusedNodeIndex { get; set; }
        public int BestSwapPathsDistance { get; set; } = int.MaxValue;
        public int BestSwapVerticesDistance { get; set; } = int.MaxValue;
        public bool ChangeMade { get; set; } = true;

        private void CheckSwapPaths(int firstIndex, int secondIndex)
        {
            //if ((firstIndex == secondIndex) || ((firstIndex + 1) == secondIndex) || (firstIndex == (secondIndex + 1))) return false;
            //var changeMade = false;            

            var totalDistance = OperatingData.Distance;

            var oldDistance1 = CalculateDistance(OperatingData.PathNodes[firstIndex], OperatingData.PathNodes[firstIndex + 1]);
            var oldDistance2 = CalculateDistance(OperatingData.PathNodes[secondIndex], OperatingData.PathNodes[secondIndex + 1]);
            totalDistance -= (oldDistance1 + oldDistance2);

            var newDistance1 = CalculateDistance(OperatingData.PathNodes[firstIndex], OperatingData.PathNodes[secondIndex]);
            var newDistance2 = CalculateDistance(OperatingData.PathNodes[firstIndex + 1], OperatingData.PathNodes[secondIndex + 1]);
            totalDistance += (newDistance1 + newDistance2);

            if (totalDistance > OperatingData.Distance || totalDistance >= BestSwapPathsDistance) return;
            BestSwapPathsDistance = totalDistance;
            SwapPathsFirstIndex = firstIndex;
            SwapPathsSecondIndex = secondIndex;
            ChangeMade = true;

            //Console.WriteLine(totalDistance);
            //OperatingData.Distance = totalDistance;
            //changeMade = true;
            //var newList = (List<Node>)OperatingData.PathNodes;
            //newList.Reverse(firstIndex + 1, Math.Abs(secondIndex - firstIndex));
            //return true;
        }

        private void FindBestSwapPaths()
        {
            for (var i = 0; i < OperatingData.PathNodes.Count - 2; i++)
            {
                for (var j = i + 2; j < OperatingData.PathNodes.Count; j++)
                {
                    //if ((i == j) || (i + 1 == j) || (i == j + 1)) continue;
                    //if (i >= OperatingData.PathNodes.Count)
                    //{
                    //    i = 0 + (i - OperatingData.PathNodes.Count);
                    //}
                    if (i == 0 && j == OperatingData.PathNodes.Count - 1) continue;

                    //if (j >= OperatingData.PathNodes.Count)
                    //{
                    //    CheckSwapPaths(i, 0 + (j - OperatingData.PathNodes.Count));
                    //    continue;                       
                    //    //j = 0 + (j - OperatingData.PathNodes.Count);
                    //}

                    CheckSwapPaths(i, j);
                }
            }
        }

        private void CheckSwapVertices(int pathNodeIndex, int unusedNodeIndex)
        {
            var totalDistance = OperatingData.Distance;
            var previousPathNodeIndex = pathNodeIndex - 1 < 0 ? OperatingData.PathNodes.Count - 1 : pathNodeIndex - 1;
            var nextPathNodeIndex = pathNodeIndex + 1 > OperatingData.PathNodes.Count - 1 ? 0 : pathNodeIndex + 1;

            totalDistance -= 
                CalculateDistance(OperatingData.PathNodes[previousPathNodeIndex], OperatingData.PathNodes[pathNodeIndex]) + 
                CalculateDistance(OperatingData.PathNodes[pathNodeIndex], OperatingData.PathNodes[nextPathNodeIndex]);

            totalDistance +=
                CalculateDistance(OperatingData.PathNodes[previousPathNodeIndex], OperatingData.UnusedNodes[unusedNodeIndex]) +
                CalculateDistance(OperatingData.UnusedNodes[unusedNodeIndex], OperatingData.PathNodes[nextPathNodeIndex]);

            if (totalDistance > OperatingData.Distance || totalDistance >= BestSwapVerticesDistance) return;
            BestSwapVerticesDistance = totalDistance;
            SwapVerticesPathNodeIndex = pathNodeIndex;
            SwapVerticesUnusedNodeIndex = unusedNodeIndex;
            ChangeMade = true;
        }

        private void FindBestSwapVertices()
        {
            for (var i = 0; i < OperatingData.PathNodes.Count; i++)
            {
                for (var j = 0; j < OperatingData.UnusedNodes.Count; j++)
                {
                    CheckSwapVertices(i, j);
                }
            }
        }

        private void SwapPaths()
        {
            var newPath = (List<Node>)OperatingData.PathNodes;
            newPath.Reverse(SwapPathsFirstIndex + 1, Math.Abs(SwapPathsSecondIndex - SwapPathsFirstIndex));
        }

        private void SwapVertices()
        {
            var newNode = OperatingData.UnusedNodes[SwapVerticesUnusedNodeIndex];
            var oldNode = OperatingData.PathNodes[SwapVerticesPathNodeIndex];
            OperatingData.PathNodes[SwapVerticesPathNodeIndex] = newNode;
            OperatingData.UnusedNodes[SwapVerticesUnusedNodeIndex] = oldNode;
        }

        public void FindBestSwap()
        {
            while (ChangeMade)
            {
                ChangeMade = false;
                FindBestSwapPaths();
                FindBestSwapVertices();
                if (!ChangeMade) continue;
                if (BestSwapPathsDistance <= BestSwapVerticesDistance)
                {
                    SwapPaths();
                }
                else
                {
                    SwapVertices();
                }
            }
        }

        //public void SwapVertices()
        //{
        //    var changeMade = true;
        //    while (changeMade)
        //    {
        //        changeMade = false;
        //        for (var i = 0; i < OperatingData.PathNodes.Count - 1; i++)
        //        {
        //            for (var j = 0; j < OperatingData.PathNodes.Count - 1; j++)
        //            {
        //                if ((i == j) || ((i + 1) == j) || (i == (j + 1))) continue;

        //                var totalDistance = OperatingData.Distance;

        //                var oldDistance1 = CalculateDistance(OperatingData.PathNodes[i], OperatingData.PathNodes[i + 1]);
        //                var oldDistance2 = CalculateDistance(OperatingData.PathNodes[j], OperatingData.PathNodes[j + 1]);
        //                totalDistance -= (oldDistance1 + oldDistance2);

        //                var newDistance1 = CalculateDistance(OperatingData.PathNodes[i], OperatingData.PathNodes[j]);
        //                var newDistance2 = CalculateDistance(OperatingData.PathNodes[i + 1], OperatingData.PathNodes[j + 1]);
        //                totalDistance += (newDistance1 + newDistance2);

        //                if (totalDistance >= OperatingData.Distance) continue;

        //                Console.WriteLine(totalDistance);
        //                OperatingData.Distance = totalDistance;
        //                changeMade = true;
        //                var newList = (List<Node>)OperatingData.PathNodes;
        //                newList.Reverse(i + 1, Math.Abs(j - i));
        //            }
        //        }
        //    }
        //}
    }
}
