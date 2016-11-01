using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Models;

namespace TSP
{
    public static class Comparer
    {
        public static int SharedVertices(IList<Node> nodes1, IList<Node> nodes2)
        {
            return nodes1.Count(nodes2.Contains);
        }

        public static int SharedEdges(IList<Node> nodes1, IList<Node> nodes2)
        {
            var numberOfSharedEdges = 0;

            for (var i = 0; i < nodes1.Count; i++)
            {               
                for (var j = 0; j < nodes2.Count; j++)
                {
                    if (nodes1[i].Equals(nodes2[j]) && nodes1[i >= nodes1.Count - 1 ? 0 : i + 1].Equals(nodes2[j >= nodes2.Count - 1 ? 0 : j + 1])
                        || nodes1[i].Equals(nodes2[j >= nodes2.Count - 1 ? 0 : j + 1]) && nodes1[i >= nodes1.Count - 1 ? 0 : i + 1].Equals(nodes2[j]))
                    {
                        numberOfSharedEdges++;
                    }
                }
            }

            return numberOfSharedEdges;
        }
    }
}
