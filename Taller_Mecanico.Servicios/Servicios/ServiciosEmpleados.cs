using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosEmpleados : IServiciosEmpleados
    {
        public ServiciosEmpleados()
        {
            _repo = new RepositorioDeEmpleados();
            _repoRoles = new RepositorioDeRoles();
        }
        private readonly IRepositorioDeEmpleados _repo;
        private readonly IRepositorioDeRoles _repoRoles;
        public void Borrar(int empleadoId)
        {
            try
            {
                _repo.Borrar(empleadoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? empleadoId)
        {
            try
            {
                return _repo.GetCantidad(empleadoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EmpleadoDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? roles)
        {
            try
            {
                return _repo.GetEmpleadosPorPagina(registrosPorPagina,paginaActual, roles);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Empleado GetEmpleadoPorId(int empleadoId)
        {
            try
            {
                return _repo.GetEmpleadoPorId(empleadoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Empleado> GetEmpleadosCombos()
        {
            try
            {
                return _repo.GetEmpleadosCombos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Empleado empleado)
        {
            try
            {
                if (empleado.IdEmpleado == 0)
                {
                    _repo.Agregar(empleado);
                }
                else
                {
                    _repo.Editar(empleado);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
