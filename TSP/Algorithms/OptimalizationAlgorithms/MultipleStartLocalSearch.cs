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
        public static readonly int MultipleStartLocalSearchIterationNumber = int.Parse(ConfigurationManager.AppSettings.Get("multipleStartLocalSearchIterationNumber"));
        public LocalSearch LocalSearch { get; set; }
        public ConstructionAlgorithm ConstructionAlgorithm { get; set; }
        
        public override void ResetAlgorithm()
        {
            throw new NotImplementedException();
        }

        public override void Optimize()
        {
            for (var i = 0; i < MultipleStartLocalSearchIterationNumber; i++)
            {
                var randomIndex = RandomGenerator.Next(0, ConstructionAlgorithm.OperatingData.UnusedNodes.Count);
                ConstructionAlgorithm.FindRoute(ConstructionAlgorithm.OperatingData.UnusedNodes[randomIndex]);
                LocalSearch.OperatingData = ConstructionAlgorithm.OperatingData.CloneData();
                LocalSearch.Optimize();
                //TODO
                ConstructionAlgorithm.ResetAlgorithm();
                LocalSearch.ResetAlgorithm();
            }          
        }
    }
}
