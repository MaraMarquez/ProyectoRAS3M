using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using proTienda.Class;
using System.Data;


namespace proTienda
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        Menu Menu = new Menu();
        
      
        public static string _USER_NAME = "";
        public static string _PATERNO = "";
        public static string _MATERNO = "";
        public static string _NOMBRE = "";
        public static bool _VENTAS = false;
        public static bool _ADMINISTRAR = false;
        public static bool _REPORTES = false;
        public static bool _FACTURACION = false;
        public static bool _CONSULTAS = false;
        public static bool _DESHACER_VENTA = false;
               

        private DataTable fnLogin(string prmUSER_NAME, string prmPASSWORD)
        {
            DataTable dt = new DataTable();
            try
            {
                
                string Query = " SELECT *";
                Query += " FROM USERS ";
                Query += " WHERE USER_NAME='" + prmUSER_NAME + "' ";
                Query += " AND USER_PASSWORD ='" + prmPASSWORD + "'";

                using (OleDbConnection connection = new OleDbConnection(Conexion.CnnStr))
                using (OleDbCommand command = new OleDbCommand(Query, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() == "" || txtContraseña.Text.Trim() == "")
            {
                MessageBox.Show("El Usuario o la Contraseña son incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (fnLogin(txtUsuario.Text, txtContraseña.Text).Rows.Count > 0)
                {

                    _USER_NAME = fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["USER_NAME"].ToString();
                    _PATERNO = fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["PATERNO"].ToString();
                    _MATERNO = fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["MATERNO"].ToString();
                    _NOMBRE = fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["NOMBRE"].ToString();
                    _VENTAS = bool.Parse(fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["VENTAS"].ToString());
                    _ADMINISTRAR = bool.Parse(fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["ADMINISTRAR"].ToString());
                    _REPORTES = bool.Parse(fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["REPORTES"].ToString());
                    _FACTURACION = bool.Parse(fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["FACTURACION"].ToString());
                    _CONSULTAS = bool.Parse(fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["CONSULTAS"].ToString());
                    _DESHACER_VENTA = bool.Parse(fnLogin(txtUsuario.Text, txtContraseña.Text).Rows[0]["DESHACER_VENTA"].ToString());

                    Menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("El Usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

     
      

        
    }
}
