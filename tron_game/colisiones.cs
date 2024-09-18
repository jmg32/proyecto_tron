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
            // Si el nodo está ocupado por un ítem, no cuenta como colisión
            if (items.Any(item => item.Posicion == nodo))
            {
                return false;
            }

            // Verificar si el nodo está ocupado por la estela o cualquier otra cosa
            return nodo != null && nodo.Ocupado;
        }



        // Verificar colisión con otra moto
        public bool VerificarColisionConOtraMoto(Nodo nodo, List<Moto> otrasMotos)
        {
            foreach (var otraMoto in otrasMotos)
            {
                if (otraMoto.Cabeza == nodo)  // Si la cabeza de otra moto está en el mismo nodo
                {
                    return true;  // Colisión con otra moto
                }
            }
            return false;
        }
    }

}


