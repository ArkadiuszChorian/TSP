using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TSP.Models;

namespace TSP.Algorithms.OptimalizationAlgorithms
{
    class IteratedLocalSearch : LocalSearch
    {
        public long IterationTime { get; set; }
        public IList<Node> PreviousBestRoute { get; set; }
        public int PreviousBestDistance { get; set; } = int.MaxValue;
        private Stopwatch Timer { get; } = new Stopwatch();
        

        public override void Optimize()
        {
            Console.WriteLine($"Input Distance: {OperatingData.Distance}");
            Timer.Start();
            while (Timer.ElapsedMilliseconds < 80000) // Avarage time of 1000 cycles of NearestNeighbour Grasp
            {
                base.Optimize();
                if (OperatingData.Distance < PreviousBestDistance || PreviousBestRoute.Count == 0)
                {
                    PreviousBestRoute = OperatingData.PathNodes.CloneList();
                    PreviousBestDistance = OperatingData.Distance;
                }
                
                Perturbation();
            }
            Timer.Stop();
            Console.WriteLine($"Iterated Local Search Distance: {PreviousBestDistance}");
            foreach (var pathNode in PreviousBestRoute)
            {
                Console.Write($"{pathNode.Id} ");
            }
            Console.WriteLine();
        }

        private void Perturbation()
        {
            for ( var i = 0; i < Constants.PermutationMoves; i++ )
            {
                SwapPathsFirstIndex = RandomGenerator.Next(0, OperatingData.PathNodes.Count-1);
                do
                    SwapPathsSecondIndex = RandomGenerator.Next(0, OperatingData.PathNodes.Count - 1);
                while ( Math.Abs(SwapPathsFirstIndex - SwapPathsSecondIndex) < 2 );

                if (SwapPathsFirstIndex > SwapPathsSecondIndex)
                {
                    var temp = SwapPathsFirstIndex;
                    SwapPathsFirstIndex = SwapPathsSecondIndex;
                    SwapPathsSecondIndex = temp;
                }

                UpdatePerturbationDistance();
                SwapPathsPerturbation();
            }
        }

        private void SwapPathsPerturbation()
        {
            var newPath = (List<Node>)OperatingData.PathNodes;
            //var absolute = Math.Abs(SwapPathsSecondIndex - SwapPathsFirstIndex);
            //if (SwapPathsFirstIndex + absolute > newPath.Count - 1)
            //{
            //    var distanceToEnd = newPath.Count - SwapPathsFirstIndex - 1;
            //    newPath.Reverse(SwapPathsFirstIndex+1, distanceToEnd);
            //    newPath.Reverse(0, absolute - distanceToEnd);
            //}
            //else
                //newPath.Reverse(SwapPathsFirstIndex + 1, absolute);
                newPath.Reverse(SwapPathsFirstIndex + 1, Math.Abs(SwapPathsSecondIndex - SwapPathsFirstIndex));
        }

        public void UpdatePerturbationDistance()
        {
            var swapPathsFirstNextIndex = SwapPathsFirstIndex > OperatingData.PathNodes.Count - 1
                ? 0
                : SwapPathsFirstIndex + 1;
            var swapPathsSecondNextIndex = SwapPathsSecondIndex > OperatingData.PathNodes.Count - 1
                ? 0
                : SwapPathsSecondIndex + 1;

            OperatingData.Distance -=
                CalculateDistance(OperatingData.PathNodes[SwapPathsFirstIndex],
                    OperatingData.PathNodes[swapPathsFirstNextIndex]) +
                CalculateDistance(OperatingData.PathNodes[SwapPathsSecondIndex],
                    OperatingData.PathNodes[swapPathsSecondNextIndex]);

            OperatingData.Distance +=
                CalculateDistance(OperatingData.PathNodes[SwapPathsFirstIndex],
                    OperatingData.PathNodes[SwapPathsSecondIndex]) +
                CalculateDistance(OperatingData.PathNodes[swapPathsFirstNextIndex],
                    OperatingData.PathNodes[swapPathsSecondNextIndex]);
        }

        public override void ResetAlgorithm()
        {
            base.ResetAlgorithm();
            PreviousBestRoute.Clear();
            PreviousBestDistance = int.MaxValue;
            Timer.Reset();
        }
    }
}
