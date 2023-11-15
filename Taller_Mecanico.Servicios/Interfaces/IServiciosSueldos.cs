using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosSueldos
    {
        void Guardar(Sueldos sueldos);
        void Borrar(int IdSueldo);
        bool Existe(Sueldos sueldos);
        bool EstaRelacionada(Sueldos sueldos);
        int GetCantidad(int? IdHistorial);
        List<SueldosDto> GetSueldosPorPagina(int registrosPorPagina, int paginaActual, int? IdHistorial);
        Sueldos GetSueldosPorId(int IdSueldo);
        List<Sueldos> GetSueldosCombos();
    }
}
