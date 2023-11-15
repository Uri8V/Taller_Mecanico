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
            throw new NotImplementedException();
        }

        public bool Existe(Vehiculos vehiculos)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? vehiculoId)
        {
            try
            {
               return _repo.GetCantidad(vehiculoId);
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

        public List<VehiculoDto> GetVehiculosPorPagina(int registrosPorPagina, int paginaActual, int? tipoId, int? modeloId)
        {
            try
            {
                return _repo.GetVehiculosPorPagina(registrosPorPagina, paginaActual, tipoId, modeloId);
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
