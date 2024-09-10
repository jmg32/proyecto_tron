using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TronGame
{
    public class Moto
    {
        // Atributos
        public int Velocidad { get; private set; }
        public int Combustible { get; private set; }
        public LinkedList<Posicion> Estela { get; private set; }
        public Posicion Cabeza { get; private set; }
        public int TamañoEstela { get; private set; }
        private int gridWidth;
        private int gridHeight;
        private Direccion ultimaDireccion; // Nueva variable para guardar la última dirección

        // Constructor
        public Moto(int velocidadInicial, int combustibleInicial, int tamañoEstelaInicial, int gridWidth, int gridHeight)
        {
            Velocidad = velocidadInicial;
            Combustible = combustibleInicial;
            TamañoEstela = tamañoEstelaInicial;
            Estela = new LinkedList<Posicion>();

            // Guardar el tamaño del grid
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;

            // Inicializa la cabeza y la estela
            Cabeza = new Posicion(0, 0); // Cabeza en la posición inicial (0,0)
            ultimaDireccion = Direccion.Derecha; // Inicializa la última dirección
            for (int i = 1; i <= TamañoEstela; i++)
            {
                Estela.AddLast(new Posicion(0, -i)); // Estela detrás de la cabeza
            }
        }

        public void Mover(Direccion direccion)
        {
            if (Combustible <= 0)
            {
                return; // No se mueve si no tiene combustible
            }

            // Calcula la nueva posición de la cabeza de la moto
            Posicion nuevaCabeza = CalcularNuevaPosicion(direccion);

            // Aquí removemos la lógica que impide que la cabeza salga del grid.
            // La colisión con la pared debe ser manejada en `MainForm`.

            // Mueve la estela: agrega la posición anterior de la cabeza al inicio de la estela
            Estela.AddFirst(Cabeza);

            // Elimina el último nodo de la estela si supera el tamaño permitido
            if (Estela.Count > TamañoEstela)
            {
                Estela.RemoveLast();
            }

            // Actualiza la posición de la cabeza
            Cabeza = nuevaCabeza;

            // Actualiza la última dirección
            ultimaDireccion = direccion;

            // Reducir combustible en función de la velocidad y la distancia recorrida
            ConsumirCombustible();
        }



        // Método para calcular la nueva posición en función de la dirección
        private Posicion CalcularNuevaPosicion(Direccion direccion)
        {
            Posicion nuevaPosicion = null;

            switch (direccion)
            {
                case Direccion.Arriba:
                    nuevaPosicion = new Posicion(Cabeza.X, Cabeza.Y - 1);
                    break;
                case Direccion.Abajo:
                    nuevaPosicion = new Posicion(Cabeza.X, Cabeza.Y + 1);
                    break;
                case Direccion.Izquierda:
                    nuevaPosicion = new Posicion(Cabeza.X - 1, Cabeza.Y);
                    break;
                case Direccion.Derecha:
                    nuevaPosicion = new Posicion(Cabeza.X + 1, Cabeza.Y);
                    break;
            }

            return nuevaPosicion;
        }
        public void EstablecerPosicionCabeza(int x, int y)
        {
            Cabeza = new Posicion(x, y);
        }


        // Método para verificar si la nueva dirección es opuesta a la última dirección
        private bool EsDireccionOpuesta(Direccion nuevaDireccion)
        {
            return (ultimaDireccion == Direccion.Arriba && nuevaDireccion == Direccion.Abajo) ||
                   (ultimaDireccion == Direccion.Abajo && nuevaDireccion == Direccion.Arriba) ||
                   (ultimaDireccion == Direccion.Izquierda && nuevaDireccion == Direccion.Derecha) ||
                   (ultimaDireccion == Direccion.Derecha && nuevaDireccion == Direccion.Izquierda);
        }

        // Método para consumir combustible
        private void ConsumirCombustible()
        {
            Combustible -= Velocidad;
            if (Combustible < 0)
            {
                Combustible = 0; // Evitar valores negativos
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

