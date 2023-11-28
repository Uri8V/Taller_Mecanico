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

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS
{
    public partial class frmSeleccionarEmpleado : Form
    {
        public frmSeleccionarEmpleado()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private Empleado empleado;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                empleado = (Empleado)comboEmpleado.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }
        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboEmpleado.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboEmpleado, "Debe seleccionar un Empleado");
                valido = false;
            }
            return valido;
        }
        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleados frm = new frmEmpleados();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboEmpleados(ref comboEmpleado);
                return;
            }
        }

        internal Empleado GetEmpleado()
        {
            return empleado;
        }

        private void frmSeleccionarEmpleado_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboEmpleados(ref comboEmpleado);
        }
    }
}
