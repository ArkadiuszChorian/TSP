﻿using System;
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
            DAL.Instance.PrepareFileToWrite();
            
            var sessionExecutionEngine = new AlgorithmExecutionEngine();

            var nearestNeighbourExecutionSession = new AlgorithmExecutionSession(new NearestNeighbour(), new LocalSearch());
            var greedyCycleExecutionSession = new AlgorithmExecutionSession(new GreedyCycle(), new LocalSearch());
            var nearestNeighbourGraspExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new LocalSearch());
            var greedyCycleGraspExecutionSession = new AlgorithmExecutionSession(new GreedyCycleGrasp(), new LocalSearch());
            var randomSolutionExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new LocalSearch());
            //var multipleStartLocalSearchExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new MultipleStartLocalSearch());
            //var iteratedLocalSearchExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new IteratedLocalSearch());
            var multipleStartLocalSearchExecutionSession = new AlgorithmExecutionSession(new NearestNeighbour(), new MultipleStartLocalSearch());
            var iteratedLocalSearchExecutionSession = new AlgorithmExecutionSession(new NearestNeighbour(), new IteratedLocalSearch());
            //var multipleStartLocalSearchExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new MultipleStartLocalSearch());
            //var iteratedLocalSearchExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new IteratedLocalSearch());

            //sessionExecutionEngine.ExecuteDefaultSession(nearestNeighbourExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(greedyCycleExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(nearestNeighbourGraspExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(greedyCycleGraspExecutionSession);
            //sessionExecutionEngine.ExecuteDefaultSession(randomSolutionExecutionSession);
            sessionExecutionEngine.ExecuteMultipleStartLocalSearchSession(multipleStartLocalSearchExecutionSession);
            sessionExecutionEngine.ExecuteIteratedLocalSearchSession(iteratedLocalSearchExecutionSession);

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

            Console.WriteLine(Constants.MultipleStartLocalSearchText);
            DAL.Instance.WriteToFile(multipleStartLocalSearchExecutionSession, Constants.MultipleStartLocalSearchText);
            drawer.DrawChart(Constants.MultipleStartLocalSearchFilename, multipleStartLocalSearchExecutionSession.OptimalizationStatisticsData.BestRoute);

            Console.WriteLine(Constants.IteratedLocalSearchText);
            DAL.Instance.WriteToFile(iteratedLocalSearchExecutionSession, Constants.IteratedLocalSearchText);
            drawer.DrawChart(Constants.IteratedLocalSearchFilename, iteratedLocalSearchExecutionSession.OptimalizationStatisticsData.BestRoute);

            //var LsExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new LocalSearch());

            DAL.Instance.CloseFileToWrite();
          
            Console.ReadKey();
        }
    }
}
