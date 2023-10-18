using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class TipoDePagos
    {
        public int IdTipoPagos { get; set; }
        public string TipoPago{ get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
