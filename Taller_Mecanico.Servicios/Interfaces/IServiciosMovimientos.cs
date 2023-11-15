using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosMovimientos
    {
        void Guardar(Movimientos movimientos);
        void Borrar(int IdMovimiento);
        bool Existe(Movimientos movimientos);
        bool EstaRelacionada(Movimientos movimientos);
        int GetCantidad(int? IdHistorial);
        List<MovimientosDto> GetMovimientosPorPagina(int registrosPorPagina, int paginaActual, int? IdTipoDePago);
        Movimientos GetMovimientosPorId(int IdMovimiento);
        List<Movimientos> GetMovimientosCombos();
    }
}
