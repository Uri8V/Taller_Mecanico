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
    public class ServiciosDeTiposDeClientes:IServiciosDeTiposDeClientes
    {
        private IRepositorioTipoDeCliente _servicio;
        public ServiciosDeTiposDeClientes()
        {
            _servicio = new RepositorioDeTipoDeCliente();
        }

        public void Guardar(TiposDeClientes tipo)
        {
            try
            {
                if (tipo.IdTipoCliente == 0)
                {
                    _servicio.Agregar(tipo);
                }
                else
                {
                    _servicio.Editar(tipo);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int TipoDeClienteId)
        {
            try
            {
                _servicio.Borrar(TipoDeClienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TiposDeClientes tipo)
        {
            try
            {
                return _servicio.Existe(tipo);
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
                return _servicio.GetCantidad(null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TiposDeClientes> GetTiposDeClientes()
        {
            try
            {
                return _servicio.GetTiposDeClientes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TiposDeClientes GetClientesPorId(int IdTipoCliente)
        {
            try
            {
                return _servicio.GetTipoClientePorId(IdTipoCliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
