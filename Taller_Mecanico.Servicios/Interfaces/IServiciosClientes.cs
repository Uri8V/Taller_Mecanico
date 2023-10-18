using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosClientes
    {
        void Guardar(Clientes cliente);
        void Borrar(int clienteId);
        bool Existe(Clientes cliente);
        bool EstaRelacionada(Clientes cliente);
        int GetCantidad(int? TipoClienteId);
        List<ClienteDto> GetClientesPorPagina(int registrosPorPagina, int paginaActual, TiposDeClientes TipoCliente = null);
        Clientes GetClientePorId(int clienteId);
        List<ClienteComboDto> GetCiudadesCombos(int paisId);
    }
}
