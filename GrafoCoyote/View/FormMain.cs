using GrafoCoyote.Controllers;
using GrafoCoyote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafoCoyote
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private int Xmin, Ymin, CellSize;
        Vertex[,] grafo;

        private void btnSolve_Click(object sender, EventArgs e)
        {
            int wid = int.Parse(numLargura.Text);
            int hgt = int.Parse(numAltura.Text);

            SolverController solverController = new SolverController();
            solverController.Solver(grafo, grafo[0, 0]);
            TerrainController terrainController = new TerrainController();
            Bitmap bitmap = new Bitmap(picTerrain.Image);
            picTerrain.Image = terrainController.DisplayPath(grafo[wid - 1, hgt - 1], int.Parse(numTamanhoBlc.Text), bitmap, Brushes.MediumSlateBlue);
            lblCost.Text = "Custo Total: " + grafo[wid - 1, hgt - 1].minPath;
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            int wid = int.Parse(numLargura.Text);
            int hgt = int.Parse(numAltura.Text);

            CellSize = int.Parse(numTamanhoBlc.Text);

            Xmin = (picTerrain.ClientSize.Width - wid * CellSize) / 2;
            Ymin = (picTerrain.ClientSize.Height - hgt * CellSize) / 2;

            TerrainController terrainController = new TerrainController();
            grafo = terrainController.GenerateTerrain(wid, hgt, CellSize, Ymin, Xmin);
            picTerrain.Image = terrainController.DisplayTerrain(grafo, picTerrain.ClientSize.Width, picTerrain.ClientSize.Height, CellSize);
        }
    }
}
