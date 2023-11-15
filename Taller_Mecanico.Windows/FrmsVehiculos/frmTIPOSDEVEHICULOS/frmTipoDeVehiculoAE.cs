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

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmTipoDeVehiculoAE : Form
    {
        public frmTipoDeVehiculoAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(tipo!=null)
            {
                txtTipoVehiculo.Text = tipo.NombreTipoVehiculo;
            }
        }
        private TipoVehiculo tipo;
        internal TipoVehiculo GetTipoVehiculo()
        {
            return tipo;
        }

        internal void SetTipoVehiculo(TipoVehiculo tipo)
        {
            this.tipo = tipo;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (tipo == null)
                {
                    tipo= new TipoVehiculo();
                }
                tipo.NombreTipoVehiculo = txtTipoVehiculo.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtTipoVehiculo.Text))
            {
                errorProvider1.SetError(txtTipoVehiculo, "Debe ingresar un tipo de vehiculo");
                valido = false;
            }
            return valido;
        }
    }
}
