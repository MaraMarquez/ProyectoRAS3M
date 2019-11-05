namespace proTienda
{
    partial class ConsultaVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultaVentas));
            this.lvListaVentas = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvListaVentas
            // 
            this.lvListaVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvListaVentas.GridLines = true;
            this.lvListaVentas.HideSelection = false;
            this.lvListaVentas.Location = new System.Drawing.Point(0, 0);
            this.lvListaVentas.Name = "lvListaVentas";
            this.lvListaVentas.Size = new System.Drawing.Size(651, 342);
            this.lvListaVentas.TabIndex = 0;
            this.lvListaVentas.UseCompatibleStateImageBehavior = false;
            this.lvListaVentas.SelectedIndexChanged += new System.EventHandler(this.lvListaVentas_SelectedIndexChanged);
            // 
            // ConsultaVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 342);
            this.Controls.Add(this.lvListaVentas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(667, 381);
            this.MinimumSize = new System.Drawing.Size(667, 381);
            this.Name = "ConsultaVentas";
            this.Text = "frmConsultaVentas";
            this.Load += new System.EventHandler(this.frmConsultaVentas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvListaVentas;
    }
}