using System;
using System.Collections.Generic;
using System.Diagnostics;
using TSP.Algorithms;
using TSP.Algorithms.OptimalizationAlgorithms;
using TSP.Models;

namespace TSP.Engines
{
    class AlgorithmExecutionEngine
    {
        public Stopwatch Timer { get; set; } = new Stopwatch();

        public void ExecuteMultipleStartLocalSearchSession(AlgorithmExecutionSession algorithmExecutionSession)
        {
            algorithmExecutionSession.OptimalizationAlgorithm.ConstructionAlgorithm = algorithmExecutionSession.ConstructionAlgorithm;

            for (var i = 0; i < Constants.NumberOfMslsAndIlsIteration; i++)
            {
                Timer.Reset();
                Timer.Start();
                algorithmExecutionSession.OptimalizationAlgorithm.Optimize();
                Timer.Stop();

                UpdateConstructionStatisticsData(algorithmExecutionSession);
                UpdateOptimalizationStatisticsData(algorithmExecutionSession);

                algorithmExecutionSession.OptimalizationAlgorithm.ResetAlgorithm();
            }
            //Console.WriteLine("XXXXXXXXXAcccc   " + algorithmExecutionSession.OptimalizationStatisticsData.AccumulatedExecutionTime);
            //Console.WriteLine("XXXXXXXXXNumbe   " + algorithmExecutionSession.OptimalizationStatisticsData.NumberOfTimeMeasureAttempts);
            //Console.WriteLine("XXXXXXXXXDivi   " + algorithmExecutionSession.OptimalizationStatisticsData.AccumulatedExecutionTime /algorithmExecutionSession.OptimalizationStatisticsData.NumberOfTimeMeasureAttempts);
            DAL.Instance.AverangeMslsTime = algorithmExecutionSession.OptimalizationStatisticsData.AccumulatedExecutionTime/algorithmExecutionSession.OptimalizationStatisticsData.NumberOfTimeMeasureAttempts;
        }

        public void ExecuteDefaultSession(AlgorithmExecutionSession algorithmExecutionSession)
        {
            var totalNumberOfNodes = DAL.Instance.Nodes.Count;

            for (var i = 0; i < totalNumberOfNodes; i++)
            {
                algorithmExecutionSession.ConstructionAlgorithm.FindRoute(algorithmExecutionSession.ConstructionAlgorithm.OperatingData.UnusedNodes[i]);

                UpdateConstructionStatisticsData(algorithmExecutionSession);
                
                algorithmExecutionSession.OptimalizationAlgorithm.OperatingData =
                    algorithmExecutionSession.ConstructionAlgorithm.OperatingData.CloneData();

                Timer.Start();

                algorithmExecutionSession.OptimalizationAlgorithm.Optimize();

                Timer.Stop();                

                UpdateOptimalizationStatisticsData(algorithmExecutionSession);
                Timer.Reset();
                algorithmExecutionSession.ConstructionAlgorithm.ResetAlgorithm();
                algorithmExecutionSession.OptimalizationAlgorithm.ResetAlgorithm();
            }
        }

        public void ExecuteIteratedLocalSearchSession(AlgorithmExecutionSession algorithmExecutionSession)
        {
            var totalNumberOfNodes = DAL.Instance.Nodes.Count;
            //var randomGenerator = new Random();
            var randomGenerator = algorithmExecutionSession.ConstructionAlgorithm.RandomGenerator;

            for (var i = 0; i < Constants.NumberOfMslsAndIlsIteration*100; i++)
            {
                Timer.Reset();
                Timer.Start();
                
                //algorithmExecutionSession.ConstructionAlgorithm.FindRoute(algorithmExecutionSession.ConstructionAlgorithm.OperatingData.UnusedNodes[randomGenerator.Next(0,totalNumberOfNodes-1)]);
                algorithmExecutionSession.ConstructionAlgorithm.FindRoute(algorithmExecutionSession.ConstructionAlgorithm.OperatingData.UnusedNodes[i]);

                UpdateConstructionStatisticsData(algorithmExecutionSession);

                algorithmExecutionSession.OptimalizationAlgorithm.OperatingData =
                    algorithmExecutionSession.ConstructionAlgorithm.OperatingData.CloneData();

                algorithmExecutionSession.OptimalizationAlgorithm.Optimize();

                Timer.Stop();

                UpdateOptimalizationStatisticsData(algorithmExecutionSession);

                algorithmExecutionSession.ConstructionAlgorithm.ResetAlgorithm();
                algorithmExecutionSession.OptimalizationAlgorithm.ResetAlgorithm();
            }
        }

