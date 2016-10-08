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

            for (int i = 0; i < 100; i++)
            {
                List<Node> list = repository.Nodes.ConvertAll(node => new Node(node.Id, node.X, node.Y));
                NearestNeighbour nn = new NearestNeighbour(list);
                Console.WriteLine(nn.CalculateRoute(repository.Nodes[0]));
            }

            Drawer dr = new Drawer();
            dr.DrawChart("NearestNeighbour.bmp", repository.Nodes, repository.Nodes);
            Console.ReadKey();
        }
    }
}
