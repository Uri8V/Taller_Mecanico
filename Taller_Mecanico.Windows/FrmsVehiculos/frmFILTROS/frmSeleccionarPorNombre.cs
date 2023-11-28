using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS
{
    public partial class frmSeleccionarPorNombre : Form
    {
        public frmSeleccionarPorNombre()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private string textoFiltro;
        public string GetTexto()
        {
            return textoFiltro;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            textoFiltro = txtNombre.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
