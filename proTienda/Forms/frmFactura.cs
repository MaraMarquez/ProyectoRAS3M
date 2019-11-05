using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace proTienda.Forms
{
    public partial class frmFactura : Form
    {
        Class.ExtraerDatos pasar = new Class.ExtraerDatos();

        private string[,] Clave_Clientes = new string[10000, 5];
        private string[,] Productoss = new string[10000, 5];
        private string[,] Nombres = new string[10000, 5];
        private string[,] Lineas = new string[10000, 5];
        private int contador = 0;
        private double importe = 0;

        public frmFactura()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ARTICULO INGRESADO", "FACTURANDO......", MessageBoxButtons.OK, MessageBoxIcon.Information);

            double a2 = Convert.ToDouble(textBox1.Text);
            double v = Convert.ToDouble(precioUnitario.Text);
            textBox3.Text = Convert.ToString(a2 * v * 1.0);

            int longitud = textBox3.TextLength - 2;
            string Datos = "";

            for (int g = 0; g < textBox3.TextLength; g++)
            {
                if ((g == longitud) && (textBox3.Text[g] != '0'))
                {
                    Datos = Datos + textBox3.Text[g];
                }
                else
                {
                    Datos = Datos + textBox3.Text[g];

                }
            }

            importe = importe + Convert.ToDouble(Datos);

            textBox3.Text = Datos;

            if ((textBox1.Text != "") && (textBox3.Text != "") && (articulo.Text != "") && (precioUnitario.Text != ""))
            {
                Lineas[contador, 0] = textBox1.Text;
                Lineas[contador, 1] = articulo.Text;
                Lineas[contador, 2] = precioUnitario.Text;
                Lineas[contador, 3] = textBox3.Text;
                contador = contador + 1;
            }
            else
            {
                MessageBox.Show("Faltan algunos campos por llenar verifique", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmFactura_Load(object sender, EventArgs e)
        {
            // Para agregar las claves de los clientes
            Productoss = pasar.Extraer_Productos();
            Clave_Clientes = pasar.Extraer_Datos();
            Nombres = pasar.Extraer_Datos();

            for (int a = 1; a <= 10000; a++)
            {
                if ((Clave_Clientes[a, 0] == null) || (Clave_Clientes[a, 0] == ""))
                {
                    a = 10000;
                }
                else
                {
                    clientes.Items.Add(Clave_Clientes[a, 0]);
                }
            }

            for (int b = 1; b <= 10000; b++)
            {
                if ((Productoss[b, 0] == null) || (Productoss[b, 0] == ""))
                {
                    b = 10000;
                }
                else
                {
                    articulo.Items.Add(Productoss[b, 0]);
                }
            }

            for (int a = 1; a <= 10000; a++)
            {
                if ((Nombres[a, 1] == null) || (Nombres[a, 1] == ""))
                {
                    a = 10000;
                }
                else
                {
                    cboNombre.Items.Add(Nombres[a, 1]);
                }
            }


        }

        private void clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int a = 1; a <= 10000; a++)
            {
                if (Clave_Clientes[a, 0] == clientes.Text)
                {
                    Nombre.Text = Clave_Clientes[a, 1];
                    Direccion.Text = Clave_Clientes[a, 2];
                    RFC.Text = Clave_Clientes[a, 3];
                    Telefono.Text = Clave_Clientes[a, 4];
                    a = 10000;
                }
                else
                {

                }
            }

            clientes.Enabled = false;
            cboNombre.Enabled = false;
        }

        private void articulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int a = 1; a <= 10000; a++)
            {
                if (Productoss[a, 0] == articulo.Text)
                {
                    precioUnitario.Text = Productoss[a, 1];
                    a = 10000;
                }
                else
                {

                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            clientes.Enabled = true;


            Class.GenerarFactura gener = new Class.GenerarFactura();
            gener.Importe = importe;
            gener.Nombre = Nombre.Text;
            gener.Direccion = Direccion.Text;
            gener.Rfc = RFC.Text;
            gener.Telefono = Telefono.Text;
            gener.Lineas = Lineas;
            gener.Generar();


            Nombre.Clear();
            Direccion.Clear();
            RFC.Clear();
            Telefono.Clear();

            textBox1.Clear();
            textBox3.Clear();
            precioUnitario.Clear();
            articulo.Text = "ELIGE";

            var word = new Word.Application();
            word.Documents.Add("C:/Factura.txt");
            word.Visible = true;
            contador = 0;
            importe = 0;
            for (int a = 0; a <= 1000; a++)
            {
                Lineas[a, 0] = null;
                Lineas[a, 1] = null;
                Lineas[a, 2] = null;
                Lineas[a, 3] = null;
            }
        }

        private void cboNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int a = 1; a <= 10000; a++)
            {
                if (Clave_Clientes[a, 1] == cboNombre.Text)
                {
                    Nombre.Text = Clave_Clientes[a, 1];
                    Direccion.Text = Clave_Clientes[a, 2];
                    RFC.Text = Clave_Clientes[a, 3];
                    Telefono.Text = Clave_Clientes[a, 4];
                    a = 10000;
                }
                else
                {

                }
            }

            clientes.Enabled = false;
            cboNombre.Enabled = false;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            frmClientes _frmClientes = new frmClientes();
            _frmClientes.StartPosition = FormStartPosition.Manual;
            _frmClientes.Show();

        }
    }
}
