using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using proTienda.Class;

namespace proTienda
{
    public partial class ConsultaVentas : Form
    {
        public ConsultaVentas()
        {
            InitializeComponent();
        }
       

        private void frmConsultaVentas_Load(object sender, EventArgs e)
        {
            this.Text = "Consulta Ventas";            
            Encabezados();
            ReadData();
        }

    private void Encabezados()
        {
            lvListaVentas.View = View.Details;
            lvListaVentas.Columns.Add("Ticket", 90,HorizontalAlignment.Left);
            lvListaVentas.Columns.Add("Fecha", 175,HorizontalAlignment.Left);
            lvListaVentas.Columns.Add("Caja", 50,HorizontalAlignment.Center);
            lvListaVentas.Columns.Add("Cajero", 175,HorizontalAlignment.Left);
            lvListaVentas.Columns.Add("Total", 150,HorizontalAlignment.Right);
        }   

       
    public void ReadData()
        {
            //Este procedimiento lee los datos 
            //que se tranferirán y los mostrará en forma de
            //lista en el ListView   
            try
            {
               string varSQL = "SELECT V.FOLIO, V.FECHA, V.ID_CAJA as CAJA,U.NOMBRE + ' ' + U.PATERNO + ' ' + U.MATERNO as CAJERO, SUM(DV.CANTIDAD * DV.P_UNITARIO) AS TOTAL FROM USERS U INNER JOIN (CAT_PRODUCTOS P INNER JOIN (VENTAS V INNER JOIN DETALLE_VENTAS DV ON V.FOLIO = DV.FOLIO) ON P.ID_PRODUCTO = DV.ID_PRODUCTO) ON U.USER_NAME = V.USER_NAME GROUP BY V.FOLIO, V.FECHA, V.ID_CAJA, U.NOMBRE,U.PATERNO, U.MATERNO";

                double varTOTAL = 0;
                //Si la conexion esta abierta la cerramos
                //en caso contrario, la abrimos
                OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                if (cnnReadData.State == ConnectionState.Open)
                    cnnReadData.Close();
                else cnnReadData.Open();
                int I = 0;
                OleDbCommand cmdReadData = new OleDbCommand(varSQL, cnnReadData);
                OleDbDataReader drReadData;
                drReadData = cmdReadData.ExecuteReader();
                lvListaVentas.Items.Clear();
                while (drReadData.Read())
                {
                    lvListaVentas.Items.Add(drReadData["FOLIO"].ToString());
                    lvListaVentas.Items[I].SubItems.Add(drReadData["FECHA"].ToString());
                    lvListaVentas.Items[I].SubItems.Add(drReadData["CAJA"].ToString());
                    lvListaVentas.Items[I].SubItems.Add(drReadData["CAJERO"].ToString());
                    lvListaVentas.Items[I].SubItems.Add(drReadData["TOTAL"].ToString());
                    varTOTAL += Convert.ToDouble(drReadData["TOTAL"]);
                    I += 1;
                }
                //Agregamos un registro más
                if (I != 0)
                {
                    lvListaVentas.Items.Add("");
                    lvListaVentas.Items[I].SubItems.Add("");
                    lvListaVentas.Items[I].SubItems.Add("");
                    lvListaVentas.Items[I].SubItems.Add("Gran Total:");
                    lvListaVentas.Items[I].SubItems.Add(varTOTAL.ToString());
                }
                drReadData.Close();
                cmdReadData.Dispose();
                cnnReadData.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvListaVentas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
