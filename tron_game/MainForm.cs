using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TronGame
{
    public partial class MainForm : Form
    {
        private Moto moto;
        private Direccion direccionActual;
        private Colisiones colisiones;

        public MainForm()
        {
            InitializeComponent();
            IniciarJuego();
        }

        private void IniciarJuego()
        {
            // Inicializar la moto con el tamaño del grid
            int gridWidth = gridButtons.GetLength(0);
            int gridHeight = gridButtons.GetLength(1);
            moto = new Moto(velocidadInicial: 5, combustibleInicial: 200, tamañoEstelaInicial: 7, gridWidth, gridHeight);
            colisiones = new Colisiones(gridWidth, gridHeight);
            direccionActual = Direccion.Derecha;

            // Ajustar el intervalo del temporizador según la velocidad de la moto
            movimientoTimer.Interval = 1000 / moto.Velocidad;
            movimientoTimer.Start();

            // Pintar la moto en el grid inicialmente
            ActualizarGrid();
        }


        private void movimientoTimer_Tick(object sender, EventArgs e)
        {
            // Verificar colisión con pared
            if (colisiones.VerificarColisionPared(moto.Cabeza))
            {
                movimientoTimer.Stop();
                MessageBox.Show("La moto ha chocado con una pared y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener la ejecución del método para evitar más movimientos
            }

            // Verificar colisión con la propia estela
            if (colisiones.VerificarColisionEstela(moto.Cabeza, moto.Estela))
            {
                movimientoTimer.Stop();
                MessageBox.Show("La moto ha chocado con su propia estela y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener la ejecución del método para evitar más movimientos
            }

            // Mover la moto en la dirección actual
            moto.Mover(direccionActual);

            // Actualizar el grid para reflejar el movimiento
            ActualizarGrid();

            // Verificar si la moto se ha quedado sin combustible
            if (moto.Combustible <= 0)
            {
                movimientoTimer.Stop();
                MessageBox.Show("La moto se ha quedado sin combustible y ha muerto.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Detener la ejecución del método para evitar más movimientos
            }
        }
        a



        private void ActualizarGrid()
        {
            // Limpiar el grid
            for (int i = 0; i < gridButtons.GetLength(0); i++)
            {
                for (int j = 0; j < gridButtons.GetLength(1); j++)
                {
                    gridButtons[i, j].BackColor = System.Drawing.Color.White;
                }
            }

            // Dibujar la estela de la moto en el grid
            foreach (var posicion in moto.Estela)
            {
                if (posicion.X >= 0 && posicion.X < gridButtons.GetLength(0) &&
                    posicion.Y >= 0 && posicion.Y < gridButtons.GetLength(1))
                {
                    gridButtons[posicion.X, posicion.Y].BackColor = System.Drawing.Color.Blue; // Color para la estela
                }
            }

            // Dibujar la cabeza de la moto en el grid
            if (moto.Cabeza.X >= 0 && moto.Cabeza.X < gridButtons.GetLength(0) &&
                moto.Cabeza.Y >= 0 && moto.Cabeza.Y < gridButtons.GetLength(1))
            {
                gridButtons[moto.Cabeza.X, moto.Cabeza.Y].BackColor = System.Drawing.Color.Red; // Color para la cabeza
            }
        }




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