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
    public partial class frmSeleccionarTipoDePago : Form
    {
        public frmSeleccionarTipoDePago()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private TipoDePagos tipo;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                tipo = (TipoDePagos)comboBoxTipoPago.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboBoxTipoPago.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboBoxTipoPago, "Debe seleccionar un Tipo de Pago");
                valido = false;
            }
            return valido;
        }

        private void frmSeleccionarTipoDePago_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboTipoDePago(ref comboBoxTipoPago);
        }

        private void btnAgregarTipoPago_Click(object sender, EventArgs e)
        {
            frmTipoDePago frm = new frmTipoDePago();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboTipoCliente(ref comboBoxTipoPago);
                return;
            }
        }

        internal TipoDePagos GetTipoPago()
        {
            return tipo;
        }
    }
}
