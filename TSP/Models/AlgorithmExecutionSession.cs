using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.SolutionConstruction.ConstructionAlgorithms;

namespace TSP
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
