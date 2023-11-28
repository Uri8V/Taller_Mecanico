using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosVehiculos:IServiciosVehiculos
    {
        private readonly IRepositorioTipoDeVehiculo _repoTipoVehiculo;
        private readonly IRepositorioModelos _repoModelos;
        private readonly IRepositorioDeVehiculos _repo;

        public ServiciosVehiculos()
        {
            _repoTipoVehiculo = new RepositorioTipoDeVehiculo();
            _repoModelos= new RepositorioModelos();
            _repo = new RepositorioDeVehiculos();
        }

        public void Borrar(int vehiculoId)
        {
            try
            {
                _repo.Borrar(vehiculoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Vehiculos vehiculos)
        {
            try
            {
                return _repo.EstaRelacionada(vehiculos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Vehiculos vehiculos)
        {
            try
            {
                return _repo.Existe(vehiculos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(int? IdModelo, int? IdTipoVehiculo)
        {
            try
            {
               return _repo.GetCantidad(IdModelo, IdTipoVehiculo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Vehiculos> GetVehiculoCombos()
        {
            try
            {
                return _repo.GetVehiculoCombos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Entidades.Entidades.Vehiculos GetVehiculoPorId(int vehiculoId)
        {
            try
            {
                return _repo.GetVehiculoPorId(vehiculoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VehiculoDto> GetVehiculosPorPagina(int registrosPorPagina, int paginaActual, int? idModelo, int? idTipo)
        {
            try
            {
                return _repo.GetVehiculosPorPagina(registrosPorPagina, paginaActual, idModelo, idTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Vehiculos vehiculos)
        {
            try
            {
                if (vehiculos.IdVehiculo == 0)
                {
                    _repo.Agregar(vehiculos);
                }
                else
                {
                    _repo.Editar(vehiculos);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
