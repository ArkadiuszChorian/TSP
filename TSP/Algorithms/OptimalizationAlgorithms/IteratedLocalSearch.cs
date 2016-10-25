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
        //public IList<Node> PreviousBestRoute { get; set; } = new List<Node>();
        //public int PreviousBestDistance { get; set; } = int.MaxValue;
        public AlgorithmOperatingData BestData { get; set; } = new AlgorithmOperatingData {Distance = int.MaxValue};
        private Stopwatch Timer { get; } = new Stopwatch();
        public long AverangeMslsTime { get; set; }
        //public long AverangeMslsTime { get; set; } = 5000;
        

        public override void Optimize()
        {
            AverangeMslsTime = DAL.Instance.AverangeMslsTime/100;
            Console.WriteLine($"Input Distance: {OperatingData.Distance}");
            Timer.Start();
            base.Optimize();
            BestData.PathNodes = OperatingData.PathNodes.CloneList();
            BestData.Distance = OperatingData.Distance;
            while (Timer.ElapsedMilliseconds < AverangeMslsTime)
            {
                OperatingData = BestData.CloneData();              
                Perturbation();
                base.Optimize();

                if (OperatingData.Distance >= BestData.Distance && BestData.PathNodes.Count != 0) continue;
                BestData = OperatingData.CloneData();
                //BestData.PathNodes = OperatingData.PathNodes.CloneList();
                //BestData.Distance = OperatingData.Distance;
            }
            Timer.Stop();
            OperatingData.PathNodes = BestData.PathNodes.CloneList();
            OperatingData.Distance = BestData.Distance;
            Console.WriteLine($"Iterated Local Search Distance: {BestData.Distance}");
            foreach (var pathNode in BestData.PathNodes)
            {
                Console.Write($"{pathNode.Id} ");
            }
            Console.WriteLine();
        }

        private void Perturbation()
        {
            for ( var i = 0; i < Constants.PerturbationMoves; i++ )
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
            //PreviousBestRoute.Clear();
            //PreviousBestDistance = int.MaxValue;
            BestData.PathNodes.Clear();
            BestData.Distance = int.MaxValue;
            Timer.Reset();
        }
    }
}
