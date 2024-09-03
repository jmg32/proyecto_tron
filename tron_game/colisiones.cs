using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TronGame
{
    public class Colisiones
    {
        private int gridWidth;
        private int gridHeight;

        public Colisiones(int gridWidth, int gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
        }

        // Verificar colisión con los límites del grid
        public bool VerificarColisionPared(Posicion cabeza)
        {
            return cabeza.X < 0 || cabeza.X >= gridWidth || cabeza.Y < 0 || cabeza.Y >= gridHeight;
        }

        // Verificar colisión con una estela
        public bool VerificarColisionEstela(Posicion cabeza, LinkedList<Posicion> estela)
        {
            foreach (var posicion in estela)
            {
                if (posicion.X == cabeza.X && posicion.Y == cabeza.Y)
                {
                    return true;
                }
            }
            return false;
        }

        // Aquí podrías agregar más métodos para verificar colisiones con otras motos o ítems
    }
}

