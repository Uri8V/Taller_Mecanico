using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioClientes
    {
        void Agregar(Clientes cliente);
        void Borrar(int clienteId);
        void Editar(Clientes cliente);
        bool Existe(Clientes cliente);
        bool EstaRelacionada(Clientes cliente);
        int GetCantidad(int? clienteId);
        List<ClienteDto> GetClientesPorPagina(int registrosPorPagina, int paginaActual, int? TipoClienteId);
        Clientes GetClientePorId(int clienteId);
        List<Clientes> GetClientesCombos();
    }
}
