using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Datos.Repositorios;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;

namespace Taller_Mecanico.Servicios.Servicios
{
    public class ServiciosTiposDePagos:IServiciosTipoDePago
    {
        private IRepositorioDeTiposDepago _servicio;
        public ServiciosTiposDePagos()
        {
            _servicio= new RepositorioDeTiposDePago();
        }

        public void Guardar(TipoDePagos tipo)
        {
            try
            {
                if (tipo.IdTipoPagos == 0)
                {
                    _servicio.Agregar(tipo);
                }
                else
                {
                    _servicio.Editar(tipo);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Borrar(int TipoDePagoId)
        {
            try
            {
                _servicio.Borrar(TipoDePagoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TipoDePagos tipo)
        {
            try
            {
                return _servicio.Existe(tipo);
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
                return _servicio.GetCantidad(null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TipoDePagos> GetTipoDePagos()
        {
            try
            {
                return _servicio.GetTipoDePagos();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
