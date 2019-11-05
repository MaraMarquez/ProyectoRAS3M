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
    public partial class ConsultaReporte : Form
    {
        public ConsultaReporte()
        {
            InitializeComponent();
            CargaRegistro();

        }

        public void CargaRegistro()
        {
            OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);

            if (cnnReadData.State == ConnectionState.Open)
                cnnReadData.Close();
            else cnnReadData.Open();

             OleDbCommand cmdReadData = new OleDbCommand("SELECT VENTAS.FOLIO, CAT_PRODUCTOS.DESC_PRODUCTO, DETALLE_VENTAS.CANTIDAD, DETALLE_VENTAS.P_UNITARIO, VENTAS.FECHA, VENTAS.USER_NAME "+ 
                         "FROM ((CAT_PRODUCTOS INNER JOIN DETALLE_VENTAS ON CAT_PRODUCTOS.ID_PRODUCTO = DETALLE_VENTAS.ID_PRODUCTO) INNER JOIN VENTAS ON DETALLE_VENTAS.FOLIO = VENTAS.FOLIO)", cnnReadData);

            try
            {
                OleDbDataAdapter sda = new OleDbDataAdapter();
                sda.SelectCommand = cmdReadData;
                DataTable dbdatasert = new DataTable();
                sda.Fill(dbdatasert);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdatasert;
                DataInstrumentos.DataSource = bSource;
                sda.Update(dbdatasert);
                cnnReadData.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BuscarRegistroCliente(string theDate)
        {
            OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);

            if (cnnReadData.State == ConnectionState.Open)
                cnnReadData.Close();
            else cnnReadData.Open();

           
            
            OleDbCommand cmdDataBase = new OleDbCommand("SELECT VENTAS.FOLIO, CAT_PRODUCTOS.DESC_PRODUCTO, DETALLE_VENTAS.CANTIDAD, DETALLE_VENTAS.P_UNITARIO, VENTAS.FECHA, VENTAS.USER_NAME "+ 
                         "FROM ((CAT_PRODUCTOS INNER JOIN DETALLE_VENTAS ON CAT_PRODUCTOS.ID_PRODUCTO = DETALLE_VENTAS.ID_PRODUCTO) INNER JOIN VENTAS ON DETALLE_VENTAS.FOLIO = VENTAS.FOLIO)" +
            "WHERE VENTAS.FECHA like '%" + theDate + "%'", cnnReadData);


            try
            {
                OleDbDataAdapter sda = new OleDbDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdatasert = new DataTable();
                sda.Fill(dbdatasert);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdatasert;
                DataInstrumentos.DataSource = bSource;
                sda.Update(dbdatasert);
                cnnReadData.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string theDate = dateTimePicker1.Value.ToShortDateString();

            BuscarRegistroCliente(theDate);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            CargaRegistro();
        }



    }
}
