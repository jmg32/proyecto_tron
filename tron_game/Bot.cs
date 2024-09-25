using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TronGame;

namespace tron_game
{
    public class Bot : Moto
    {
        private Random random;

        public Bot(Nodo posicionInicial, int velocidadInicial, int combustibleInicial, int tamañoEstelaInicial, Mapa mapa, Random randomGlobal)
            : base(posicionInicial, velocidadInicial, combustibleInicial, tamañoEstelaInicial)
        {
            random = randomGlobal;  // Usar el `Random` global
        }

        // Método para mover el bot de forma aleatoria
        public void MoverAleatoriamente(List<Item> items)
        {
            Direccion direccionAleatoria = (Direccion)random.Next(0, 4);  // Cada bot genera su propia dirección aleatoria
            Mover(direccionAleatoria, items);  // Pasar la lista de ítems al mover
        }

        // Verificar colisión del bot con los ítems y aplicar el efecto si hay colisión
        public void VerificarColisionConItems(List<Item> items, ColaDeItems colaDeItems)
        {
            foreach (var item in items)
            {
                if (Cabeza == item.Posicion)
                {
                    item.AplicarEfecto(this);  // Aplicar el efecto del ítem al bot
                    item.Remover();  // Liberar el nodo del ítem
                    colaDeItems.Desencolar();  // Remover el ítem de la cola
                    break;  // Terminar la verificación después de recoger un ítem
                }
            }
        }

        // Verificar si el bot ha muerto (sin combustible)
        public bool EstaMuerto()
        {
            return Combustible <= 0;
        }
    }
}
