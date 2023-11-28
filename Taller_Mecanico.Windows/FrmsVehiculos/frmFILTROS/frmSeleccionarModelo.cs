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
    public partial class frmSeleccionarModelo : Form
    {
        public frmSeleccionarModelo()
        {
            InitializeComponent();
        }

        private void frmSeleccionarModelo_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboModelo(ref comboBoxModelo);
        }
        private Model modelo;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                modelo = (Model)comboBoxModelo.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboBoxModelo.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboBoxModelo, "Debe seleccionar un Modelo");
                valido = false;
            }
            return valido;
        }

        private void btnAgregarModelo_Click(object sender, EventArgs e)
        {
            frmModelos frm = new frmModelos();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboModelo(ref comboBoxModelo);
                return;
            }
        }

        internal Model GetModelo()
        {
            return modelo;
        }
    }
}
