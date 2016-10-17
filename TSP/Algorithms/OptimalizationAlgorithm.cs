using System;
using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP.Algorithms
{
    abstract class OptimalizationAlgorithm : Algorithm
    {
        //public AlgorithmOperatingData OperatingData { get; set; }

        //public OptimalizationAlgorithm(AlgorithmOperatingData algorithmOperatingData)
        //{
        //    OperatingData = algorithmOperatingData;

        //    // Temporary solution for accessing variable from OutputNodes[50]
        //    //OperatingData.PathNodes.Add(OperatingData.PathNodes.First());
        //}
        public abstract void ResetAlgorithm();
        //{
            //OperatingData.UnusedNodes = OperatingData.ClonedNodes.CloneList();
            //OperatingData = initialOperatingData.CloneData();
        //}

        public abstract void Optimize();
    }
}
