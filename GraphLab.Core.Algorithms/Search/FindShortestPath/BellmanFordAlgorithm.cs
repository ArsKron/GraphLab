using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLab.Core.Algorithms.Search.FindShortestPath
{
    public class BellmanFordAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public List<List<int>> lenght { get; set; }
        public List<TVertex> AllVertices { get; set; }


        public BellmanFordAlgorithm(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            AllVertices = new();
            Graph.Verticies.First().Lenght = 0;
        }

        public List<List<int>> Begin()
        {
            lenght = new();
            AllVertices = Graph.Verticies.ToList();

            List<int> Distances = new();
            for (int i = 0; i < AllVertices.Count; i++)
            {
                if (i == 0)
                {
                    Distances.Add(0);
                }
                else
                {
                    Distances.Add(2147483647);
                }
            }


            for (int i = 0; i < AllVertices.Count - 1; i++)
            {
                foreach (var edge in Graph.Edges)
                {
                    if (Distances[AllVertices.IndexOf(edge.From)] < 2147483647)
                    {
                        Distances[AllVertices.IndexOf(edge.To)] = Math.Min(Distances[AllVertices.IndexOf(edge.To)], Distances[AllVertices.IndexOf(edge.From)] + edge.Price);
                        edge.To.Lenght = Distances[AllVertices.IndexOf(edge.To)];
                    }
                }

                List<int> Res = new();
                foreach (var Dist in Distances)
                {
                    Res.Add(Dist);
                }

                lenght.Add(Res);
            }
            return lenght;
        }
    }
}
