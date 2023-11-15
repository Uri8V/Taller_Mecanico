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

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOSSERVICIOS
{
    public partial class frmVehiculosServiciosAE : Form
    {
        public frmVehiculosServiciosAE()
        {
            InitializeComponent();
        }
        private VehiculosServicios servicios;
        internal VehiculosServicios GetServicio()
        {
            return servicios;
        }

        internal void SetServicio(VehiculosServicios servicios)
        {
           this.servicios = servicios;  
        }
    }
}
