﻿using System;
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
            try
            {
                return _repo.EstaRelacionada(reserva);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Reservas reserva)
        {
            try
            {
                return _repo.Existe(reserva);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(int? reservaId, DateTime? FechaEntrada)
        {
            try
            {
                return _repo.GetCantidad(reservaId, FechaEntrada);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReservaComboDto> GetReservasCombos()
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

        public List<ReservaDto> GetReservasPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, DateTime? FechaEntrada)
        {
            try
            {
                return _repo.GetReservasPorPagina(registrosPorPagina, paginaActual, clienteId, FechaEntrada);
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
