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
using Taller_Mecanico.Windows.FrmsVehiculos.frmMOVIMIENTOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmFILTROS
{
    public partial class frmSeleccionarServicios : Form
    {
        public frmSeleccionarServicios()
        {
            InitializeComponent();
        }
        private Movimientos movimientos;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                movimientos = (Movimientos)comboServicios.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboServicios.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboServicios, "Debe seleccionar un Servicio");
                valido = false;
            }
            return valido;
        }

        private void frmSeleccionarServicios_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboMovimiento(ref comboServicios);
        }

        private void btnAgregarServicios_Click(object sender, EventArgs e)
        {
            frmMovimientos frm = new frmMovimientos();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboMovimiento(ref comboServicios);
                return;
            }
        }

        internal Movimientos GetMovimiento()
        {
            return movimientos;
        }
    }
}
