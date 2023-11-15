using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Entidades.Dtos.Empleados
{
    public class EmpleadoComboDto
    {
        public int IdEmpleado { get; set; }
        public string Documento { get; set; }

        public static explicit operator Empleado(EmpleadoComboDto v)
        {
            return new Empleado(v.IdEmpleado, v.Documento);
        }
    }
}
