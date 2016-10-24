using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Models;

namespace TSP.Algorithms.OptimalizationAlgorithms
{
    class MultipleStartLocalSearch : OptimalizationAlgorithm
    {
        //public static readonly int MultipleStartLocalSearchIterationNumber = int.Parse(ConfigurationManager.AppSettings.Get("multipleStartLocalSearchIterationNumber"));
        public LocalSearch LocalSearch { get; } = new LocalSearch();
        //public ConstructionAlgorithm ConstructionAlgorithm { get; set; }
        public int BestDistance { get; private set; } = int.MaxValue;
        public AlgorithmOperatingData BestSolutionInitialData { get; set; } = new AlgorithmOperatingData();
        
        public override void ResetAlgorithm()
        {
            BestDistance = int.MaxValue;
            LocalSearch.ResetAlgorithm();
            ConstructionAlgorithm.ResetAlgorithm();
        }

        public override void Optimize()
        {
            for (var i = 0; i < Constants.MultipleStartLocalSearchIterationNumber; i++)
            {
                var randomIndex = RandomGenerator.Next(0, ConstructionAlgorithm.OperatingData.UnusedNodes.Count);
                ConstructionAlgorithm.FindRoute(ConstructionAlgorithm.OperatingData.UnusedNodes[randomIndex]);
                LocalSearch.OperatingData = ConstructionAlgorithm.OperatingData.CloneData();
                LocalSearch.Optimize();
                if (LocalSearch.OperatingData.Distance < BestDistance)
                {
                    BestDistance = LocalSearch.OperatingData.Distance;
                    OperatingData = LocalSearch.OperatingData.CloneData();
                    BestSolutionInitialData = ConstructionAlgorithm.OperatingData.CloneData();
                }
                ConstructionAlgorithm.ResetAlgorithm();
                LocalSearch.ResetAlgorithm();
            }
            ConstructionAlgorithm.OperatingData = BestSolutionInitialData.CloneData();
        }
    }
}
