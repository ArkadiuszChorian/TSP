using System;
using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP.Algorithms
{
    abstract class OptimalizationAlgorithm : Algorithm
    {
        public abstract void ResetAlgorithm();
        public abstract void Optimize();
    }
}
