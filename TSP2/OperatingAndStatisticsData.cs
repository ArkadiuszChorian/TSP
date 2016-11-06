using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP2
{
    public class OperatingAndStatisticsData
    {
        public OperatingData OperatingData { get; set; } = new OperatingData();
        public double AverangeSimilarityToOthersBySharedVertices { get; set; } = 0;
        public double AverangeSimilarityToOthersBySharedEdges { get; set; } = 0;
        public double SimilarityToBestBySharedVertices { get; set; } = 0;
        public double SimilarityToBestBySharedEdges { get; set; } = 0;
        public OperatingAndStatisticsData CloneData()
        {
            return new OperatingAndStatisticsData
            {
                OperatingData = OperatingData.CloneData()
            };
        }

        public void ClearData()
        {
            OperatingData.PathNodes.Clear();
            OperatingData.UnusedNodes.Clear();
            OperatingData.Distance = 0;
        }
    }
}
