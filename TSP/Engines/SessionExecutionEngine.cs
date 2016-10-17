using System;
using System.Collections.Generic;
using System.Diagnostics;
using TSP.Models;

namespace TSP.Engines
{
    class SessionExecutionEngine
    {
        public Stopwatch Timer { get; set; } = new Stopwatch();

        public void ExecuteSession(AlgorithmExecutionSession algorithmExecutionSession)
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

        private void UpdateConstructionStatisticsData(AlgorithmExecutionSession algorithmExecutionSession)
        {
            algorithmExecutionSession.ConstructionStatisticsData.AccumulatedDistance += algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;

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

            if (Timer.ElapsedMilliseconds > algorithmExecutionSession.OptimalizationStatisticsData.MaximumExecutionTime)
            {
                algorithmExecutionSession.OptimalizationStatisticsData.MaximumExecutionTime = Timer.ElapsedMilliseconds;
            }

            if (Timer.ElapsedMilliseconds >= algorithmExecutionSession.OptimalizationStatisticsData.MinimumExecutionTime) return;
            algorithmExecutionSession.OptimalizationStatisticsData.MinimumExecutionTime = Timer.ElapsedMilliseconds;
        }
    }
}
