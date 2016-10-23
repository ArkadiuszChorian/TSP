using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Models
{
    class OptimalizationStatisticsData : ConstructionStatisticsData
    {
        public long AccumulatedExecutionTime { get; set; } = 0;
        public int NumberOfTimeMeasureAttempts { get; set; } = 0;
        public long MinimumExecutionTime { get; set; } = long.MaxValue;
        public long MaximumExecutionTime { get; set; } = 0;
    }
}
