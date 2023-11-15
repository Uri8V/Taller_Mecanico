using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosVehiculosServicios:IServiciosVehiculosServicios
    {
        private readonly IRepositorioDeVehiculosServicios _repo;
        public ServiciosVehiculosServicios()
        {
            _repo = new RepositorioDeVehiculosServicios();
        }

        public void Borrar(int IdVehiculoServicio)
        {
            try
            {
                _repo.Borrar(IdVehiculoServicio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(VehiculosServicios vehiculosServicios)
        {
            throw new NotImplementedException();
        }

        public bool Existe(VehiculosServicios vehiculosServicios)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdVehiculo)
        {
            try
            {
                return _repo.GetCantidad(IdVehiculo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VehiculosServicios> GetVehiculoServicioCombos()
        {
            throw new NotImplementedException();
        }

        public VehiculosServicios GetVehiculoServicioPorId(int IdVehiculoServicio)
        {
            try
            {
                return _repo.GetVehiculoServicioPorId(IdVehiculoServicio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VehiculosServiciosDto> GetVehiculoServicioPorPagina(int registrosPorPagina, int paginaActual, int? IdVehiculo)
        {
            try
            {
                return _repo.GetVehiculoServicioPorPagina(registrosPorPagina, paginaActual, IdVehiculo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(VehiculosServicios vehiculosServicios)
        {
            try
            {
                if (vehiculosServicios.IdVehiculoServicios==0)
                {
                    _repo.Agregar(vehiculosServicios);
                }
                else
                {
                    _repo.Editar(vehiculosServicios);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
