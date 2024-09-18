using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tron_game
{

    public class ColaDeItems
    {
        private Nodo primero;  // Nodo al frente de la cola
        private Nodo ultimo;   // Nodo al final de la cola

        public bool EstaVacia => primero == null;

        // Encolar un ítem
        public void Encolar(Item item)
        {
            Nodo nuevoNodo = new Nodo(item.Posicion.X, item.Posicion.Y) { Item = item };  // Usamos la posición y agregamos el ítem al nodo

            if (EstaVacia)
            {
                primero = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                ultimo.NodoDerecha = nuevoNodo;  // El último nodo apunta al nuevo nodo
                ultimo = nuevoNodo;  // Ahora el último nodo es el nuevo nodo
            }
        }


        // Desencolar un ítem
        public Item Desencolar()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            Item item = primero.Item;  // Obtener el ítem del nodo
            primero = primero.NodoDerecha;  // Mover el puntero de "primero" al siguiente nodo
            return item;
        }

        // Obtener el ítem al frente de la cola sin desencolarlo
        public Item Peek()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            return primero.Item;
        }

        // Obtener todos los ítems en la cola (para el método de colisiones)
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