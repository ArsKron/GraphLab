using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLab.Core.Algorithms.Search
{
    public class EulerianCycle<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        private List<TVertex> VertArray1 { get; set; }
        private List<TVertex> AllVertices { get; set; }
        private Stack<TVertex> VertArray2 { get; set; }
        private List<TEdge> UnusedEdges { get; set; }


        public EulerianCycle(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            AllVertices = new();
            VertArray1 = new();
        }

        public List<TVertex> Begin()
        {
            VertArray2 = new();
            VertArray2.Push(Graph.Verticies.First());

            foreach (var Vertex in Graph.Verticies)
            {
                AllVertices.Add(Vertex);
            }

            UnusedEdges = new();
            foreach (var edge in Graph.Edges)
                UnusedEdges.Add(edge);

            while (VertArray2.Count != 0)
            {
                TVertex From = VertArray2.Peek();
                List<TEdge> UnusedEdgesForFrom = new();
                foreach (var Vertex in Graph[From])
                {
                    TEdge edge = UnusedEdges.Find(x => x.From == From && x.To == Vertex);
                    TEdge edge2 = UnusedEdges.Find(x => x.From == Vertex && x.To == From);
                    if (edge != null && edge2 != null)
                        UnusedEdgesForFrom.Add(edge);
                }

                if(UnusedEdgesForFrom.Count == 0)
                {
                    From = VertArray2.Pop();
                    VertArray1.Add(From);
                }
                else
                {
                    VertArray2.Push(UnusedEdgesForFrom[0].To);
                    TEdge edge2 = UnusedEdges.Find(x => x.From == UnusedEdgesForFrom[0].To && x.To == UnusedEdgesForFrom[0].From);
                    UnusedEdges.Remove(UnusedEdgesForFrom[0]);
                    UnusedEdges.Remove(edge2);
                }
            }

            return VertArray1;
        }
    }
}
