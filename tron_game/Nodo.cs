using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tron_game
{
    public class Nodo
    {
        public Nodo NodoArriba { get; set; }
        public Nodo NodoAbajo { get; set; }
        public Nodo NodoIzquierda { get; set; }
        public Nodo NodoDerecha { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Ocupado { get; set; }  // Indica si una moto o estela está ocupando el nodo

        public Nodo(int x, int y)
        {
            X = x;
            Y = y;
            Ocupado = false;
        }
    }

}
