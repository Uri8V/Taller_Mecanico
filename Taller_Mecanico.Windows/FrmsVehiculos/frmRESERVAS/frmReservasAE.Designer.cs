namespace Taller_Mecanico.Windows.FrmsVehiculos.frmRESERVAS
{
    partial class frmReservasAE
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.checkBoxSePresento = new System.Windows.Forms.CheckBox();
            this.checkBoxEsSobreturno = new System.Windows.Forms.CheckBox();
            this.dateTimePickerFechaDeEntrada = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFechaDeSalida = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerHoraDeEntrada = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerHoraDeSalida = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCliente = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(389, 223);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(93, 54);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(123, 223);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(93, 54);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // checkBoxSePresento
            // 
            this.checkBoxSePresento.AutoSize = true;
            this.checkBoxSePresento.Location = new System.Drawing.Point(27, 173);
            this.checkBoxSePresento.Name = "checkBoxSePresento";
            this.checkBoxSePresento.Size = new System.Drawing.Size(90, 17);
            this.checkBoxSePresento.TabIndex = 1;
            this.checkBoxSePresento.Text = "Se Presento?";
            this.checkBoxSePresento.UseVisualStyleBackColor = true;
            // 
            // checkBoxEsSobreturno
            // 
            this.checkBoxEsSobreturno.AutoSize = true;
            this.checkBoxEsSobreturno.Location = new System.Drawing.Point(27, 136);
            this.checkBoxEsSobreturno.Name = "checkBoxEsSobreturno";
            this.checkBoxEsSobreturno.Size = new System.Drawing.Size(99, 17);
            this.checkBoxEsSobreturno.TabIndex = 1;
            this.checkBoxEsSobreturno.Text = "Es Sobreturno?";
            this.checkBoxEsSobreturno.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerFechaDeEntrada
            // 
            this.dateTimePickerFechaDeEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFechaDeEntrada.Location = new System.Drawing.Point(170, 29);
            this.dateTimePickerFechaDeEntrada.Name = "dateTimePickerFechaDeEntrada";
            this.dateTimePickerFechaDeEntrada.Size = new System.Drawing.Size(90, 20);
            this.dateTimePickerFechaDeEntrada.TabIndex = 2;
            // 
            // dateTimePickerFechaDeSalida
            // 
            this.dateTimePickerFechaDeSalida.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFechaDeSalida.Location = new System.Drawing.Point(485, 29);
            this.dateTimePickerFechaDeSalida.Name = "dateTimePickerFechaDeSalida";
            this.dateTimePickerFechaDeSalida.Size = new System.Drawing.Size(90, 20);
            this.dateTimePickerFechaDeSalida.TabIndex = 2;
            this.dateTimePickerFechaDeSalida.Value = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Indique la fecha de entrada:";
            // 
            // dateTimePickerHoraDeEntrada
            // 
            this.dateTimePickerHoraDeEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerHoraDeEntrada.Location = new System.Drawing.Point(170, 90);
            this.dateTimePickerHoraDeEntrada.Name = "dateTimePickerHoraDeEntrada";
            this.dateTimePickerHoraDeEntrada.Size = new System.Drawing.Size(90, 20);
            this.dateTimePickerHoraDeEntrada.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Indique la hora de entrada:";
            // 
            // dateTimePickerHoraDeSalida
            // 
            this.dateTimePickerHoraDeSalida.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerHoraDeSalida.Location = new System.Drawing.Point(485, 89);
            this.dateTimePickerHoraDeSalida.Name = "dateTimePickerHoraDeSalida";
            this.dateTimePickerHoraDeSalida.Size = new System.Drawing.Size(90, 20);
            this.dateTimePickerHoraDeSalida.TabIndex = 2;
            this.dateTimePickerHoraDeSalida.Value = new System.DateTime(2023, 11, 13, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Indique la fecha de salida:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Indique la hora de salida:";
            // 
            // comboBoxCliente
            // 
            this.comboBoxCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCliente.FormattingEnabled = true;
            this.comboBoxCliente.Location = new System.Drawing.Point(310, 158);
            this.comboBoxCliente.Name = "comboBoxCliente";
            this.comboBoxCliente.Size = new System.Drawing.Size(194, 21);
            this.comboBoxCliente.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(257, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Clientes:";
            // 
            // frmReservasAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 289);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxCliente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerHoraDeEntrada);
            this.Controls.Add(this.dateTimePickerHoraDeSalida);
            this.Controls.Add(this.dateTimePickerFechaDeSalida);
            this.Controls.Add(this.dateTimePickerFechaDeEntrada);
            this.Controls.Add(this.checkBoxEsSobreturno);
            this.Controls.Add(this.checkBoxSePresento);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "frmReservasAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReservasAE";
            this.Load += new System.EventHandler(this.frmReservasAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerHoraDeEntrada;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaDeSalida;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaDeEntrada;
        private System.Windows.Forms.CheckBox checkBoxEsSobreturno;
        private System.Windows.Forms.CheckBox checkBoxSePresento;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerHoraDeSalida;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxCliente;
    }
}