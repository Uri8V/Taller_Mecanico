using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmMOVIMIENTOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOSSERVICIOS
{
    public partial class frmVehiculosServiciosAE : Form
    {
        public frmVehiculosServiciosAE()
        {
            InitializeComponent();
            _servicio = new ServiciosMovimientos();
        }
        private IServiciosMovimientos _servicio;
        private VehiculosServicios servicios;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!checkBoxEmpresa.Checked)
            {
                checkBoxEmpresa.Checked = false;
                comboEmpresa.Enabled = false;
                comboCliente.Enabled = true;
            }
            else
            {
                checkBoxEmpresa.Checked = true;
                comboEmpresa.Enabled = true;
                comboCliente.Enabled = false;
            }
            if (servicios != null)
            {
                if (checkBoxEmpresa.Checked)
                {
                    comboEmpresa.SelectedValue = servicios.IdCliente;
                }
                else
                {
                    comboCliente.SelectedValue = servicios.IdCliente;
                }
                comboMovimiento.SelectedValue = servicios.IdMovimiento;
                txtDebe.Text = _servicio.GetMovimientosPorId(servicios.IdMovimiento).Debe.ToString();
                comboVehiculo.SelectedValue=servicios.IdVehiculo;
                txtDescripcion.Text = servicios.Descripcion;
                txtHaber.Text=servicios.Haber.ToString();
                dateTimePickerFecha.Value = servicios.Fecha.Date;
            }
        }
        internal VehiculosServicios GetServicio()
        {
            return servicios;
        }

        internal void SetServicio(VehiculosServicios servicios)
        {
           this.servicios = servicios;  
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmVehiculosServiciosAE_Load(object sender, EventArgs e)
        {
            ComboHelper.CargarComboClientesEmpresas(ref comboEmpresa);
            ComboHelper.CargarComboClientesPersonas(ref comboCliente);
            ComboHelper.CargarComboVehiculos(ref comboVehiculo);
            ComboHelper.CargarComboMovimiento(ref comboMovimiento);

        }

        private void checkBoxEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxEmpresa.Checked)
            {
                comboCliente.Enabled = false;
                comboEmpresa.Enabled=true;
            }
            else
            {
                comboCliente.Enabled = true;
                comboEmpresa.Enabled = false;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (servicios == null)
                {
                    servicios = new VehiculosServicios();
                }

                if (checkBoxEmpresa.Checked)
                {
                    servicios.Cliente=(Clientes)comboEmpresa.SelectedItem;
                    servicios.IdCliente = (int)comboEmpresa.SelectedValue;
                }
                else
                {
                    servicios.Cliente=(Clientes)comboCliente.SelectedItem;
                    servicios.IdCliente=(int)comboCliente.SelectedValue;
                }
                servicios.Movimiento=(Movimientos)comboMovimiento.SelectedItem;
                servicios.IdMovimiento = (int)comboMovimiento.SelectedValue;

                servicios.Vehiculo = (Vehiculos)comboVehiculo.SelectedItem;
                servicios.IdVehiculo = (int)comboVehiculo.SelectedValue;

                servicios.Debe= Decimal.Parse(txtDebe.Text);
                servicios.Haber=Decimal.Parse(txtHaber.Text);
                servicios.Descripcion=txtDescripcion.Text;

                servicios.Fecha=dateTimePickerFecha.Value;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (!checkBoxEmpresa.Checked)
            {
                if (comboCliente.SelectedIndex == 0)
                {
                    valido = false;
                    errorProvider1.SetError(comboCliente, "Debe seleccionar un Cliente");
                } 
            }
            else
            {
                if(comboEmpresa.SelectedIndex == 0)
                {
                    valido = false;
                    errorProvider1.SetError(comboEmpresa, "Debe seleccionar una Empresa");
                }
            }
            if(comboMovimiento.SelectedIndex == 0)
            {
                valido=false;
                errorProvider1.SetError(comboMovimiento, "Debe seleccionar un Movimiento");
            }
            if(comboVehiculo.SelectedIndex == 0)
            {
                valido=false;
                errorProvider1.SetError(comboVehiculo, "Debe selccionar un Vehiculo");
            }
            //if(!int.TryParse(txtDebe.Text, out int Debe))
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtDebe, "Debe poner cuanto debe");
            //}
            //else if (Debe < 0)
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtDebe, "Lo que debe ser positivo");
            //}
            if (!int.TryParse(txtHaber.Text, out int Haber))
            {
                valido = false;
                errorProvider1.SetError(txtHaber, "Debe poner el Haber");
            }
            else if (Haber < 0)
            {
                valido = false;
                errorProvider1.SetError(txtHaber, "El haber debe ser positivo");
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                valido = false;
                errorProvider1.SetError(txtDescripcion, "Debe ingresar una Descripción");
            }
            if (dateTimePickerFecha.Value.Date < new DateTime(2023,1,1))
            {
                valido = false;
                errorProvider1.SetError(dateTimePickerFecha, "Debe ingresar una fecha mayor a 2023/01/01");
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

        private void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            frmVehiculos frm = new frmVehiculos();
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.Cancel)
            {
                ComboHelper.CargarComboVehiculos(ref comboVehiculo);
                return;
            }
        }

        private void btnAgregarMovimiento_Click(object sender, EventArgs e)
        {
            frmMovimientos frm = new frmMovimientos();
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.Cancel)
            {
                ComboHelper.CargarComboMovimiento(ref comboMovimiento);
                return;
            }
        }

        private void comboMovimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMovimiento.SelectedIndex!=0)
            {
            txtDebe.Text = (_servicio.GetMovimientosPorId((int)comboMovimiento.SelectedValue).Debe).ToString();
            }
        }
    }
}
