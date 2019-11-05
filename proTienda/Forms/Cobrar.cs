using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Ticket;
using proTienda.Class;

namespace proTienda
{
    public partial class Cobrar : Form
    {
        public Cobrar()
        {
            InitializeComponent();
        }

        public Cobrar(int prmFolio)
        {
            InitializeComponent();
            varFolio = prmFolio;
        }

        int varFolio = 0;
        double varTotal = 0;

        private void frmCobrar_Load(object sender, EventArgs e)
        {
            this.Text = "Cobrar";
            //Form_Load           
            this.txtEfectivo.TextChanged += new EventHandler(txtEfectivo_TextChanged);
            varTotal = fnCalculaPago(varFolio);
            txtTotal.Text = varTotal.ToString();
        }

        void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCambio.Text = (Convert.ToDouble(txtEfectivo.Text) - varTotal).ToString();
            }
            catch (Exception ex)
            {
                txtCambio.Text = ex.Message;
            }
        }

        private double fnCalculaPago(int prmFolio)
        {
            try
            {
                OleDbConnection _cnnCalculaPago = new OleDbConnection(Conexion.CnnStr);
                _cnnCalculaPago.Open();
                OleDbCommand _cmdCalculaPago = new OleDbCommand("SELECT SUM(CANTIDAD*P_UNITARIO) " +
                    "FROM DETALLE_VENTAS " +
                    "WHERE FOLIO=" + prmFolio + "", _cnnCalculaPago);
                double _return = Convert.ToDouble(_cmdCalculaPago.ExecuteScalar());
                _cnnCalculaPago.Close();
                _cnnCalculaPago.Dispose();
                _cmdCalculaPago.Dispose();
                return (_return);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "fnCalculaPago");
                return (0);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((txtEfectivo.Text != "") && (Convert.ToDouble(txtEfectivo.Text) >= varTotal)) 
            {
                DialogResult _Resp = new DialogResult();
                _Resp = MessageBox.Show("¿Desea imprimir el ticket?",
                    "Ticket", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (_Resp == DialogResult.Yes)
                {
                    //Imprmmir el ticket
                    GenerarTicket(varFolio);
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Debe introducir una Cantidad Valida",
                    "Faltan datos");
            }
        }








        // pendejo
        private void HOLA()
        {
            try
            {
                OleDbConnection cnnInsert = new OleDbConnection(Conexion.CnnStr);
                cnnInsert.Open();

                OleDbCommand cmdInsert = new OleDbCommand();
                cmdInsert.Connection = cnnInsert;

                //insertamos el registro padre
                cmdInsert.CommandText = "INSERT INTO ARTICULOS_VENDIDOS (DESC_PRODUCTO,CANTIDAD, P_U_VENTA, P_UNITARIO) VALUES('A,'100', ')";

                cmdInsert.ExecuteNonQuery();

                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //

        private void GenerarTicket(int prmFOLIO)
        {
            try
            {
                string Ticket = "Nombre de la tienda: "+AppConfig._NEGOCIO+"\n" +
                    "RFC:"+AppConfig._RFC+"\n" +
                    "------------------------------\n" +
                    "ARTICULO   CANT   PRECIO   TOTAL\n" +
                    "------------------------------\n";



                string varSQL =
                    "SELECT LEFT(DESC_PRODUCTO,10) as DESC_PRODUCTO," +
                    " CANTIDAD,P_UNITARIO,TOTAL" +
                    " FROM vVENTAS WHERE FOLIO=" + prmFOLIO + "";

                string DetalleTicket = "";
                double varGranTotal = 0;
                OleDbConnection cnnTicket = new OleDbConnection(Conexion.CnnStr);
                cnnTicket.Open();
                OleDbCommand cmdTicket = new OleDbCommand(varSQL, cnnTicket);
                OleDbDataReader drTicket;
                drTicket = cmdTicket.ExecuteReader();

                while (drTicket.Read())
                {
                    DetalleTicket +=
                        drTicket["DESC_PRODUCTO"].ToString() + "   " +
                        drTicket["CANTIDAD"].ToString() + "   " +  
                        drTicket["P_UNITARIO"].ToString() + "   " +
                        drTicket["TOTAL"] + "\n";
                    varGranTotal += (double)drTicket["TOTAL"];
                }

                DetalleTicket += "------------------------------\n" +
                    "TOTAL: " + varGranTotal.ToString();
                Ticket += DetalleTicket;

                mPrintDocument _mPrintDocument = new mPrintDocument(Ticket);
                _mPrintDocument.PrintPreview();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
    }
}
