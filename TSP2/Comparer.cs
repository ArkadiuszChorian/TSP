using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP2
{
    public static class Comparer
    {
        public static int SharedVerticesRatio(IList<Node> nodes1, IList<Node> nodes2)
        {
            var numberOfSharedVertices = 0;

            for (var i = 0; i < nodes1.Count; i++)
            {
                for (var j = i; j < nodes2.Count; j++)
                {
                    if (nodes1[i].Id == nodes2[j].Id)
                    {
                        numberOfSharedVertices++;
                    }
                }
            }

            return numberOfSharedVertices;
            //return nodes1.Count(nodes2.Contains);
        }

        public static int SharedEdgesRatio(IList<Node> nodes1, IList<Node> nodes2)
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

        public static List<Node> SharedVertices(IList<Node> nodes1, IList<Node> nodes2)
        {
            var outputList = new List<Node>();

            for (var i = 0; i < nodes1.Count; i++)
            {
                for (var j = i; j < nodes2.Count; j++)
                {
                    if (nodes1[i].Id == nodes2[j].Id)
                    {
                        outputList.Add(nodes1[i]);
                    }
                }
            }

            return outputList;
            //return nodes1.Count(nodes2.Contains);
        }

        public static List<List<Node>> SharedEdges(IList<Node> nodes1, IList<Node> nodes2)
        {
            var outputList = new List<List<Node>>();
            var currentEdge = new List<Node>();
            var creatingEdge = false;           
            var sharedVertexIndex = nodes2.Count;
            var shiftingList = true;
            var originNodeId = nodes1.First().Id;

            while (shiftingList)
            {
                shiftingList = false;

                for (var i = 0; i < nodes2.Count; i++)
                {
                    if (nodes1.First().Equals(nodes2[i]))
                    {
                        shiftingList = true;
                        nodes1.ShiftElementsRight();
                        break;
                    }                   
                }

                if (nodes1.First().Id == originNodeId)
                {
                    break;
                }
            }

            for (var i = 0; i < nodes1.Count; i++)
            {
                for (var j = 0; j < nodes2.Count; j++)
                {
                    if (j >= sharedVertexIndex)
                    {
                        break;
                    }
                    if (nodes1[i].Equals(nodes2[j]))
                    {
                        creatingEdge = true;
                        sharedVertexIndex = j;
                        currentEdge.Add(nodes1[i]);
                        break;
                    }
                    else
                    {
                        creatingEdge = false;
                    }                 
                }                

                if (currentEdge.Count > 0 && creatingEdge == false)
                {
                    outputList.Add(currentEdge);
                    currentEdge.Clear();
                }
            }

            return outputList;
        }

        private static void ResolveStartingNode(IList<Node> nodes1, IList<Node> nodes2)
        {
            
        }
    }
}

//else if (nodes1[i].Equals(nodes2[j >= nodes2.Count - 1 ? 0 : j + 1]))
//{

//}
//if (nodes1[i].Equals(nodes2[j]) && nodes1[i >= nodes1.Count - 1 ? 0 : i + 1].Equals(nodes2[j >= nodes2.Count - 1 ? 0 : j + 1])
//    || nodes1[i].Equals(nodes2[j >= nodes2.Count - 1 ? 0 : j + 1]) && nodes1[i >= nodes1.Count - 1 ? 0 : i + 1].Equals(nodes2[j]))
//{
//    creatingEdge = true;
//}
//else
//{
//    creatingEdge = false;
//}
