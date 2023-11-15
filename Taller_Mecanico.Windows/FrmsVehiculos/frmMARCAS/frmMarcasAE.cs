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

namespace Taller_Mecanico.Windows.FrmsMarcas
{
    public partial class frmMarcasAE : Form
    {
        public frmMarcasAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (marca != null)
            {
                txtMarca.Text = marca.NombreMarca;
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        Marca marca;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (marca==null)
                {
                    marca = new Marca();
                }
                marca.NombreMarca=txtMarca.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(txtMarca.Text))
            {
                errorProvider1.SetError(txtMarca, "Debe ingresar una Marca");
                valido= false;
            }
            return valido;
        }

        public Marca GetMarca()
        {
           return marca;
        }

        internal void SetMarca(Marca marca)
        {
           this.marca = marca;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
