using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosDeHorasLaborales
    {
        void Guardar(HorasLaborales horas);
        void Borrar(int horasLaboralesId);
        bool Existe(HorasLaborales horas);
        int GetCantidad(DateTime? Fecha);
        List<HorasLaborales> GetHoras();
        bool EstRelacionada(HorasLaborales horas);
        List<HorasLaborales> Filtrar(DateTime horas);
        HorasLaborales GetHorasLaboralesPorId(int idHorasLaborales);
        List<HorasLaborales> GetHorasLaboralesCombo();
    }
}
