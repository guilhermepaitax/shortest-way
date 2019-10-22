using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoCoyote.Models
{
    class Connections
    {
        private int cost;
        private Vertex connectedVertex;

        public Connections(int cost, Vertex connectedVertex)
        {
            this.cost = cost;
            this.connectedVertex = connectedVertex;
        }

        public int Cost { get => cost; set => cost = value; }
        internal Vertex ConnectedVertex { get => connectedVertex; set => connectedVertex = value; }
    }
}
