using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeSueldos
    {
        void Agregar(Sueldos sueldos);
        void Borrar(int IdSueldo);
        void Editar(Sueldos sueldos);
        bool Existe(Sueldos sueldos);
        bool EstaRelacionada(Sueldos sueldos);
        int GetCantidad(int? IdEmpleado, DateTime? Fecha);
        List<SueldosDto> GetSueldosPorPagina(int registrosPorPagina, int paginaActual, int? IdEmpleado, DateTime? Fecha);
        Sueldos GetSueldosPorId(int IdSueldo);
        List<Sueldos> GetSueldosCombos();

    }
}
