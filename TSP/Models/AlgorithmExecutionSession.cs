using System;
using TSP.Algorithms;

namespace TSP.Models
{
    class AlgorithmExecutionSession
    {
        public AlgorithmExecutionSession(Type algorithmType)
        {
            Algorithm = (IAlgorithm) Activator.CreateInstance(algorithmType);
            AlgorithmResultData = new AlgorithmResultData();
        }
        public IAlgorithm Algorithm { get; set; }
        public AlgorithmResultData AlgorithmResultData { get; set; }
    }
}
