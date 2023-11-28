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
    public class ServiciosDeHorasLaborales:IServiciosDeHorasLaborales
    {
        private readonly IRepositorioDeHorasLaborales _repo;
        public ServiciosDeHorasLaborales()
        {
            _repo = new RepositorioDeHorasLaborales();
        }

        public void Borrar(int horasLaboralesId)
        {
            try
            {
                _repo.Borrar(horasLaboralesId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstRelacionada(HorasLaborales horas)
        {
            try
            {
                return _repo.EstRelacionada(horas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(HorasLaborales horas)
        {
            try
            {
               return _repo.Existe(horas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HorasLaborales> Filtrar(DateTime horas)
        {
            try
            {
                return _repo.Filtrar(horas);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(DateTime? Fecha)
        {
            try
            {
                return _repo.GetCantidad(Fecha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HorasLaborales> GetHoras()
        {
            try
            {
                return _repo.GetHoras();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HorasLaborales> GetHorasLaboralesCombo()
        {
            try
            {
                return _repo.GetHorasLaboralesCombo();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public HorasLaborales GetHorasLaboralesPorId(int idHorasLaborales)
        {
            try
            {
                return _repo.GetHorasLaboralesPorId(idHorasLaborales);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(HorasLaborales horas)
        {
            try
            {
                if (horas.IdHorasLaborales == 0)
                {
                    _repo.Agregar(horas);
                }
                else
                {
                    _repo.Editar(horas);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
