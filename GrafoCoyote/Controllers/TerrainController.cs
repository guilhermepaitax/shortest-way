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

        public List<Vertex> GenerateTerrain(int wid, int hgt, int cellSize, int Ymin, int Xmin)
        {
            List<Vertex> vertices = new List<Vertex>();

            for (int i = 0; i < hgt; i++)
            {
                int y = Ymin + cellSize * i;
                for (int j = 0; j < wid; j++)
                {
                    int x = Xmin + cellSize * j;
                    string type;
                    if (i == 0 && j == 0) type = "coyote";
                    else if (i == hgt - 1 && j == wid - 1) type = "papaleguas";
                    else type = RandonTarrainType();
                    Vertex vertex = new Vertex(x, y, cellSize, type);
                    vertices.Add(vertex);
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

        public Bitmap DisplayTerrain(List<Vertex> vertices, int picWid, int picHgt, int cellSize)
        {
            Pen pen = new Pen(Brushes.DarkCyan);
            Bitmap bm = new Bitmap(picWid, picHgt);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                foreach (Vertex vert in vertices)
                {
                    gr.FillRectangle(Brushes.DarkCyan, vert.Bounds);
                    vert.DrawBlock(gr, pen);
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
