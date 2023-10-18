using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosMarcas
    {
        void Borrar(int marcaId);
        void Guardar(Marca marca);
        bool Existe(Marca marca);
        int GetCantidad(string textoFiltro);
        List<Marca> GetMarcas();
    }
}
