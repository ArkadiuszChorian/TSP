using System;
using System.Collections.Generic;
using TSP.Models;

namespace TSP.Engines
{
    class SolutionConstructionEngine
    {
        public IDictionary<Type, AlgorithmExecutionSession> AlgorithmsExecutionSessions { get; set; } = new Dictionary<Type, AlgorithmExecutionSession>();

        public void AddExecutionSession(Type algorithmType)
        {
            AlgorithmsExecutionSessions.Add(algorithmType, new AlgorithmExecutionSession(algorithmType));
        }

        public void ExecuteAlgorithm(Type algorithmType)
        {
            var algorithmExecutionSession = AlgorithmsExecutionSessions[algorithmType];

            for (var i = 0; i < algorithmExecutionSession.Algorithm.ClonedNodes.Count; i++)
            {
                algorithmExecutionSession.Algorithm.FindRoute(algorithmExecutionSession.Algorithm.InputNodes[i]);
                algorithmExecutionSession.AlgorithmResultData.AccumulatedDistance += algorithmExecutionSession.Algorithm.Distance;

                if (algorithmExecutionSession.Algorithm.Distance > algorithmExecutionSession.AlgorithmResultData.MaximumDistance)
                {
                    algorithmExecutionSession.AlgorithmResultData.MaximumDistance = algorithmExecutionSession.Algorithm.Distance;
                }

                if (algorithmExecutionSession.Algorithm.Distance < algorithmExecutionSession.AlgorithmResultData.MinimumDistance)
                {
                    algorithmExecutionSession.AlgorithmResultData.MinimumDistance = algorithmExecutionSession.Algorithm.Distance;
                    algorithmExecutionSession.AlgorithmResultData.BestRoute = algorithmExecutionSession.Algorithm.OutputNodes.CloneList();
                }

                algorithmExecutionSession.Algorithm.ResetAlgorithm();
            }
        }
    }
}
