using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Dtos.Reservas
{
    public class ReservaDto:ICloneable
    {
        public int IdReserva { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public bool SePresento { get; set; }
        public bool EsSobreturno { get; set; }
        public string CUIT { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
