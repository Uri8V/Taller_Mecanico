using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS
{
    public partial class frmTelefonosAE : Form
    {
        public frmTelefonosAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboClientes(ref comboClientes);
            ComboHelper.CargarComboEmpleados(ref comboEmpleados);
            if (telefonos != null)
            {
                    txtTelefono.Text = telefonos.Telefono;
                    txtTipoDeTelefono.Text = telefonos.TipoDeTelefono;
               
                if (telefonos.IdCliente!=0)
                {
                    comboClientes.SelectedValue = telefonos.IdCliente;
                    comboEmpleados.SelectedIndex = 0;
                    CheckBoxCliente.Checked = true;
                }
                else
                {
                    CheckBoxCliente.Checked=false;
                    comboClientes.SelectedIndex = 0;
                    comboEmpleados.SelectedValue = telefonos.IdEmpleado;
                }
            }
        }
        private Telefonos telefonos;
        internal Telefonos GetTelefono()
        {
           return telefonos;
        }

        internal void SetTelefono(Telefonos telefono)
        {
           this.telefonos = telefono;
        }

        private void frmTelefonosAE_Load(object sender, EventArgs e)
        {
            if (CheckBoxCliente.Checked)
            {
                comboClientes.Enabled = true;
                btnAgregarCliente.Enabled = true;
                comboEmpleados.Enabled = false;
                btnAgregarEmpleado.Enabled = false;
            }
          
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                valido= false;
                errorProvider1.SetError(txtTelefono, "Debe ingresar un Telefono");
            }
            if(string.IsNullOrEmpty(txtTipoDeTelefono.Text))
            {
                valido= false;
                errorProvider1.SetError(txtTipoDeTelefono, "Debe ingresar un Tipo De Telefono");
            }
            if (CheckBoxCliente.Checked == true)
            {
                if (comboClientes.SelectedIndex == 0)
                {
                    valido=false;
                    errorProvider1.SetError(comboClientes, "Debe seleccionar un Cliente");
                }
            }
            else
            {
                if (comboEmpleados.SelectedIndex == 0)
                {
                    valido = false;
                    errorProvider1.SetError(comboEmpleados, "Debe seleccionar un Empleado");
                }
            }
            return valido;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            frmClientes frm= new frmClientes();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboClientes(ref comboClientes);
                return;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (telefonos == null)
                {
                    telefonos = new Telefonos();
                }
                telefonos.Telefono=txtTelefono.Text;
                telefonos.TipoDeTelefono= txtTipoDeTelefono.Text;
                if (CheckBoxCliente.Checked == true)
                {   //No se puede convertir un objeto de tipo 'Taller_Mecanico.Entidades.Dtos.Clientes.ClienteComboDto' al tipo 'Taller_Mecanico.Entidades.Entidades.Clientes'.'
                    telefonos.Cliente = (Clientes)comboClientes.SelectedItem;
                    telefonos.IdCliente = (int)comboClientes.SelectedValue;
                }
                else
                {
                    //Lo mismo
                    telefonos.Empleado = (Empleado)comboEmpleados.SelectedItem;
                    telefonos.IdEmpleado = (int)comboEmpleados.SelectedValue;
                }
                DialogResult= DialogResult.OK;
            }
        }

        private void CheckBoxCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCliente.Checked)
            {
                comboClientes.Enabled = true;
                btnAgregarCliente.Enabled = true;
                comboEmpleados.Enabled = false;
                btnAgregarEmpleado.Enabled = false;
            }
            else
            {
                comboClientes.Enabled = false;
                btnAgregarCliente.Enabled = false;
                comboEmpleados.Enabled = true;
                btnAgregarEmpleado.Enabled = true;
            }
        }

        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleados frm = new frmEmpleados();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                //Metodo que racargue el combo de empleados
                ComboHelper.CargarComboEmpleados(ref comboEmpleados);
                return;
            }
        }
    }
}
