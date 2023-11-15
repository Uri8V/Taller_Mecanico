using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosMovimientos:IServiciosMovimientos
    {
        private readonly IRepositorioDeMovimientos _repo;
        public ServiciosMovimientos()
        {
            _repo= new RepositorioDeMovimientos();
        }

        public void Borrar(int IdMovimiento)
        {
            try
            {
                _repo.Borrar(IdMovimiento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Movimientos movimientos)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Movimientos movimientos)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdHistorial)
        {
            try
            {
                return _repo.GetCantidad(IdHistorial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Movimientos> GetMovimientosCombos()
        {
            throw new NotImplementedException();
        }

        public Movimientos GetMovimientosPorId(int IdMovimiento)
        {
            try
            {
                return _repo.GetMovimientosPorId(IdMovimiento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<MovimientosDto> GetMovimientosPorPagina(int registrosPorPagina, int paginaActual, int? IdTipoDePago)
        {
            try
            {
                return _repo.GetMovimientosPorPagina(registrosPorPagina, paginaActual, IdTipoDePago);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Movimientos movimientos)
        {
            try
            {
                if (movimientos.IdMovimiento == 0)
                {
                    _repo.Agregar(movimientos);
                }
                else
                {
                    _repo.Editar(movimientos);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
