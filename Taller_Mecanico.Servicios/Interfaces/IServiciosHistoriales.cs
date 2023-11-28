﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosHistoriales
    {
        void Guardar(Historiales historial);
        void Borrar(int IdHistorial);
        bool Existe(Historiales historial);
        bool EstaRelacionada(Historiales historial);
        int GetCantidad(int? IdCliente, int? IdEmpresa, DateTime? Fecha);
        List<HistorialDto> GetHistorialesPorPagina(int registrosPorPagina, int paginaActual, int? IdCliente, int? IdEmpresa, DateTime? Fecha);
        Historiales GetHistorialPorId(int IdHistorial);
        List<Historiales> GetHistorialesCombos();
    }
}
