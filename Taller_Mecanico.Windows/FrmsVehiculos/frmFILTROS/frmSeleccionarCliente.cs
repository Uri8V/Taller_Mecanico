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
    public partial class frmSeleccionarCliente : Form
    {
        public frmSeleccionarCliente()
        {
            InitializeComponent();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private Clientes clientes;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                if (!checkBoxEmpresa.Checked)
                {       
                    clientes = (Clientes)comboCliente.SelectedItem;
                } 
                else 
                { 
                    clientes=(Clientes)comboEmpresa.SelectedItem;
                }
                DialogResult = DialogResult.OK;
            }
        }
        private bool ValidaDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (checkBoxEmpresa.Checked)
            {
                if (comboEmpresa.SelectedIndex == 0)
                {
                    errorProvider1.SetError(comboEmpresa, "Debe seleccionar una Empresa");
                    valido = false;
                }
            }
            else
            {
                if (comboCliente.SelectedIndex == 0)
                {
                    errorProvider1.SetError(comboCliente, "Debe seleccionar un Cliente");
                    valido = false;
                }
            }

            return valido;
        }
        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboClientesPersonas(ref comboCliente);
                ComboHelper.CargarComboClientesEmpresas(ref comboEmpresa);
                return;
            }
        }

        internal Clientes GetCliente()
        {
            return clientes;
        }

        private void frmSeleccionarCliente_Load(object sender, EventArgs e)
        {
            if (checkBoxEmpresa.Checked)
            {
                comboEmpresa.Enabled = true;
                comboCliente.Enabled = false;
            }
            else
            {
                comboEmpresa.Enabled = false;
                comboCliente.Enabled = true;
            }
            ComboHelper.CargarComboClientesPersonas(ref comboCliente);
            ComboHelper.CargarComboClientesEmpresas(ref comboEmpresa);
        }

        private void checkBoxEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEmpresa.Checked)
            {
                comboEmpresa.Enabled = true;
                comboCliente.Enabled = false;
            }
            else
            {
                comboEmpresa.Enabled = false;
                comboCliente.Enabled = true;
            }
        }
    }
}
