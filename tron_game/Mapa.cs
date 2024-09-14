using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tron_game
{
    public class Mapa
    {
        private Nodo[,] grid;
        public int Ancho { get; private set; }
        public int Alto { get; private set; }

        public Mapa(int ancho, int alto)
        {
            Ancho = ancho;
            Alto = alto;
            grid = new Nodo[ancho, alto];

            // Crear nodos y enlazarlos
            for (int x = 0; x < ancho; x++)
            {
                for (int y = 0; y < alto; y++)
                {
                    grid[x, y] = new Nodo(x, y);
                }
            }

            // Enlazar los nodos en sus respectivas direcciones
            for (int x = 0; x < ancho; x++)
            {
                for (int y = 0; y < alto; y++)
                {
                    if (x > 0) grid[x, y].NodoIzquierda = grid[x - 1, y];
                    if (x < ancho - 1) grid[x, y].NodoDerecha = grid[x + 1, y];
                    if (y > 0) grid[x, y].NodoArriba = grid[x, y - 1];
                    if (y < alto - 1) grid[x, y].NodoAbajo = grid[x, y + 1];
                }
            }
        }

        // Devuelve el nodo en una posición específica
        public Nodo ObtenerNodo(int x, int y)
        {
            if (x >= 0 && x < Ancho && y >= 0 && y < Alto)
            {
                return grid[x, y];
            }
            return null;
        }
    }

}
