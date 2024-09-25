using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tron_game;

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

        // Verificar colisión con los bordes del grid (paredes)
        public bool VerificarColisionPared(Nodo nodo)
        {
            return nodo == null;  // Si el nodo es null, significa que ha intentado salir del grid
        }

        // Verificar colisión con la estela (o cualquier nodo ocupado)
        public bool VerificarColisionEstela(Nodo nodo, List<Item> items)
        {
            if (items.Any(item => item.Posicion == nodo))
            {
                return false;  // Si el nodo está ocupado por un ítem, no es una colisión
            }
            return nodo != null && nodo.Ocupado;  // Verificar si está ocupado por una estela o algo más
        }

        // Verificar colisión con otra moto o su estela
        public bool VerificarColisionConOtraMoto(Nodo nodo, List<Moto> todasLasMotos, Moto motoActual)
        {
            foreach (var otraMoto in todasLasMotos)
            {
                if (otraMoto == motoActual)
                {
                    continue;  // No verificar colisiones con la propia moto
                }

                if (otraMoto.Cabeza == nodo || otraMoto.Estela.Any(posicion => posicion == nodo))
                {
                    return true;  // Colisión con la cabeza o la estela de otra moto
                }
            }
            return false;
        }
    }



}


