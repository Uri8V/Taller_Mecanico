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
    }
}
