namespace Taller_Mecanico.Windows
{
    partial class frmPrincipal
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnMarcas = new System.Windows.Forms.Button();
            this.btnModelos = new System.Windows.Forms.Button();
            this.btnVehiculos = new System.Windows.Forms.Button();
            this.btnTipoVehiculos = new System.Windows.Forms.Button();
            this.btnTipoDePago = new System.Windows.Forms.Button();
            this.btnTiposClientes = new System.Windows.Forms.Button();
            this.btnRoles = new System.Windows.Forms.Button();
            this.btnHorariosLaborales = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(769, 293);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnMarcas
            // 
            this.btnMarcas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarcas.Location = new System.Drawing.Point(593, 229);
            this.btnMarcas.Name = "btnMarcas";
            this.btnMarcas.Size = new System.Drawing.Size(92, 57);
            this.btnMarcas.TabIndex = 0;
            this.btnMarcas.Text = "Marcas";
            this.btnMarcas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMarcas.UseVisualStyleBackColor = true;
            this.btnMarcas.Click += new System.EventHandler(this.btnMarcas_Click);
            // 
            // btnModelos
            // 
            this.btnModelos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModelos.Location = new System.Drawing.Point(466, 229);
            this.btnModelos.Name = "btnModelos";
            this.btnModelos.Size = new System.Drawing.Size(92, 57);
            this.btnModelos.TabIndex = 0;
            this.btnModelos.Text = "Modelos";
            this.btnModelos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModelos.UseVisualStyleBackColor = true;
            // 
            // btnVehiculos
            // 
            this.btnVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVehiculos.Location = new System.Drawing.Point(339, 229);
            this.btnVehiculos.Name = "btnVehiculos";
            this.btnVehiculos.Size = new System.Drawing.Size(92, 57);
            this.btnVehiculos.TabIndex = 0;
            this.btnVehiculos.Text = "Vehiculos";
            this.btnVehiculos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVehiculos.UseVisualStyleBackColor = true;
            // 
            // btnTipoVehiculos
            // 
            this.btnTipoVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTipoVehiculos.Location = new System.Drawing.Point(212, 229);
            this.btnTipoVehiculos.Name = "btnTipoVehiculos";
            this.btnTipoVehiculos.Size = new System.Drawing.Size(99, 57);
            this.btnTipoVehiculos.TabIndex = 0;
            this.btnTipoVehiculos.Text = "Tipo Vehiculos";
            this.btnTipoVehiculos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTipoVehiculos.UseVisualStyleBackColor = true;
            this.btnTipoVehiculos.Click += new System.EventHandler(this.btnTipoVehiculos_Click);
            // 
            // btnTipoDePago
            // 
            this.btnTipoDePago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTipoDePago.Location = new System.Drawing.Point(80, 229);
            this.btnTipoDePago.Name = "btnTipoDePago";
            this.btnTipoDePago.Size = new System.Drawing.Size(99, 57);
            this.btnTipoDePago.TabIndex = 0;
            this.btnTipoDePago.Text = "Tipo De Pago";
            this.btnTipoDePago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTipoDePago.UseVisualStyleBackColor = true;
            this.btnTipoDePago.Click += new System.EventHandler(this.btnTipoDePago_Click);
            // 
            // btnTiposClientes
            // 
            this.btnTiposClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiposClientes.Location = new System.Drawing.Point(80, 150);
            this.btnTiposClientes.Name = "btnTiposClientes";
            this.btnTiposClientes.Size = new System.Drawing.Size(99, 57);
            this.btnTiposClientes.TabIndex = 0;
            this.btnTiposClientes.Text = "Tipos De Clientes";
            this.btnTiposClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTiposClientes.UseVisualStyleBackColor = true;
            this.btnTiposClientes.Click += new System.EventHandler(this.btnTiposClientes_Click);
            // 
            // btnRoles
            // 
            this.btnRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoles.Location = new System.Drawing.Point(212, 150);
            this.btnRoles.Name = "btnRoles";
            this.btnRoles.Size = new System.Drawing.Size(99, 57);
            this.btnRoles.TabIndex = 0;
            this.btnRoles.Text = "Roles";
            this.btnRoles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRoles.UseVisualStyleBackColor = true;
            this.btnRoles.Click += new System.EventHandler(this.btnRoles_Click);
            // 
            // btnHorariosLaborales
            // 
            this.btnHorariosLaborales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHorariosLaborales.Location = new System.Drawing.Point(339, 150);
            this.btnHorariosLaborales.Name = "btnHorariosLaborales";
            this.btnHorariosLaborales.Size = new System.Drawing.Size(99, 57);
            this.btnHorariosLaborales.TabIndex = 0;
            this.btnHorariosLaborales.Text = "Horarios Laborales";
            this.btnHorariosLaborales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHorariosLaborales.UseVisualStyleBackColor = true;
            this.btnHorariosLaborales.Click += new System.EventHandler(this.btnHorariosLaborales_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientes.Location = new System.Drawing.Point(466, 150);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(92, 57);
            this.btnClientes.TabIndex = 0;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 328);
            this.ControlBox = false;
            this.Controls.Add(this.btnHorariosLaborales);
            this.Controls.Add(this.btnTiposClientes);
            this.Controls.Add(this.btnTipoDePago);
            this.Controls.Add(this.btnRoles);
            this.Controls.Add(this.btnTipoVehiculos);
            this.Controls.Add(this.btnVehiculos);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnModelos);
            this.Controls.Add(this.btnMarcas);
            this.Controls.Add(this.btnSalir);
            this.MaximumSize = new System.Drawing.Size(872, 367);
            this.MinimumSize = new System.Drawing.Size(872, 367);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPrincipal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnMarcas;
        private System.Windows.Forms.Button btnModelos;
        private System.Windows.Forms.Button btnVehiculos;
        private System.Windows.Forms.Button btnTipoVehiculos;
        private System.Windows.Forms.Button btnTipoDePago;
        private System.Windows.Forms.Button btnTiposClientes;
        private System.Windows.Forms.Button btnRoles;
        private System.Windows.Forms.Button btnHorariosLaborales;
        private System.Windows.Forms.Button btnClientes;
    }
}