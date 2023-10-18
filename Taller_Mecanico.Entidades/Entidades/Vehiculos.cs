using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Vehiculos
    {
        public int IdVehiculo { get; set; }
        public string Patente { get; set; }
        public string Kilometros { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdModelo { get; set; }
        public TipoVehiculo NombreTipoVehiculo { get; set; }
        public Modelos NombreModelo { get; set; }
    }
}
