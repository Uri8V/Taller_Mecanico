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
using Taller_Mecanico.Windows.FrmsVehiculos.frmRESERVAS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmHISTORIALES
{
    public partial class frmHistorialesAE : Form
    {
        public frmHistorialesAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboEmpleados(ref comboEmpleados);
            ComboHelper.CargarComboVehiculos(ref comboVehiculos);
            ComboHelper.CargarComboReservas(ref comboReserva);
            if (historiales != null)
            {
                txtValorPorHora.Text = historiales.ValorPorHora.ToString();
                txtValorPorHoraExtra.Text = historiales.ValorPorHoraExtra.ToString();
                comboEmpleados.SelectedValue = historiales.IdEmpleado;
                comboReserva.SelectedValue=historiales.IdReserva;
                comboVehiculos.SelectedValue = historiales.IdVehiculo;
            }
        }
        private Historiales historiales;
        internal Historiales GetHistorial()
        {
           return historiales;
        }

        private void frmHistorialesAE_Load(object sender, EventArgs e)
        {

        }

        internal void SetHistorial(Historiales historiales)
        {
            this.historiales = historiales;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (historiales == null)
                {
                    historiales= new Historiales();
                }
                historiales.ValorPorHora = decimal.Parse(txtValorPorHora.Text);
                if (string.IsNullOrEmpty(txtValorPorHoraExtra.Text))
                {
                    txtValorPorHoraExtra.Text=0.ToString();
                }
                historiales.ValorPorHoraExtra=decimal.Parse(txtValorPorHoraExtra.Text);
                historiales.IdReserva=(int)comboReserva.SelectedValue;
                historiales.Reserva = (Reservas)comboReserva.SelectedItem;
                historiales.IdVehiculo=(int)comboVehiculos.SelectedValue;
                historiales.Vehiculo = (Vehiculos)comboVehiculos.SelectedItem;
                historiales.IdEmpleado=(int)comboEmpleados.SelectedValue;
                historiales.Empledado = (Empleado)comboEmpleados.SelectedItem;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if(!decimal.TryParse(txtValorPorHora.Text, out decimal ValorPorHora))
            {
                valido = false;
                errorProvider1.SetError(txtValorPorHora, "Debe ingresar un valor númerico");
            }
            else if (ValorPorHora <= 0)
            {
                valido=false;
                errorProvider1.SetError(txtValorPorHora, "Debe ingresar un valor Mayor a 0");
            }
            if (!decimal.TryParse(txtValorPorHora.Text, out decimal ValorPorHoraExtra))
            {
                valido = false;
                errorProvider1.SetError(txtValorPorHora, "Debe ingresar un valor númerico");
            }
            if(comboReserva.SelectedIndex == 0)
            {
                valido=false;
                errorProvider1.SetError(comboReserva, "Debe seleccionar un Cliente");
            }
            if (comboEmpleados.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboEmpleados, "Debe seleccionar un Empleado");
            }
            if(comboVehiculos.SelectedIndex== 0)
            {
                valido=false;
                errorProvider1.SetError(comboVehiculos, "Debe seleccionar un Vehiculo");
            }
            return valido;
        }

        private void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            frmVehiculos frm= new frmVehiculos();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboVehiculos(ref comboVehiculos);
                return;
            }
        }

        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            frmEmpleados frm= new frmEmpleados();
            DialogResult dr = frm.ShowDialog(this);
            if(dr== DialogResult.Cancel)
            {
                ComboHelper.CargarComboEmpleados(ref comboEmpleados);
                return;
            }
        }

        private void btnAgregarReserva_Click(object sender, EventArgs e)
        {
            frmReserva frm= new frmReserva();
            DialogResult dr = frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboReservas(ref comboReserva);
                return;
            }
        }
    }
}
