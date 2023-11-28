using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosDeTiposDeClientes
    {
        void Guardar(TiposDeClientes tipo);
        void Borrar(int TipoDeClienteId);
        bool Existe(TiposDeClientes tipo);
        int GetCantidad(string textoFiltro);
        List<TiposDeClientes> GetTiposDeClientes();
        TiposDeClientes GetClientesPorId(int IdTipoCliente);
        bool EstaRelacionado(TiposDeClientes tipo);
    }
}
