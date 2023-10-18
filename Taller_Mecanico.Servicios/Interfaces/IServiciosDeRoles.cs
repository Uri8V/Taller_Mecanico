using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosDeRoles
    {
        void Borrar(int rolId);
        void Guardar(Roles rol);
        bool Existe(Roles rol);
        int GetCantidad(string textoFiltro);
        List<Roles> GetRoles();
    }
}
