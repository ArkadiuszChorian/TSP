using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Algorithm
    {
        public int Distance(Node node1, Node node2)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(node2.X - node1.X, 2) + Math.Pow(node2.Y - node1.Y, 2)));
        }
    }
}
