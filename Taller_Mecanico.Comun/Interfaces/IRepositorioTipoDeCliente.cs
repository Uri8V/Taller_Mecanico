using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioTipoDeCliente
    {
        void Agregar(TiposDeClientes tipo);
        void Borrar(int TipoDeClienteId);
        void Editar(TiposDeClientes tipo);
        bool Existe(TiposDeClientes tipo);
        int GetCantidad(string textoFiltro);
        List<TiposDeClientes> GetTiposDeClientes();
        bool EstaRelacionado(TiposDeClientes tipo);
        TiposDeClientes GetTipoClientePorId(int tipoId);
    }
}
