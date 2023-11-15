using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosReservas:IServiciosReservas
    {
        private IRepositorioDeReservas _repo;
        private IRepositorioClientes _repoClientes;
        public ServiciosReservas()
        {
            _repo = new RepositorioDeReservas();
            _repoClientes= new RepositorioClientes();
        }

        public void Borrar(int reservaId)
        {
            try
            {
                _repo.Borrar(reservaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Reservas reserva)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Reservas reserva)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? reservaId)
        {
            try
            {
                return _repo.GetCantidad(reservaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Reservas> GetReservasCombos()
        {
            try
            {
                return _repo.GetReservasCombos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Reservas GetReservasPorId(int clienteId)
        {
            try
            {
               return _repo.GetReservasPorId(clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReservaDto> GetReservasPorPagina(int registrosPorPagina, int paginaActual, int? clienteId)
        {
            try
            {
                return _repo.GetReservasPorPagina(registrosPorPagina, paginaActual, clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Reservas reserva)
        {
            try
            {
                if (reserva.IdReserva == 0)
                {
                    _repo.Agregar(reserva);
                }
                else
                {
                    _repo.Editar(reserva);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
