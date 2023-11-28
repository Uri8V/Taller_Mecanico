using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeRoles
    {
        void Agregar(Roles rol);
        void Borrar(int rolId);
        void Editar(Roles rol);
        bool Existe(Roles rol);
        int GetCantidad(string textoFiltro);
        List<Roles> GetRoles();
        bool EstaRelacionado(Roles rol);
        Roles GetRolesPorId(int idRolEmpleado);
    }
}
