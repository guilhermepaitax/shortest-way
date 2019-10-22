using GrafoCoyote.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoCoyote.Controllers
{
    class TerrainController
    {
        private string[] terrainTypes = new string[5] { "asphalt", "grass", "sand", "water", "pedra" };

        public Vertex[,] GenerateTerrain(int wid, int hgt, int cellSize, int Ymin, int Xmin)
        {
            Vertex[,] vertices = new Vertex[hgt, wid];

            for (int i = 0; i < hgt; ++i)
            {
                int y = Ymin + cellSize * i;
                for (int j = 0; j < wid; ++j)
                {
                    int x = Xmin + cellSize * j;
                    string type;
                    if (i == 0 && j == 0) type = "coyote";
                    else if (i == hgt - 1 && j == wid - 1) type = "papaleguas";
                    else type = RandonTarrainType();
                    vertices[i, j] = new Vertex(x, y, cellSize, type);
                }
            }

            // Inicialize os neighbors dos nós.
            for (int i = 0; i < hgt; ++i)
            {
                for (int j = 0; j < wid; ++j)
                {
                    if (i > 0 && vertices[i - 1, j].terrainType != "pedra")
                    {
                        int cost = Array.IndexOf(terrainTypes, vertices[i - 1, j].terrainType);
                        Connections con = new Connections(cost + 1, vertices[i - 1, j]);
                        vertices[i, j].connections.Add(con);
                    }

                    if (i < hgt - 1 && vertices[i + 1, j].terrainType != "pedra")
                    {
                        int cost = Array.IndexOf(terrainTypes, vertices[i + 1, j].terrainType);
                        Connections con = new Connections(cost + 1, vertices[i + 1, j]);
                        vertices[i, j].connections.Add(con);
                    }
                        
                    if (j > 0 && vertices[i, j - 1].terrainType != "pedra")
                    {
                        int cost = Array.IndexOf(terrainTypes, vertices[i, j - 1].terrainType);
                        Connections con = new Connections(cost + 1, vertices[i, j - 1]);
                        vertices[i, j].connections.Add(con);
                    }
                        
                    if (j < wid - 1 && vertices[i, j + 1].terrainType != "pedra")
                    {
                        int cost = Array.IndexOf(terrainTypes, vertices[i, j + 1].terrainType);
                        Connections con = new Connections(cost + 1, vertices[i, j + 1]);
                        vertices[i, j].connections.Add(con);
                    }
                }
            }

            return vertices;
        }

        private string RandonTarrainType()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int rndNum = rnd.Next(0, 5);
            return terrainTypes[rndNum];
        }

        public Bitmap DisplayTerrain(Vertex[,] vertices, int picWid, int picHgt, int cellSize)
        {
            Pen pen = new Pen(Brushes.DarkCyan);
            Bitmap bm = new Bitmap(picWid, picHgt);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                for (int i = 0; i < vertices.GetLength(0); i++)
                {
                    for (int j = 0; j < vertices.GetLength(1); j++)
                    {
                        gr.FillRectangle(Brushes.DarkCyan, vertices[i, j].Bounds);
                        vertices[i, j].DrawBlock(gr, pen);
                    }
                }
            }

            return bm;
        }

        public Bitmap DisplayPath(List<Vertex> path, int cellSize, Bitmap image, Brush color)
        {
            Bitmap bm = image;

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                foreach (Vertex vertice in path)
                {
                    vertice.DrawCenter(gr, color, cellSize);
                }
            }
            return bm;
        }
    }
}
