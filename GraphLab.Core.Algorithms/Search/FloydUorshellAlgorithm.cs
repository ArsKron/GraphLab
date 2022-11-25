using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class FloydUorshellAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }
        private int[,] LenghtMatrix { get; set; }
        public int [,] Next { get; set; }
        private List<TVertex> AllVertices { get; set; }


        public FloydUorshellAlgorithm(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
            LenghtMatrix = new int[Graph.Verticies.Count, Graph.Verticies.Count];
            Next = new int[Graph.Verticies.Count, Graph.Verticies.Count];
            AllVertices = new();
            foreach(var vert in Graph.Verticies) {
                AllVertices.Add(vert);
            }
            Graph.Verticies.First().Lenght = 0;
        }

        public int[,] Begin()
        {
            for (int i = 0; i < Graph.Verticies.Count; i++)
                for (int j = 0; j < Graph.Verticies.Count; j++)
                {
                    LenghtMatrix[i, j] = (i == j) ? 0 : int.MaxValue;
                    Next[i, j] = (i == j) ? i : 0;
                }

            foreach (var edge in Graph.Edges)
            {
                int k = AllVertices.IndexOf(edge.From);
                int m = AllVertices.IndexOf(edge.To);
                LenghtMatrix[k, m] = edge.Price;
                Next[k, m] = m;
            }

            for (int k = 0; k < Graph.Verticies.Count; k++)
                for (int i = 0; i < Graph.Verticies.Count; i++)
                    for (int j = 0; j < Graph.Verticies.Count; j++)
                        if (LenghtMatrix[i, j] > LenghtMatrix[i, k] + LenghtMatrix[k, j] 
                            && LenghtMatrix[i,k] != int.MaxValue && LenghtMatrix[k,j] != int.MaxValue 
                            && k != i && k != j && i != j)
                        {
                            int a = LenghtMatrix[i, k];
                            int b = LenghtMatrix[k, j];
                            LenghtMatrix[i, j] = a + b;
                            int c = LenghtMatrix[i, j];
                            int l = Next[i, k];
                            Next[i, j] = l;
                        }

            return LenghtMatrix;
        }
    }
}
