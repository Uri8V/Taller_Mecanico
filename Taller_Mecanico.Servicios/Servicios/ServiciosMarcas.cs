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
    public class ServiciosMarcas : IServiciosMarcas
    {
        private readonly IRepositorioDeMarcas _servicio;
        public ServiciosMarcas()
        {
            _servicio= new RepositorioDeMarcas();
        }
        public void Guardar(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    _servicio.Agregar(marca);
                }
                else
                {
                    _servicio.Editar(marca);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Borrar(int marcaId)
        {
            try
            {
                _servicio.Borrar(marcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(string textoFiltro=null)
        {
            try
            {
                return _servicio.GetCantidad(textoFiltro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Marca> GetMarcas()
        {
            try
            {
               return _servicio.GetMarcas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Marca marca)
        {
            try
            {
               return _servicio.Existe(marca);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Marca GetMarcasPorId(int idMarca)
        {
            try
            {
                return _servicio.GetMarcasPorId(idMarca);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Marca marca)
        {
            try
            {
                return _servicio.EstaRelacionado(marca);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
