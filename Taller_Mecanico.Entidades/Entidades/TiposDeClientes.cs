using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class TiposDeClientes
    {
        public int IdTipoCliente { get; set; }
        public string TipoCliente { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
