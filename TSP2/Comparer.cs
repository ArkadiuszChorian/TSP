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

        public static Dictionary<Node, Dictionary<Node, bool>> ResolveSharedEdgesMatrix(List<Node> sharedVertices, Dictionary<Node, Dictionary<Node, bool>> edges1, Dictionary<Node, Dictionary<Node, bool>> edges2, out Dictionary<Node, int> edgesCountMatrix)
        {
            var sharedEdgesMatrix = DAL.Instance.Nodes.CloneList()
                .ToDictionary(node => node,
                    node => DAL.Instance.Nodes.CloneList().ToDictionary(node2 => node2, node2 => false));   
            
            edgesCountMatrix = new Dictionary<Node, int>(sharedVertices.Count);       

            for (var i = 0; i < sharedVertices.Count; i++)
            {
                for (var j = i + 1; j < sharedVertices.Count; j++)
                {
                    if (edges1[sharedVertices[i]][sharedVertices[j]] && edges2[sharedVertices[i]][sharedVertices[j]])
                    {
                        sharedEdgesMatrix[sharedVertices[i]][sharedVertices[j]] = true;
                        sharedEdgesMatrix[sharedVertices[j]][sharedVertices[i]] = true;
                        edgesCountMatrix[sharedVertices[i]]++;
                        edgesCountMatrix[sharedVertices[j]]++;
                        if (edgesCountMatrix[sharedVertices[i]] >= 2 || edgesCountMatrix[sharedVertices[j]] >= 2)
                        {
                            break;
                        }
                    }
                }
            }

            return sharedEdgesMatrix;
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


//public static List<List<Node>> SharedEdges(IList<Node> nodes1, IList<Node> nodes2)
//{
//    var outputList = new List<List<Node>>();
//    var currentEdge = new List<Node>();
//    var creatingEdge = false;           
//    var sharedVertexIndex = nodes2.Count;
//    var shiftingList = true;
//    var originNodeId = nodes1.First().Id;

//    while (shiftingList)
//    {
//        shiftingList = false;

//        for (var i = 0; i < nodes2.Count; i++)
//        {
//            if (nodes1.First().Equals(nodes2[i]))
//            {
//                shiftingList = true;
//                nodes1.ShiftElementsRight();
//                break;
//            }                   
//        }

//        if (nodes1.First().Id == originNodeId)
//        {
//            break;
//        }
//    }

//    for (var i = 0; i < nodes1.Count; i++)
//    {
//        for (var j = 0; j < nodes2.Count; j++)
//        {
//            if (j >= sharedVertexIndex)
//            {
//                break;
//            }
//            if (nodes1[i].Equals(nodes2[j]))
//            {
//                creatingEdge = true;
//                sharedVertexIndex = j;
//                currentEdge.Add(nodes1[i]);
//                break;
//            }
//            else
//            {
//                creatingEdge = false;
//            }                 
//        }                

//        if (currentEdge.Count > 0 && creatingEdge == false)
//        {
//            outputList.Add(currentEdge);
//            currentEdge.Clear();
//        }
//    }

//    return outputList;
//}