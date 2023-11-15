using System;

namespace Taller_Mecanico.Entidades.Dtos.Empleados
{
    public class EmpleadoDto:ICloneable
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Rol { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
