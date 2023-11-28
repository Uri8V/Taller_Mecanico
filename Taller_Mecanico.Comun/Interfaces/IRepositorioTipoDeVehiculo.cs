using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioTipoDeVehiculo
    {
        void Agregar(TipoVehiculo tipo);
        void Borrar(int tipoVehiculoId);
        void Editar(TipoVehiculo tipo);
        bool Existe(TipoVehiculo tipo);
        int GetCantidad(string textoFiltro);
        List<TipoVehiculo> GetTipoVehiculos();
        TipoVehiculo GetTipoVehiculosPorId(int idVehiculo);
        bool EstaRelacionado(TipoVehiculo TIPO);
    }
}