        public void ExecuteLocalSearchSession(AlgorithmExecutionSession algorithmExecutionSession)
        {
            var totalNumberOfNodes = DAL.Instance.Nodes.Count;
            var randomGenerator = new Random();

            for (var i = 0; i < 1000; i++)
            {
                algorithmExecutionSession.ConstructionAlgorithm.FindRoute(algorithmExecutionSession.ConstructionAlgorithm.OperatingData.UnusedNodes[randomGenerator.Next(0, totalNumberOfNodes)]);
                algorithmExecutionSession.OptimalizationAlgorithm.OperatingData =
                    algorithmExecutionSession.ConstructionAlgorithm.OperatingData.CloneData();
                algorithmExecutionSession.OptimalizationAlgorithm.Optimize();

                if (algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance < DAL.Instance.BestLsData.Distance)
                {
                    DAL.Instance.BestLsData =
                        algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.CloneData();
                }
            }
        }

        private void UpdateConstructionStatisticsData(AlgorithmExecutionSession algorithmExecutionSession)
        {
            algorithmExecutionSession.ConstructionStatisticsData.AccumulatedDistance += algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;
            algorithmExecutionSession.ConstructionStatisticsData.NumberOfDistanceMeasureAttempts++;

            if (algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance > algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance)
            {
                algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance = algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;
            }

            if (algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance >=
                algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance) return;
            algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance = algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;
            algorithmExecutionSession.ConstructionStatisticsData.BestRoute = algorithmExecutionSession.ConstructionAlgorithm.OperatingData.PathNodes.CloneList();
        }

        private void UpdateOptimalizationStatisticsData(AlgorithmExecutionSession algorithmExecutionSession)
        {
            algorithmExecutionSession.OptimalizationStatisticsData.AccumulatedDistance += algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance;
            algorithmExecutionSession.OptimalizationStatisticsData.NumberOfDistanceMeasureAttempts++;

            if (algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance > algorithmExecutionSession.OptimalizationStatisticsData.MaximumDistance)
            {
                algorithmExecutionSession.OptimalizationStatisticsData.MaximumDistance = algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance;
            }

            if (algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance <
                algorithmExecutionSession.OptimalizationStatisticsData.MinimumDistance)
            {
                algorithmExecutionSession.OptimalizationStatisticsData.MinimumDistance = algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance;
                algorithmExecutionSession.OptimalizationStatisticsData.BestRoute = algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.PathNodes.CloneList();
            }           
            
            algorithmExecutionSession.OptimalizationStatisticsData.AccumulatedExecutionTime += Timer.ElapsedMilliseconds;
            algorithmExecutionSession.OptimalizationStatisticsData.NumberOfTimeMeasureAttempts++;

            if (Timer.ElapsedMilliseconds > algorithmExecutionSession.OptimalizationStatisticsData.MaximumExecutionTime)
            {
                algorithmExecutionSession.OptimalizationStatisticsData.MaximumExecutionTime = Timer.ElapsedMilliseconds;
            }

            if (Timer.ElapsedMilliseconds >= algorithmExecutionSession.OptimalizationStatisticsData.MinimumExecutionTime) return;
            algorithmExecutionSession.OptimalizationStatisticsData.MinimumExecutionTime = Timer.ElapsedMilliseconds;
        }
    }
}
