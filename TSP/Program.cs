using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Program
    {
        const string fileName = "kroA100.tsp";
        [STAThread]
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            repository.ReadFromFile(fileName);

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

            Console.WriteLine("MIN: " + minimumDistance);
            Console.WriteLine("AVG: " + accumulatedDistance/nearestNeighbour.ClonedNodes.Count);
            Console.WriteLine("MAX: " + maximumDistance);
            Console.WriteLine(bestRoute.Count);

            //nearestNeighbour.FindRoute(nearestNeighbour.InputNodes[0]);
            //Console.WriteLine(repository.Nodes.Equals(nearestNeighbour.InputNodes));
            //Console.WriteLine(nearestNeighbour.OutputNodes.Count);                    

            Drawer drawer = new Drawer(repository.Nodes);
            //drawer.FindMinimalBitmapSize(repository.Nodes);
            drawer.DrawChart("NearestNeighbour.bmp", repository.Nodes, bestRoute);

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
