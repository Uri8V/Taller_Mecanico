using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRepositorioTipoDeVehiculo _servicios;
        public SeviciosTipoDeVehiculo()
        {
            _servicios = new RepositorioTipoDeVehiculo();
        }

        public void Borrar(int tipoVehiculoId)
        {
            try
            {
                _servicios.Borrar(tipoVehiculoId);
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
                return _servicios.Existe(tipo);
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
                return _servicios.GetCantidad(null);
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
                return _servicios.GetTipoVehiculos();
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
                if (tipo.TipoVehiculoId == 0)
                {
                   _servicios.Agregar(tipo);
                }
                else
                {
                    _servicios.Editar(tipo);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
