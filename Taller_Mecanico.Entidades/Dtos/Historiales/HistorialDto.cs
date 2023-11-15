using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Entidades.Dtos.Historiales
{
    public class HistorialDto:ICloneable
    {
        public int IdHistorial { get; set; }
        public string Documento { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Patente { get; set; }
        public string DocumentoCliente { get; set;}
        public string ApellidoCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaEntrada { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public decimal ValorPorHora { get; set; }
        public decimal ValorPorHoraExtra { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
