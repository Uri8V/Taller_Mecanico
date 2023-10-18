using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Roles
    {
        public int IdRolEmpleado { get; set; }
        public string Rol { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
