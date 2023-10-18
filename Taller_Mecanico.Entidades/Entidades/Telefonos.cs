using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Telefonos
    {
        public int IdTelefono { get; set; }
        public int IdCliente { get; set; }
        public Clientes Cliente { get; set; }
        public int IdEmpleado { get; set; }
        public Empleado Empleado { get; set; }
        public string Telefono { get; set; }
        public string TipoDeTelefono { get; set; }
    }
}
