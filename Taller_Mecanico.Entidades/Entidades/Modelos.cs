using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class Modelos
    {
        public int IdModelo { get; set; }
        public string Modelo { get; set; }
        public int IdMarca { get; set; }
        public Marca Marca { get; set; }
    }
}
