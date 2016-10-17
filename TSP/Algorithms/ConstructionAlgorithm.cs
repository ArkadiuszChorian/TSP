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
            OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
            OperatingData.PathNodes = new List<Node>();
            OperatingData.Distance = 0;         
        }

        public abstract void FindRoute(Node node);

        public void ResetAlgorithm()
        {
            OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
            OperatingData.PathNodes.Clear();
            OperatingData.Distance = 0;
        }

        public static readonly int ResultNodesLimit = int.Parse(ConfigurationManager.AppSettings.Get("resultNodesLimit"));
    }
}
