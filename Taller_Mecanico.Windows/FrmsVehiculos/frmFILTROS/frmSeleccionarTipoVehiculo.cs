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

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOS
{
    public partial class frmSeleccionarTipoVehiculo : Form
    {
        public frmSeleccionarTipoVehiculo()
        {
            InitializeComponent();
        }

        private void frmSeleccionarTipoVehiculo_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboTipoVehiculo(ref comboBoxTipoDeVehiculo);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private TipoVehiculo tipo;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                tipo = (TipoVehiculo)comboBoxTipoDeVehiculo.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }
        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboBoxTipoDeVehiculo.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboBoxTipoDeVehiculo, "Debe seleccionar un Tipo de Vehiculo");
                valido = false;
            }
            return valido;
        }

        private void btnAgregarTipoDeVehiculo_Click(object sender, EventArgs e)
        {
            frmTipoVehiculo frm = new frmTipoVehiculo();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboEmpleados(ref comboBoxTipoDeVehiculo);
                return;
            }
        }

        internal TipoVehiculo GetTipoVehiculo()
        {
            return tipo;
        }
    }
}
