using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Entidades.Dtos.Sueldos
{
    public class SueldosDto:ICloneable
    {
        public int IdSueldo { get; set; }
        public DateTime Fecha { get; set; }
        public int HorasLaborales { get; set; }
        public decimal ValorPorHora { get; set; }
        public decimal ValorPorHoraExtra { get; set; }
        public int HorasExtras { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public decimal TotalAPagar { get; set; }
        public decimal TotalExtra { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
