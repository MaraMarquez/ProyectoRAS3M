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
using System.Media;
using proTienda.Class;

namespace proTienda
{
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }
        

        public frmVentas(string prmUSER_LOGIN, int prmID_CAJA)
        {
            InitializeComponent();
            varID_CAJA = prmID_CAJA;
            varUSER_LOGIN = prmUSER_LOGIN;

        }

        //Declaraciones
        string varUSER_LOGIN = Login._USER_NAME;
        int varID_CAJA = 1; 


        private void frmVentas_Load(object sender, EventArgs e)
        {

            this.Text = "Ventas";

            Encabezados();

            ReadData(varUSER_LOGIN, varID_CAJA);
            this.txtCANTIDAD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCANTIDAD_KeyPress);
            
            this.txtCANTIDAD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCANTIDAD_KeyDown);
            
                        this.txtID_PRODUCTO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_PRODUCTO_KeyPress);

            this.txtID_PRODUCTO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_PRODUCTO_KeyDown);
        }

        void Encabezados()
        {
            //Encabezados del litView
            lvVenta.View = View.Details;
            lvVenta.Columns.Add("Producto", 100,HorizontalAlignment.Left);
            lvVenta.Columns.Add("Descripcion", 250,HorizontalAlignment.Left);
            lvVenta.Columns.Add("Cant", 75,HorizontalAlignment.Right);
            lvVenta.Columns.Add("Prec", 75,HorizontalAlignment.Right);
            //lvVenta.Columns.Add("Iva", 75,HorizontalAlignment.Right);
            lvVenta.Columns.Add("Total", 100,HorizontalAlignment.Right);
        }

        void txtCANTIDAD_KeyPress(object sender, KeyPressEventArgs e)
        {


            try
            {
                lblMensaje.Text = "";

                //if (!((txtID_PRODUCTO.Text == "") || (txtCANTIDAD.Text == "")))

                



                if (((txtID_PRODUCTO.Text != "") || (txtCANTIDAD.Text == "")))
                {
                    if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;

                    }


                    if (e.KeyChar == 13)
                    {//if (e.KeyChar != 13)

                        //Insertar código aqui
                        SaveTemp_Ventas(txtID_PRODUCTO.Text, Convert.ToDouble(txtCANTIDAD.Text));
                        txtID_PRODUCTO.Focus();

                    }
                }
                else
                {
                    lblMensaje.Text = "Debe introducir una clave de producto y/o una cantidad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void txtCANTIDAD_KeyDown(object sender, KeyEventArgs e)
        {
            
        
            if (e.KeyCode == Keys.F5)
            {
                
                RealizaVenta();
                
               
            }
           
        }

       
        void txtID_PRODUCTO_KeyDown(object sender, KeyEventArgs e)
        {
           
            
            if (e.KeyCode == Keys.F5)
            {
                RealizaVenta();
                
            }
            
        }

        void txtID_PRODUCTO_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            lblMensaje.Text = "";
            try
            {
                if (!((txtID_PRODUCTO.Text == "") || (txtCANTIDAD.Text == "")))
                //if (((txtID_PRODUCTO.Text != "") || (txtCANTIDAD.Text != "")))
                {
                    if (e.KeyChar == 13)
                   //if (e.KeyChar != 13)
                    {
                        //Inserttar código aqui
                        
                        SaveTemp_Ventas(txtID_PRODUCTO.Text,Convert.ToDouble(txtCANTIDAD.Text));
                        txtID_PRODUCTO.Focus();
                        ;
                       
                    }
                }
                else
                {
                    lblMensaje.Text = "Debe introducir una clave de producto y/o una cantidad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SaveTemp_Ventas(string prmID_PRODUCTO, double prmCANTIDAD)
        {
            double[] varProductDetails = new double[1];
            varProductDetails = FindProductDetails(prmID_PRODUCTO);
            double varPRECIO = varProductDetails[0];
            try
            {
                if (varPRECIO == 0.0)
                {
                    lblMensaje.Text = "El producto no existe!!!";
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                    Temp_Ventas(varUSER_LOGIN, varID_CAJA,prmID_PRODUCTO, prmCANTIDAD, varPRECIO);
                    ReadData(varUSER_LOGIN, varID_CAJA);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        void ReadData(string prmUSER_NAME, int prmID_CAJA)
        {
            lblMensaje.Text = "";
            double varIVA = 0.0;
            double varGRAND_TOTAL = 0.0;
            try
            {
                OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);

                // string varSQL = "SELECT CAT_PRODUCTOS.ID_PRODUCTO, CAT_PRODUCTOS.DESC_PRODUCTO, TEMP_VENTAS.CANTIDAD, TEMP_VENTAS.P_UNITARIO, TEMP_VENTAS.IVA, (TEMP_VENTAS.CANTIDAD * TEMP_VENTAS.P_UNITARIO) AS TOTAL FROM CAT_PRODUCTOS INNER JOIN TEMP_VENTAS ON CAT_PRODUCTOS.ID_PRODUCTO = TEMP_VENTAS.ID_PRODUCTO WHERE TEMP_VENTAS.USER_NAME ='"+prmUSER_NAME+"'";
                //original
                string varSQL = "SELECT CAT_PRODUCTOS.ID_PRODUCTO, CAT_PRODUCTOS.DESC_PRODUCTO, TEMP_VENTAS.CANTIDAD, TEMP_VENTAS.P_UNITARIO, TEMP_VENTAS.IVA,  (TEMP_VENTAS.CANTIDAD * TEMP_VENTAS.P_UNITARIO) AS TOTAL FROM CAT_PRODUCTOS INNER JOIN TEMP_VENTAS ON CAT_PRODUCTOS.ID_PRODUCTO = TEMP_VENTAS.ID_PRODUCTO WHERE TEMP_VENTAS.USER_NAME ='" + prmUSER_NAME + "'";


                int I = 0;
                OleDbCommand cmdReadData = new OleDbCommand(varSQL, cnnReadData);
                OleDbDataReader drReadData;

                if (cnnReadData.State == ConnectionState.Open)
                    cnnReadData.Close();
                    cnnReadData.Open();
                    drReadData = cmdReadData.ExecuteReader();
                    lvVenta.Items.Clear();

                while (drReadData.Read())
                {
                    lvVenta.Items.Add(drReadData["ID_PRODUCTO"].ToString());                    
                    lvVenta.Items[I].SubItems.Add(drReadData["DESC_PRODUCTO"].ToString());
                    lvVenta.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());
                    lvVenta.Items[I].SubItems.Add(drReadData["P_UNITARIO"].ToString());
                    //lvVenta.Items[I].SubItems.Add(drReadData["IVA"].ToString() + " %");
                    lvVenta.Items[I].SubItems.Add(drReadData["TOTAL"].ToString());

                    //Obtenemos el Grand Total y el Iva
                    varGRAND_TOTAL += Convert.ToDouble(drReadData[5]);
                    varIVA += Convert.ToDouble(drReadData["IVA"]) *((Convert.ToDouble(drReadData["TOTAL"])) / 100);
                    I += 1;
                }
                drReadData.Close();
                cmdReadData.Dispose();
                cnnReadData.Close();
                cnnReadData.Dispose();
                txtGRAND_TOTAL.Text =varGRAND_TOTAL.ToString();
                txtIVA.Text = varIVA.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Temp_Ventas(string prmUSER_LOGIN, int prmID_CAJA, string prmID_PRODUCTO, double prmCANTIDAD,double prmPRECIO)
        {
            //Para cargar la venta tenporal
            string varSQL = "";
            
            try
            {
               OleDbConnection cnnTempVentas = new OleDbConnection(Conexion.CnnStr);
                if (GetSale(prmUSER_LOGIN, prmID_CAJA, prmID_PRODUCTO) == 0)
                {
                    varSQL = "INSERT  INTO TEMP_VENTAS(USER_NAME,ID_PRODUCTO,CANTIDAD,P_UNITARIO,IVA)VALUES('" + prmUSER_LOGIN + "','" + prmID_PRODUCTO + "'," + prmCANTIDAD + "," + prmPRECIO + ",0.16)";
                   
                               }

                else
                {
                    varSQL = "UPDATE TEMP_VENTAS SET CANTIDAD = "+ prmCANTIDAD + " WHERE USER_NAME = '" + prmUSER_LOGIN + "' AND ID_PRODUCTO = '" + prmID_PRODUCTO + "'";
                }

                OleDbCommand cmdTempVentas = new OleDbCommand(varSQL, cnnTempVentas);

                if (cnnTempVentas.State == ConnectionState.Open)
                    cnnTempVentas.Close();
                    cnnTempVentas.Open();
                    cmdTempVentas.ExecuteNonQuery();
                    cnnTempVentas.Close();
                    cmdTempVentas.Dispose();
                    cnnTempVentas.Dispose();
                    txtID_PRODUCTO.Text = "";
                    txtCANTIDAD.Text = "";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TempVentas");
            }
        }


        double[] FindProductDetails(string prmID_PRODUCTO)
        {
            double[] Retorno = new double[2];
            try
            {
                OleDbConnection cnnFindProductDetails =
                    new OleDbConnection(Conexion.CnnStr);
                string varSQL = "SELECT count(*) " +
                    "FROM CAT_PRODUCTOS " +
                    "WHERE ID_PRODUCTO = '" +
                    prmID_PRODUCTO + "'";
                OleDbCommand cmdFindProductDetails =
                    new OleDbCommand();
                cmdFindProductDetails.Connection =
                    cnnFindProductDetails;
                cmdFindProductDetails.CommandText = varSQL;
                if (cnnFindProductDetails.State == ConnectionState.Open)
                    cnnFindProductDetails.Close();
                cnnFindProductDetails.Open();

                if (!(Convert.ToInt32(cmdFindProductDetails.ExecuteScalar()) == 0))
                {
                    varSQL = "SELECT P_U_VENTA " +
                        " FROM CAT_PRODUCTOS " +
                        "WHERE ID_PRODUCTO ='" +
                        prmID_PRODUCTO + "'";
                    cmdFindProductDetails.CommandText = varSQL;
                    Retorno[0] = Convert.
                        ToDouble(cmdFindProductDetails.ExecuteScalar());
                    varSQL = "SELECT IVA " +
                        "FROM CAT_PRODUCTOS " +
                        "WHERE ID_PRODUCTO ='" +
                        prmID_PRODUCTO + "'";
                    cmdFindProductDetails.CommandText = varSQL;
                    Retorno[1] = Convert.
                        ToDouble(cmdFindProductDetails.ExecuteScalar());
                }
                else
                {
                    Retorno[0] = 0.0;
                    Retorno[1] = 0.0;
                }
                cmdFindProductDetails.Dispose();
                cnnFindProductDetails.Close();
                cnnFindProductDetails.Dispose();
                return (Retorno);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "FindProductDetails");
                return (Retorno);
            }
        }

        double GetSale(string prmUSER_LOGIN, int prmID_CAJA,string prmID_PRODUCTO)
        {
            //Para cargar la venta tenporal
            double Retorno;
            
            try
            {
                OleDbConnection cnnGetSale = new OleDbConnection(Conexion.CnnStr);

                string varSQL = "SELECT COUNT(*) FROM TEMP_VENTAS  WHERE USER_NAME = '"+ prmUSER_LOGIN +"' AND ID_PRODUCTO = '" + prmID_PRODUCTO + "'";

                OleDbCommand cmdGetSale = new OleDbCommand(varSQL, cnnGetSale);

                if (cnnGetSale.State == ConnectionState.Open)
                    cnnGetSale.Close();
                    cnnGetSale.Open();

                    Retorno = Convert.ToDouble(cmdGetSale.ExecuteScalar());
                    cmdGetSale.Dispose();
                    cnnGetSale.Close();
                    cnnGetSale.Dispose();
                    return (Retorno);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetSale");
                return (0);
            }
        }

        private void RealizaVenta()
        {
            SystemSounds.Asterisk.Play();
            if (lvVenta.Items.Count != 0)
            //if (lvVenta.Items.Count == 0)
            {
                int varFolio = RealizaVenta(varUSER_LOGIN, varID_CAJA);
                if (varFolio != 0)
                {
                    Cobrar _frmCobrar = new Cobrar(varFolio);
                    _frmCobrar.StartPosition = FormStartPosition.CenterScreen;
                    _frmCobrar.ShowDialog();
                }
            }
        }

        private int RealizaVenta(string prmUSER_LOGIN, int prmID_CAJA)
        {
            int varFolio = 0;
            try
            {
                OleDbConnection cnnInsert = new OleDbConnection(Conexion.CnnStr);
                cnnInsert.Open();
                OleDbCommand cmdInsert = new OleDbCommand();
                cmdInsert.Connection = cnnInsert;

                //insertamos el registro padre
                cmdInsert.CommandText ="INSERT INTO VENTAS (USER_NAME,ID_CAJA,FECHA) " +
                                        "VALUES('" + prmUSER_LOGIN +
                                        "'," + prmID_CAJA + ",NOW())";

                // MADAS 2
               

                cmdInsert.ExecuteNonQuery();

                //obtenemos el autonumerico
                cmdInsert.CommandText = "SELECT @@IDENTITY";
                varFolio = Convert.ToInt32(cmdInsert.ExecuteScalar());

                ////GENERAMOS LA VENTA
                cmdInsert.CommandText = "INSERT INTO DETALLE_VENTAS (ID_PRODUCTO,FOLIO,CANTIDAD,P_UNITARIO,IVA)SELECT ID_PRODUCTO," + varFolio + ",CANTIDAD,P_UNITARIO,IVA FROM TEMP_VENTAS WHERE USER_NAME ='" + prmUSER_LOGIN + "'";
                cmdInsert.ExecuteNonQuery();

                //ACTUALIZAMOS LAS EXISTENCIAS ORIGUBAL
                //cmdInsert.CommandText = "UPDATE CAT_PRODUCTOS " +
                //    " INNER JOIN TEMP_VENTAS " +
                //    " ON CAT_PRODUCTOS.ID_PRODUCTO = " +
                //    " TEMP_VENTAS.ID_PRODUCTO " +
                //    " SET CAT_PRODUCTOS.CANTIDAD = " +
                //    " CAT_PRODUCTOS.CANTIDAD-[TEMP_VENTAS].[CANTIDAD]" +
                //    " WHERE (([TEMP_VENTAS].[USER_NAME]= " +
                //    " '" + prmUSER_LOGIN + "'));";
                //cmdInsert.ExecuteNonQuery();

                //ACTUALIZAMOS LAS EXISTENCIAS
                cmdInsert.CommandText = "UPDATE CAT_PRODUCTOS " +
                    " INNER JOIN TEMP_VENTAS " +
                    " ON CAT_PRODUCTOS.ID_PRODUCTO = " +
                    " TEMP_VENTAS.ID_PRODUCTO " +
                    " SET CAT_PRODUCTOS.CANTIDAD = " +
                    " CAT_PRODUCTOS.CANTIDAD-[TEMP_VENTAS].[CANTIDAD]" +
                    " WHERE (([TEMP_VENTAS].[USER_NAME]= " +
                    " '" + prmUSER_LOGIN + "'));";
                cmdInsert.ExecuteNonQuery();

                //borramos las ventas temporales
                cmdInsert.CommandText = "DELETE FROM TEMP_VENTAS " +
                " WHERE USER_NAME = '" + prmUSER_LOGIN + "'";
                cmdInsert.ExecuteNonQuery();

                //MAMADAS DE EDUAR
                


                //LIBERAMOS LOS RECUSROS
                cnnInsert.Close();
                cnnInsert.Dispose();
                cmdInsert.Dispose();

                //mostramos la info                
                ReadData(varUSER_LOGIN, varID_CAJA);
                return (varFolio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RealizaVenta");
                return (0);
            }
        }

        private void DeshacerVenta(string prmUSER_LOGIN, int prmID_CAJA)
        {
            OleDbConnection conDeshacerVenta;
            OleDbCommand cmdDeshacerVenta;
            string strSQL_Delete = "DELETE FROM TEMP_VENTAS " +
                " WHERE USER_NAME = '" + prmUSER_LOGIN + "' ";
            try
            {
                conDeshacerVenta =
                    new OleDbConnection(Conexion.CnnStr);
                conDeshacerVenta.Open();
                cmdDeshacerVenta = new OleDbCommand(strSQL_Delete, conDeshacerVenta);
                cmdDeshacerVenta.ExecuteNonQuery();
                cmdDeshacerVenta.Dispose();
                conDeshacerVenta.Close();
                ReadData(varUSER_LOGIN, varID_CAJA);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeshacerVenta");
            }

        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                frmBuscaProducto myForm = new frmBuscaProducto();
                myForm.StartPosition = FormStartPosition.CenterScreen;
                myForm.ShowDialog();
                if (!(myForm.varID_PRODUCTO == ""))
                {
                    txtID_PRODUCTO.Text = myForm.varID_PRODUCTO;
                    txtID_PRODUCTO.Focus();
                }
                myForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btnBuscaProducto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID_PRODUCTO.Text = "";
            }
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
         
            RealizaVenta();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvVenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvVenta_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lblCANTIDAD_Click(object sender, EventArgs e)
        {

        }

        private void menuPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void PlaySystemSound()
        {
            SystemSounds.Question.Play();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            OleDbConnection conDeshacerVenta;
            OleDbCommand cmdDeshacerVenta;
            string strSQL_Delete = "DELETE FROM TEMP_VENTAS ";
            try
            {
                conDeshacerVenta =
                    new OleDbConnection(Conexion.CnnStr);
                conDeshacerVenta.Open();
                cmdDeshacerVenta = new OleDbCommand(strSQL_Delete, conDeshacerVenta);
                cmdDeshacerVenta.ExecuteNonQuery();
                cmdDeshacerVenta.Dispose();
                conDeshacerVenta.Close();
                ReadData(varUSER_LOGIN, varID_CAJA);
                txtID_PRODUCTO.Text = "";
                txtCANTIDAD.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DeshacerVenta");
            }
        }


        private void Audio()
        {
            
        }


        



        
    }
}
