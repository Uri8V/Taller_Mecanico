using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosRoles:IServiciosDeRoles
    {
        private readonly IRepositorioDeRoles _repositorio;
        
        public ServiciosRoles()
        {
            _repositorio= new RepositorioDeRoles();
        }

        public void Borrar(int rolId)
        {
            try
            {
                _repositorio.Borrar(rolId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Roles rol)
        {
            try
            {
               return _repositorio.Existe(rol);
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
                return _repositorio.GetCantidad(null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Roles> GetRoles()
        {
            try
            {
                return _repositorio.GetRoles();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Roles rol)
        {
            try
            {

                if (rol.IdRolEmpleado == 0)
                {
                    _repositorio.Agregar(rol);
                }
                else
                {
                    _repositorio.Editar(rol);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
