using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Algorithms;
using TSP.Algorithms.OptimalizationAlgorithms;

namespace TSP.Models
{
    class MultipleStartLocalSearchExecutionSession
    {
        public MultipleStartLocalSearchExecutionSession(ConstructionAlgorithm constructionAlgorithm, MultipleStartLocalSearch multipleStartLocalSearch)
        {
            MultipleStartLocalSearch = multipleStartLocalSearch;
            ConstructionAlgorithm = constructionAlgorithm;
            OptimalizationStatisticsData = new OptimalizationStatisticsData();
        }       
        public ConstructionAlgorithm ConstructionAlgorithm { get; set; }
        public MultipleStartLocalSearch MultipleStartLocalSearch { get; set; }
        public OptimalizationStatisticsData OptimalizationStatisticsData { get; set; }
    }
}
