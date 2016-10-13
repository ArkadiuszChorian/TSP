using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Console.WriteLine("---Nearest Neighbour---");          
            DAL.Instance.WriteToFile(nearestNeighbourExecutionSession, "---Nearest Neighbour---");
            drawer.DrawChart("NearestNeighbour.bmp", nearestNeighbourExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine("---Greedy Cycle---");            
            DAL.Instance.WriteToFile(greedyCycleExecutionSession, "---Greedy Cycle---");
            drawer.DrawChart("GreedyCycle.bmp", greedyCycleExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine("---Nearest Neighbour Grasp---");            
            DAL.Instance.WriteToFile(nearestNeighbourGraspExecutionSession, "---Nearest Neighbour Grasp---");
            drawer.DrawChart("NearestNeighbourGrasp.bmp", nearestNeighbourGraspExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine("---Greedy Cycle Grasp---");            
            DAL.Instance.WriteToFile(greedyCycleGraspExecutionSession, "---Greedy Cycle Grasp---");
            drawer.DrawChart("GreedyCycleGrasp.bmp", greedyCycleGraspExecutionSession.AlgorithmResultData.BestRoute);

            Console.WriteLine("---Random Solution---");            
            DAL.Instance.WriteToFile(randomSolutionExecutionSession, "---Random Solution---");
            drawer.DrawChart("RandomSolution.bmp", randomSolutionExecutionSession.AlgorithmResultData.BestRoute);

            DAL.Instance.CloseFileToWrite();
          
            Console.ReadKey();
        }
    }
}
