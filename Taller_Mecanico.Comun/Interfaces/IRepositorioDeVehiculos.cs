using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;
using Vehiculos = Taller_Mecanico.Entidades.Entidades.Vehiculos;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeVehiculos
    {
        void Agregar(Vehiculos vehiculos);
        void Borrar(int vehiculoId);
        void Editar(Vehiculos vehiculos);
        bool Existe(Vehiculos vehiculos);
        bool EstaRelacionada(Vehiculos vehiculos);
        int GetCantidad(int? vehiculoId);
        List<VehiculoDto> GetVehiculosPorPagina(int registrosPorPagina, int paginaActual, int? tipo, int? modelId);
        Vehiculos GetVehiculoPorId(int vehiculoId);
        List<Vehiculos> GetVehiculoCombos();
    }
}
