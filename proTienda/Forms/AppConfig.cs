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
using System.IO;

namespace proTienda
{
    public partial class AppConfig : Form
    {
        public AppConfig()
        {
            InitializeComponent();
        }

        public static string _NEGOCIO = "NAR Accesorios";
        public static string _RFC = "NAR12313542";

        private void frmAppConfig_Load(object sender, EventArgs e)
        {
            this.Text = "Configuracion";

            txtFileName.Text = ConfigurationManager.AppSettings["DataFile1"];

            txtNombreNegocio.Text = ConfigurationManager.AppSettings["NombreNegocio"];

            txtRFC.Text = ConfigurationManager.AppSettings["RFC"];

            txtTelefono.Text = ConfigurationManager.AppSettings["Telefono"];

            txtDireccionFiscal.Text = ConfigurationManager.AppSettings["DireccionFiscal"];
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
          //  txtFileName.Text = "C:\\proTienda\\proTienda\\dbTienda.mdb";
            
            try
            {
                if (File.Exists(txtFileName.Text))
                {
                    _NEGOCIO = txtNombreNegocio.Text;
                    _RFC = txtRFC.Text;

                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    //Borramos la configuración actual
                    config.AppSettings.Settings.Remove("DataFile1");
                    config.AppSettings.Settings.Remove("NombreNegocio");
                    config.AppSettings.Settings.Remove("RFC");
                    config.AppSettings.Settings.Remove("Telefono");
                    config.AppSettings.Settings.Remove("DireccionFiscal");
                    config.Save(ConfigurationSaveMode.Modified);

                    //Force a reload of the changed section.
                    ConfigurationManager.RefreshSection("appSettings");

                    //Grabamos la configuración nueva
                    config.AppSettings.Settings.Add("DataFile1", txtFileName.Text);
                    config.AppSettings.Settings.Add("NombreNegocio", txtNombreNegocio.Text);
                    config.AppSettings.Settings.Add("RFC", txtRFC.Text);
                    config.AppSettings.Settings.Add("Telefono", txtTelefono.Text);
                    config.AppSettings.Settings.Add("DireccionFiscal", txtDireccionFiscal.Text);
                    // Save the configuration file.
                    config.Save(ConfigurationSaveMode.Modified);
                    //Force a reload of the changed section.
                    ConfigurationManager.RefreshSection("appSettings");
                    if (txtRFC.Text == "")
                    {
                        MessageBox.Show("Es Necesario llenar campo RFC");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("¡El archivo de Base de datos no existe! Favor de Seleccionar el archivo que dese");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {

            OpenFileDialog m_OpenFile = new OpenFileDialog();
            m_OpenFile.Title = "Buscar Base de datos de Microsoft Access";
            m_OpenFile.Filter = "Todos los archivos(*.*)|*.*|Base de datos Access (*.mdb)|*.mdb";
            m_OpenFile.FilterIndex = 2;

            if (m_OpenFile.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = m_OpenFile.FileName.ToString();

            }
            else
            {
                btnOK.Enabled = false;
            }
        }


        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
        }

    }
}
