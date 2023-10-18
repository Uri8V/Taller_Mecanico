using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Dtos.Telefono
{
    public class TelefonoDto
    {
        public int IdTelefono { get; set; }
        public string Cliente { get; set; }
        public string Empleado { get; set; }
        public string Telefono { get; set; }
        public string TipoDeTelefono { get; set; }
    }
}
