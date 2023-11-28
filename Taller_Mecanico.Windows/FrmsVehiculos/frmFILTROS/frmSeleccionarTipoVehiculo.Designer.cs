namespace Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOS
{
    partial class frmSeleccionarTipoVehiculo
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTipoDeVehiculo = new System.Windows.Forms.ComboBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregarTipoDeVehiculo = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Seleccionar Tipo De Vehiculo:";
            // 
            // comboBoxTipoDeVehiculo
            // 
            this.comboBoxTipoDeVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoDeVehiculo.FormattingEnabled = true;
            this.comboBoxTipoDeVehiculo.Location = new System.Drawing.Point(173, 24);
            this.comboBoxTipoDeVehiculo.Name = "comboBoxTipoDeVehiculo";
            this.comboBoxTipoDeVehiculo.Size = new System.Drawing.Size(175, 21);
            this.comboBoxTipoDeVehiculo.TabIndex = 18;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(37, 69);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(87, 23);
            this.btnConfirmar.TabIndex = 16;
            this.btnConfirmar.Text = "CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(199, 69);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAgregarTipoDeVehiculo
            // 
            this.btnAgregarTipoDeVehiculo.Image = global::Taller_Mecanico.Windows.Properties.Resources.add_32px;
            this.btnAgregarTipoDeVehiculo.Location = new System.Drawing.Point(354, 12);
            this.btnAgregarTipoDeVehiculo.Name = "btnAgregarTipoDeVehiculo";
            this.btnAgregarTipoDeVehiculo.Size = new System.Drawing.Size(50, 43);
            this.btnAgregarTipoDeVehiculo.TabIndex = 20;
            this.btnAgregarTipoDeVehiculo.UseVisualStyleBackColor = true;
            this.btnAgregarTipoDeVehiculo.Click += new System.EventHandler(this.btnAgregarTipoDeVehiculo_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmSeleccionarTipoVehiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 111);
            this.ControlBox = false;
            this.Controls.Add(this.btnAgregarTipoDeVehiculo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTipoDeVehiculo);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnCancelar);
            this.MaximumSize = new System.Drawing.Size(436, 150);
            this.MinimumSize = new System.Drawing.Size(436, 150);
            this.Name = "frmSeleccionarTipoVehiculo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSeleccionarTipoVehiculo";
            this.Load += new System.EventHandler(this.frmSeleccionarTipoVehiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAgregarTipoDeVehiculo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTipoDeVehiculo;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}