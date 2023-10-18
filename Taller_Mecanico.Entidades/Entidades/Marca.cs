using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Marca
    {
        public int MarcaId { get; set; }
        public string NombreMarca { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
