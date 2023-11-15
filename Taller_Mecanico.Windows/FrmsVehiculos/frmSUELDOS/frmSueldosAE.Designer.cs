namespace Taller_Mecanico.Windows.FrmsVehiculos.frmSUELDOS
{
    partial class frmSueldosAE
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboHorasLaborales = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboHistorial = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAgregarHorasLaborales = new System.Windows.Forms.Button();
            this.btnAgregarHistorial = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalAPagar = new System.Windows.Forms.TextBox();
            this.txtTotalExtra = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(124, 215);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(121, 54);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "CONFIRMAR";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(378, 215);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 54);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // comboHorasLaborales
            // 
            this.comboHorasLaborales.FormattingEnabled = true;
            this.comboHorasLaborales.Location = new System.Drawing.Point(139, 40);
            this.comboHorasLaborales.Name = "comboHorasLaborales";
            this.comboHorasLaborales.Size = new System.Drawing.Size(121, 21);
            this.comboHorasLaborales.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seleccione la Fecha:";
            // 
            // comboHistorial
            // 
            this.comboHistorial.FormattingEnabled = true;
            this.comboHistorial.Location = new System.Drawing.Point(139, 111);
            this.comboHistorial.Name = "comboHistorial";
            this.comboHistorial.Size = new System.Drawing.Size(121, 21);
            this.comboHistorial.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seleccione el Historial:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnAgregarHorasLaborales
            // 
            this.btnAgregarHorasLaborales.Image = global::Taller_Mecanico.Windows.Properties.Resources.add_32px;
            this.btnAgregarHorasLaborales.Location = new System.Drawing.Point(280, 28);
            this.btnAgregarHorasLaborales.Name = "btnAgregarHorasLaborales";
            this.btnAgregarHorasLaborales.Size = new System.Drawing.Size(48, 42);
            this.btnAgregarHorasLaborales.TabIndex = 12;
            this.btnAgregarHorasLaborales.UseVisualStyleBackColor = true;
            this.btnAgregarHorasLaborales.Click += new System.EventHandler(this.btnAgregarHorasLaborales_Click);
            // 
            // btnAgregarHistorial
            // 
            this.btnAgregarHistorial.Image = global::Taller_Mecanico.Windows.Properties.Resources.add_32px;
            this.btnAgregarHistorial.Location = new System.Drawing.Point(280, 99);
            this.btnAgregarHistorial.Name = "btnAgregarHistorial";
            this.btnAgregarHistorial.Size = new System.Drawing.Size(48, 42);
            this.btnAgregarHistorial.TabIndex = 12;
            this.btnAgregarHistorial.UseVisualStyleBackColor = true;
            this.btnAgregarHistorial.Click += new System.EventHandler(this.btnAgregarHistorial_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Total a Pagar:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total Extra:";
            // 
            // txtTotalAPagar
            // 
            this.txtTotalAPagar.Enabled = false;
            this.txtTotalAPagar.Location = new System.Drawing.Point(456, 35);
            this.txtTotalAPagar.Name = "txtTotalAPagar";
            this.txtTotalAPagar.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAPagar.TabIndex = 14;
            // 
            // txtTotalExtra
            // 
            this.txtTotalExtra.Enabled = false;
            this.txtTotalExtra.Location = new System.Drawing.Point(442, 116);
            this.txtTotalExtra.Name = "txtTotalExtra";
            this.txtTotalExtra.Size = new System.Drawing.Size(100, 20);
            this.txtTotalExtra.TabIndex = 14;
            // 
            // frmSueldosAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 295);
            this.ControlBox = false;
            this.Controls.Add(this.txtTotalExtra);
            this.Controls.Add(this.txtTotalAPagar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregarHistorial);
            this.Controls.Add(this.btnAgregarHorasLaborales);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboHistorial);
            this.Controls.Add(this.comboHorasLaborales);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnOk);
            this.Name = "frmSueldosAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSueldosAE";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox comboHorasLaborales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboHistorial;
        private System.Windows.Forms.Button btnAgregarHistorial;
        private System.Windows.Forms.Button btnAgregarHorasLaborales;
        private System.Windows.Forms.TextBox txtTotalExtra;
        private System.Windows.Forms.TextBox txtTotalAPagar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}