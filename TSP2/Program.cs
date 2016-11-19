using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TSP2
{
    class Program
    {
        //private const int NumberOfLocalSearchResults = 1000;
        //private const int EvaluationTimeInMiliseconds = 83360;
        //private const int NumberOfHybridGeneticAlgorithmExecutions = 10;
        //private const int PopulationSize = 20;
        static void Main(string[] args)
        {
            DAL.Instance.ReadFromFile();

            var hybridAlgorithmGlobalTimer = new Stopwatch();
            var localSearchTimer = new Stopwatch();
            var randomSolution = new RandomSolution();
            var localSearch = new LocalSearch();
            var random = new Random();           
            var hybridStatistics = new List<List<HybridAlgorithmStatisticsData>>(DAL.NumberOfHybridGeneticAlgorithmExecutions);
            //var population = new List<OperatingData>(20);          

            for (var i = 0; i < DAL.NumberOfHybridGeneticAlgorithmExecutions; i++)
            {
                Console.WriteLine("Started iteration number:    " + (i+1));
                
                var population = GenerateInitialPopulation();
                //var hybridStatisticsData = new HybridAlgorithmStatisticsData();
                var iterationNumber = 0;
                //var singleExecutionTime = long.MinValue;    
                var hybridStatisticsSingleExecutionsList = new List<HybridAlgorithmStatisticsData>();        

                hybridAlgorithmGlobalTimer.Restart();

                //Console.WriteLine("--- Ready to start iterating ---");
                //Console.ReadKey();

                //while (hybridAlgorithmGlobalTimer.ElapsedMilliseconds < DAL.EvaluationTimeInMiliseconds)
                while (hybridAlgorithmGlobalTimer.ElapsedMilliseconds < DAL.EvaluationTimeInMiliseconds)
                {                    
                    var parent1 = population[random.Next(0, population.Count)];
                    var parent2 = population[random.Next(0, population.Count)];
                    var worstSolution = FindWorstSolution(population);

                    while (parent1.Equals(parent2))
                    {
                        parent2 = population[random.Next(0, population.Count)];
                    }

                    var child = Recombine(parent1, parent2);

                    localSearchTimer.Reset();
                    localSearchTimer.Start();
                    localSearch.Optimize(child);
                    localSearchTimer.Stop();
                    //singleExecutionTime += localSearchTimer.ElapsedMilliseconds;

                    if (population.Any(solution => solution.Distance == child.Distance) == false && child.Distance < worstSolution.Distance)
                    {
                        population.Remove(worstSolution);
                        population.Add(child);
                    }

                    hybridStatisticsSingleExecutionsList.Add(new HybridAlgorithmStatisticsData
                    {
                        Distance = FindBestDistance(population),
                        ExecutionTime = localSearchTimer.Elapsed.Milliseconds,
                        IterationNumber = ++iterationNumber
                    });

                    if (iterationNumber % 100 == 0)
                    {
                        Console.WriteLine("--- Iteration number: " + iterationNumber + " ---");
                    }                  
                }

                hybridStatistics.Add(hybridStatisticsSingleExecutionsList);

                Console.WriteLine("\n--- BEST DISTANCE = " + FindBestDistance(population) + " ---\n");

                //DAL.Instance.HybridAlgorithmStatistics[i] = Tuple.Create(i+1, long.MinValue, FindBestDistance(population));
            }        
            
            hybridAlgorithmGlobalTimer.Stop();
                  
            DAL.Instance.PrepareFileToWrite();  
            DAL.Instance.WriteToFileForHybridAlgorithm(hybridStatistics);  
            DAL.Instance.CloseFileToWrite();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static void SimplifyResults(List<List<HybridAlgorithmStatisticsData>> data)
        {
            
        }

        private static OperatingData FindWorstSolution(List<OperatingData> population)
        {
            var worstSolution = new OperatingData { Distance = 0 };

            foreach (var solution in population)
            {
                if (solution.Distance > worstSolution.Distance)
                {
                    worstSolution = solution;
                }
            }

            return worstSolution;
        }

        private static int FindBestDistance(List<OperatingData> population)
        {
            var bestDistance = int.MaxValue;

            foreach (var solution in population)
            {
                if (solution.Distance < bestDistance)
                {
                    bestDistance = solution.Distance;
                }
            }

            return bestDistance;
        }

        private static List<OperatingData> GenerateInitialPopulation()
        {
            var randomSolution = new RandomSolution();
            var localSearch = new LocalSearch();
            var population = new List<OperatingData>();

            for (var i = 0; i < DAL.PopulationSize; i++)
            {
                var solution = new OperatingData {UnusedNodes = DAL.Instance.Nodes.CloneList()};               
                localSearch.Optimize(randomSolution.FindRouteFromRandomStart(solution));

                while (population.Any(solution2 => solution2.Distance == solution.Distance))
                {
                    solution = new OperatingData { UnusedNodes = DAL.Instance.Nodes.CloneList() };
                    localSearch.Optimize(randomSolution.FindRouteFromRandomStart(solution));
                }

                population.Add(solution);
            }

            return population;
        }

        private static int recombineIteration = 0;

        private static OperatingData Recombine(OperatingData data1, OperatingData data2)
        {
            recombineIteration++;
            var recombinedData = new OperatingData();

            ResolveEdgesMatrix(data1);
            ResolveEdgesMatrix(data2);

            var edgesCountMatrix = new Dictionary<Node, int>();
            var sharedVertices = ResolveSharedVertices(data1.PathNodes, data2.PathNodes);
            var sharedEdgesMatrix = ResolveSharedEdgesMatrix(sharedVertices, data1.EdgesMatrix, data2.EdgesMatrix,
                out edgesCountMatrix);

            var newPathNodes = FillNodeList(sharedVertices);

            if (sharedVertices.Count < 50)
            {               
                var nodesToConnect = RemoveUnnecessaryNodes(newPathNodes, edgesCountMatrix);
                //ConnectRemainingVertices(sharedVertices, sharedEdgesMatrix);
                ConnectRemainingVertices(nodesToConnect, sharedEdgesMatrix);
            }
            
            recombinedData.EdgesMatrix = sharedEdgesMatrix;
            //recombinedData.PathNodes = ConvertEdgesMatrixToList(sharedEdgesMatrix);
            recombinedData.PathNodes = newPathNodes;
            recombinedData.UnusedNodes = FindUnusedNodes(recombinedData.PathNodes.ToList());
            recombinedData.Distance = CalculatePathDistance(recombinedData.PathNodes.ToList());

            return recombinedData;
        }

        private static List<Node> FindUnusedNodes(List<Node> pathNodes)
        {
            var unusedNodes = DAL.Instance.Nodes.CloneList().ToList();

            pathNodes.ForEach(pathNode => unusedNodes.Remove(pathNode));

            return unusedNodes;
        }

        private static int CalculatePathDistance(List<Node> nodes)
        {
            var distance = (int)(Math.Sqrt(Math.Pow(nodes.First().X - nodes.Last().X, 2) + Math.Pow(nodes.First().Y - nodes.Last().Y, 2)) + 0.5);

            for (var i = 0; i < nodes.Count - 1; i++)
            {
                distance += (int)(Math.Sqrt(Math.Pow(nodes[i+1].X - nodes[i].X, 2) + Math.Pow(nodes[i+1].Y - nodes[i].Y, 2)) + 0.5);
            }

            return distance;
        }

        private static void ResolveEdgesMatrix(OperatingData data)
        {
            var nodes = data.PathNodes;

            data.EdgesMatrix[nodes.First()][nodes.Last()] = true;
            data.EdgesMatrix[nodes.Last()][nodes.First()] = true;

            for (var i = 0; i < nodes.Count - 1; i++)
            {
                data.EdgesMatrix[nodes[i]][nodes[i+1]] = true;
                data.EdgesMatrix[nodes[i+1]][nodes[i]] = true;
            }
        }

        private static List<Node> FillNodeList(List<Node> nodes)
        {
            var random = new Random();

            while (nodes.Count < 50)
            {
                var nodeToAdd = (Node)DAL.Instance.Nodes[random.Next(0, DAL.Instance.Nodes.Count)].Clone();

                while (nodes.Contains(nodeToAdd))
                {
                    nodeToAdd = (Node)DAL.Instance.Nodes[random.Next(0, DAL.Instance.Nodes.Count)].Clone();
                }

                nodes.Add(nodeToAdd);
            }

            return nodes;
        }

        //private static void RemoveUnnecessaryNodes(List<Node> nodes, Dictionary<Node, int> edgesCountMatrix)
        //{
        //    foreach (var node in edgesCountMatrix.Keys)
        //    {
        //        if (edgesCountMatrix[node] >= 2)
        //        {
        //            nodes.Remove(node);
        //        }
        //    }
        //}

        private static List<Node> RemoveUnnecessaryNodes(List<Node> nodes, Dictionary<Node, int> edgesCountMatrix)
        {
            var outputList = (List<Node>)nodes.CloneList();

            foreach (var node in edgesCountMatrix.Keys)
            {
                if (edgesCountMatrix[node] >= 2)
                {
                    outputList.Remove(node);
                }
            }

            return outputList;
        }

        private static void ConnectRemainingVertices(List<Node> nodes, Dictionary<Node, Dictionary<Node, bool>> edgesMatrix)
        {
            var random = new Random();
           
            while (nodes.Count > 2)
            {
                var firstNode = nodes[random.Next(0, nodes.Count)];
                var secondNode = nodes[random.Next(0, nodes.Count)];

                while (!IsConnectionValid(firstNode, secondNode, nodes, edgesMatrix))
                {
                    secondNode = nodes[random.Next(0, nodes.Count)];
                }

                edgesMatrix[firstNode][secondNode] = true;
                edgesMatrix[secondNode][firstNode] = true;

                if (CanBeConnected(firstNode, edgesMatrix) == false)
                {
                    nodes.Remove(firstNode);
                }

                if (CanBeConnected(secondNode, edgesMatrix) == false)
                {
                    nodes.Remove(secondNode);
                }
            }

            edgesMatrix[nodes.First()][nodes.Last()] = true;
            edgesMatrix[nodes.Last()][nodes.First()] = true;
        }

        private static bool IsConnectionValid(Node node1, Node node2, List<Node> nodesToConnect, Dictionary<Node, Dictionary<Node, bool>> edgesMatrix)
        {
            //foreach (var row in edgesMatrix)
            //{
            //    foreach (var column in row.Value)
            //    {
            //        Console.Write(Convert.ToInt32(column.Value) + " ");
            //    }

            //    Console.WriteLine("\n");
            //}

            //var noOne = 0;
            //var singleOne = 0;
            //var doubleOne = 0;
            //var errors = 0;

            //foreach (var row in edgesMatrix)
            //{
            //    var oneInRow = 0;

            //    foreach (var column in row.Value)
            //    {
            //        if (column.Value)
            //        {
            //            oneInRow++;
            //        }
            //    }

            //    switch (oneInRow)
            //    {
            //        case 0:
            //            noOne++;
            //            break;
            //        case 1:
            //            singleOne++;
            //            break;
            //        case 2:
            //            doubleOne++;
            //            break;
            //        default:
            //            errors++;
            //            break;
            //    }
            //}

            //Console.WriteLine("none  :  " + noOne);
            //Console.WriteLine("single:  " + singleOne);
            //Console.WriteLine("double:  " + doubleOne);
            //Console.WriteLine("errors:  " + errors);

            //if (singleOne < 4)
            //{
            //    Console.WriteLine("\n### ERROR ###");
            //    Console.WriteLine("Recombine iteration: " + recombineIteration);
            //    Debugger.Break();
            //    //Console.ReadKey();
            //}       

            //Console.Write("#");

            if (node1.Equals(node2))
            {
                return false;
            }

            if (!edgesMatrix[node1].Any(column => column.Value) || !edgesMatrix[node2].Any(column => column.Value))
            {
                return true;
            }

            //var numberOfAloneNodes = 0;
            var numberOfEdgePartNodes = 0;

            foreach (var row in edgesMatrix)
            {
                var numberOfOnes = 0;

                foreach (var column in row.Value)
                {
                    if (column.Value)
                    {
                        numberOfOnes++;
                    }
                }

                if (numberOfOnes != 1) continue;
                if (++numberOfEdgePartNodes >= 4)
                {
                    return true;
                }

                //switch (numberOfOnes)
                //{
                //    case 0:
                //        numberOfAloneNodes++;
                //        break;
                //    case 1:
                //        numberOfEdgePartNodes++;
                //        break;
                //}
            }           

            //if (numberOfEdgePartNodes < 4 && numberOfAloneNodes > 0)
            //{
            //    return false;
            //}                    

            //var isAnalyzingEdge = true;
            //var currentNode = node1;
            //var previousNode = node2;
            //var counter = 0;

            //while (isAnalyzingEdge)
            //{
            //    //Console.Write(++counter + " ");
            //    if (counter > 60)
            //    {

            //    }
            //    foreach (var column in edgesMatrix[currentNode])
            //    {
            //        isAnalyzingEdge = false;

            //        if (column.Value && !column.Key.Equals(previousNode))
            //        {
            //            previousNode = (Node)currentNode.Clone();
            //            currentNode = column.Key;
            //            isAnalyzingEdge = true;
            //            break;
            //        }
            //    }
            //}

            //Console.WriteLine(!currentNode.Equals(node2));
            //Console.WriteLine("PRESS ENTER");
            ////Console.ReadKey();
            //return !currentNode.Equals(node2);
            return true;
        }

        private static bool CanBeConnected(Node node, Dictionary<Node, Dictionary<Node, bool>> edgesMatrix)
        {
            var numberOfEdges = edgesMatrix[node].Count(column => column.Value);

            return numberOfEdges < 2;
        }

        private static List<Node> ConvertEdgesMatrixToList(Dictionary<Node, Dictionary<Node, bool>> edgesMatrix)
        {
            var nodes = new List<Node>();

            var isAnalyzingEdge = true;
            Node firstNode = null, currentNode = null, previousNode = null;

            foreach (var row in edgesMatrix)
            {
                if (edgesMatrix[row.Key].Any(column => column.Value))
                {
                    firstNode = row.Key;
                    nodes.Add(firstNode);
                    currentNode = firstNode;
                    break;
                }
            }

            while (isAnalyzingEdge)
            {
                foreach (var column in edgesMatrix[currentNode])
                {
                    isAnalyzingEdge = false;                   

                    if (column.Value && !column.Key.Equals(previousNode))
                    {
                        if (column.Key.Equals(firstNode))
                        {
                            break;
                        }

                        previousNode = (Node)currentNode.Clone();
                        currentNode = column.Key;
                        nodes.Add(currentNode);
                        isAnalyzingEdge = true;
                        break;
                    }
                }
            }

            return nodes;
        }

        private static List<Node> ResolveSharedVertices(IList<Node> nodes1, IList<Node> nodes2)
        {
            var outputList = new List<Node>();

            for (var i = 0; i < nodes1.Count; i++)
            {
                for (var j = 0; j < nodes2.Count; j++)
                {
                    if (nodes1[i].Id == nodes2[j].Id)
                    {
                        if (outputList.Contains(nodes1[i]))
                        {
                            Console.WriteLine("### PROBLEM ###\n");
                            Console.WriteLine("nodes1");
                            var nodes1Sorted = nodes1.OrderBy(node => node.Id);
                            var nodes2Sorted = nodes2.OrderBy(node => node.Id);
                            foreach (var node in nodes1Sorted)
                            {
                                Console.Write(node.Id + " ");
                            }
                            Console.WriteLine("\n\nnodes2");
                            foreach (var node in nodes2Sorted)
                            {
                                Console.Write(node.Id + " ");
                            }
                            Debugger.Break();
                        }
                        outputList.Add((Node)nodes1[i].Clone());
                    }
                }
            }

            return outputList;
            //return nodes1.Count(nodes2.Contains);
        }

        private static Dictionary<Node, Dictionary<Node, bool>> ResolveSharedEdgesMatrix(List<Node> sharedVertices, Dictionary<Node, Dictionary<Node, bool>> edges1, Dictionary<Node, Dictionary<Node, bool>> edges2, out Dictionary<Node, int> edgesCountMatrix)
        {
            var sharedEdgesMatrix = DAL.Instance.Nodes.CloneList()
                .ToDictionary(node => node,
                    node => DAL.Instance.Nodes.CloneList().ToDictionary(node2 => node2, node2 => false));

            edgesCountMatrix = sharedVertices.ToDictionary(node => node, node => 0);
            //edgesCountMatrix = new Dictionary<Node, int>(sharedVertices.Count);

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

//OLD MAIN

//var randomSolution = new RandomSolution();
//var localSearch = new LocalSearch();
//var currentOperatingAndStatisticsData = new OperatingAndStatisticsData();
//var optimizedResults = new List<OperatingAndStatisticsData>();
//var bestOptimizedResultDistance = int.MaxValue;
//var bestOptimizedResultIndex = 0;

//for (var i = 0; i < NumberOfLocalSearchResults; i++)
//{
//    currentOperatingAndStatisticsData.ClearData();
//    currentOperatingAndStatisticsData.OperatingData.UnusedNodes = DAL.Instance.Nodes.CloneList();
//    currentOperatingAndStatisticsData.OperatingData =
//        localSearch.Optimize(randomSolution.FindRouteFromRandomStart(currentOperatingAndStatisticsData.OperatingData));
//    optimizedResults.Add(currentOperatingAndStatisticsData.CloneData());

//    if (currentOperatingAndStatisticsData.OperatingData.Distance >= bestOptimizedResultDistance) continue;
//    bestOptimizedResultDistance = currentOperatingAndStatisticsData.OperatingData.Distance;
//    bestOptimizedResultIndex = i;
//}

//for (var i = 0; i < optimizedResults.Count; i++)
//{
//    for (var j = 0; j < optimizedResults.Count; j++)
//    {
//        if (i == j) continue;
//        optimizedResults[i].AverangeSimilarityToOthersBySharedVertices += Comparer.SharedVerticesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[j].OperatingData.PathNodes);
//        optimizedResults[i].AverangeSimilarityToOthersBySharedEdges += Comparer.SharedEdgesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[j].OperatingData.PathNodes);

//    }
//    if (i == bestOptimizedResultIndex) continue;
//    optimizedResults[i].SimilarityToBestBySharedVertices = Comparer.SharedVerticesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[bestOptimizedResultIndex].OperatingData.PathNodes);
//    optimizedResults[i].SimilarityToBestBySharedEdges = Comparer.SharedEdgesRatio(optimizedResults[i].OperatingData.PathNodes, optimizedResults[bestOptimizedResultIndex].OperatingData.PathNodes);
//}

//optimizedResults.ForEach(result => result.AverangeSimilarityToOthersBySharedVertices = result.AverangeSimilarityToOthersBySharedVertices / (optimizedResults.Count - 1));
//optimizedResults.ForEach(result => result.AverangeSimilarityToOthersBySharedEdges = result.AverangeSimilarityToOthersBySharedEdges / (optimizedResults.Count - 1));