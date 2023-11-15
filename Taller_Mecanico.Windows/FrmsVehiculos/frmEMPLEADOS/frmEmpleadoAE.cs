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

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmEmpleadoAE : Form
    {
        public frmEmpleadoAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboRol(ref comboRol);
            if (empleado != null)
            {
                txtNombre.Text = empleado.Nombre;
                txtApellido.Text = empleado.Apellido;
                txtDocumento.Text = empleado.Documento;
                comboRol.SelectedValue = empleado.IdRolEmpleado;
                esEdicion = true;
            }
        }
        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            frmRoles frm= new frmRoles();
            DialogResult dr=frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboRol(ref comboRol);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool esEdicion = false;
        private Empleado empleado;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (empleado == null)
                {
                    empleado = new Empleado();
                }
                empleado.Nombre = txtNombre.Text;
                empleado.Apellido = txtApellido.Text;
                empleado.Documento = txtDocumento.Text;
                empleado.Rol = (Roles)comboRol.SelectedItem;
                empleado.IdRolEmpleado = (int)comboRol.SelectedValue;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool validar= true;
            if (esEdicion == false)
            {
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    errorProvider1.SetError(txtNombre, "DEBE Ingresar un nombre");
                    validar = false;
                }
                if (string.IsNullOrEmpty(txtApellido.Text))
                {
                    errorProvider1.SetError(txtApellido, "DEBE Ingresar un Apellido");
                    validar = false;
                }
                if ((string.IsNullOrEmpty(txtDocumento.Text)))
                {
                    errorProvider1.SetError(txtDocumento, "DEBE Ingresar un Documento");
                    validar = false;
                }
                if(comboRol.SelectedIndex == 0)
                {
                    errorProvider1.SetError(comboRol, "Debe seleccionar un Rol");
                    validar = false;
                }
            }
            return validar;
        }

        internal Empleado GetEmpleado()
        {
            return empleado;
        }

        internal void SetEmpleado(Empleado empleados)
        {
            this.empleado= empleados;
        }
    }
}
