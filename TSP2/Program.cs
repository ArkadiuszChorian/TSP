using System;
using System.Collections.Generic;
using System.Linq;
using TSP;

namespace TSP2
{
    class Program
    {
        private const int NumberOfLocalSearchResults = 100;
        static void Main(string[] args)
        {
            DAL.Instance.ReadFromFile();
            DAL.Instance.PrepareFileToWrite();

            var randomSolution = new RandomSolution();
            var localSearch = new LocalSearch();
            //var currentOperatingData = new OperatingData();
            //var optimizedResults = new List<OperatingData>();
            var currentOperatingAndStatisticsData = new OperatingAndStatisticsData();
            var optimizedResults = new List<OperatingAndStatisticsData>();
            //var bestLocalSearchResult = new AlgorithmOperatingData {Distance = int.MaxValue};
            var bestOptimizedResultDistance = int.MaxValue;
            var bestOptimizedResultIndex = 0;

            //currentOperatingAndStatisticsData.OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
            //currentOperatingAndStatisticsData.OperatingData = randomSolution.FindRouteFromRandomStart(currentOperatingAndStatisticsData.OperatingData);

            //var tmpOperatingAndStatsData = currentOperatingAndStatisticsData.CloneData();

            //var tmp = currentOperatingAndStatisticsData.OperatingData.PathNodes[3];
            //currentOperatingAndStatisticsData.OperatingData.PathNodes[3] =
            //    currentOperatingAndStatisticsData.OperatingData.UnusedNodes[3];
            //currentOperatingAndStatisticsData.OperatingData.UnusedNodes[3] = tmp;

            //Console.WriteLine(Comparer.SharedEdges(tmpOperatingAndStatsData.OperatingData.PathNodes, currentOperatingAndStatisticsData.OperatingData.PathNodes));
            //Console.WriteLine(Comparer.SharedVertices(tmpOperatingAndStatsData.OperatingData.PathNodes, currentOperatingAndStatisticsData.OperatingData.PathNodes));

            for (var i = 0; i < NumberOfLocalSearchResults; i++)
            {
                currentOperatingAndStatisticsData.ClearData();
                currentOperatingAndStatisticsData.OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
                currentOperatingAndStatisticsData.OperatingData =
                    localSearch.Optimize(randomSolution.FindRouteFromRandomStart(currentOperatingAndStatisticsData.OperatingData));
                optimizedResults.Add(currentOperatingAndStatisticsData.CloneData());

                if (currentOperatingAndStatisticsData.OperatingData.Distance >= bestOptimizedResultDistance) continue;
                bestOptimizedResultDistance = currentOperatingAndStatisticsData.OperatingData.Distance;
                bestOptimizedResultIndex = i;
            }

            for (var i = 0; i < optimizedResults.Count; i++)
            {
                for (var j = 0; j < optimizedResults.Count; j++)
                {
                    if (i == j) continue;
                    optimizedResults[i].AverangeSimilarityToOthersBySharedVertices += Comparer.SharedVertices(optimizedResults[i].OperatingData.PathNodes, optimizedResults[j].OperatingData.PathNodes);
                    optimizedResults[i].AverangeSimilarityToOthersBySharedEdges += Comparer.SharedEdges(optimizedResults[i].OperatingData.PathNodes, optimizedResults[j].OperatingData.PathNodes);

                }
                if (i == bestOptimizedResultIndex) continue;
                optimizedResults[i].SimilarityToBestBySharedVertices = Comparer.SharedVertices(optimizedResults[i].OperatingData.PathNodes, optimizedResults[bestOptimizedResultIndex].OperatingData.PathNodes);
                optimizedResults[i].SimilarityToBestBySharedEdges = Comparer.SharedEdges(optimizedResults[i].OperatingData.PathNodes, optimizedResults[bestOptimizedResultIndex].OperatingData.PathNodes);
            }

            optimizedResults.ForEach(result => result.AverangeSimilarityToOthersBySharedVertices = result.AverangeSimilarityToOthersBySharedVertices / optimizedResults.Count - 1);
            optimizedResults.ForEach(result => result.AverangeSimilarityToOthersBySharedEdges = result.AverangeSimilarityToOthersBySharedEdges / optimizedResults.Count - 1);

            DAL.Instance.WriteToFile(optimizedResults);
            DAL.Instance.CloseFileToWrite();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}

