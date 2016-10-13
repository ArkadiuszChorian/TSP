using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TSP.Algorithms.ConstructionAlgorithms;
using TSP.SolutionConstruction;

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

            //var repository = new DAL();
            //repository.ReadFromFile();            
            //repository.PrepareFileToWrite();          

            //Console.WriteLine("---Nearest Neighbour---");
            //var nearestNeighbour = new NearestNeighbour(repository.Nodes);
            //var nearestNeighbourData = GetData(nearestNeighbour);
            //var drawer = new Drawer(repository.Nodes);
            //repository.WriteToFile(nearestNeighbour, nearestNeighbourData, "---Nearest Neighbour---");
            //drawer.DrawChart("NearestNeighbour.bmp", repository.Nodes, nearestNeighbourData.BestRoute);

            //Console.WriteLine("---Greedy Cycle---");
            //var greedyCycle = new GreedyCycle(repository.Nodes);
            //var greedyCycleData = GetData(greedyCycle);
            //repository.WriteToFile(greedyCycle, greedyCycleData, "---Greedy Cycle---");
            //drawer.DrawChart("GreedyCycle.bmp", repository.Nodes, greedyCycleData.BestRoute);

            //Console.WriteLine("---Grasp Nearest Neighbour---");
            //var nearestNeighbourGrasp = new NearestNeighbourGrasp(repository.Nodes);
            //var nearestNeighbourGraspData = GetData(nearestNeighbourGrasp);
            //repository.WriteToFile(nearestNeighbourGrasp, nearestNeighbourGraspData, "---Nearest Neighbour Grasp---");
            //drawer.DrawChart("GraspNN.bmp", repository.Nodes, nearestNeighbourGraspData.BestRoute);

            //Console.WriteLine("---Grasp Greedy Cycle---");
            //var greedyCycleGrasp = new GreedyCycleGrasp(repository.Nodes);
            //var greedyCycleGraspData = GetData(greedyCycleGrasp);
            //repository.WriteToFile(greedyCycleGrasp, greedyCycleGraspData, "---Greedy Cycle Grasp---");
            //drawer.DrawChart("GraspGC.bmp", repository.Nodes, greedyCycleGraspData.BestRoute);

            //repository.CloseFileToWrite();
            DAL.Instance.CloseFileToWrite();
          
            Console.ReadKey();
        }

        //private static AlgorithmResultData GetData(ConstructionAlgorithm constructionAlgorithm)
        //{
        //    var data = new AlgorithmResultData();
        //    for ( var i = 0; i < constructionAlgorithm.ClonedNodes.Count; i++ )
        //    {
        //        constructionAlgorithm.FindRoute(constructionAlgorithm.InputNodes[i]);
        //        data.AccumulatedDistance += constructionAlgorithm.Distance;

        //        if ( constructionAlgorithm.Distance > data.MaximumDistance )
        //        {
        //            data.MaximumDistance = constructionAlgorithm.Distance;
        //        }

        //        if ( constructionAlgorithm.Distance < data.MinimumDistance )
        //        {
        //            data.MinimumDistance = constructionAlgorithm.Distance;
        //            data.BestRoute = constructionAlgorithm.OutputNodes.CloneList();
        //        }

        //        constructionAlgorithm.ResetAlgorithm();
        //    }         
                                                  
        //    return data;
        //}
    }
}
