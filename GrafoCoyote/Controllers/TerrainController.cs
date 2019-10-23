using GrafoCoyote.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrafoCoyote.Controllers
{
    class TerrainController
    {
        private string[] terrainTypes = new string[5] { "asphalt", "grass", "sand", "water", "pedra" };

        public int[] Coyote = new int[2];
        public int[] Papaleguas = new int[2];

        public Vertex[,] GenerateTerrain(int wid, int hgt, int cellSize, int Ymin, int Xmin)
        {
            Vertex[,] vertices = new Vertex[wid, hgt];
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            validPosition(wid, hgt);

            for (int i = 0; i < hgt; ++i)
            {
                int y = Ymin + cellSize * i;
                for (int j = 0; j < wid; ++j)
                {
                 
                    int x = Xmin + cellSize * j;
                    string type;
                    if (i == Coyote[0] && j == Coyote[1])
                    {
                        type = "coyote";
                    }
                    else if (i == Papaleguas[0] && j == Papaleguas[1])
                    {
                        type = "papaleguas";
                    }
                    else
                    {                     
                        Thread.Sleep(1);
                        type = terrainTypes[rnd.Next(0, 5)];
                    }
                    vertices[i, j] = new Vertex(x, y, cellSize, type);
                }
            }

            // Inicialize os vixinhos dos nós.
            for (int i = 0; i < hgt; ++i)
            {
                for (int j = 0; j < wid; ++j)
                {
                    if (vertices[i, j].terrainType != "pedra")
                    {
                        if (i > 0 && vertices[i - 1, j].terrainType != "pedra")
                        {
                            int cost;
                            if (vertices[i - 1, j].terrainType == "papaleguas") cost = 1;
                            else if (vertices[i - 1, j].terrainType == "coyote") cost = 1;
                            else cost = Array.IndexOf(terrainTypes, vertices[i - 1, j].terrainType);
                            Connections con = new Connections(cost + 1, vertices[i - 1, j]);
                            vertices[i, j].connections.Add(con);
                        }

                        if (i < hgt - 1 && vertices[i + 1, j].terrainType != "pedra")
                        {
                            int cost;
                            if (vertices[i + 1, j].terrainType == "papaleguas") cost = 1;
                            else if (vertices[i + 1, j].terrainType == "coyote") cost = 1;
                            else cost = Array.IndexOf(terrainTypes, vertices[i + 1, j].terrainType);
                            Connections con = new Connections(cost + 1, vertices[i + 1, j]);
                            vertices[i, j].connections.Add(con);
                        }
                        
                        if (j > 0 && vertices[i, j - 1].terrainType != "pedra")
                        {
                            int cost;
                            if (vertices[i, j - 1].terrainType == "papaleguas") cost = 1;
                            else if (vertices[i, j - 1].terrainType == "coyote") cost = 1;
                            else cost = Array.IndexOf(terrainTypes, vertices[i, j - 1].terrainType);
                            Connections con = new Connections(cost + 1, vertices[i, j - 1]);
                            vertices[i, j].connections.Add(con);
                        }
                        
                        if (j < wid - 1 && vertices[i, j + 1].terrainType != "pedra")
                        {
                            int cost;
                            if (vertices[i, j + 1].terrainType == "papaleguas") cost = 1;
                            else if (vertices[i, j + 1].terrainType == "coyote") cost = 1;
                            else cost = Array.IndexOf(terrainTypes, vertices[i, j + 1].terrainType);
                            Connections con = new Connections(cost + 1, vertices[i, j + 1]);
                            vertices[i, j].connections.Add(con);
                        }
                    }
                }
            }

            return vertices;
        }

        private void validPosition(int wid, int hgt)
        {
            int[] p = new int[2];
            int[] c = new int[2];
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            c[0] = rnd.Next(0, wid);
            c[1] = rnd.Next(0, hgt);
            p[0] = rnd.Next(0, wid);
            p[1] = rnd.Next(0, hgt);

            if (c[0] != p[0] && c[1] != p[1])
            {
                Papaleguas = p;
                Coyote = c;
            }
            else validPosition(wid, hgt);
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
                     //   gr.FillRectangle(Brushes.DarkCyan, vertices[i, j].Bounds);
                        vertices[i, j].DrawBlock(gr, pen);
                    }
                }
            }

            return bm;
        }

        public Bitmap DisplayPath(Vertex end, int cellSize, Bitmap image, Brush color)
        {
            Bitmap bm = image;

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                Vertex current = end;
                while(current.antecessor != null)
                {
                    current.DrawCenter(gr, color, cellSize);
                    current = current.antecessor;
                }
            }
            return bm;
        }
    }
}
