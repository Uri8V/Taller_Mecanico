using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosHistoriales:IServiciosHistoriales
    {
        private readonly IRepositorioDeHistoriales _repo;
        public ServiciosHistoriales()
        {
            _repo = new RepositorioDeHistoriales();
        }

        public void Borrar(int IdHistorial)
        {
            try
            {
                _repo.Borrar(IdHistorial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Historiales historial)
        {
            try
            {
                return _repo.EstaRelacionada(historial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Historiales historial)
        {
            try
            {
                return _repo.Existe(historial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(int? IdCliente, int? IdEmpresa, DateTime? Fecha)
        {
            try
            {
                return _repo.GetCantidad(IdCliente,IdEmpresa,Fecha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HistorialComboDto> GetHistorialesCombos()
        {
            try
            {
                return _repo.GetHistorialesCombos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HistorialDto> GetHistorialesPorPagina(int registrosPorPagina, int paginaActual, int? IdCliente, int? IdEmpresa, DateTime? Fecha)
        {
            try
            {
                return _repo.GetHistorialesPorPagina(registrosPorPagina, paginaActual, IdCliente, IdEmpresa, Fecha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Historiales GetHistorialPorId(int IdHistorial)
        {
            try
            {
                return _repo.GetHistorialPorId(IdHistorial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Historiales historial)
        {
            try
            {
                if (historial.IdHistorial == 0)
                {
                    _repo.Agregar(historial);
                }
                else
                {
                    _repo.Editar(historial);    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
