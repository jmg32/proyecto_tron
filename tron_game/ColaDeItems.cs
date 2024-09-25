using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tron_game
{

    public class ColaDeItems
    {
        private Nodo primero;
        private Nodo ultimo;

        public bool EstaVacia => primero == null;

        // Encolar un ítem
        public void Encolar(Item item)
        {
            Nodo nuevoNodo = new Nodo(item.Posicion.X, item.Posicion.Y) { Item = item };
            if (EstaVacia)
            {
                primero = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                ultimo.NodoDerecha = nuevoNodo;
                ultimo = nuevoNodo;
            }
        }

        // Desencolar un ítem
        public void Desencolar()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            primero = primero.NodoDerecha;  // Mover el puntero de "primero" al siguiente nodo
            if (primero == null)
            {
                ultimo = null;  // Si la cola está vacía, reiniciar el último nodo
            }
        }

        // Obtener todos los ítems
        public List<Item> ObtenerTodosLosItems()
        {
            List<Item> items = new List<Item>();
            Nodo actual = primero;

            while (actual != null)
            {
                items.Add(actual.Item);
                actual = actual.NodoDerecha;
            }

            return items;
        }
    }


}