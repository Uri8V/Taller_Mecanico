using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class SeviciosTipoDeVehiculo : IServiciosTipoDeVehiculo
    {
        private readonly IRepositorioTipoDeVehiculo _repo;
        public SeviciosTipoDeVehiculo()
        {
            _repo = new RepositorioTipoDeVehiculo();
        }

        public void Borrar(int tipoVehiculoId)
        {
            try
            {
                _repo.Borrar(tipoVehiculoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(TipoVehiculo TIPO)
        {
            try
            {
                return _repo.EstaRelacionado(TIPO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TipoVehiculo tipo)
        {
            try
            {
                return _repo.Existe(tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(string textoFiltro)
        {
            try
            {
                return _repo.GetCantidad(null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TipoVehiculo> GetTipoVehiculos()
        {
            try
            {
                return _repo.GetTipoVehiculos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TipoVehiculo GetTipoVehiculosPorId(int idVehiculo)
        {
            try
            {
                return _repo.GetTipoVehiculosPorId(idVehiculo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(TipoVehiculo tipo)
        {
            try
            {
                if (tipo.IdTipoVehiculo == 0)
                {
                   _repo.Agregar(tipo);
                }
                else
                {
                    _repo.Editar(tipo);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
