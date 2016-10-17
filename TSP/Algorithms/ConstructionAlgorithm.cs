using System;
using System.Collections.Generic;
using System.Configuration;
using TSP.Models;

namespace TSP.Algorithms
{
    internal abstract class ConstructionAlgorithm : Algorithm
    {
        protected ConstructionAlgorithm()
        {
            //OperatingData.ClonedNodes = DAL.Instance.Nodes.CloneList();
            //OperatingData.UnusedNodes = OperatingData.ClonedNodes.CloneList();
            OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
            OperatingData.PathNodes = new List<Node>();
            OperatingData.Distance = 0;         
        }

        public abstract void FindRoute(Node node);

        public void ResetAlgorithm()
        {
            //OperatingData.UnusedNodes = OperatingData.ClonedNodes.CloneList();
            OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
            OperatingData.PathNodes.Clear();
            OperatingData.Distance = 0;
        }
   
        public static readonly int ResultNodesLimit = int.Parse(ConfigurationManager.AppSettings.Get("resultNodesLimit"));
        

        //public int CalculateDistance(Node node1, Node node2)
        //{
        //    return (int)Math.Round(Math.Sqrt(Math.Pow(node2.X - node1.X, 2) + Math.Pow(node2.Y - node1.Y, 2)));
        //}

        //public AlgorithmOperatingData OperatingData { get; set; }

        //public IList<Node> ClonedNodes { get; set; }
        //public IList<Node> InputNodes { get; set; }
        //public IList<Node> OutputNodes { get; set; }
        //public int Distance { get; set; }
    }
}
