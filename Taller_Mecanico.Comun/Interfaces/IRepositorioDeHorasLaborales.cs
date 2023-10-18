using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeHorasLaborales
    {
        void Agregar(HorasLaborales horas);
        void Borrar(int horasLaboralesId);
        void Editar(HorasLaborales horas);
        bool Existe(HorasLaborales horas);
        int GetCantidad(DateTime? Fecha);
        List<HorasLaborales> GetHoras();
        List<HorasLaborales> Filtrar(DateTime horas);
    }
}
