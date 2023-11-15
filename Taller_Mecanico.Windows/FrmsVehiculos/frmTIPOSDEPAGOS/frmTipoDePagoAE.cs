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
    public partial class frmTipoDePagoAE : Form
    {
        public frmTipoDePagoAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(tipo!=null)
            {
                txtTipoDePago.Text = tipo.NombreDePago;
            }
        }
        private TipoDePagos tipo;
        internal TipoDePagos GetTipoDePago()
        {
            return tipo;
        }

        internal void SetMarca(TipoDePagos tipo)
        {
            this.tipo = tipo;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (Validardatos())
            {
                if(tipo == null)
                {
                    tipo= new TipoDePagos();
                }
                tipo.NombreDePago=txtTipoDePago.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private bool Validardatos()
        {
            errorProvider1.Clear();
            bool validar = true;
            if (string.IsNullOrEmpty(txtTipoDePago.Text))
            {
                errorProvider1.SetError(txtTipoDePago, "Debe ingresar un Tipo de Pago");
                validar = false;
            }
            return validar;
        }
    }
}
