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
            try
            {
                return _repo.Existe(telefono);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(int? telefonoId, int? clienteid, string texto = null)
        {
            try
            {
                return _repo.GetCantidad(telefonoId, clienteid,texto);
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

        public List<TelefonoDto> GetTelefonosPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, int? empleadoId, string texto = null)
        {
            try
            {
                return _repo.GetTelefonosPorPagina(registrosPorPagina, paginaActual, clienteId,empleadoId, texto);
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
