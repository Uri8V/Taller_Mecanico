using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeTiposDepago
    {
        void Agregar(TipoDePagos tipo);
        void Borrar(int TipoDePagoId);
        void Editar(TipoDePagos tipo);
        bool Existe(TipoDePagos tipo);
        int GetCantidad(string textoFiltro);
        List<TipoDePagos> GetTipoDePagos();
        TipoDePagos GetTipoDePagosPorId(int idMovimiento);
    }
}
