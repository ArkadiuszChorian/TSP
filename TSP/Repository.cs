using System;
using System.Collections.Generic;
using System.IO;

namespace TSP
{
    class Repository
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public void ReadFromFile(string fileName)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    for (int i = 0; i < 6; i++)
                    {
                        streamReader.ReadLine();
                    }
                    
                    string[] parameters;

                    while (!streamReader.EndOfStream)
                    {
                        parameters = streamReader.ReadLine().Split(' ');

                        Nodes.Add(new Node(int.Parse(parameters[0]), int.Parse(parameters[1]), int.Parse(parameters[2])));
                        //Console.WriteLine("Check this out: " + Nodes[1].Id + " " + Nodes[1].X + " " + Nodes[1].Y);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            //Console.WriteLine("Check this out: " + Nodes[1].Id + " " + Nodes[1].X + " " + Nodes[1].Y);
        }
    }
}
