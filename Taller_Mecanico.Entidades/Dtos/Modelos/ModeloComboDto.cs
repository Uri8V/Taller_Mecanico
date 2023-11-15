using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Entidades.Dtos.Modelos
{
    public class ModeloComboDto
    {
        public int IdModelo { get; set; }
        public string Modelo { get; set; }

        public static explicit operator Model (ModeloComboDto model)
        {
            return new Model( model.Modelo);
        }
    }
}
    

    

