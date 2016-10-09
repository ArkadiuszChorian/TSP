using System;
using System.Collections.Generic;

namespace TSP
{
    class Program
    {
        const string fileName = "kroA100.tsp";
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            repository.ReadFromFile(fileName);

            NearestNeighbour nearestNeighbour = new NearestNeighbour(repository.Nodes);
          
            nearestNeighbour.FindRoute(nearestNeighbour.InputNodes[0]);
            Console.WriteLine(repository.Nodes.Equals(nearestNeighbour.InputNodes));
            Console.WriteLine(nearestNeighbour.OutputNodes.Count);          

            var correctNodes = 0;

            foreach (var outputNode in nearestNeighbour.OutputNodes)
            {
                if (repository.Nodes.Contains(outputNode))
                {
                    correctNodes++;
                }
            }
            Console.WriteLine(correctNodes);

            Drawer drawer = new Drawer(repository.Nodes);
            //drawer.FindMinimalBitmapSize(repository.Nodes);
            drawer.DrawChart("NearestNeighbour.bmp", repository.Nodes, nearestNeighbour.OutputNodes);

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
