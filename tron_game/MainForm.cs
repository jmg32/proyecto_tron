using System;
using System.Collections.Generic;
using System.Windows.Forms;
using tron_game;

namespace TronGame
{
    public partial class MainForm : Form
    {
        private Mapa mapa;
        private Moto jugadorMoto;
        private Direccion direccionActual;
        private int gridAncho = 20;
        private int gridAlto = 20;
        private Nodo[,] gridBotones;
        private const int tamanioCelda = 30;
        private Colisiones colisiones;  // Declarar la variable colisiones

        public MainForm()
        {
            InitializeComponent();
            IniciarJuego();
        }

        private void IniciarJuego()
        {
            // Crear el mapa
            mapa = new Mapa(gridAncho, gridAlto);

            // Inicializar la moto del jugador en un nodo específico del mapa
            jugadorMoto = new Moto(mapa.ObtenerNodo(0, 0), velocidadInicial: 5, combustibleInicial: 200, tamañoEstelaInicial: 7);

            // Inicializar colisiones con el tamaño del grid
            colisiones = new Colisiones(gridAncho, gridAlto);  // Inicializa colisiones

            // Crear visualización del grid con botones
            CrearVisualizacionGrid();

            // Definir la dirección inicial de la moto
            direccionActual = Direccion.Derecha;

            // Iniciar el temporizador para el movimiento automático de la moto
            movimientoTimer.Interval = 1000 / jugadorMoto.Velocidad;
            movimientoTimer.Start();
        }


        // Crear el grid de botones para representar los nodos visualmente
        private void CrearVisualizacionGrid()
        {
            gridBotones = new Nodo[gridAncho, gridAlto];
            this.Controls.Clear();  // Limpiar los controles previos si los hay

            for (int x = 0; x < gridAncho; x++)
            {
                for (int y = 0; y < gridAlto; y++)
                {
                    Button btn = new Button();
                    btn.Width = btn.Height = tamanioCelda;
                    btn.Left = x * tamanioCelda;
                    btn.Top = y * tamanioCelda;
                    this.Controls.Add(btn);
                    gridBotones[x, y] = mapa.ObtenerNodo(x, y);  // Asociar cada nodo a su botón
                }
            }
        }

        // Método para actualizar la visualización del grid
        private void ActualizarVisualizacionGrid()
        {
            for (int x = 0; x < gridAncho; x++)
            {
                for (int y = 0; y < gridAlto; y++)
                {
                    Nodo nodo = mapa.ObtenerNodo(x, y);
                    Button btn = (Button)this.Controls[x * gridAlto + y];  // Obtener el botón visual asociado

                    if (nodo == jugadorMoto.Cabeza)
                    {
                        btn.BackColor = System.Drawing.Color.Red;  // Cabeza de la moto
                    }
                    else if (jugadorMoto.Estela.Contains(nodo))
                    {
                        btn.BackColor = System.Drawing.Color.Blue;  // Estela de la moto
                    }
                    else
                    {
                        btn.BackColor = System.Drawing.Color.White;  // Nodo vacío
                    }
                }
            }
        }

        // Lógica del temporizador para mover la moto
        private void movimientoTimer_Tick(object sender, EventArgs e)
        {
            Nodo nuevoNodo = ObtenerNuevoNodo(jugadorMoto.Cabeza, direccionActual);  // Obtener el nodo adyacente basado en la dirección

            // Verificar colisión con pared
            if (colisiones.VerificarColisionPared(nuevoNodo))
            {
                movimientoTimer.Stop();
                MessageBox.Show("La moto ha chocado con una pared y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener el juego
            }

            // Verificar colisión con la estela
            if (colisiones.VerificarColisionEstela(nuevoNodo))
            {
                movimientoTimer.Stop();
                MessageBox.Show("La moto ha chocado con su propia estela o una estela enemiga y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener el juego
            }

            // Mover la moto en la dirección actual
            jugadorMoto.Mover(direccionActual);

            // Actualizar la visualización del grid
            ActualizarVisualizacionGrid();

            // Verificar si la moto se ha quedado sin combustible
            if (jugadorMoto.Combustible <= 0)
            {
                movimientoTimer.Stop();
                MessageBox.Show("La moto se ha quedado sin combustible y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener el juego
            }
        }

        // Método para obtener el nodo adyacente basado en la dirección actual
        private Nodo ObtenerNuevoNodo(Nodo nodoActual, Direccion direccion)
        {
            switch (direccion)
            {
                case Direccion.Arriba:
                    return nodoActual.NodoArriba;
                case Direccion.Abajo:
                    return nodoActual.NodoAbajo;
                case Direccion.Izquierda:
                    return nodoActual.NodoIzquierda;
                case Direccion.Derecha:
                    return nodoActual.NodoDerecha;
                default:
                    return null;
            }
        }


        // Control de las teclas para mover la moto
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    if (direccionActual != Direccion.Abajo) direccionActual = Direccion.Arriba;
                    break;
                case Keys.Down:
                    if (direccionActual != Direccion.Arriba) direccionActual = Direccion.Abajo;
                    break;
                case Keys.Left:
                    if (direccionActual != Direccion.Derecha) direccionActual = Direccion.Izquierda;
                    break;
                case Keys.Right:
                    if (direccionActual != Direccion.Izquierda) direccionActual = Direccion.Derecha;
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
