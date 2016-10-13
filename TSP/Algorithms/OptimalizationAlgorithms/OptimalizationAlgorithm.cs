using System;
using System.Collections.Generic;
using System.Linq;
using TSP.Algorithms.ConstructionAlgorithms;
using TSP.Models;

namespace TSP.Algorithms.OptimalizationAlgorithms
{
    class OptimalizationAlgorithm : ConstructionAlgorithm
    {
        public DataModel InputData { get; set; }

        public OptimalizationAlgorithm( DataModel inputData )
        {
            InputData = inputData;

            // Temporary solution for accessing variable from OutputNodes[50]
            InputData.OutputNodes.Add(InputData.OutputNodes.First());
        }

        public void SwapPaths()
        {
            var changeMade = true;
            while ( changeMade )
            {
                changeMade = false;
                for ( var i = 0; i < InputData.OutputNodes.Count - 1; i++ )
                {
                    for ( var j = 0; j < InputData.OutputNodes.Count - 1; j++ )
                    {
                        if ( ( i == j ) || ( ( i + 1 ) == j ) || ( i == ( j + 1 ) ) ) continue;

                        var totalDistance = InputData.Distance;

                        var oldDistance1 = CalculateDistance(InputData.OutputNodes[i], InputData.OutputNodes[i + 1]);
                        var oldDistance2 = CalculateDistance(InputData.OutputNodes[j], InputData.OutputNodes[j + 1]);
                        totalDistance -= ( oldDistance1 + oldDistance2 );

                        var newDistance1 = CalculateDistance(InputData.OutputNodes[i], InputData.OutputNodes[j]);
                        var newDistance2 = CalculateDistance(InputData.OutputNodes[i + 1], InputData.OutputNodes[j + 1]);
                        totalDistance += ( newDistance1 + newDistance2 );

                        if ( totalDistance >= InputData.Distance ) continue;

                        Console.WriteLine(totalDistance);
                        InputData.Distance = totalDistance;
                        changeMade = true;
                        var newList = (List<Node>)InputData.OutputNodes;
                        newList.Reverse(i + 1, Math.Abs(j - i));
                    }
                }
            }
        }
    }
}
