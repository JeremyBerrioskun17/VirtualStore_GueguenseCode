namespace Sistema_TiendaVirtual_GueguenseCode.Views
{
    partial class DialogVerDetalleFactura
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCodigoFactura = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblFechaFactura = new System.Windows.Forms.Label();
            this.DTDetalleFactura = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalFactura = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DTDetalleFactura)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCodigoFactura);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 56);
            this.panel1.TabIndex = 0;
            // 
            // lblCodigoFactura
            // 
            this.lblCodigoFactura.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCodigoFactura.Location = new System.Drawing.Point(0, 0);
            this.lblCodigoFactura.Name = "lblCodigoFactura";
            this.lblCodigoFactura.Size = new System.Drawing.Size(347, 56);
            this.lblCodigoFactura.TabIndex = 0;
            this.lblCodigoFactura.Text = "Codigo Factura: 1";
            this.lblCodigoFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblFechaFactura);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(730, 57);
            this.panel2.TabIndex = 1;
            // 
            // lblFechaFactura
            // 
            this.lblFechaFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaFactura.Location = new System.Drawing.Point(0, 0);
            this.lblFechaFactura.Name = "lblFechaFactura";
            this.lblFechaFactura.Size = new System.Drawing.Size(730, 57);
            this.lblFechaFactura.TabIndex = 1;
            this.lblFechaFactura.Text = "Fecha: 10/06/2025";
            this.lblFechaFactura.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // DTDetalleFactura
            // 
            this.DTDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DTDetalleFactura.Dock = System.Windows.Forms.DockStyle.Top;
            this.DTDetalleFactura.Location = new System.Drawing.Point(0, 113);
            this.DTDetalleFactura.Name = "DTDetalleFactura";
            this.DTDetalleFactura.RowHeadersWidth = 51;
            this.DTDetalleFactura.RowTemplate.Height = 24;
            this.DTDetalleFactura.Size = new System.Drawing.Size(730, 333);
            this.DTDetalleFactura.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblTotalFactura);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 446);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(730, 91);
            this.panel3.TabIndex = 3;
            // 
            // lblTotalFactura
            // 
            this.lblTotalFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalFactura.Location = new System.Drawing.Point(0, 0);
            this.lblTotalFactura.Name = "lblTotalFactura";
            this.lblTotalFactura.Size = new System.Drawing.Size(730, 91);
            this.lblTotalFactura.TabIndex = 2;
            this.lblTotalFactura.Text = "Total: C$2000";
            this.lblTotalFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DialogVerDetalleFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 537);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.DTDetalleFactura);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DialogVerDetalleFactura";
            this.Text = "DialogVerDetalleFactura";
            this.Load += new System.EventHandler(this.DialogVerDetalleFactura_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DTDetalleFactura)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCodigoFactura;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblFechaFactura;
        private System.Windows.Forms.DataGridView DTDetalleFactura;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalFactura;
    }
}