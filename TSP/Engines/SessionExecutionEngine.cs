using System;
using System.Collections.Generic;
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

        public void ExecuteSession(AlgorithmExecutionSession algorithmExecutionSession)
        {
            var totalNumberOfNodes = DAL.Instance.Nodes.Count;

            for (var i = 0; i < totalNumberOfNodes; i++)
            {
                algorithmExecutionSession.ConstructionAlgorithm.FindRoute(algorithmExecutionSession.ConstructionAlgorithm.OperatingData.UnusedNodes[i]);
                algorithmExecutionSession.ConstructionStatisticsData.AccumulatedDistance += algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;

                if (algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance > algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance)
                {
                    algorithmExecutionSession.ConstructionStatisticsData.MaximumDistance = algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;
                }

                if (algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance < algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance)
                {
                    algorithmExecutionSession.ConstructionStatisticsData.MinimumDistance = algorithmExecutionSession.ConstructionAlgorithm.OperatingData.Distance;
                    algorithmExecutionSession.ConstructionStatisticsData.BestRoute = algorithmExecutionSession.ConstructionAlgorithm.OperatingData.PathNodes.CloneList();
                }

                //DAL.Instance.AlgorithmsData.Add(new AlgorithmOperatingData(algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.UnusedNodes.CloneList(), algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.PathNodes.CloneList(), algorithmExecutionSession.OptimalizationAlgorithm.OperatingData.Distance));
                algorithmExecutionSession.ConstructionAlgorithm.ResetAlgorithm();
            }
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
