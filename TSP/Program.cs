using System;
using System.Linq;

namespace TSP
{
    class Program
    {
        const string FileName = "kroA100.tsp";
        static void Main()
        {
            Repository repository = new Repository();
            repository.ReadFromFile(FileName);

            //var minimumDistance = int.MaxValue;
            //var maximumDistance = 0;
            //var accumulatedDistance = 0;
            //List<Node> bestRoute = new List<Node>();

            NearestNeighbour nearestNeighbour = new NearestNeighbour(repository.Nodes);
            Data dataNn = GetData(nearestNeighbour);

            Console.WriteLine("---Nearest Neighbour---");
            Console.WriteLine("MIN: " + dataNn.MinimumDistance);
            Console.WriteLine("AVG: " + dataNn.AccumulatedDistance/nearestNeighbour.ClonedNodes.Count);
            Console.WriteLine("MAX: " + dataNn.MaximumDistance);
            Console.WriteLine(dataNn.BestRoute.Count);
            Console.WriteLine();

            //nearestNeighbour.FindRoute(nearestNeighbour.InputNodes[0]);
            //Console.WriteLine(repository.Nodes.Equals(nearestNeighbour.InputNodes));
            //Console.WriteLine(nearestNeighbour.OutputNodes.Count);                    

            Drawer drawer = new Drawer(repository.Nodes);
            //drawer.FindMinimalBitmapSize(repository.Nodes);
            drawer.DrawChart("NearestNeighbour.bmp", repository.Nodes, dataNn.BestRoute);

            //minimumDistance = int.MaxValue;
            //maximumDistance = 0;
            //accumulatedDistance = 0;
            //bestRoute.Clear();

            GreedyCycle greedyCycle = new GreedyCycle(repository.Nodes);
            Data dataGc = GetData(greedyCycle);

            //for ( int i = 0; i < greedyCycle.ClonedNodes.Count; i++ )
            //{
            //    greedyCycle.FindRoute(greedyCycle.InputNodes[i]);
            //    accumulatedDistance += greedyCycle.Distance;

            //    if ( greedyCycle.Distance > maximumDistance )
            //    {
            //        maximumDistance = greedyCycle.Distance;
            //    }

            //    if ( greedyCycle.Distance < minimumDistance )
            //    {
            //        minimumDistance = greedyCycle.Distance;
            //        var temporaryArray = new Node[greedyCycle.OutputNodes.Count];
            //        greedyCycle.OutputNodes.CopyTo(temporaryArray);
            //        bestRoute = temporaryArray.ToList();
            //    }

            //    greedyCycle.ResetAlgorithm();
            //}

            Console.WriteLine("---Greedy Cycle---");
            Console.WriteLine("MIN: " + dataGc.MinimumDistance);
            Console.WriteLine("AVG: " + dataGc.AccumulatedDistance / greedyCycle.ClonedNodes.Count);
            Console.WriteLine("MAX: " + dataGc.MaximumDistance);
            Console.WriteLine(dataGc.BestRoute.Count);
            Console.WriteLine();

            drawer.DrawChart("GreedyCycle.bmp", repository.Nodes, dataGc.BestRoute);

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
                    var temporaryArray = new Node[algorithm.OutputNodes.Count];
                    algorithm.OutputNodes.CopyTo(temporaryArray);
                    data.BestRoute = temporaryArray.ToList();
                }

                algorithm.ResetAlgorithm();
            }

            return data;
        }
    }
}
