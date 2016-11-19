using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TSP2
{
    public class DAL
    {
        private static readonly Lazy<DAL> Lazy = new Lazy<DAL>(() => new DAL());
        public static DAL Instance => Lazy.Value;

        private DAL()
        {
            //HybridAlgorithmStatistics = new List<Tuple<int, long, int>>(NumberOfHybridGeneticAlgorithmExecutions);
            //foreach (var hybridAlgorithmStatistic in HybridAlgorithmStatistics)
            //{
            //    hybridAlgorithmStatistic = new Tuple<int, long, int>();
            //}
        }

        public static readonly int NumberOfLocalSearchResults = 1000;
        public static readonly int EvaluationTimeInMiliseconds = 83360;
        public static readonly int NumberOfHybridGeneticAlgorithmExecutions = 10;
        public static readonly int PopulationSize = 20;

        public IList<Node> Nodes { get; set; } = new List<Node>();
        public List<Tuple<int, long, int>> HybridAlgorithmStatistics { get; set; } = new List<Tuple<int, long, int>>();
        //public List<Tuple<int, long, int>> HybridAlgorithmStatistics { get; set; } = new List<Tuple<int, long, int>>(NumberOfHybridGeneticAlgorithmExecutions);
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

        public void WriteToFileForHybridAlgorithm(List<List<HybridAlgorithmStatisticsData>> data)
        {
            //StreamWriter = new StreamWriter("resultsFull1.csv");
            //if (StreamWriter == null) return;

            for (var i = 0; i < data.Count; i++)
            {
                StreamWriter = new StreamWriter("resultsFull" + (i+1) + ".csv");
                data[i].ForEach(singleIteration =>
                {
                    StreamWriter.WriteLine(singleIteration.IterationNumber + ";" + singleIteration.ExecutionTime + ";" + singleIteration.Distance);
                });
                StreamWriter.Close();
            }
        }

        public void WriteToFileOld(List<OperatingAndStatisticsData> operatingAndStatisticsDatas)
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
