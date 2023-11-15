using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServicioSueldos:IServiciosSueldos
    {
        private readonly IRepositorioDeSueldos _repo;
        public ServicioSueldos()
        {
            _repo = new RepositorioDeSueldos();
        }

        public void Borrar(int IdSueldo)
        {
            try
            {
                _repo.Borrar(IdSueldo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Sueldos sueldos)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Sueldos sueldos)
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

        public List<Sueldos> GetSueldosCombos()
        {
            try
            {
                return _repo.GetSueldosCombos();
            }
            catch ( Exception)
            {

                throw;
            }
        }

        public Sueldos GetSueldosPorId(int IdSueldo)
        {
            try
            {
                return _repo.GetSueldosPorId(IdSueldo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SueldosDto> GetSueldosPorPagina(int registrosPorPagina, int paginaActual, int? IdHistorial)
        {
            try
            {
                return _repo.GetSueldosPorPagina(registrosPorPagina, paginaActual, IdHistorial);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Sueldos sueldos)
        {
            if (sueldos.IdSueldo == 0)
            {
                _repo.Agregar(sueldos);
            }
            else
            {
                _repo.Editar(sueldos);
            }
        }
    }
}
