using System;
using System.Collections.Generic;
using System.Linq;
using TSP.Models;

namespace TSP.Algorithms
{
    class OptimalizationAlgorithm : Algorithm
    {
        //public AlgorithmOperatingData OperatingData { get; set; }

        //public OptimalizationAlgorithm(AlgorithmOperatingData algorithmOperatingData)
        //{
        //    OperatingData = algorithmOperatingData;

        //    // Temporary solution for accessing variable from OutputNodes[50]
        //    //OperatingData.PathNodes.Add(OperatingData.PathNodes.First());
        //}

        public void SwapPaths()
        {
            var changeMade = true;
            while ( changeMade )
            {
                changeMade = false;
                for ( var i = 0; i < OperatingData.PathNodes.Count - 1; i++ )
                {
                    for ( var j = 0; j < OperatingData.PathNodes.Count - 1; j++ )
                    {
                        if ( ( i == j ) || ( ( i + 1 ) == j ) || ( i == ( j + 1 ) ) ) continue;

                        var totalDistance = OperatingData.Distance;

                        var oldDistance1 = CalculateDistance(OperatingData.PathNodes[i], OperatingData.PathNodes[i + 1]);
                        var oldDistance2 = CalculateDistance(OperatingData.PathNodes[j], OperatingData.PathNodes[j + 1]);
                        totalDistance -= ( oldDistance1 + oldDistance2 );

                        var newDistance1 = CalculateDistance(OperatingData.PathNodes[i], OperatingData.PathNodes[j]);
                        var newDistance2 = CalculateDistance(OperatingData.PathNodes[i + 1], OperatingData.PathNodes[j + 1]);
                        totalDistance += ( newDistance1 + newDistance2 );

                        if ( totalDistance >= OperatingData.Distance ) continue;

                        Console.WriteLine(totalDistance);
                        OperatingData.Distance = totalDistance;
                        changeMade = true;
                        var newList = (List<Node>)OperatingData.PathNodes;
                        newList.Reverse(i + 1, Math.Abs(j - i));
                    }
                }
            }
        }
    }
}
