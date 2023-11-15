using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosVehiculosServicios
    {
        void Guardar(VehiculosServicios vehiculosServicios);
        void Borrar(int IdVehiculoServicio);
        bool Existe(VehiculosServicios vehiculosServicios);
        bool EstaRelacionada(VehiculosServicios vehiculosServicios);
        int GetCantidad(int? IdVehiculo);
        List<VehiculosServiciosDto> GetVehiculoServicioPorPagina(int registrosPorPagina, int paginaActual, int? IdVehiculo);
        VehiculosServicios GetVehiculoServicioPorId(int IdVehiculoServicio);
        List<VehiculosServicios> GetVehiculoServicioCombos();
    }
}
