using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServicioModelos : IServicioModelos
    {
        private readonly IRepositorioModelos _repo;
        public ServicioModelos()
        {
            _repo= new RepositorioModelos();
        }

        public void Guardar(Model modelos)
        {
            try
            {
                if (modelos.IdModelo == 0)
                {
                    _repo.Agregar(modelos);
                }
                else
                {
                    _repo.Editar(modelos);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int modeloId)
        {
            try
            {
                _repo.Borrar(modeloId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Model modelos)
        {
            try
            {
                return _repo.EstaRelacionada(modelos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Model modelos)
        {
            try
            {
                return _repo.Existe(modelos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(int? MarcaId)
        {
            try
            {
                return _repo.GetCantidad(MarcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Model> GetModelosCombos()
        {
            try
            {
                return _repo.GetModelosCombos();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Model GetModelosPorId(int modelosId)
        {
            try
            {
                return _repo.GetModelosPorId(modelosId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ModelosDto> GetModelosPorPagina(int registrosPorPagina, int paginaActual, int? marcaId)
        {
            try
            {
                return _repo.GetModelosPorPagina(registrosPorPagina, paginaActual, marcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
