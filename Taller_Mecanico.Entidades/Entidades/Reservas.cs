using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Reservas
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public Clientes Cliente { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public bool SePresento { get; set; }
        public bool EsSobreturno { get; set; }
    }
}
