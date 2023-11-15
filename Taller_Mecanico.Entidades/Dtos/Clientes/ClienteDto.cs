using System;

namespace Taller_Mecanico.Entidades.Dtos.Clientes
{
    public class ClienteDto : ICloneable
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Domicilio { get; set; }
        public string CUIT { get; set; }
        public string TipoCliente { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
