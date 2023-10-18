using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosClientes:IServiciosClientes
    {
        private readonly IRepositorioClientes _repositorio;
        public ServiciosClientes()
        {
            _repositorio = new RepositorioClientes();
        }

        public void Guardar(Clientes cliente)
        {
            try
            {
                if (cliente.IdCliente == 0)
                {
                    _repositorio.Agregar(cliente);
                }
                else
                {
                    _repositorio.Editar(cliente);   
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int clienteId)
        {
            try
            {
                _repositorio.Borrar(clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Clientes cliente)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Clientes cliente)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? TipoClienteId)
        {
            try
            {
                return _repositorio.GetCantidad(TipoClienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteComboDto> GetCiudadesCombos(int tipoId)
        {
            try
            {
                return _repositorio.GetCiudadesCombos(tipoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Clientes GetClientePorId(int clienteId)
        {
            try
            {
               return _repositorio.GetClientePorId(clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteDto> GetClientesPorPagina(int registrosPorPagina, int paginaActual, TiposDeClientes TipoCliente = null)
        {
            try
            {
                var lista= _repositorio.GetClientesPorPagina(registrosPorPagina, paginaActual, TipoCliente);
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
