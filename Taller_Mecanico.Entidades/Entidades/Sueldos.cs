using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Sueldos
    {
        public int IdSueldo { get; set; }
        public int IdHistorial { get; set; }
        public Historiales Historial { get; set; }
        public int IdHorasLaborales { get; set; }
        public HorasLaborales HoraLaboral { get; set; }
        public decimal TotalAPagar { get; set; }
        public decimal TotalExtra { get; set; }
    }
}
