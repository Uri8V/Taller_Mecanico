using System;

namespace Taller_Mecanico.Entidades.Dtos.Modelos
{
    public class ModelosDto : ICloneable
    {
        public int IdModelo { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
