using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosVehiculos
    {
        void Guardar(Vehiculos vehiculos);
        void Borrar(int vehiculoId);
        bool Existe(Vehiculos vehiculos);
        bool EstaRelacionada(Vehiculos vehiculos);
        int GetCantidad(int? IdModelo, int? IdTipoVehiculo);
        List<VehiculoDto> GetVehiculosPorPagina(int registrosPorPagina, int paginaActual, int? Idmodelo, int? IdTipo);
        Vehiculos GetVehiculoPorId(int vehiculoId);
        List<Vehiculos> GetVehiculoCombos();
    }
}
