

using System;
namespace Taller_Mecanico.Entidades.Dtos.Vehiculos
{
    public class VehiculoDto:ICloneable
    {
        public int IdVehiculo { get; set; }
        public string Patente { get; set; }
        public string Kilometros { get; set; }
        public string TipoVehiculo { get; set; }
        public string Modelo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
