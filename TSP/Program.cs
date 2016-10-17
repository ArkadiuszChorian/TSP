using System;
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
            
            var sessionExecutionEngine = new SessionExecutionEngine();
            //solutionConstructionEngine.AddExecutionSession(typeof(NearestNeighbour));
            //solutionConstructionEngine.AddExecutionSession(typeof(NearestNeighbourGrasp));
            //solutionConstructionEngine.AddExecutionSession(typeof(GreedyCycle));
            //solutionConstructionEngine.AddExecutionSession(typeof(GreedyCycleGrasp));
            //solutionConstructionEngine.AddExecutionSession(typeof(RandomSolution));

            //var nearestNeighbourExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(NearestNeighbour)];
            //var greedyCycleExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(GreedyCycle)];
            //var nearestNeighbourGraspExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(NearestNeighbourGrasp)];
            //var greedyCycleGraspExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(GreedyCycleGrasp)];
            //var randomSolutionExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(RandomSolution)];

            var nearestNeighbourExecutionSession = new AlgorithmExecutionSession(new NearestNeighbour(), new LocalSearch());
            var greedyCycleExecutionSession = new AlgorithmExecutionSession(new GreedyCycle(), new LocalSearch());
            var nearestNeighbourGraspExecutionSession = new AlgorithmExecutionSession(new NearestNeighbourGrasp(), new LocalSearch());
            var greedyCycleGraspExecutionSession = new AlgorithmExecutionSession(new GreedyCycleGrasp(), new LocalSearch());
            var randomSolutionExecutionSession = new AlgorithmExecutionSession(new RandomSolution(), new LocalSearch());

            //solutionConstructionEngine.ExecuteSession(typeof(NearestNeighbour));          
            //solutionConstructionEngine.ExecuteSession(typeof(GreedyCycle));            
            //solutionConstructionEngine.ExecuteSession(typeof(NearestNeighbourGrasp));            
            //solutionConstructionEngine.ExecuteSession(typeof(GreedyCycleGrasp));            
            //solutionConstructionEngine.ExecuteSession(typeof(RandomSolution));

            sessionExecutionEngine.ExecuteSession(nearestNeighbourExecutionSession);
            sessionExecutionEngine.ExecuteSession(greedyCycleExecutionSession);
            sessionExecutionEngine.ExecuteSession(nearestNeighbourGraspExecutionSession);
            sessionExecutionEngine.ExecuteSession(greedyCycleGraspExecutionSession);
            sessionExecutionEngine.ExecuteSession(randomSolutionExecutionSession);

            var drawer = new Drawer();

            Console.WriteLine(Constants.NearestNeighbourText);          
            DAL.Instance.WriteToFile(nearestNeighbourExecutionSession, Constants.NearestNeighbourText);
            drawer.DrawChart(Constants.NearestNeighbourFilename, nearestNeighbourExecutionSession.ConstructionStatisticsData.BestRoute);

            Console.WriteLine(Constants.GreedyCycleText);            
            DAL.Instance.WriteToFile(greedyCycleExecutionSession, Constants.GreedyCycleText);
            drawer.DrawChart(Constants.GreedyCycleFilename, greedyCycleExecutionSession.ConstructionStatisticsData.BestRoute);

            Console.WriteLine(Constants.NearestNeighboutGraspText);            
            DAL.Instance.WriteToFile(nearestNeighbourGraspExecutionSession, Constants.NearestNeighboutGraspText);
            drawer.DrawChart(Constants.NearestNeighbourGraspFilename, nearestNeighbourGraspExecutionSession.ConstructionStatisticsData.BestRoute);

            Console.WriteLine(Constants.GreedyCycleGraspText);            
            DAL.Instance.WriteToFile(greedyCycleGraspExecutionSession, Constants.GreedyCycleGraspText);
            drawer.DrawChart(Constants.GreedyCycleGraspFilename, greedyCycleGraspExecutionSession.ConstructionStatisticsData.BestRoute);

            Console.WriteLine(Constants.RandomSolutionText);            
            DAL.Instance.WriteToFile(randomSolutionExecutionSession, Constants.RandomSolutionText);
            drawer.DrawChart(Constants.RandomSolutionFilename, randomSolutionExecutionSession.ConstructionStatisticsData.BestRoute);

            //OptimalizationAlgorithm a = new OptimalizationAlgorithm(DAL.Instance.AlgorithmsData[0]);
            //a.SwapPaths();

            DAL.Instance.CloseFileToWrite();
          
            Console.ReadKey();
        }
    }
}
