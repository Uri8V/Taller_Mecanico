using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioModelos
    {
        void Agregar(Model modelos);
        void Borrar(int modeloId);
        void Editar(Model modelos);
        bool Existe(Model modelos);
        bool EstaRelacionada(Model modelos);
        int GetCantidad(int? ModeloId);
        List<ModelosDto> GetModelosPorPagina(int registrosPorPagina, int paginaActual, int? marca);
        Model  GetModelosPorId(int marcaId);
        List<Model> GetModelosCombos();
    }
}
