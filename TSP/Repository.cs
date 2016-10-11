using System;
using System.Collections.Generic;
using System.IO;

namespace TSP
{
    class Repository
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        //public StreamWriter _file = new StreamWriter("results.txt");
        public StreamWriter StreamWriter { get; set; }
        public const string InputFileName = "kroA100.tsp";
        public const string OutputFileName = "results.txt";
        public void ReadFromFile()
        {
            try
            {   // Open the text file using a stream reader.
                using (var streamReader = new StreamReader(InputFileName))
                {
                    for (var i = 0; i < 6; i++)
                    {
                        streamReader.ReadLine();
                    }

                    while (!streamReader.EndOfStream)
                    {
                        var readLine = streamReader.ReadLine();
                        if (readLine == null) continue;
                        var parameters = readLine.Split(' ');

                        Nodes.Add(new Node(int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2])));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void PrepareFileToWrite()
        {
            if (StreamWriter == null) StreamWriter = new StreamWriter(OutputFileName);
        }

        public void WriteToFile(Algorithm algorithm, Data data, string title)
        {
            if (StreamWriter == null) return;
            StreamWriter.WriteLine(title);
            StreamWriter.WriteLine("MIN: " + data.MinimumDistance);
            StreamWriter.WriteLine("AVG: " + data.AccumulatedDistance / algorithm.ClonedNodes.Count);
            StreamWriter.WriteLine("MAX: " + data.MaximumDistance);
            foreach (var nodes in data.BestRoute)
            {
                StreamWriter.Write($"{nodes.Id} ");
            }
            StreamWriter.WriteLine();
        }

        public void CloseFileToWrite()
        {
            StreamWriter.Close();
        }
    }
}
