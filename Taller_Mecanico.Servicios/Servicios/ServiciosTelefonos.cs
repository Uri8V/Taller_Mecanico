using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosTelefonos : IServiciosTelefonos
    {
        private readonly IRepositorioDeTelefonos _repo;
        private readonly IRepositorioClientes _reposClientes;
        private readonly IRepositorioDeEmpleados _reposEmpleados;
        public ServiciosTelefonos()
        {
            _repo = new RepositorioDeTelefonos();
            _reposClientes = new RepositorioClientes();
            _reposEmpleados = new RepositorioDeEmpleados();
        }
        public void Borrar(int telefonoId)
        {
            try
            {
                _repo.Borrar(telefonoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Telefonos telefono)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Telefonos telefono)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(string telefonoId = null)
        {
            try
            {
                return _repo.GetCantidad(telefonoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Telefonos GetTelefonoPorId(int telefonoId)
        {
            try
            {
                return _repo.GetTelefonoPorId(telefonoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TelefonoComboDto> GetTelefonosCombos(int paisId)
        {
            throw new NotImplementedException();
        }

        public List<TelefonoDto> GetTelefonosPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, int? empleadoId)
        {
            try
            {
                return _repo.GetTelefonosPorPagina(registrosPorPagina, paginaActual, clienteId,empleadoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Telefonos telefono)
        {
            if (telefono.IdTelefono == 0)
            {
                _repo.Agregar(telefono);
            }
            else
            {
                _repo.Editar(telefono);
            }
        }
    }
}
