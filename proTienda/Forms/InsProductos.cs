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
    public partial class InsProductos : Form
    {
        public InsProductos()
        {
            InitializeComponent();
        }

        public string varID_PRODUCTO = "";
        public int varUnidad = 0;
        public string _FOLIO = "";
        public int area = 0;
         

        //Evento cuando se carga la ventana corre los metodos Encabezados y ReadData
        private void frmInsProductos_Load(object sender, EventArgs e)
        {
            this.Text = "Productos";
            this.lvProductos.DoubleClick += new System.EventHandler(this.lvProductos_DoubleClick);
            Encabezados();
            ReadData();
           
         
        }     

        //Evento del Menu Strip el cual cierra la ventana
        private void menuPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Metodo el cual Declaramos los Encabezados del List View
        private void Encabezados()
        {
            lvProductos.View = View.Details;
            lvProductos.Columns.Add("Nombre Producto", 210,HorizontalAlignment.Left);
            lvProductos.Columns.Add("Folio", 100,HorizontalAlignment.Left);
            lvProductos.Columns.Add("Precio", 110, HorizontalAlignment.Left);
            lvProductos.Columns.Add("Cantidad", 80,HorizontalAlignment.Center);
            lvProductos.Columns.Add("Stock Minima", 100, HorizontalAlignment.Center); 
        }

        //Metodo el cual Muestra los valores de la Base de Datos en el List View
        private void ReadData()
        {
            //Este procedimiento lee los datos 
            //que se tranferirán y los mostrará en forma de
            //lista en el ListView   
            try
            {
                string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO, P_U_VENTA as PRECIO, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS ";
                          
                //Si la conexion esta abierta la cerramos
                //en caso contrario, la abrimos
                OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                if (cnnReadData.State == ConnectionState.Open)
                    cnnReadData.Close();

                else cnnReadData.Open();
                int I = 0;

                OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                OleDbDataReader drReadData;

                drReadData = cmdReadData.ExecuteReader();
                lvProductos.Items.Clear();

                while (drReadData.Read())
                {
                    lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                    lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                    lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                    lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                    lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());

                    if (drReadData["CANTIDAD"].ToString() == "1"
                        || drReadData["CANTIDAD"].ToString() == "2" ||
                        drReadData["CANTIDAD"].ToString() == "3"
                        || drReadData["CANTIDAD"].ToString() == "4")
                    {
                        MessageBox.Show("(" + drReadData["NOMBRE"].ToString() + ")" + "  'Cantidad de Producto menor a Sera Necesario Surtir'");

                    }

                    
                                    
                    I += 1;
                }

                //Agregamos un registro más
                if (I != 0)
                {
                    lvProductos.Items.Add("");
                    lvProductos.Items[I].SubItems.Add("");
                    lvProductos.Items[I].SubItems.Add("");     
                }
                drReadData.Close();
                cmdReadData.Dispose();
                cnnReadData.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Metodo para eliminar el valor seleccionado del List View
        private void DeleteData()
        {
            if (varID_PRODUCTO != "")
            {
                try
                {
                    //Buscamos el Folio 
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);

                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();
                    else cnnReadData.Open();


                    OleDbCommand cmdReadData = new OleDbCommand("SELECT ID_PRODUCTO, DESC_PRODUCTO ,CANTIDAD" +
                        " FROM CAT_PRODUCTOS" +
                        " WHERE DESC_PRODUCTO like '%" + varID_PRODUCTO + "%'", cnnReadData);

                    OleDbDataReader drReadData;
                    drReadData = cmdReadData.ExecuteReader();                  
            
                   


                    OleDbConnection cnndetDelete = new OleDbConnection(Conexion.CnnStr);
                    cnndetDelete.Open();

                    OleDbCommand cmddetDelete = new OleDbCommand();
                    cmddetDelete.Connection = cnndetDelete;
                    //Eliminamos el Registro seleccionado DE DETALLE_VENTAS
                    cmddetDelete.CommandText = "DELETE FROM DETALLE_VENTAS WHERE ID_PRODUCTO like '%" + _FOLIO + "%'";

                    cmddetDelete.ExecuteNonQuery();





                    OleDbConnection cnnDelete = new OleDbConnection(Conexion.CnnStr);
                    cnnDelete.Open();
                    OleDbCommand cmdDelete = new OleDbCommand();
                    cmdDelete.Connection = cnnDelete;

                    //Eliminamos el Registro seleccionado
                    cmdDelete.CommandText = "DELETE FROM CAT_PRODUCTOS WHERE DESC_PRODUCTO like '%" + varID_PRODUCTO + "%'";

                    cmdDelete.ExecuteNonQuery();

                    MessageBox.Show("Se Elimino el Producto Seleccionado");


                    InsProductos _frmInsProductos = new InsProductos();
                    _frmInsProductos.StartPosition = FormStartPosition.Manual;
                    _frmInsProductos.Show();

                    this.Close();
                   
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Seleccione un Producto de la Lista");
            }            
        }

        //Metodo para actualizar el valor seleccionado del List View
        private void UpdateData()
        {
            try
            {
                OleDbConnection cnnUpdate = new OleDbConnection(Conexion.CnnStr);
                cnnUpdate.Open();

                OleDbCommand cmdUpdate = new OleDbCommand();
                cmdUpdate.Connection = cnnUpdate;

                //insertamos el registro padre
                //cmdUpdate.CommandText = "UPDATE CAT_PRODUCTOS SET DESC_PRODUCTO='" + txtNombre.Text + "', ID_PRODUCTO='" + txtFolio.Text + "', P_U_VENTA=" + Convert.ToDecimal(txtPrecio.Text) + ", ID_UNIDAD_MEDIDA=" + varUnidad + ", CANTIDAD=" + Convert.ToInt32(txtCantidad.Text) + ", CANTIDAD_MIN=" + Convert.ToInt32(btnstockMinima.Text) + " WHERE DESC_PRODUCTO like '%" + varID_PRODUCTO + "%'";


                cmdUpdate.CommandText = "UPDATE CAT_PRODUCTOS SET DESC_PRODUCTO ='" + txtNombre.Text + "', P_U_VENTA=" + Convert.ToDecimal(txtPrecio.Text) + ", ID_UNIDAD_MEDIDA=" + varUnidad + ", CANTIDAD=" + Convert.ToInt32(txtCantidad.Text) + ", CANTIDAD_MIN=" + Convert.ToInt32(btnstockMinima.Text) + " WHERE DESC_PRODUCTO like '%" + varID_PRODUCTO + "%'";


                cmdUpdate.ExecuteNonQuery();

                MessageBox.Show("Se Modifico el Producto con Exito");


                InsProductos _frmInsProductos = new InsProductos();
                _frmInsProductos.StartPosition = FormStartPosition.Manual;
                _frmInsProductos.Show();

                this.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show("1.- No puedes modificar este articulo ya que tiene varias ventas relacionadas \n 2.- No Puedes Modiciar el Nombre \n 3.- La minimo Stock tiene que ser menor al actual cantidad \n 4.- Error de Sistema Verifiquelo con el Proveedor");
                
            }
            
        }

        //Metodo para agregar el valor seleccionado del List View
        private void AddData()
        {
            try
            {
                OleDbConnection cnnInsert = new OleDbConnection(Conexion.CnnStr);
                cnnInsert.Open();

                OleDbCommand cmdInsert = new OleDbCommand();
                cmdInsert.Connection = cnnInsert;

                //insertamos el registro padre
                cmdInsert.CommandText = "INSERT INTO CAT_PRODUCTOS (DESC_PRODUCTO, ID_PRODUCTO, P_U_VENTA, ID_UNIDAD_MEDIDA, CANTIDAD, P_U_COMPRA, ID_DEPARTAMENTO, CANTIDAD_MIN, IVA, P_U_MAYOREO, P_S_MAYOREO) VALUES('" + txtNombre.Text + "','" + txtFolio.Text + "'," + Convert.ToDecimal(txtPrecio.Text) + "," + varUnidad + "," + Convert.ToInt32(txtCantidad.Text) + ", 7.5,'100', " + Convert.ToInt32(btnstockMinima.Text) + ", 0.16," + txtPrecio.Text + "," + txtPrecio.Text + ")";

                cmdInsert.ExecuteNonQuery();

                MessageBox.Show("Se Agrego Producto con Exito");
                cnnInsert.Close();

                InsProductos _frmInsProductos = new InsProductos();
                _frmInsProductos.StartPosition = FormStartPosition.Manual;
                _frmInsProductos.Show();

                this.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validar(string dato)
        {
            if (dato != "")
                return true;

            else
                return false;
        }


        private void Campo_requerido()
        {
            if (txtNombre.Text == "")
            {
                CampoRequerrido.SetError(txtNombre, "Campo Requerido");
            }
            else
            {
                CampoRequerrido.SetError(txtNombre, "");
            }

            if (txtPrecio.Text == "")
            {
                CampoRequerrido.SetError(txtPrecio, "Campo Requerido");
            }
            else
            {
                CampoRequerrido.SetError(txtPrecio, "");
            }

            if (txtFolio.Text == "")
            {
                CampoRequerrido.SetError(txtFolio, "Campo Requerido");
            }
            else
            {
                CampoRequerrido.SetError(txtFolio, "");
            }

            if (txtCantidad.Text == "")
            {
                CampoRequerrido.SetError(txtCantidad, "Campo Requerido");
            }
            else
            {
                CampoRequerrido.SetError(txtCantidad, "");
            }

            if (btnstockMinima.Text == "")
            {
                CampoRequerrido.SetError(btnstockMinima, "Campo Requerido");
            }
            else
            {
                CampoRequerrido.SetError(btnstockMinima, "");
            }



            if (cboxUnidad.Text == "")
            {
                CampoRequerrido.SetError(cboxUnidad, "Campo Requerido");
            }
            else
            {
                CampoRequerrido.SetError(cboxUnidad, "");
            }
        }


       //Evento Boton Aceptar
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Campo_requerido();
            //si el textbox esta vacio mostrar mensaje
            if (!Validar(txtNombre.Text) && !Validar(txtFolio.Text) && !Validar(txtPrecio.Text) && !Validar(txtCantidad.Text) && !Validar(btnstockMinima.Text) && !Validar(cboxUnidad.Text))
            {
                MessageBox.Show("Los campos están vacíos");            }
            //si no entonces tu codigo
            else
            {
                AddData();
            }
            
        }

        //Evento Boton Modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Campo_requerido();
            //si el textbox esta vacio mostrar mensaje
            if (!Validar(txtNombre.Text))//envias el textbox que queres comprobar
            {
                MessageBox.Show("Ingresa un nombre");
            }

            if (!Validar(txtFolio.Text))
            {
                MessageBox.Show("Ingresa un id");
            }

            if (!Validar(txtPrecio.Text))
            {
                MessageBox.Show("Ingresa un precio");
            }

            if (!Validar(txtCantidad.Text))
            {
                MessageBox.Show("Ingresa una cantidad valida");
            }

            if (!Validar(btnstockMinima.Text))
            {
                MessageBox.Show("Ingresa un stock minimo");
            }

            if (!Validar(cboxUnidad.Text))
            {
                MessageBox.Show("Ingresa un area valida");
            }
            
            //si no entonces tu codigo
            else
            {
                UpdateData();
            }
            
        }     

        //Evento Boton Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DeleteData();                      
        }

        //Evento Boton Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
          
            btnAceptar.Enabled = true;
            
            txtNombre.Clear();
            txtCantidad.Clear();
            txtFolio.Clear();
            txtPrecio.Clear();
            btnstockMinima.Clear();
            

        }
       
        //Evento del txtPrecio el cual al precionar una tecla valida que no sean letras
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            }


        private void lvProductos_DoubleClick(object sender, EventArgs e)
        {
           
            Producto();
            txtCantidad.Focus();
            btnAceptar.Enabled = false;
            
        }


        private void Producto()
        {
            try
            {
                if (lvProductos.Items.Count != 0)
                {
                    varID_PRODUCTO = lvProductos.SelectedItems[0].Text;

                    ListViewItem listItem = lvProductos.SelectedItems[0];
                    _FOLIO = listItem.SubItems[1].Text;


                    //Buscamos el Folio para agregar en los TEXTBOX
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);

                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();
                    else cnnReadData.Open();


                    OleDbCommand cmdReadData = new OleDbCommand("SELECT ID_PRODUCTO, DESC_PRODUCTO ,CANTIDAD, P_U_VENTA, CANTIDAD_MIN" +
                        " FROM CAT_PRODUCTOS" +
                        " WHERE DESC_PRODUCTO like '%" + varID_PRODUCTO + "%'", cnnReadData);

                    OleDbDataReader drReadData;
                    drReadData = cmdReadData.ExecuteReader();

                  //  int I = 0;
                    while (drReadData.Read())
                    {
                        txtFolio.Text = drReadData["ID_PRODUCTO"].ToString();
                        txtCantidad.Text = drReadData["CANTIDAD"].ToString();
                        txtNombre.Text = drReadData["DESC_PRODUCTO"].ToString();
                        txtPrecio.Text = drReadData["P_U_VENTA"].ToString();
                        btnstockMinima.Text = drReadData["CANTIDAD_MIN"].ToString();
                         
                    }

                }

                else
                {
                    varID_PRODUCTO = "";
                }

                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe seleccionar un elemento de la lista. \nDescripción del error: \n" + ex.Message, "Operación no válida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void cboxUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (cboxUnidad.Text == "FUNDAS")
            {
                varUnidad = 1;       
            }

            if (cboxUnidad.Text == "CASE")
            {

                varUnidad = 2; 
            }

            if (cboxUnidad.Text == "TEMPLADO")
            {
                varUnidad = 3; 

            }

            if (cboxUnidad.Text == "CELULARES")
            {
                varUnidad = 4;

            }

            if (cboxUnidad.Text == "MEMORIAS")
            {
                varUnidad = 5;

            }
            if (cboxUnidad.Text == "TECNOLOGIA")
            {
                varUnidad = 6;

            }
            if (cboxUnidad.Text == "AUDIO")
            {
                varUnidad = 7;

            }
            if (cboxUnidad.Text == "TEMPORADA")
            {
                varUnidad = 8;

            }
            if (cboxUnidad.Text == "OFERTAS")
            {
                varUnidad = 9;

            }
            if (cboxUnidad.Text == "PROMOCIONES")
            {
                varUnidad = 10;

            }


        }


        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnstockMinima_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboxarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            
