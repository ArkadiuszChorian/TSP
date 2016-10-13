using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Models
{
    class DataModel
    {
        public IList<Node> InputNodes;
        public IList<Node> OutputNodes;
        public int Distance;

        public DataModel(IList<Node> inputNodes, IList<Node> outputNodes, int distance)
        {
            InputNodes = inputNodes;
            OutputNodes = outputNodes;
            Distance = distance;
        }
    }
}
