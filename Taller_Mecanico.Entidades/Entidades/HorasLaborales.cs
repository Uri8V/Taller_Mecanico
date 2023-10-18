using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class HorasLaborales
    {
        public int IdHorasLaborales { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int horaslaborales { get; set; }
        public int horasExtras { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
