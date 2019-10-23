using GrafoCoyote.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoCoyote.Controllers
{
    class SolverController
    {
        
        public bool Solver(Vertex[,] grafo, Vertex start)
        {
            List<Vertex> unvisited = new List<Vertex>();
            Vertex u;

            for (int i = 0; i < grafo.GetLength(0); i++)
            {
                for (int j = 0; j < grafo.GetLength(1); j++)
                {
                    grafo[i, j].antecessor = null;
                    if (grafo[i, j] == start) grafo[i, j].minPath = 0;
                    else grafo[i, j].minPath = int.MaxValue;

                    unvisited.Add(grafo[i, j]);
                }
            }
            if (unvisited.Count == 0) return false;
            while (unvisited.Count > 0)
            {
                u = MenorDist(unvisited);
                if (u == null) return false;

                unvisited.Remove(u);

                foreach(Connections con in u.connections)
                {
                    if (con.ConnectedVertex.minPath == int.MaxValue)
                    {
                        con.ConnectedVertex.minPath = u.minPath + con.Cost;
                        con.ConnectedVertex.antecessor = u;
                    } else if (con.ConnectedVertex.minPath > u.minPath + con.Cost)
                    {
                        con.ConnectedVertex.minPath = u.minPath + con.Cost;
                        con.ConnectedVertex.antecessor = u;
                    }
                }
            }
            return true;
        }

        private Vertex MenorDist(List<Vertex> unvisited)
        {
            Vertex menor = null;
            foreach(Vertex v in unvisited)
            {
                if (menor == null) menor = v;
                else if(menor.minPath > v.minPath) menor = v;
            }

            return menor;
        }

    }
}
