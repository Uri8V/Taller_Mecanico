using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmMOVIMIENTOS
{
    public partial class frmMovimientosAE : Form
    {
        public frmMovimientosAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboTipoDePago(ref comboBox1);
            if (movimientos != null)
            {
                txtServicio.Text = movimientos.Servicio;
                txtDebe.Text=movimientos.Debe.ToString();   
                txtSenia.Text=movimientos.Senia.ToString();
                comboBox1.SelectedValue = movimientos.IdTipoDePago;
            }
        }
        private Movimientos movimientos;
        internal Movimientos GetMovimiento()
        {
           return movimientos;
        }

        internal void SetMovimiento(Movimientos movimientos)
        {
            this.movimientos = movimientos;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if(movimientos==null)
                {
                    movimientos= new Movimientos();
                }
               movimientos.Servicio= txtServicio.Text;
                movimientos.Debe=decimal.Parse(txtDebe.Text);
                if(string.IsNullOrEmpty(txtSenia.Text))
                {
                    movimientos.Senia = 0;
                }
                else
                {
                    movimientos.Senia=decimal.Parse(txtSenia.Text);
                }
                movimientos.IdTipoDePago = (int)comboBox1.SelectedValue;
                movimientos.TipoDePago = (TipoDePagos)comboBox1.SelectedItem;
                DialogResult=DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtServicio.Text))
            {
                valido = false;
                errorProvider1.SetError(txtServicio, "Debe ingresar un Servicio");
            }
            if (!decimal.TryParse(txtDebe.Text, out decimal Debe))
            {
                valido = false;
                errorProvider1.SetError(txtDebe, "Se necesita ingresar el total a deber ");
            }
            else if (Debe <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtDebe, "El monto debe ser positivo");
            }
            if (comboBox1.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboBox1, "Debe seleccionar una forma de Pago");
            }
            return valido;
        }

        internal object GetServicio()
        {
            throw new NotImplementedException();
        }
    }
}
