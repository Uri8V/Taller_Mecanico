using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Movimientos
    {
        public int IdMovimiento { get; set; }
        public string Servicio  { get; set; }
        public decimal Debe { get; set; }
        public decimal Senia { get; set; }
        public int IdTipoDePago { get; set; }
        public TipoDePagos TipoDePago { get; set; }
    }
}
