using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Historiales
    {
        public int IdHistorial { get; set; }
        public int IdEmpleado { get; set; }
        public Empleado Empledado { get; set; }
        public int IdReserva { get; set; }
        public Reservas Reserva { get; set; }
        public int IdVehiculo { get; set; }
        public Vehiculos Vehiculo { get; set; }
        public decimal ValorPorHora { get; set; }
        public decimal ValorPorHoraExtra { get; set; }

    }
}
