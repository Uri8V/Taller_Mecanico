using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Windows.FrmsVehiculos;
using Taller_Mecanico.Windows.FrmsVehiculos.frmHISTORIALES;
using Taller_Mecanico.Windows.FrmsVehiculos.frmMOVIMIENTOS;
using Taller_Mecanico.Windows.FrmsVehiculos.frmRESERVAS;
using Taller_Mecanico.Windows.FrmsVehiculos.frmSUELDOS;
using Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOSSERVICIOS;

namespace Taller_Mecanico.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            frmMarcas frm= new frmMarcas();
            frm.ShowDialog(this);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTipoVehiculos_Click(object sender, EventArgs e)
        {
            frmTipoVehiculo frm= new frmTipoVehiculo();
            frm.ShowDialog(this);
        }

        private void btnTipoDePago_Click(object sender, EventArgs e)
        {
            frmTipoDePago frm= new frmTipoDePago();
            frm.ShowDialog(this);
        }

        private void btnTiposClientes_Click(object sender, EventArgs e)
        {
            frmTiposDeClientes frm= new frmTiposDeClientes();
            frm.ShowDialog(this);   
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles frm= new frmRoles();
            frm.ShowDialog(this);
        }

        private void btnHorariosLaborales_Click(object sender, EventArgs e)
        {
            frmHorariosLaborales frm= new frmHorariosLaborales();
            frm.ShowDialog(this);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes frm= new frmClientes();
            frm.ShowDialog(this);
        }

        private void btnModelos_Click(object sender, EventArgs e)
        {
            frmModelos frm= new frmModelos();
            frm.ShowDialog(this);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmEmpleados frm= new frmEmpleados();
            frm.ShowDialog(this);
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            frmVehiculos frm= new frmVehiculos();
            frm.ShowDialog(this);
        }

        private void btnTelefonos_Click(object sender, EventArgs e)
        {
            frmTelefonos frm= new frmTelefonos();
            frm.ShowDialog(this);
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            frmReserva frm= new frmReserva();
            frm.ShowDialog(this);
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            frmHistoriales frm= new frmHistoriales();
            frm.ShowDialog(this);
        }

        private void btnSueldos_Click(object sender, EventArgs e)
        {
            frmSueldos frm= new frmSueldos();
            frm.ShowDialog(this);
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            frmMovimientos frm= new frmMovimientos();
            frm.ShowDialog(this);
        }

        private void btnVehiculosServicios_Click(object sender, EventArgs e)
        {
            frmVehiculosServicios frm= new frmVehiculosServicios();
            frm.ShowDialog(this);
        }
    }
}
