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

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmEMPLEADOS
{
    public partial class frmSeleccionarRol : Form
    {
        public frmSeleccionarRol()
        {
            InitializeComponent();
        }

        private void comboBoxTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private Roles rol;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                rol = (Roles)comboBoxRol.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboBoxRol.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboBoxRol, "Debe seleccionar un Rol");
                valido = false;
            }
            return valido;
        }

        private void frmSeleccionarRol_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboRol(ref comboBoxRol);
        }

        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            frmRoles frm= new frmRoles();
            DialogResult dr = frm.ShowDialog(this);
            if(dr == DialogResult.Cancel) 
            {
                ComboHelper.CargarComboRol(ref comboBoxRol);
                return;
            }
        }

        internal Roles GetRol()
        {
            return rol;
        }

      
    }
}
