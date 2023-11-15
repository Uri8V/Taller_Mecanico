using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeVehiculosServicios
    {
        void Agregar(VehiculosServicios vehiculosServicios);
        void Borrar(int IdVehiculoServicio);
        void Editar(VehiculosServicios vehiculosServicios);
        bool Existe(VehiculosServicios vehiculosServicios);
        bool EstaRelacionada(VehiculosServicios vehiculosServicios);
        int GetCantidad(int? IdVehiculoServicio);
        List<VehiculosServiciosDto> GetVehiculoServicioPorPagina(int registrosPorPagina, int paginaActual, int? IdTipoDePago);
        VehiculosServicios GetVehiculoServicioPorId(int IdVehiculoServicio);
        List<VehiculosServicios> GetVehiculoServicioCombos();
    }
}
