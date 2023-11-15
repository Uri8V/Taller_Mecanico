using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServicioModelos
    {
        void Guardar(Model modelos);
        void Borrar(int modeloId);
        bool Existe(Model modelos);
        bool EstaRelacionada(Model modelos);
        int GetCantidad(int? ModeloId);
        List<ModelosDto> GetModelosPorPagina(int registrosPorPagina, int paginaActual, int? marcaId);
        Model GetModelosPorId(int marcaId);
        List<Model> GetModelosCombos();
    }
}
