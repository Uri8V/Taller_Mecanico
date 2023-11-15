using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosTipoDePago
    {
        void Borrar(int TipoDePagoId);
        void Guardar(TipoDePagos tipo);
        bool Existe(TipoDePagos tipo);
        int GetCantidad(string textoFiltro);
        List<TipoDePagos> GetTipoDePagos();
        TipoDePagos GetTipoDePagosPorId(int idMovimiento);
    }
}
