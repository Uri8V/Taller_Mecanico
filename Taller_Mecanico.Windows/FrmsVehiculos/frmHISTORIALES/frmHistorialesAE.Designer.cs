namespace Taller_Mecanico.Windows.FrmsVehiculos.frmHISTORIALES
{
    partial class frmHistorialesAE
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
            this.components = new System.ComponentModel.Container();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregarEmpleado = new System.Windows.Forms.Button();
            this.btnAgregarReserva = new System.Windows.Forms.Button();
            this.comboReserva = new System.Windows.Forms.ComboBox();
            this.comboEmpleados = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtValorPorHora = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtValorPorHoraExtra = new System.Windows.Forms.TextBox();
            this.btnAgregarVehiculo = new System.Windows.Forms.Button();
            this.comboVehiculos = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(253, 176);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(106, 45);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(545, 176);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 45);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregarEmpleado
            // 
            this.btnAgregarEmpleado.Image = global::Taller_Mecanico.Windows.Properties.Resources.add_32px;
            this.btnAgregarEmpleado.Location = new System.Drawing.Point(870, 12);
            this.btnAgregarEmpleado.Name = "btnAgregarEmpleado";
            this.btnAgregarEmpleado.Size = new System.Drawing.Size(48, 42);
            this.btnAgregarEmpleado.TabIndex = 18;
            this.btnAgregarEmpleado.UseVisualStyleBackColor = true;
            this.btnAgregarEmpleado.Click += new System.EventHandler(this.btnAgregarEmpleado_Click);
            // 
            // btnAgregarReserva
            // 
            this.btnAgregarReserva.Image = global::Taller_Mecanico.Windows.Properties.Resources.add_32px;
            this.btnAgregarReserva.Location = new System.Drawing.Point(732, 71);
            this.btnAgregarReserva.Name = "btnAgregarReserva";
            this.btnAgregarReserva.Size = new System.Drawing.Size(48, 42);
            this.btnAgregarReserva.TabIndex = 19;
            this.btnAgregarReserva.UseVisualStyleBackColor = true;
            this.btnAgregarReserva.Click += new System.EventHandler(this.btnAgregarReserva_Click);
            // 
            // comboReserva
            // 
            this.comboReserva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReserva.FormattingEnabled = true;
            this.comboReserva.Location = new System.Drawing.Point(156, 83);
            this.comboReserva.Name = "comboReserva";
            this.comboReserva.Size = new System.Drawing.Size(548, 21);
            this.comboReserva.TabIndex = 16;
            // 
            // comboEmpleados
            // 
            this.comboEmpleados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEmpleados.FormattingEnabled = true;
            this.comboEmpleados.Location = new System.Drawing.Point(562, 23);
            this.comboEmpleados.Name = "comboEmpleados";
            this.comboEmpleados.Size = new System.Drawing.Size(293, 21);
            this.comboEmpleados.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Reserva:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Empleados:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtValorPorHora
            // 
            this.txtValorPorHora.Location = new System.Drawing.Point(94, 136);
            this.txtValorPorHora.Name = "txtValorPorHora";
            this.txtValorPorHora.Size = new System.Drawing.Size(311, 20);
            this.txtValorPorHora.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Valor Por Hora:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(513, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Valor Por Hora Extra:";
            // 
            // txtValorPorHoraExtra
            // 
            this.txtValorPorHoraExtra.Location = new System.Drawing.Point(625, 139);
            this.txtValorPorHoraExtra.Name = "txtValorPorHoraExtra";
            this.txtValorPorHoraExtra.Size = new System.Drawing.Size(230, 20);
            this.txtValorPorHoraExtra.TabIndex = 28;
            // 
            // btnAgregarVehiculo
            // 
            this.btnAgregarVehiculo.Image = global::Taller_Mecanico.Windows.Properties.Resources.add_32px;
            this.btnAgregarVehiculo.Location = new System.Drawing.Point(422, 12);
            this.btnAgregarVehiculo.Name = "btnAgregarVehiculo";
            this.btnAgregarVehiculo.Size = new System.Drawing.Size(48, 42);
            this.btnAgregarVehiculo.TabIndex = 32;
            this.btnAgregarVehiculo.UseVisualStyleBackColor = true;
            this.btnAgregarVehiculo.Click += new System.EventHandler(this.btnAgregarVehiculo_Click);
            // 
            // comboVehiculos
            // 
            this.comboVehiculos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVehiculos.FormattingEnabled = true;
            this.comboVehiculos.Location = new System.Drawing.Point(71, 23);
            this.comboVehiculos.Name = "comboVehiculos";
            this.comboVehiculos.Size = new System.Drawing.Size(334, 21);
            this.comboVehiculos.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Vehiculos:";
            // 
            // frmHistorialesAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 232);
            this.ControlBox = false;
            this.Controls.Add(this.btnAgregarVehiculo);
            this.Controls.Add(this.comboVehiculos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtValorPorHoraExtra);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtValorPorHora);
            this.Controls.Add(this.btnAgregarEmpleado);
            this.Controls.Add(this.btnAgregarReserva);
            this.Controls.Add(this.comboReserva);
            this.Controls.Add(this.comboEmpleados);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Name = "frmHistorialesAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistorialesAE";
            this.Load += new System.EventHandler(this.frmHistorialesAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregarEmpleado;
        private System.Windows.Forms.Button btnAgregarReserva;
        private System.Windows.Forms.ComboBox comboReserva;
        private System.Windows.Forms.ComboBox comboEmpleados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtValorPorHoraExtra;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtValorPorHora;
        private System.Windows.Forms.Button btnAgregarVehiculo;
        private System.Windows.Forms.ComboBox comboVehiculos;
        private System.Windows.Forms.Label label7;
    }
}