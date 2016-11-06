using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TSP.Models;

namespace TSP2
{
    public class DAL
    {
        private static readonly Lazy<DAL> Lazy = new Lazy<DAL>(() => new DAL());
        public static DAL Instance => Lazy.Value;
        private DAL(){}

        public IList<Node> Nodes { get; set; } = new List<Node>();
        //public IList<AlgorithmOperatingData> AlgorithmsData { get; set; } = new List<AlgorithmOperatingData>();
        //public AlgorithmOperatingData BestLsData { get; set; } = new AlgorithmOperatingData
        //{
        //    Distance = int.MaxValue
        //};
        public StreamWriter StreamWriter { get; set; }
        public void ReadFromFile()
        {
            try
            {
                using (var streamReader = new StreamReader("kroA100.tsp"))
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
            if (StreamWriter == null) StreamWriter = new StreamWriter("results.csv");
        }

        public void WriteToFile(List<OperatingAndStatisticsData> operatingAndStatisticsDatas)
        {
            if (StreamWriter == null) return;
            operatingAndStatisticsDatas.ForEach(data =>
            {
                StreamWriter.WriteLine(data.OperatingData.Distance + ";"
                    + data.AverangeSimilarityToOthersBySharedVertices + ";"
                    + data.AverangeSimilarityToOthersBySharedEdges + ";"
                    + data.SimilarityToBestBySharedVertices + ";"
                    + data.SimilarityToBestBySharedEdges);
            });            
        }

        public void CloseFileToWrite()
        {
            StreamWriter.Close();
        }
    }
}
