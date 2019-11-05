namespace proTienda
{
    partial class frmBuscaProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscaProducto));
            this.lblDESC_PRODUCTO = new System.Windows.Forms.Label();
            this.txtDESC_PRODUCTO = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lvProductos = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDESC_PRODUCTO
            // 
            this.lblDESC_PRODUCTO.AutoSize = true;
            this.lblDESC_PRODUCTO.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDESC_PRODUCTO.Location = new System.Drawing.Point(3, 1);
            this.lblDESC_PRODUCTO.Name = "lblDESC_PRODUCTO";
            this.lblDESC_PRODUCTO.Size = new System.Drawing.Size(192, 17);
            this.lblDESC_PRODUCTO.TabIndex = 0;
            this.lblDESC_PRODUCTO.Text = "Indroduzca datos de búsqueda";
            // 
            // txtDESC_PRODUCTO
            // 
            this.txtDESC_PRODUCTO.Location = new System.Drawing.Point(6, 21);
            this.txtDESC_PRODUCTO.Name = "txtDESC_PRODUCTO";
            this.txtDESC_PRODUCTO.Size = new System.Drawing.Size(274, 20);
            this.txtDESC_PRODUCTO.TabIndex = 1;
            this.txtDESC_PRODUCTO.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Orange;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBuscar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscar.Location = new System.Drawing.Point(286, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 22);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lvProductos
            // 
            this.lvProductos.FullRowSelect = true;
            this.lvProductos.GridLines = true;
            this.lvProductos.HideSelection = false;
            this.lvProductos.Location = new System.Drawing.Point(15, 134);
            this.lvProductos.Name = "lvProductos";
            this.lvProductos.Size = new System.Drawing.Size(612, 279);
            this.lvProductos.TabIndex = 2;
            this.lvProductos.UseCompatibleStateImageBehavior = false;
            this.lvProductos.SelectedIndexChanged += new System.EventHandler(this.lvProductos_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Schoolbook", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(457, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 34);
            this.label4.TabIndex = 26;
            this.label4.Text = "Productos";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SandyBrown;
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.lblDESC_PRODUCTO);
            this.panel1.Controls.Add(this.txtDESC_PRODUCTO);
            this.panel1.Location = new System.Drawing.Point(15, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 49);
            this.panel1.TabIndex = 27;
            // 
            // frmBuscaProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(639, 425);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lvProductos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBuscaProducto";
            this.Text = "Búsqueda de productos";
            this.Load += new System.EventHandler(this.frmBuscaProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDESC_PRODUCTO;
        private System.Windows.Forms.TextBox txtDESC_PRODUCTO;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ListView lvProductos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
    }
}