// inicio del comobo box
            


            if (cboxarea.Text == "ABARROTES")
            {
                area = 1;
            }
            {
                try
                {
                   // string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '" + area + "' ";
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //WHERE ID_PRODUCTO like '%" + _FOLIO + "%'";
                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {
                        
                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());

                                        
                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            }
            //
            if (cboxarea.Text == "BEBIDAS")
            {
                area = 2;
            }
            {
                try
                {
                    // string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '" + area + "' ";
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //WHERE ID_PRODUCTO like '%" + _FOLIO + "%'";
                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());
                      
                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //
            if (cboxarea.Text == "LACTEOS")
            {
                area = 3;
            }
            {
                try
                {
                    // string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '" + area + "' ";
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());

                      

                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }            
            //
            if (cboxarea.Text == "BOTANA-FRITURA")
            {
                area = 4;
            }
            {
                try
                {
                    // string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '" + area + "' ";
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            //
            if (cboxarea.Text == "CEREALES")
            {
                area = 5;
            }
            {
                try
                {
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    
                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());

                        // lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        //lvProductos.Items[I].SubItems.Add(drReadData["AREA"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            //
            if (cboxarea.Text == "TECNOLOGIA")
            {
                area = 6;
            }
            {
                try
                {
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            //
            if (cboxarea.Text == "LIMPIEZA")
            {
                area = 7;
            }
            {
                try
                {
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            //
            if (cboxarea.Text == "PANADERIA")
            {
                area = 8;
            }
            {
                try
                {
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            //
            if (cboxarea.Text == "REFRIJERADOS")
            {
                area = 9;
            }
            {
                try
                {
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD, CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 
            //
            if (cboxarea.Text == "TORTILLERIA")
            {
                area = 10;
            }
            {
                try
                {
                    string selectSQL = "SELECT DESC_PRODUCTO as NOMBRE, ID_PRODUCTO as FOLIO ,P_U_VENTA as PRECIO ,ID_UNIDAD_MEDIDA as AREA, CANTIDAD,CANTIDAD_MIN FROM CAT_PRODUCTOS WHERE ID_UNIDAD_MEDIDA LIKE '%" + area + "%'";

                    //Si la conexion esta abierta la cerramos
                    //en caso contrario, la abrimos
                    OleDbConnection cnnReadData = new OleDbConnection(Conexion.CnnStr);
                    if (cnnReadData.State == ConnectionState.Open)
                        cnnReadData.Close();

                    else cnnReadData.Open();
                    int I = 0;

                    OleDbCommand cmdReadData = new OleDbCommand(selectSQL, cnnReadData);
                    OleDbDataReader drReadData;

                    drReadData = cmdReadData.ExecuteReader();
                    lvProductos.Items.Clear();

                    while (drReadData.Read())
                    {

                        lvProductos.Items.Add(drReadData["NOMBRE"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["FOLIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["PRECIO"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD"].ToString());

                        lvProductos.Items[I].SubItems.Add(drReadData["CANTIDAD_MIN"].ToString());


                        I += 1;
                    }

                    //Agregamos un registro más
                    if (I != 0)
                    {
                        lvProductos.Items.Add("");
                        lvProductos.Items[I].SubItems.Add("");
                        lvProductos.Items[I].SubItems.Add("");


                    }
                    drReadData.Close();
                    cmdReadData.Dispose();
                    cnnReadData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } 

            //
            // fin de selecccion combobox
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            ReadData();
            cboxarea.Text = "TODOS";
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

                   
             


        



    }
}
