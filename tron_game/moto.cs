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
        public int Combustible { get;  set; }      // Cantidad de combustible restante
        public int TamañoEstela { get;  set; }     // Tamaño máximo de la estela

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
        public void Mover(Direccion direccion, List<Item> items)
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

            // Verificar si el nodo está ocupado por un ítem
            bool nodoOcupadoPorItem = items.Any(item => item.Posicion == nuevoNodo);

            // Permitir que la moto avance si el nodo no está ocupado por una estela o si está ocupado por un ítem
            if (nuevoNodo != null && (!nuevoNodo.Ocupado || nodoOcupadoPorItem))
            {
                Console.WriteLine($"Moto moviéndose a [{nuevoNodo.X}, {nuevoNodo.Y}]");

                // Mueve la estela
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

                // Si el nodo no está ocupado por un ítem, marcarlo como ocupado
                if (!nodoOcupadoPorItem)
                {
                    Cabeza.Ocupado = true;  // Marcar el nuevo nodo como ocupado
                }

                // Reducir el combustible cada vez que la moto se mueve
                ConsumirCombustible();
            }
            else
            {
                Console.WriteLine($"No se puede mover a [{nuevoNodo?.X}, {nuevoNodo?.Y}] porque está ocupado por una estela o no es válido.");
            }
        }

        public void ConsumirCombustible()
        {
            if (Combustible > 0)
            {
                Combustible--;  // Reducir el combustible en cada movimiento
                Console.WriteLine($"Combustible restante: {Combustible}");

                // Si el combustible llega a cero, detener la moto
                if (Combustible == 0)
                {
                    Console.WriteLine("La moto se ha quedado sin combustible.");
                }
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

