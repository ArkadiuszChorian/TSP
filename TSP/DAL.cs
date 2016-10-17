using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TSP.Models;

namespace TSP
{
    class DAL
    {
        private static readonly Lazy<DAL> Lazy = new Lazy<DAL>(() => new DAL());
        public static DAL Instance => Lazy.Value;
        private DAL(){}

        public IList<Node> Nodes { get; set; } = new List<Node>();
        public IList<AlgorithmOperatingData> AlgorithmsData { get; set; } = new List<AlgorithmOperatingData>();
        public StreamWriter StreamWriter { get; set; }
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
            StreamWriter.WriteLine("MIN: " + algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance);
            StreamWriter.WriteLine("AVG: " + algorithmExecutionSession.ConstructionStatisticsData.AccumulatedDistance / Nodes.Count);
            StreamWriter.WriteLine("MAX: " + algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance);
            foreach (var nodes in algorithmExecutionSession.ConstructionStatisticsData.BestRoute)
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
