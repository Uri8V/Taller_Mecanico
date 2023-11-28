using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Dtos.Telefono
{
    public class TelefonoDto:ICloneable
    {
        public int IdTelefono { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public string DocumentoEmpleado { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public string Telefono { get; set; }
        public string TipoDeTelefono { get; set; }
        public string CUIT { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
