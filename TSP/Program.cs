using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TSP
{
    class Program
    {
        static void Main()
        {
            var repository = new Repository();
            repository.ReadFromFile();            
            repository.PrepareFileToWrite();

            Console.WriteLine("---Nearest Neighbour---");
            var nearestNeighbour = new NearestNeighbour(repository.Nodes);
            var nearestNeighbourData = GetData(nearestNeighbour);
            var drawer = new Drawer(repository.Nodes);
            repository.WriteToFile(nearestNeighbour, nearestNeighbourData, "---Nearest Neighbour---");
            drawer.DrawChart("NearestNeighbour.bmp", repository.Nodes, nearestNeighbourData.BestRoute);
            
            Console.WriteLine("---Greedy Cycle---");
            var greedyCycle = new GreedyCycle(repository.Nodes);
            var greedyCycleData = GetData(greedyCycle);
            repository.WriteToFile(greedyCycle, greedyCycleData, "---Greedy Cycle---");
            drawer.DrawChart("GreedyCycle.bmp", repository.Nodes, greedyCycleData.BestRoute);

            Console.WriteLine("---Grasp Nearest Neighbour---");
            var nearestNeighbourGrasp = new NearestNeighbourGrasp(repository.Nodes);
            var nearestNeighbourGraspData = GetData(nearestNeighbourGrasp);
            repository.WriteToFile(nearestNeighbourGrasp, nearestNeighbourGraspData, "---Nearest Neighbour Grasp---");
            drawer.DrawChart("GraspNN.bmp", repository.Nodes, nearestNeighbourGraspData.BestRoute);

            Console.WriteLine("---Grasp Greedy Cycle---");
            var greedyCycleGrasp = new GreedyCycleGrasp(repository.Nodes);
            var greedyCycleGraspData = GetData(greedyCycleGrasp);
            repository.WriteToFile(greedyCycleGrasp, greedyCycleGraspData, "---Greedy Cycle Grasp---");
            drawer.DrawChart("GraspGC.bmp", repository.Nodes, greedyCycleGraspData.BestRoute);

            repository.CloseFileToWrite();
          
            Console.ReadKey();
        }

        private static Data GetData(Algorithm algorithm)
        {
            var data = new Data();
            for ( var i = 0; i < algorithm.ClonedNodes.Count; i++ )
            {
                algorithm.FindRoute(algorithm.InputNodes[i]);
                data.AccumulatedDistance += algorithm.Distance;

                if ( algorithm.Distance > data.MaximumDistance )
                {
                    data.MaximumDistance = algorithm.Distance;
                }

                if ( algorithm.Distance < data.MinimumDistance )
                {
                    data.MinimumDistance = algorithm.Distance;
                    data.BestRoute = algorithm.OutputNodes.CloneList();
                }

                algorithm.ResetAlgorithm();
            }         
                                                  
            return data;
        }
    }
}
