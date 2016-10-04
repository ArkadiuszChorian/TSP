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
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            repository.ReadFromFile(fileName);
            Console.ReadKey();
        }
    }
}
