using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Entidades.Dtos.Movimientos
{
    public class MovimientosDto:ICloneable
    {
        public int IdMovimiento { get; set; }
        public string Servicio { get; set; }
        public decimal Debe { get; set; }
        public string NombreDePago { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
