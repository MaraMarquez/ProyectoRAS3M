using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;



namespace proTienda
{
    public partial class Menu : Form
    {
        
        public Menu()
        {
            InitializeComponent();
        }

      

        private void mdiMain_Load(object sender, EventArgs e)
        {
            
            this.Text = "Módulo de Control de Ventas, Usuario: " +
            Login._NOMBRE + " " +
            Login._PATERNO + " " +
            Login._MATERNO;
            mnuVentas.Enabled = Login._VENTAS;
            mnuAdministrar.Enabled = Login._ADMINISTRAR;
            mnuConsultas.Enabled = Login._CONSULTAS;
           

            btnVneta.Enabled = Login._VENTAS;
            btnConsultasVentas.Enabled = Login._CONSULTAS;
            btnProductos.Enabled = Login._CONSULTAS;
            btnUsuarios.Enabled = Login._ADMINISTRAR;
            
           
        }      
  

        private void mnuUsuarios_Click(object sender, EventArgs e)
        {
            Users _frmUsers = new Users();            
            _frmUsers.StartPosition = FormStartPosition.CenterScreen;
            _frmUsers.ShowDialog();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void mnuVentas_Click(object sender, EventArgs e)
        {
            frmVentas _frmVentas = new frmVentas(Login._USER_NAME, 1);
            _frmVentas.StartPosition = FormStartPosition.CenterScreen;
            _frmVentas.ShowDialog();
        }

        private void mnuCatalogos_Click(object sender, EventArgs e)
        {

        }

        //Consultas Productos
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscaProducto _frmBuscaProducto = new frmBuscaProducto();
            _frmBuscaProducto.StartPosition = FormStartPosition.CenterScreen;
            _frmBuscaProducto.ShowDialog();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaVentas _frmConsultaVentas = new ConsultaVentas();
            _frmConsultaVentas.StartPosition = FormStartPosition.CenterScreen;
            _frmConsultaVentas.ShowDialog();
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InsProductos _frmInsProductos = new InsProductos();
            _frmInsProductos.StartPosition = FormStartPosition.CenterScreen;
            _frmInsProductos.ShowDialog();
          
        }    

        //
            private void btnVneta_Click(object sender, EventArgs e)
        {
            frmVentas _frmVentas = new frmVentas(Login._USER_NAME, 1);
            _frmVentas.StartPosition = FormStartPosition.CenterScreen;
            _frmVentas.ShowDialog();
        }

        private void btnConsultasVentas_Click(object sender, EventArgs e)
        {
            ConsultaVentas _frmConsultaVentas = new ConsultaVentas();
            _frmConsultaVentas.StartPosition = FormStartPosition.CenterScreen;
            _frmConsultaVentas.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmBuscaProducto _frmBuscaProducto = new frmBuscaProducto();
            _frmBuscaProducto.StartPosition = FormStartPosition.CenterScreen;
            _frmBuscaProducto.ShowDialog();

            
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Users _frmUsers = new Users();
            _frmUsers.StartPosition = FormStartPosition.CenterScreen;
            _frmUsers.ShowDialog();
        }



        private void calculadoraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "calc.exe";
            p.StartInfo.Arguments = "";
            p.Start();
        }

        private void acercaDeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Acercade _Acercade = new Acercade();
            _Acercade.StartPosition = FormStartPosition.CenterScreen;
            _Acercade.ShowDialog();
        }

        private void btnProductos_Click_1(object sender, EventArgs e)
        {
            InsProductos _frmproducto = new InsProductos();
            _frmproducto.StartPosition = FormStartPosition.CenterScreen;
            _frmproducto.ShowDialog();


        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ConsultaReporte _Consultareporte = new ConsultaReporte();
            _Consultareporte.StartPosition = FormStartPosition.CenterScreen;
            _Consultareporte.ShowDialog();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Hide();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

       
        }

       
        
      
    }

