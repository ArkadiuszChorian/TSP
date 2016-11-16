using System;
using System.Collections.Generic;
using System.Linq;
using TSP;

namespace TSP2
{
    class Program
    {
        private const int NumberOfLocalSearchResults = 1000;
        static void Main(string[] args)
        {
            //DAL.Instance.ReadFromFile();
            //DAL.Instance.PrepareFileToWrite();

            //var randomSolution = new RandomSolution();
            //var localSearch = new LocalSearch();
            //var currentOperatingAndStatisticsData = new OperatingAndStatisticsData();
            //var optimizedResults = new List<OperatingAndStatisticsData>();
            //var bestOptimizedResultDistance = int.MaxValue;
            //var bestOptimizedResultIndex = 0;

            //for (var i = 0; i < NumberOfLocalSearchResults; i++)
            //{
            //    currentOperatingAndStatisticsData.ClearData();
            //    currentOperatingAndStatisticsData.OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
            //    currentOperatingAndStatisticsData.OperatingData =
            //        localSearch.Optimize(randomSolution.FindRouteFromRandomStart(currentOperatingAndStatisticsData.OperatingData));
            //    optimizedResults.Add(currentOperatingAndStatisticsData.CloneData());

            //    if (currentOperatingAndStatisticsData.OperatingData.Distance >= bestOptimizedResultDistance) continue;
            //    bestOptimizedResultDistance = currentOperatingAndStatisticsData.OperatingData.Distance;
            //    bestOptimizedResultIndex = i;
            //}

            //for (var i = 0; i < optimizedResults.Count; i++)
            //{
            //    for (var j = 0; j < optimizedResults.Count; j++)
            //    {
            //        if (i == j) continue;
            //        optimizedResults[i].AverangeSimilarityToOthersBySharedVertices += Comparer.SharedVerticesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[j].OperatingData.PathNodes);
            //        optimizedResults[i].AverangeSimilarityToOthersBySharedEdges += Comparer.SharedEdgesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[j].OperatingData.PathNodes);

            //    }
            //    if (i == bestOptimizedResultIndex) continue;
            //    optimizedResults[i].SimilarityToBestBySharedVertices = Comparer.SharedVerticesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[bestOptimizedResultIndex].OperatingData.PathNodes);
            //    optimizedResults[i].SimilarityToBestBySharedEdges = Comparer.SharedEdgesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[bestOptimizedResultIndex].OperatingData.PathNodes);
            //}

            //optimizedResults.ForEach(result => result.AverangeSimilarityToOthersBySharedVertices = result.AverangeSimilarityToOthersBySharedVertices / (optimizedResults.Count - 1));
            //optimizedResults.ForEach(result => result.AverangeSimilarityToOthersBySharedEdges = result.AverangeSimilarityToOthersBySharedEdges / (optimizedResults.Count - 1));

            //DAL.Instance.WriteToFile(optimizedResults);
            //DAL.Instance.CloseFileToWrite();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}

