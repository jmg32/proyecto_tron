using System.Windows.Forms;


namespace TronGame
{
    partial class MainForm
    {
        private System.Windows.Forms.Timer movimientoTimer;
        private System.ComponentModel.IContainer components = null;
        private Button[,] gridButtons;

        /// <summary>
        /// Limpiar los recursos que se están utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Método requerido para la compatibilidad con el Diseñador de Windows Forms.
        /// No se debe modificar el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.movimientoTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // movimientoTimer
            // 
            this.movimientoTimer.Interval = 1000;
            this.movimientoTimer.Tick += new System.EventHandler(this.movimientoTimer_Tick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Name = "MainForm";
            this.Text = "Tron Game";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void InitializeGrid()
        {
            int gridSize = 20; // Tamaño del grid
            int buttonSize = 30; // Tamaño de cada celda (botón)
            gridButtons = new Button[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    gridButtons[i, j] = new Button();
                    gridButtons[i, j].Size = new System.Drawing.Size(buttonSize, buttonSize);
                    gridButtons[i, j].Location = new System.Drawing.Point(i * buttonSize, j * buttonSize);
                    gridButtons[i, j].Enabled = false;
                    gridButtons[i, j].BackColor = System.Drawing.Color.White;

                    this.Controls.Add(gridButtons[i, j]);
                }
            }
        }
    }
}
