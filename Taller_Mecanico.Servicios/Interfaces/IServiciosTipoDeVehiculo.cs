using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosTipoDeVehiculo
    {
        void Guardar(TipoVehiculo tipo);
        void Borrar(int tipoVehiculoId);
        bool Existe(TipoVehiculo tipo);
        int GetCantidad(string textoFiltro);
        List<TipoVehiculo> GetTipoVehiculos();
        TipoVehiculo GetTipoVehiculosPorId(int idVehiculo);
    }
}
