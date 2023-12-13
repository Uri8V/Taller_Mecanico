using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeReservas
    {
        void Agregar(Reservas reserva);
        void Borrar(int reservaId);
        void Editar(Reservas reserva);
        bool Existe(Reservas reserva);
        bool EstaRelacionada(Reservas reserva);
        int GetCantidad(int? reservaId, DateTime? FechaEntrada);
        List<ReservaDto> GetReservasPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, DateTime? FechaEntrada);
        Reservas GetReservasPorId(int clienteId);
        List<ReservaComboDto> GetReservasCombos();
    }
}
