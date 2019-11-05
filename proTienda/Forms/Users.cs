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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
                
        static OleDbConnection cnnUsers;
        static OleDbDataAdapter daUsers;
        static OleDbCommandBuilder cbUsers;
        DataSet dsUsers = new DataSet("dsUsers");
        CurrencyManager cmUsers;

        private void frmUsers_Load(object sender, EventArgs e)
        {
            cnnUsers = new OleDbConnection(Conexion.CnnStr);
            daUsers = new OleDbDataAdapter();
            daUsers.SelectCommand = new OleDbCommand("SELECT * FROM USERS", cnnUsers);
            cbUsers = new OleDbCommandBuilder(daUsers);

            if (cnnUsers.State == ConnectionState.Open)
                cnnUsers.Close();
                cnnUsers.Open();
                dsUsers.Clear();

            daUsers.Fill(dsUsers, "USERS");
            txtUsuario.DataBindings.Add("Text", dsUsers, "USERS.USER_NAME");
            txtContraseña.DataBindings.Add("Text", dsUsers, "USERS.USER_PASSWORD");
            txtConContraseña.DataBindings.Add("Text", dsUsers, "USERS.USER_PASSWORD");
            txtPaterno.DataBindings.Add("Text", dsUsers, "USERS.PATERNO");
            txtMaterno.DataBindings.Add("Text", dsUsers, "USERS.MATERNO");
            txtNombre.DataBindings.Add("Text", dsUsers, "USERS.NOMBRE");
            chkAdministrar.DataBindings.Add("Checked", dsUsers, "USERS.ADMINISTRAR", false);
            chkVentas.DataBindings.Add("Checked", dsUsers, "USERS.VENTAS", true);
            chkReportes.DataBindings.Add("Checked", dsUsers, "USERS.REPORTES", true);
            chkFacturacion.DataBindings.Add("Checked", dsUsers, "USERS.FACTURACION", true);
            chkConsultas.DataBindings.Add("Checked", dsUsers, "USERS.CONSULTAS", true);
            chkDeshacer.DataBindings.Add("Checked", dsUsers, "USERS.DESHACER_VENTA", true);
            cmUsers = (CurrencyManager)this.BindingContext[dsUsers, "USERS"];
            cnnUsers.Close();

        }
                
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpia();
        }

        private void btnGarabar_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection cnnInsert = new OleDbConnection(Conexion.CnnStr);
                cnnInsert.Open();

                OleDbCommand cmdInsert = new OleDbCommand();
                cmdInsert.Connection = cnnInsert;

                cmdInsert.CommandText += "INSERT INTO USERS (USER_NAME, USER_PASSWORD, GRUPO, PATERNO, MATERNO, NOMBRE, VENTAS, ADMINISTRAR, REPORTES, CATALOGOS, CONSULTAS, DESHACER_VENTA, LOGON, FACTURACION)";
                cmdInsert.CommandText += " VALUES('" + txtNombre.Text + "', '" + txtConContraseña.Text + "', 'Usuarios', '" + txtPaterno.Text + "', '" + txtMaterno.Text + "', '" + txtNombre.Text + "','" + (chkVentas.Checked ? -1 : 0) + "', '" + (chkAdministrar.Checked? -1 : 0) + "', '" + (chkReportes.Checked? -1 : 0) + "', '" + (chkCatalogos.Checked? -1 : 0) + "', '" + (chkConsultas.Checked? -1 : 0) + "', '" + (chkDeshacer.Checked? -1 : 0) + "', '" + (chkLogon.Checked? -1 : 0) + "', '" + (chkFacturacion.Checked? -1 : 0) + "')    ";
                cmdInsert.ExecuteNonQuery();
                MessageBox.Show("El Agrego con exito el Usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cnnInsert.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "" + ex.StackTrace);
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Eliminar
            try
            {
                OleDbConnection cnnInsert = new OleDbConnection(Conexion.CnnStr);
                cnnInsert.Open();

                OleDbCommand cmdInsert = new OleDbCommand();
                cmdInsert.Connection = cnnInsert;
                cmdInsert.CommandText += "DELETE FROM USERS";
                cmdInsert.CommandText += " WHERE USER_NAME= '" + txtUsuario.Text + "' AND USER_PASSWORD = '" + txtContraseña.Text + "' AND  NOMBRE = '" + txtNombre.Text + "'   ";
                cmdInsert.ExecuteNonQuery();
                MessageBox.Show("Se elimino con exito el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cnnInsert.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //OleDbConnection cnn = new OleDbConnection(Conexion.CnnStr);
                //cnn.Open();

                //OleDbCommand cmd = new OleDbCommand();
                //cmd.Connection = cnn;

                //cmd.CommandText += "UPDATE USERS SET USER_NAME=?, USER_PASSWORD='" + txtConContraseña.Text + "', GRUPO= 'Usuarios', PATERNO = '" + txtPaterno.Text + "', MATERNO= '" + txtMaterno.Text + "', NOMBRE = '" + txtNombre.Text + "', VENTAS = '" + (chkVentas.Checked ? -1 : 0) + "', ADMINISTRAR = '" + (chkAdministrar.Checked ? -1 : 0) + "', REPORTES = " + (chkReportes.Checked ? -1 : 0) + "', CATALOGOS = '" + (chkCatalogos.Checked ? -1 : 0) + "', CONSULTAS = '" + (chkConsultas.Checked ? -1 : 0) + "', DESHACER_VENTA = '" + (chkDeshacer.Checked ? -1 : 0) + "', LOGON = '" + (chkLogon.Checked ? -1 : 0) + "', FACTURACION = '" + (chkFacturacion.Checked ? -1 : 0) + "' ";
                //cmd.CommandText += " WHERE USER_NAME= '" + txtUsuario.Text + "' AND USER_PASSWORD = '" + txtContraseña.Text + "' AND  NOMBRE = '" + txtNombre.Text + "' ";

                //cmd.Parameters.AddWithValue("?", txtNombre.Text);

                //cmd.ExecuteNonQuery();
                //MessageBox.Show("El Modifico con exito", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //cnn.Close();
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "" + ex.StackTrace);
            }
        }



        private void btnInicio_Click(object sender, EventArgs e)
        {
            cmUsers.Position = 0;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            cmUsers.Position -= 1;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            cmUsers.Position += 1;
        }

        private void btnFinal_Click(object sender, EventArgs e)
        {
            cmUsers.Position = cmUsers.Count - 1;
        }



        private void new_usuario()
        {

            if (chkAdministrar.Checked == true)
            {
                chkAdministrar.Checked = true;
            }
            else chkAdministrar.Checked = false;

            if (chkVentas.Checked == true)
            {
                chkVentas.Checked = true;
            }
            else chkVentas.Checked = false;

            if (chkFacturacion.Checked == true)
            {
                chkFacturacion.Checked = true;
            }
            else chkFacturacion.Checked = false;

            if (chkDeshacer.Checked == true)
            {
                chkDeshacer.Checked = true;
            }
            else chkDeshacer.Checked = false;

            if (chkConsultas.Checked == true)
            {
                chkConsultas.Checked = true;
            }
            else chkConsultas.Checked = false;

            if (chkReportes.Checked == true)
            {
                chkReportes.Checked = true;
            }
            else chkReportes.Checked = false;


            OleDbConnection cnnInsert = new OleDbConnection(Conexion.CnnStr);
            cnnInsert.Open();

            OleDbCommand cmdInsert = new OleDbCommand();
            cmdInsert.Connection = cnnInsert;
         //   cmdInsert.CommandText = ("INSERT INTO USERS (USER_NAME, USER_PASSWORD, PATERNO, MATERNO, NOMBRE) VALUES('" + txtUSER_LOGIN.Text + "','"+ txtPASSWORD.Text + "','" + txtPATERNO.Text + "'," + txtMATERNO.Text + "," + txtNOMBRE.Text +")");
   cmdInsert.CommandText = "INSERT INTO USERS (USER_NAME, USER_PASSWORD, PATERNO, MATERNO, NOMBRE) VALUES('" + txtUsuario.Text + "','" + txtContraseña.Text + "','" + txtPaterno.Text + "','" + txtMaterno.Text + "','" + txtNombre.Text + "')";

          //  cmdInsert.CommandText = "INSERT INTO USERS VALUES(@USER_NAME,@USER_PASSWORD,@PATERNO , @MATERNO , @NOMBRE)";

               
            cmdInsert.Parameters.AddWithValue("@USER_NAME", txtUsuario.Text);

            cmdInsert.Parameters.AddWithValue("@USER_PASSWORD",txtContraseña.Text);

            cmdInsert.Parameters.AddWithValue("@PATERNO", txtPaterno.Text);

            cmdInsert.Parameters.AddWithValue("@MATERNO", txtMaterno.Text);

            cmdInsert.Parameters.AddWithValue("@NOMBRE", txtNombre.Text);

          
           
            //if (chkADMINISTRAR.Checked)
            //{
            //    cmdInsert.Parameters.AddWithValue("@VENTAS", chkADMINISTRAR.Checked);
            //}
            //if (chkVENTAS.Checked)
            //{
            //    cmdInsert.Parameters.AddWithValue("@ADMINISTRAR", chkVENTAS.Checked);
            //}
            //if (chkREPORTES.Checked)
            //{
            //    cmdInsert.Parameters.AddWithValue("@REPORTES", chkREPORTES.Checked);
            //}
            //if (chkCONSULTAS.Checked)
            //{
            //    cmdInsert.Parameters.AddWithValue("@CONSULTAS", chkREPORTES.Checked);
            //}

            //if (chkDESHACER_VENTA.Checked)
            //{
            //    cmdInsert.Parameters.AddWithValue("@DESHACER_VENTA", chkREPORTES.Checked);
            //}

            //if (chkFACTURACION.Checked)
            //{
            //    cmdInsert.Parameters.AddWithValue("@FACTURACION", chkREPORTES.Checked);
            //}

            

            cmdInsert.ExecuteNonQuery();


                 cnnInsert.Close();
         
          
            MessageBox.Show("Se Agrego Producto con Exito");

            

            //txtUSER_LOGIN.DataBindings.Add("Text", dsUsers, "USERS.USER_NAME");

            //txtPASSWORD.DataBindings.Add("Text", dsUsers, "USERS.USER_PASSWORD");

            //txtPATERNO.DataBindings.Add("Text", dsUsers, "USERS.PATERNO");

            //txtMATERNO.DataBindings.Add("Text", dsUsers, "USERS.MATERNO");

            //txtNOMBRE.DataBindings.Add("Text", dsUsers, "USERS.NOMBRE");

            //chkADMINISTRAR.DataBindings.Add("Checked", dsUsers, "USERS.ADMINISTRAR", false);

            //chkVENTAS.DataBindings.Add("Checked", dsUsers, "USERS.VENTAS", true);

            //chkREPORTES.DataBindings.Add("Checked", dsUsers, "USERS.REPORTES", true);

            //chkFACTURACION.DataBindings.Add("Checked", dsUsers, "USERS.FACTURACION", true);

            //chkCONSULTAS.DataBindings.Add("Checked", dsUsers, "USERS.CONSULTAS", true);

            //chkDESHACER_VENTA.DataBindings.Add("Checked", dsUsers, "USERS.DESHACER_VENTA", true);

           
        
        }

        private void Limpia()
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            txtConContraseña.Text = "";
            txtPaterno.Text = "";
            txtMaterno.Text = "";
            txtNombre.Text = "";
            chkVentas.Checked = false;
            chkReportes.Checked = false;
            chkConsultas.Checked = false;
            chkAdministrar.Checked = false;
            chkFacturacion.Checked = false;
            chkDeshacer.Checked = false;
        }

        private void txtConContraseña_Validating(object sender, CancelEventArgs e)
        {
            if (txtConContraseña.Text.Trim() != txtContraseña.Text.Trim())
            {
                btnGarabar.Enabled = false;
                btnEditar.Enabled = false;
                txtContraseña.Focus();
                MessageBox.Show("Las Contraseñas no Coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                btnGarabar.Enabled = true;
                btnEditar.Enabled = true;
            }
           
        }

       

    }
}
