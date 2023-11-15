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
    public partial class frmRolesAE : Form
    {
        public frmRolesAE()
        {
            InitializeComponent();
        }
        private Roles rol;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (rol != null)
            {
                txtRol.Text = rol.Rol;
            }
        }
        internal Roles GetRol()
        {
            return rol;
        }

        internal void SetRol(Roles rol)
        {
            this.rol = rol;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (rol == null)
                {
                    rol= new Roles();
                }
                rol.Rol=txtRol.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool validar = true;
            if(string.IsNullOrEmpty(txtRol.Text))
            {
                errorProvider1.SetError(txtRol, "Debe ingresar un Rol");
                validar = false;
            }
            return validar;
        }
    }
}
