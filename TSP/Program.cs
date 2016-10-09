using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP
{
    class Program
    {
        const string FileName = "kroA100.tsp";
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            repository.ReadFromFile(FileName);

            var minimumDistance = int.MaxValue;
            var maximumDistance = 0;
            var accumulatedDistance = 0;
            List<Node> bestRoute = new List<Node>();

            NearestNeighbour nearestNeighbour = new NearestNeighbour(repository.Nodes);

            for (int i = 0; i < nearestNeighbour.ClonedNodes.Count; i++)
            {
                nearestNeighbour.FindRoute(nearestNeighbour.InputNodes[i]);
                accumulatedDistance += nearestNeighbour.Distance;

                if (nearestNeighbour.Distance > maximumDistance)
                {
                    maximumDistance = nearestNeighbour.Distance;
                }

                if (nearestNeighbour.Distance < minimumDistance)
                {
                    minimumDistance = nearestNeighbour.Distance;
                    var temporaryArray = new Node[nearestNeighbour.OutputNodes.Count];
                    nearestNeighbour.OutputNodes.CopyTo(temporaryArray);
                    bestRoute = temporaryArray.ToList();
                }

                nearestNeighbour.ResetAlgorithm();
            }

            Console.WriteLine("---Nearest Neighbour---");
            Console.WriteLine("MIN: " + minimumDistance);
            Console.WriteLine("AVG: " + accumulatedDistance/nearestNeighbour.ClonedNodes.Count);
            Console.WriteLine("MAX: " + maximumDistance);
            Console.WriteLine(bestRoute.Count);
            Console.WriteLine();

            //nearestNeighbour.FindRoute(nearestNeighbour.InputNodes[0]);
            //Console.WriteLine(repository.Nodes.Equals(nearestNeighbour.InputNodes));
            //Console.WriteLine(nearestNeighbour.OutputNodes.Count);                    

            Drawer drawer = new Drawer(repository.Nodes);
            //drawer.FindMinimalBitmapSize(repository.Nodes);
            drawer.DrawChart("NearestNeighbour.bmp", repository.Nodes, bestRoute);

            minimumDistance = int.MaxValue;
            maximumDistance = 0;
            accumulatedDistance = 0;
            bestRoute.Clear();

            GreedyCycle greedyCycle = new GreedyCycle(repository.Nodes);

            for ( int i = 0; i < greedyCycle.ClonedNodes.Count; i++ )
            {
                greedyCycle.FindRoute(greedyCycle.InputNodes[i]);
                accumulatedDistance += greedyCycle.Distance;

                if ( greedyCycle.Distance > maximumDistance )
                {
                    maximumDistance = greedyCycle.Distance;
                }

                if ( greedyCycle.Distance < minimumDistance )
                {
                    minimumDistance = greedyCycle.Distance;
                    var temporaryArray = new Node[greedyCycle.OutputNodes.Count];
                    greedyCycle.OutputNodes.CopyTo(temporaryArray);
                    bestRoute = temporaryArray.ToList();
                }

                greedyCycle.ResetAlgorithm();
            }

            Console.WriteLine("---Greedy Cycle---");
            Console.WriteLine("MIN: " + minimumDistance);
            Console.WriteLine("AVG: " + accumulatedDistance / greedyCycle.ClonedNodes.Count);
            Console.WriteLine("MAX: " + maximumDistance);
            Console.WriteLine(bestRoute.Count);
            Console.WriteLine();

            drawer.DrawChart("GreedyCycle.bmp", repository.Nodes, bestRoute);

            //for (int i = 0; i < 100; i++)
            //{
            //    List<Node> list = repository.Nodes.ConvertAll(node => new Node(node.Id, node.X, node.Y));
            //    NearestNeighbour nn = new NearestNeighbour(list);
            //    Console.WriteLine(nn.CalculateRoute(repository.Nodes[0]));
            //}

            //Drawer dr = new Drawer();
            //dr.DrawChart("NearestNeighbour.bmp", repository.Nodes, repository.Nodes);
            Console.ReadKey();
        }
    }
}
