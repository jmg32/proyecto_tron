using System;
using System.Collections.Generic;
using System.Linq;
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
        private ColaDeItems colaDeItems;  // Usar la nueva clase ColaDeItem
        private Random random = new Random();
        private Timer itemSpawnTimer;  // Declarar el temporizador para ítems

        public MainForm()
        {
            InitializeComponent();
            IniciarJuego();
        }

        private void IniciarJuego()
        {
            // Inicializar el mapa, la moto y colisiones
            mapa = new Mapa(gridAncho, gridAlto);
            jugadorMoto = new Moto(mapa.ObtenerNodo(0, 0), velocidadInicial: 5, combustibleInicial: 500, tamañoEstelaInicial: 1);
            colisiones = new Colisiones(gridAncho, gridAlto);

            // Inicializar la nueva cola de ítems
            colaDeItems = new ColaDeItems();

            // Crear visualización del grid con botones
            CrearVisualizacionGrid();

            // Iniciar el temporizador para el movimiento automático de la moto
            direccionActual = Direccion.Derecha;
            movimientoTimer.Interval = 1000 / jugadorMoto.Velocidad;
            movimientoTimer.Start();

            // Inicializar el temporizador para generar ítems cada 5 segundos
            itemSpawnTimer = new Timer();
            itemSpawnTimer.Interval = 1000;  // 5000 ms = 5 segundos
            itemSpawnTimer.Tick += itemSpawnTimer_Tick;  // Asociar el evento Tick
            itemSpawnTimer.Start();  // Iniciar el temporizador de ítems
        }

        private void itemSpawnTimer_Tick(object sender, EventArgs e)
        {
            // Generar ítem aleatorio cada 5 segundos
            GenerarItemAleatorio();
        }

        private void GenerarItemAleatorio()
        {
            Nodo nodoAleatorio;
            do
            {
                int x = random.Next(0, gridAncho);
                int y = random.Next(0, gridAlto);
                nodoAleatorio = mapa.ObtenerNodo(x, y);
            } while (nodoAleatorio.Ocupado);  // Buscar un nodo que no esté ocupado

            // Crear un ítem aleatorio
            int tipoItem = random.Next(0, 3);  // 0 = CeldaCombustible, 1 = CrecimientoEstela, 2 = Bomba
            Item nuevoItem;
            if (tipoItem == 0)
            {
                nuevoItem = new CeldaCombustible(nodoAleatorio);
            }
            else if (tipoItem == 1)
            {
                nuevoItem = new CrecimientoEstela(nodoAleatorio);
            }
            else
            {
                nuevoItem = new Bomba(nodoAleatorio);
            }

            // Encolar el nuevo ítem
            colaDeItems.Encolar(nuevoItem);

            // Actualizar la visualización del grid
            ActualizarVisualizacionGrid();
        }



        private void VerificarColisionConItem()
        {
            // Obtener todos los ítems en la cola
            List<Item> items = colaDeItems.ObtenerTodosLosItems();

            foreach (var itemColisionado in items)
            {
                // Verificar si la cabeza de la moto está en la misma posición que algún ítem
                if (jugadorMoto.Cabeza == itemColisionado.Posicion)
                {

                    // Primero, remover el ítem del nodo
                    itemColisionado.Remover();  // Liberar el nodo del ítem
                    colaDeItems.Desencolar();  // Eliminar el ítem de la cola

                    // Ahora aplicar el efecto del ítem
                    itemColisionado.AplicarEfecto(jugadorMoto);
                    Console.WriteLine("Efecto del ítem aplicado a la moto");

                    // Actualizar la visualización del grid después de aplicar el ítem
                    ActualizarVisualizacionGrid();
                    break;  // Salir del bucle después de aplicar el ítem
                }
            }
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
        private void ActualizarVisualizacionGrid()
        {
            for (int x = 0; x < gridAncho; x++)
            {
                for (int y = 0; y < gridAlto; y++)
                {
                    Nodo nodo = mapa.ObtenerNodo(x, y);
                    Button btn = (Button)this.Controls[x * gridAlto + y];  // Obtener el botón visual asociado

                    // Si la moto está en el nodo
                    if (jugadorMoto.Cabeza == nodo)
                    {
                        btn.BackColor = System.Drawing.Color.Red;  // Color para la cabeza de la moto
                    }
                    else if (jugadorMoto.Estela.Contains(nodo))
                    {
                        btn.BackColor = System.Drawing.Color.Blue;  // Color para la estela de la moto
                    }
                    else
                    {
                        // Iterar sobre todos los ítems para verificar si hay uno en este nodo
                        bool nodoTieneItem = false;
                        foreach (var item in colaDeItems.ObtenerTodosLosItems())
                        {
                            if (item.Posicion == nodo)
                            {
                                nodoTieneItem = true;
                                // Asignar el color adecuado dependiendo del tipo de ítem
                                switch (item.Tipo)
                                {
                                    case "CeldaCombustible":
                                        btn.BackColor = System.Drawing.Color.Yellow;  // Amarillo para el combustible
                                        break;
                                    case "CrecimientoEstela":
                                        btn.BackColor = System.Drawing.Color.Cyan;  // Celeste para el crecimiento de estela
                                        break;
                                    case "Bomba":
                                        btn.BackColor = System.Drawing.Color.Black;  // Negro para las bombas
                                        break;
                                }
                                break;  // Salir del bucle una vez que se encuentra el ítem
                            }
                        }

                        // Si no hay ítems, marcar el nodo como vacío
                        if (!nodoTieneItem)
                        {
                            btn.BackColor = System.Drawing.Color.White;  // Color para los nodos vacíos
                        }
                    }
                }
            }
        }




        // Lógica del temporizador para mover la moto
        private void movimientoTimer_Tick(object sender, EventArgs e)
        {
            Nodo nuevoNodo = ObtenerNuevoNodo(jugadorMoto.Cabeza, direccionActual);  // Obtener el nodo adyacente basado en la dirección
            Console.WriteLine($"Intentando mover la moto a [{nuevoNodo?.X}, {nuevoNodo?.Y}]");

            // Verificar si la moto tiene combustible
            if (jugadorMoto.Combustible <= 0)
            {
                movimientoTimer.Stop();
                Console.WriteLine("La moto se ha quedado sin combustible.");
                MessageBox.Show("La moto se ha quedado sin combustible y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Verificar colisión con pared
            if (colisiones.VerificarColisionPared(nuevoNodo))
            {
                movimientoTimer.Stop();
                Console.WriteLine("La moto ha chocado con una pared y ha muerto.");
                MessageBox.Show("La moto ha chocado con una pared y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener el juego
            }

            // Verificar colisión con la estela (excluyendo los ítems)
            if (colisiones.VerificarColisionEstela(nuevoNodo, colaDeItems.ObtenerTodosLosItems()))
            {
                movimientoTimer.Stop();
                Console.WriteLine("La moto ha chocado con su propia estela y ha muerto.");
                MessageBox.Show("La moto ha chocado con su propia estela y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener el juego
            }

            // Verificar colisión con los ítems antes de mover la moto
            VerificarColisionConItem();  // Aquí se libera el nodo ocupado por un ítem

            // Mover la moto en la dirección actual pasando la lista de ítems
            jugadorMoto.Mover(direccionActual, colaDeItems.ObtenerTodosLosItems());

            // Actualizar la visualización del grid después del movimiento
            ActualizarVisualizacionGrid();
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
