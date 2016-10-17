using System;
using System.Collections.Generic;
using System.Diagnostics;
using TSP.Models;

namespace TSP.Engines
{
    class SessionExecutionEngine
    {
        //public IDictionary<Type, AlgorithmExecutionSession> AlgorithmsExecutionSessions { get; set; } = new Dictionary<Type, AlgorithmExecutionSession>();

        //public void AddExecutionSession(Type algorithmType)
        //{
        //    AlgorithmsExecutionSessions.Add(algorithmType, new AlgorithmExecutionSession(algorithmType));
        //}
        public Stopwatch Timer { get; set; } = new Stopwatch();

        public void ExecuteSession(AlgorithmExecutionSession algorithmExecutionSession)
        {
            var totalNumberOfNodes = DAL.Instance.Nodes.Count;
            //var licznik = 0;

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
                //var currentOperatingData = algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.CloneData();
                //var bestOperatingData = new AlgorithmOperatingData {Distance = int.MaxValue};


                //DAL.Instance.AlgorithmsData.Add(new AlgorithmOperatingData(algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.UnusedNodes.CloneList(), algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.PathNodes.CloneList(), algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance));
                Timer.Reset();
                algorithmExecutionSession.ConstructionAlgorithm.ResetAlgorithm();
                algorithmExecutionSession.OptimalizationAlgorithm.ResetAlgorithm();
                //algorithmExecutionSession.OptimalizationAlgorithm.ResetAlgorithm();
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

            Constants.licznik++;
            algorithmExecutionSession.OptimalizationStatisticsData.AccumulatedExecutionTime += Timer.ElapsedMilliseconds;

            if (Timer.ElapsedMilliseconds > algorithmExecutionSession.OptimalizationStatisticsData.MaximumExecutionTime)
            {
                algorithmExecutionSession.OptimalizationStatisticsData.MaximumExecutionTime = Timer.ElapsedMilliseconds;
            }

            if (Timer.ElapsedMilliseconds >= algorithmExecutionSession.OptimalizationStatisticsData.MinimumExecutionTime) return;
            algorithmExecutionSession.OptimalizationStatisticsData.MinimumExecutionTime = Timer.ElapsedMilliseconds;
        }

        //public void ExecuteAlgorithm(Type algorithmType)
        //{
        //    var algorithmExecutionSession = AlgorithmsExecutionSessions[algorithmType];

        //    for (var i = 0; i < algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.ClonedNodes.Count; i++)
        //    {
        //        algorithmExecutionSession.OptimalizationAlgorithm.FindRoute(algorithmExecutionSession.OptimalizationAlgorithm.InputNodes[i]);
        //        algorithmExecutionSession.ConstructionStatisticsData.AccumulatedDistance += algorithmExecutionSession.OptimalizationAlgorithm.Distance;

        //        if (algorithmExecutionSession.OptimalizationAlgorithm.Distance > algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance)
        //        {
        //            algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance = algorithmExecutionSession.OptimalizationAlgorithm.Distance;
        //        }

        //        if (algorithmExecutionSession.OptimalizationAlgorithm.Distance < algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance)
        //        {
        //            algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance = algorithmExecutionSession.OptimalizationAlgorithm.Distance;
        //            algorithmExecutionSession.ConstructionStatisticsData.BestRoute = algorithmExecutionSession.OptimalizationAlgorithm.OutputNodes.CloneList();
        //        }

        //        DAL.Instance.AlgorithmsData.Add(new AlgorithmOperatingData(algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.UnusedNodes.CloneList(), algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.PathNodes.CloneList(), algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance));
        //        algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.ResetAlgorithm();
        //    }
        //}
    }
}
