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

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmFILTROS
{
    public partial class frmSeleccionarVehiculo : Form
    {
        public frmSeleccionarVehiculo()
        {
            InitializeComponent();
        }

        private void frmSeleccionarVehiculo_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboVehiculos(ref comboVehiculo);
        }
        private Vehiculos vehiculos;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                vehiculos = (Vehiculos)comboVehiculo.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboVehiculo.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboVehiculo, "Debe seleccionar un Vehiculo");
                valido = false;
            }
            return valido;
        }

        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            frmVehiculos frm = new frmVehiculos();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboVehiculos(ref comboVehiculo);
                return;
            }
        }

        internal Vehiculos GetVehiculos()
        {
            return vehiculos;
        }
    }
}
