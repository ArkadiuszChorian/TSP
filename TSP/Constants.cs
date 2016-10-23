using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    static class Constants
    {
        public static readonly string NearestNeighbourText = "---Nearest Neighbour---";
        public static readonly string NearestNeighboutGraspText = "---Nearest Neighbour Grasp---";
        public static readonly string GreedyCycleText = "---Greedy Cycle---";
        public static readonly string GreedyCycleGraspText = "---Greedy Cycle Grasp---";
        public static readonly string RandomSolutionText = "---Random Solution---";
        public static readonly string MultipleStartLocalSearchText = "---MultipleStartLocalSearch---";
        public static readonly string IteratedLocalSearchText = "---IteratedLocalSearch---";

        public static readonly string NearestNeighbourFilename = "NearestNeighbour.bmp";
        public static readonly string NearestNeighbourGraspFilename = "NearestNeighbourGrasp.bmp";
        public static readonly string GreedyCycleFilename = "GreedyCycle.bmp";
        public static readonly string GreedyCycleGraspFilename = "GreedyCycleGrasp.bmp";
        public static readonly string RandomSolutionFilename = "RandomSolution.bmp";
         
        public static readonly string NearestNeighbourOptimalizedFilename = "NearestNeighbourOptimalized.bmp";
        public static readonly string NearestNeighbourGraspOptimalizedFilename = "NearestNeighbourGraspOptimalized.bmp";
        public static readonly string GreedyCycleOptimalizedFilename = "GreedyCycleOptimalized.bmp";
        public static readonly string GreedyCycleGraspOptimalizedFilename = "GreedyCycleGraspOptimalized.bmp";
        public static readonly string RandomSolutionOptimalizedFilename = "RandomSolutionOptimalized.bmp";

        public static readonly string MultipleStartLocalSearchFilename = "MultipleStartLocalSearch.bmp";
        public static readonly string IteratedLocalSearchFilename = "IteratedLocalSearch.bmp";

        public static readonly int PermutationMoves = 5;
    }
}
