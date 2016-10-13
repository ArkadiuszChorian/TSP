using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace TSP
{
    class DAL
    {
        private static readonly Lazy<DAL> Lazy = new Lazy<DAL>(() => new DAL());
        public static DAL Instance => Lazy.Value;
        private DAL(){}

        public IList<Node> Nodes { get; set; } = new List<Node>();
        public StreamWriter StreamWriter { get; set; }
        //public const string InputFileName = "kroA100.tsp";
        //public const string OutputFileName = "results.txt";
        public void ReadFromFile()
        {
            try
            {
                using (var streamReader = new StreamReader(ConfigurationManager.AppSettings.Get("instanceFile")))
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
            if (StreamWriter == null) StreamWriter = new StreamWriter(ConfigurationManager.AppSettings.Get("resultsFile"));
        }

        public void WriteToFile(AlgorithmExecutionSession algorithmExecutionSession, string title)
        {
            if (StreamWriter == null) return;
            StreamWriter.WriteLine(title);
            StreamWriter.WriteLine("MIN: " + algorithmExecutionSession.AlgorithmResultData.MinimumDistance);
            StreamWriter.WriteLine("AVG: " + algorithmExecutionSession.AlgorithmResultData.AccumulatedDistance / algorithmExecutionSession.Algorithm.ClonedNodes.Count);
            StreamWriter.WriteLine("MAX: " + algorithmExecutionSession.AlgorithmResultData.MaximumDistance);
            foreach (var nodes in algorithmExecutionSession.AlgorithmResultData.BestRoute)
            {
                StreamWriter.Write($"{nodes.Id} ");
            }
            StreamWriter.WriteLine();
        }
        //public void WriteToFile(ConstructionAlgorithm constructionAlgorithm, AlgorithmResultData algorithmResultData, string title)
        //{
        //    if (StreamWriter == null) return;
        //    StreamWriter.WriteLine(title);
        //    StreamWriter.WriteLine("MIN: " + algorithmResultData.MinimumDistance);
        //    StreamWriter.WriteLine("AVG: " + algorithmResultData.AccumulatedDistance / constructionAlgorithm.ClonedNodes.Count);
        //    StreamWriter.WriteLine("MAX: " + algorithmResultData.MaximumDistance);
        //    foreach (var nodes in algorithmResultData.BestRoute)
        //    {
        //        StreamWriter.Write($"{nodes.Id} ");
        //    }
        //    StreamWriter.WriteLine();
        //}

        public void CloseFileToWrite()
        {
            StreamWriter.Close();
        }
    }
}
