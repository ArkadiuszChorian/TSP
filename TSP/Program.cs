using System;
using System.Collections.Generic;
using System.Linq;
using TSP.Algorithms;
using TSP.Algorithms.ConstructionAlgorithms;
using TSP.Algorithms.OptimalizationAlgorithms;
using TSP.Engines;
using TSP.Models;

namespace TSP
{
    class Program
    {
        static void Main()
        {
            DAL.Instance.ReadFromFile();
            //DAL.Instance.PrepareFileToWrite();
            
            var sessionExecutionEngine = new AlgorithmExecutionEngine();

            var nearestNeighbourExecutionSession = new AlgorithmExecutionSession(new NearestNeighbour(), new LocalSearch());
            var greedyCycleExecutionSession = new AlgorithmExecutionSession(new GreedyCycle(), new LocalSearch());
            var nearestNeighbourGraspExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new LocalSearch());
            var greedyCycleGraspExecutionSession = new AlgorithmExecutionSession(new GreedyCycleGrasp(), new LocalSearch());
            var randomSolutionExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new LocalSearch());
            var multipleStartLocalSearchExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new MultipleStartLocalSearch());
            var iteratedLocalSearchExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new IteratedLocalSearch());
            //var multipleStartLocalSearchExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new MultipleStartLocalSearch());
            //var iteratedLocalSearchExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new IteratedLocalSearch());

            //sessionExecutionEngine.ExecuteDefaultSession(nearestNeighbourExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(greedyCycleExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(nearestNeighbourGraspExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(greedyCycleGraspExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(randomSolutionExecutionSession);
            //sessionExecutionEngine.ExecuteMultipleStartLocalSearchSession(multipleStartLocalSearchExecutionSession);
            //sessionExecutionEngine.ExecuteIteratedLocalSearchSession(iteratedLocalSearchExecutionSession);

            var drawer = new Drawer();

            //Console.WriteLine(Constants.NearestNeighbourText);
            //DAL.Instance.WriteToFile(nearestNeighbourExecutionSession, Constants.NearestNeighbourText);
            //drawer.DrawChart(Constants.NearestNeighbourFilename, nearestNeighbourExecutionSession.ConstructionStatisticsData.BestRoute);
            //drawer.DrawChart(Constants.NearestNeighbourOptimalizedFilename, nearestNeighbourExecutionSession.OptimalizationStatisticsData.BestRoute);

            //Console.WriteLine(Constants.GreedyCycleText);
            //DAL.Instance.WriteToFile(greedyCycleExecutionSession, Constants.GreedyCycleText);
            //drawer.DrawChart(Constants.GreedyCycleFilename, greedyCycleExecutionSession.ConstructionStatisticsData.BestRoute);
            //drawer.DrawChart(Constants.GreedyCycleOptimalizedFilename, greedyCycleExecutionSession.OptimalizationStatisticsData.BestRoute);

            //Console.WriteLine(Constants.NearestNeighboutGraspText);
            //DAL.Instance.WriteToFile(nearestNeighbourGraspExecutionSession, Constants.NearestNeighboutGraspText);
            //drawer.DrawChart(Constants.NearestNeighbourGraspFilename, nearestNeighbourGraspExecutionSession.ConstructionStatisticsData.BestRoute);
            //drawer.DrawChart(Constants.NearestNeighbourGraspOptimalizedFilename, nearestNeighbourGraspExecutionSession.OptimalizationStatisticsData.BestRoute);

            //Console.WriteLine(Constants.GreedyCycleGraspText);
            //DAL.Instance.WriteToFile(greedyCycleGraspExecutionSession, Constants.GreedyCycleGraspText);
            //drawer.DrawChart(Constants.GreedyCycleGraspFilename, greedyCycleGraspExecutionSession.ConstructionStatisticsData.BestRoute);
            //drawer.DrawChart(Constants.GreedyCycleGraspOptimalizedFilename, greedyCycleGraspExecutionSession.OptimalizationStatisticsData.BestRoute);

            //Console.WriteLine(Constants.RandomSolutionText);
            //DAL.Instance.WriteToFile(randomSolutionExecutionSession, Constants.RandomSolutionText);
            //drawer.DrawChart(Constants.RandomSolutionFilename, randomSolutionExecutionSession.ConstructionStatisticsData.BestRoute);
            //drawer.DrawChart(Constants.RandomSolutionOptimalizedFilename, randomSolutionExecutionSession.OptimalizationStatisticsData.BestRoute);

            //Console.WriteLine(Constants.MultipleStartLocalSearchText);
            //DAL.Instance.WriteToFile(multipleStartLocalSearchExecutionSession, Constants.MultipleStartLocalSearchText);
            //drawer.DrawChart(Constants.MultipleStartLocalSearchFilename, multipleStartLocalSearchExecutionSession.OptimalizationStatisticsData.BestRoute);

            //Console.WriteLine(Constants.IteratedLocalSearchText);
            //DAL.Instance.WriteToFile(iteratedLocalSearchExecutionSession, Constants.IteratedLocalSearchText);
            //drawer.DrawChart(Constants.IteratedLocalSearchFilename, iteratedLocalSearchExecutionSession.OptimalizationStatisticsData.BestRoute);

            //var LsExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new LocalSearch());

            var localSearchesResults = new List<AlgorithmOperatingData>();          
            //var bestLocalSearchResult = new AlgorithmOperatingData {Distance = int.MaxValue};
            var bestLocalSearchDistance = int.MaxValue;
            var bestLocalSearchIndex = 0;
            var localSearch = new LocalSearch {ConstructionAlgorithm = new RandomSolution()};

            for (var i = 0; i < 10; i++)
            {
                localSearch.ConstructionAlgorithm.ResetAlgorithm();
                localSearch.ResetAlgorithm();
                localSearch.ConstructionAlgorithm.FindRoute(localSearch.ConstructionAlgorithm.OperatingData.UnusedNodes[0]);
                localSearch.OperatingData = localSearch.ConstructionAlgorithm.OperatingData.CloneData();
                localSearch.Optimize();
                var temporaryList = localSearch.OperatingData.CloneData();
                localSearchesResults.Add(temporaryList);
                if (localSearch.OperatingData.Distance >= bestLocalSearchDistance) continue;
                bestLocalSearchDistance = localSearch.OperatingData.Distance;
                bestLocalSearchIndex = i;
            }

            var averangeSimilarityToOthersBySharedVertices = new List<double>(new double[localSearchesResults.Count]);
            var averangeSimilarityToOthersBySharedEdges = new List<double>(new double[localSearchesResults.Count]);           
            var similarityToBestBySharedVertices = new List<double>(new double[localSearchesResults.Count]);
            var similarityToBestBySharedEdges = new List<double>(new double[localSearchesResults.Count]);

            for (var i = 0; i < localSearchesResults.Count; i++)
            {               
                for (var j = 0; j < localSearchesResults.Count; j++)
                {
                    if (i == j) continue;
                    averangeSimilarityToOthersBySharedVertices[i] += Comparer.SharedVertices(localSearchesResults[i].PathNodes, localSearchesResults[j].PathNodes);
                    averangeSimilarityToOthersBySharedEdges[i] += Comparer.SharedEdges(localSearchesResults[i].PathNodes, localSearchesResults[j].PathNodes);

                }
                if (i == bestLocalSearchIndex) continue;
                similarityToBestBySharedVertices[i] = Comparer.SharedVertices(localSearchesResults[i].PathNodes, localSearchesResults[bestLocalSearchIndex].PathNodes);
                similarityToBestBySharedEdges[i] = Comparer.SharedEdges(localSearchesResults[i].PathNodes, localSearchesResults[bestLocalSearchIndex].PathNodes);
            }

            averangeSimilarityToOthersBySharedVertices = averangeSimilarityToOthersBySharedVertices.Select(avg => avg / localSearchesResults.Count - 1) as List<double>;
            averangeSimilarityToOthersBySharedEdges = averangeSimilarityToOthersBySharedEdges.Select(avg => avg / localSearchesResults.Count - 1) as List<double>;



            //DAL.Instance.CloseFileToWrite();
          
            Console.ReadKey();
        }
    }
}
