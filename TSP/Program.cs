using System;
using TSP.Algorithms.ConstructionAlgorithms;
using TSP.Engines;

namespace TSP
{
    class Program
    {
        static void Main()
        {
            DAL.Instance.ReadFromFile();
            DAL.Instance.PrepareFileToWrite();
            
            var solutionConstructionEngine = new SolutionConstructionEngine();
            solutionConstructionEngine.AddExecutionSession(typeof(NearestNeighbour));
            solutionConstructionEngine.AddExecutionSession(typeof(NearestNeighbourGrasp));
            solutionConstructionEngine.AddExecutionSession(typeof(GreedyCycle));
            solutionConstructionEngine.AddExecutionSession(typeof(GreedyCycleGrasp));
            solutionConstructionEngine.AddExecutionSession(typeof(RandomSolution));

            var nearestNeighbourExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(NearestNeighbour)];
            var greedyCycleExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(GreedyCycle)];
            var nearestNeighbourGraspExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(NearestNeighbourGrasp)];
            var greedyCycleGraspExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(GreedyCycleGrasp)];
            var randomSolutionExecutionSession = solutionConstructionEngine.AlgorithmsExecutionSessions[typeof(RandomSolution)];

            solutionConstructionEngine.ExecuteAlgorithm(typeof(NearestNeighbour));          
            solutionConstructionEngine.ExecuteAlgorithm(typeof(GreedyCycle));            
            solutionConstructionEngine.ExecuteAlgorithm(typeof(NearestNeighbourGrasp));            
            solutionConstructionEngine.ExecuteAlgorithm(typeof(GreedyCycleGrasp));            
            solutionConstructionEngine.ExecuteAlgorithm(typeof(RandomSolution));

            var drawer = new Drawer();

            Console.WriteLine(Constants.NearestNeighbourText);          
            DAL.Instance.WriteToFile(nearestNeighbourExecutionSession, Constants.NearestNeighbourText);
            drawer.DrawChart(Constants.NearestNeighbourFilename, nearestNeighbourExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine(Constants.GreedyCycleText);            
            DAL.Instance.WriteToFile(greedyCycleExecutionSession, Constants.GreedyCycleText);
            drawer.DrawChart(Constants.GreedyCycleFilename, greedyCycleExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine(Constants.NearestNeighboutGraspText);            
            DAL.Instance.WriteToFile(nearestNeighbourGraspExecutionSession, Constants.NearestNeighboutGraspText);
            drawer.DrawChart(Constants.NearestNeighbourGraspFilename, nearestNeighbourGraspExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine(Constants.GreedyCycleGraspText);            
            DAL.Instance.WriteToFile(greedyCycleGraspExecutionSession, Constants.GreedyCycleGraspText);
            drawer.DrawChart(Constants.GreedyCycleGraspFilename, greedyCycleGraspExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine(Constants.RandomSolutionText);            
            DAL.Instance.WriteToFile(randomSolutionExecutionSession, Constants.RandomSolutionText);
            drawer.DrawChart(Constants.RandomSolutionFilename, randomSolutionExecutionSession.AlgorithmResultData.BestRoute);

            DAL.Instance.CloseFileToWrite();
          
            Console.ReadKey();
        }
    }
}
