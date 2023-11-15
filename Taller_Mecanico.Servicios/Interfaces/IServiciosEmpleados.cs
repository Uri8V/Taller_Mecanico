using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosEmpleados
    {
        void Guardar(Empleado empleado);
        void Borrar(int empleadoId);
        bool Existe(Empleado empleado);
        bool EstaRelacionada(Empleado empleado);
        int GetCantidad(int? empleadoId);
        List<EmpleadoDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual,int? rolId);
        Empleado GetEmpleadoPorId(int empleadoId);
        List<Empleado> GetEmpleadosCombos();
    }
}
