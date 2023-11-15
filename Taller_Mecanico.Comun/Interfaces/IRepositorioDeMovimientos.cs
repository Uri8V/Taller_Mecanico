using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeMovimientos
    {
        void Agregar(Movimientos movimientos);
        void Borrar(int IdMovimiento);
        void Editar(Movimientos movimientos);
        bool Existe(Movimientos movimientos);
        bool EstaRelacionada(Movimientos movimientos);
        int GetCantidad(int? IdHistorial);
        List<MovimientosDto> GetMovimientosPorPagina(int registrosPorPagina, int paginaActual, int? IdTipoDePago);
        Movimientos GetMovimientosPorId(int IdMovimiento);
        List<Movimientos> GetMovimientosCombos();
    }
}
