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
using Taller_Mecanico.Windows.FrmsMarcas;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmModelosAE : Form
    {
        public frmModelosAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboMarcas(ref comboMarca);
            if (modelos != null)
            {
                txtModelo.Text = modelos.Modelo;
                comboMarca.SelectedValue=modelos.IdMarca;
            }
        }
        Model modelos;
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if(modelos == null)
                {
                    modelos= new Model();
                }
                modelos.Modelo=txtModelo.Text;
                modelos.Marca = (Marca)comboMarca.SelectedItem;
                modelos.IdMarca = (int)comboMarca.SelectedValue;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if(string.IsNullOrEmpty(txtModelo.Text))
            {
                errorProvider1.SetError(txtModelo, "Debe ingresar un Modelo");
                valido = false;
            }
            if (comboMarca.SelectedIndex == 0)
            {
                errorProvider1.SetError(comboMarca, "Debe seleccionar una Marca");
                valido = false;
            }
            return valido;
        }

        internal Model GetModelo()
        {
           return modelos;
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            frmMarcas frm= new frmMarcas();
            DialogResult dr = frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboMarcas(ref comboMarca);
                return;
            }
        }

        internal void SetModelos(Model modelos)
        {
            this.modelos = modelos;
        }
    }
}
