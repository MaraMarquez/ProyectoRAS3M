using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using proTienda.Class;

namespace proTienda
{
    public partial class frmBuscaProducto : Form
    {
        public frmBuscaProducto()
        {
            InitializeComponent();
        }

        //Declaraciones
        public string varID_PRODUCTO = "";

        private void frmBuscaProducto_Load(object sender, EventArgs e)
        {
            this.Text = "Busca Producto";
            //Form_Load
            this.lvProductos.DoubleClick += new System.EventHandler(this.lvProductos_DoubleClick);
            this.lvProductos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvProductos_KeyPress);
            Encabezados();
        }



        //Evento el cual al seleccionar el producto lo intoduce en la ventana de frmVentas
        void lvProductos_KeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    Producto();
                    break;

                case (char)Keys.Escape:
                    varID_PRODUCTO = "";
                    this.Close();
                    break;
            }
        }


        void lvProductos_DoubleClick(object sender, System.EventArgs e)
        {
            Producto();
        }


        private void Encabezados()
        {
            lvProductos.View = View.Details;
            lvProductos.Columns.Add("Clave producto", 0,
                HorizontalAlignment.Left);
            lvProductos.Columns.Add("Descripción", 250,
                HorizontalAlignment.Left);
            lvProductos.Columns.Add("Existencia", 90,
                HorizontalAlignment.Right);
            lvProductos.Columns.Add("Precio", 90,
                HorizontalAlignment.Right);
        }


        private void Producto()
        {
            try
            {
                if (lvProductos.Items.Count != 0)
                {
                    varID_PRODUCTO = lvProductos.SelectedItems[0].Text;
                }

                else
                {
                    varID_PRODUCTO = "";
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un elemento de la lista. \nDescripción del error: \n" + ex.Message, "Operación no válida", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }


        private void ReadData(string prmDESC_PRODUCTO)
        {
            //Este procedimiento lee los datos que se 
            //tranferirán y los mostrará en forma de
            //lista en el ListView
            try
            {
                //Si la conexion esta abierta la cerramos; 
                //en caso contrario, la abrimos
                OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);

                if (cnnReadData.State == ConnectionState.Open)
                    cnnReadData.Close();
                else cnnReadData.Open();

                int I = 0;

                OleDbCommand cmdReadData = new OleDbCommand("SELECT ID_PRODUCTO," +
                    " DESC_PRODUCTO," +
                    " CANTIDAD,P_U_VENTA " +
                    " FROM CAT_PRODUCTOS" +
                    " WHERE DESC_PRODUCTO like '%" +prmDESC_PRODUCTO+"%'", cnnReadData);

                OleDbDataReader drReadData;
                drReadData = cmdReadData.ExecuteReader();
                lvProductos.Items.Clear();

                while (drReadData.Read())
                {
                    lvProductos.Items.Add(drReadData["ID_PRODUCTO"].ToString());
                    lvProductos.Items[I].SubItems.Add(drReadData["DESC_PRODUCTO"].ToString());
                    lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());
                    lvProductos.Items[I].SubItems.Add(drReadData["P_U_VENTA"].ToString());
                    I += 1;



                }
                drReadData.Close();
                cmdReadData.Dispose();
                cnnReadData.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ReadData(txtDESC_PRODUCTO.Text);
        }


        private void lvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
