using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace proTienda.Forms
{
    public partial class frmClientes : Form
    {
        //Globales
        StreamReader Leer;
        StreamWriter Escribir;
        private int a;        

        public frmClientes()
        {
            InitializeComponent();

        }


        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            Escribir = File.AppendText("Informacion/Clientes.txt");
            Escribir.WriteLine(txtFolio.Text.Trim());
            Escribir.WriteLine(txtNombre.Text);
            Escribir.WriteLine(txtDireccion.Text);
            Escribir.WriteLine(txtRFC.Text);
            Escribir.WriteLine(txtTelefono.Text);
            Escribir.Close();
            MessageBox.Show("Se Agrego el Cliente con Exito");
        }
    }
}
