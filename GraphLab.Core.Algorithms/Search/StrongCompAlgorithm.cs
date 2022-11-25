using GraphLab.Core.Algorithms.Utils;
using System;
using System.Collections.Generic;

namespace GraphLab.Core.Algorithms.Search
{
    public class StrongComponentAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public StateManager<TVertex, SearchState> States { get; private set; }

        public StrongComponentAlgorithm()
        {
            States = new StateManager<TVertex, SearchState>(SearchState.None);
        }

        public List<TVertex[]> Search(Graph<TVertex, TEdge> Graph)
        {
            List<TVertex[]> Components = new List<TVertex[]>();

            foreach(var Vertex in Graph.Verticies)
            {
                if(States[Vertex] == SearchState.None)
                {
                    TVertex[] VertexesArray = Array.Empty<TVertex>();
                    Step(Vertex, ref VertexesArray, Graph);
                    Components.Add(VertexesArray);
                }
            }
            return Components;
        }

        private void Step(TVertex vertex, ref TVertex[] VertexesArray, Graph<TVertex, TEdge> Graph)
        {
            Array.Resize(ref VertexesArray, VertexesArray.Length + 1);
            States[vertex] = SearchState.Visited;
            VertexesArray[^1] = vertex;

            foreach(var Vertex in Graph[vertex])
            {
                if(States[Vertex] == SearchState.None)
                {
                    Step(Vertex, ref VertexesArray, Graph);
                }
            }   
        }

    }
}
