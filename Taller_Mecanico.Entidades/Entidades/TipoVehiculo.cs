﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Entidades.Entidades
{
    public class TipoVehiculo
    {
        public int IdTipoVehiculo { get; set; }
        public string NombreTipoVehiculo { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
