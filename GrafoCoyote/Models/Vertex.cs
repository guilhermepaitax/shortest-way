using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoCoyote.Models
{
    class Vertex
    {
        public string terrainType;
        public List<Connections> connections = new List<Connections>();
        public Rectangle Bounds;
        public int minPath;
        public Vertex antecessor;

        public Vertex(int positionX, int positionY, int size, string terrainType)
        {
            this.terrainType = terrainType;
            Bounds = new Rectangle(positionX, positionY, size, size);
        }

        // Desenhe um círculo .
        public void DrawCenter(Graphics gr, Brush brush, int cellSize)
        {
            float size = cellSize / 3;
            int cx = Bounds.Left + Bounds.Width / 2;
            int cy = Bounds.Top + Bounds.Height / 2;

            gr.FillEllipse(brush, cx - size / 2, cy - size / 2, size, size);
        }

        public void DrawBlock(Graphics gr, Pen pen)
        {
            switch (terrainType)
            {
                case "asphalt":
                    Bitmap asphalt = new Bitmap(Properties.Resources.asphalt);
                    using (Graphics gre = Graphics.FromImage(asphalt))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(asphalt, Bounds);
                    }
                    break;

                case "grass":
                    Bitmap grass = new Bitmap(Properties.Resources.grass);
                    using (Graphics gre = Graphics.FromImage(grass))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(grass, Bounds);
                    }
                    break;

                case "sand":
                    Bitmap sand = new Bitmap(Properties.Resources.sand);
                    using (Graphics gre = Graphics.FromImage(sand))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(sand, Bounds);
                    }
                    break;

                case "water":
                    Bitmap water = new Bitmap(Properties.Resources.water);
                    using (Graphics gre = Graphics.FromImage(water))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(water, Bounds);
                    }
                    break;
                case "pedra":
                    Bitmap pedra = new Bitmap(Properties.Resources.pedra);
                    using (Graphics gre = Graphics.FromImage(pedra))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(pedra, Bounds);
                    }
                    break;
                case "coyote":
                    Bitmap coyote = new Bitmap(Properties.Resources.coyote);
                    using (Graphics gre = Graphics.FromImage(coyote))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(coyote, Bounds);
                    }
                    break;
                case "papaleguas":
                    Bitmap papaleguas = new Bitmap(Properties.Resources.papaleguas);
                    using (Graphics gre = Graphics.FromImage(papaleguas))
                    {
                        gre.SmoothingMode = SmoothingMode.AntiAlias;
                        gr.DrawImage(papaleguas, Bounds);
                    }
                    break;
            }
        }
    }
}
