using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Comun.Interfaces
{
    public interface IRepositorioDeMarcas
    {
        void Agregar(Marca marca);
        void Borrar(int marcaId);
        void Editar(Marca marca);
        bool Existe(Marca marca);
        int GetCantidad(string textoFiltro);
        List<Marca> GetMarcas();
        bool EstaRelacionado(Marca marca);
        Marca GetMarcasPorId(int idMarca);
    }
}
