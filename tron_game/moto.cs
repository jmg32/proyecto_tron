using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tron_game;

namespace TronGame
{
    using System.Collections.Generic;

    public class Moto
    {
        public Nodo Cabeza { get; private set; }          // Nodo actual donde está la cabeza de la moto
        public LinkedList<Nodo> Estela { get; private set; } // Lista enlazada para representar la estela
        public int Velocidad { get; private set; }        // Velocidad de la moto
        public int Combustible { get; private set; }      // Cantidad de combustible restante
        public int TamañoEstela { get; private set; }     // Tamaño máximo de la estela

        // Constructor
        public Moto(Nodo nodoInicial, int velocidadInicial, int combustibleInicial, int tamañoEstelaInicial)
        {
            Cabeza = nodoInicial;  // Posición inicial de la moto
            Velocidad = velocidadInicial;
            Combustible = combustibleInicial;
            TamañoEstela = tamañoEstelaInicial;
            Estela = new LinkedList<Nodo>();

            // Marcar el nodo inicial como ocupado
            Cabeza.Ocupado = true;
        }

        // Método para mover la moto en una dirección específica
        public void Mover(Direccion direccion)
        {
            Nodo nuevoNodo = null;

            // Determinar el nuevo nodo basándose en la dirección
            switch (direccion)
            {
                case Direccion.Arriba:
                    nuevoNodo = Cabeza.NodoArriba;
                    break;
                case Direccion.Abajo:
                    nuevoNodo = Cabeza.NodoAbajo;
                    break;
                case Direccion.Izquierda:
                    nuevoNodo = Cabeza.NodoIzquierda;
                    break;
                case Direccion.Derecha:
                    nuevoNodo = Cabeza.NodoDerecha;
                    break;
            }

            // Solo se mueve si el nuevo nodo no está ocupado y es válido (no es null)
            if (nuevoNodo != null && !nuevoNodo.Ocupado)
            {
                // Mover la estela
                Estela.AddFirst(Cabeza);  // Agregar el nodo actual al inicio de la estela

                // Limitar la longitud de la estela
                if (Estela.Count > TamañoEstela)
                {
                    Nodo ultimo = Estela.Last.Value;
                    Estela.RemoveLast();  // Quitar el último nodo de la estela
                    ultimo.Ocupado = false;  // Marcar el último nodo como libre
                }

                // Actualizar la posición de la cabeza de la moto
                Cabeza = nuevoNodo;
                Cabeza.Ocupado = true;  // Marcar el nuevo nodo como ocupado
            }

            // Reducir combustible
            ConsumirCombustible();
        }

        // Método para consumir combustible
        private void ConsumirCombustible()
        {
            Combustible -= Velocidad;
            if (Combustible < 0)
            {
                Combustible = 0; // Asegurarse de que el combustible no sea negativo
            }
        }
    }





    // Clase para representar posiciones en el grid
    public class Posicion
    {
        public int X { get; }
        public int Y { get; }

        public Posicion(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Enumeración para direcciones posibles
    public enum Direccion
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha
    }
}

