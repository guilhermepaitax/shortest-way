using GrafoCoyote.Controllers;
using GrafoCoyote.Models;
using System;
using System.Drawing;
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
        private Vertex[,] grafo;
        private TerrainController terrainController = new TerrainController();
        private SolverController solverController = new SolverController();

        private void btnSolve_Click(object sender, EventArgs e)
        {
            int wid = int.Parse(numLargura.Text);
            int hgt = int.Parse(numAltura.Text);
            if (solverController.Solver(grafo, grafo[terrainController.Coyote[0], terrainController.Coyote[1]]))
            {

                Bitmap bitmap = new Bitmap(picTerrain.Image);
                picTerrain.Image = terrainController.DisplayPath(grafo[terrainController.Papaleguas[0], terrainController.Papaleguas[1]], int.Parse(numTamanhoBlc.Text), bitmap, Brushes.MediumSlateBlue);
                lblCost.Text = "Custo Total: " + grafo[terrainController.Papaleguas[0], terrainController.Papaleguas[1]].minPath;
            }
            else MessageBox.Show("No Path!");
       
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            int wid = int.Parse(numLargura.Text);
            int hgt = int.Parse(numAltura.Text);
            CellSize = int.Parse(numTamanhoBlc.Text);
            Xmin = (picTerrain.ClientSize.Width - wid * CellSize) / 2;
            Ymin = (picTerrain.ClientSize.Height - hgt * CellSize) / 2;
            grafo = terrainController.GenerateTerrain(wid, hgt, CellSize, Ymin, Xmin);
            picTerrain.Image = terrainController.DisplayTerrain(grafo, picTerrain.ClientSize.Width, picTerrain.ClientSize.Height, CellSize);
        }
    }
}
