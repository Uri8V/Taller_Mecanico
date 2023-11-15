using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeEmpleados
    {
        void Agregar(Empleado empleado);
        void Borrar(int empleadoId);
        void Editar(Empleado empleado);
        bool Existe(Empleado empleado);
        bool EstaRelacionada(Empleado empleado);
        int GetCantidad(int? empleadoId);
        List<EmpleadoDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? rolesId);
        Empleado GetEmpleadoPorId(int empleadoId);
        List<Empleado> GetEmpleadosCombos();
    }
}
