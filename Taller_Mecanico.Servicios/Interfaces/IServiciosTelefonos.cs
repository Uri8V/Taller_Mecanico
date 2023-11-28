using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Servicios.Interfaces
{
    public interface IServiciosTelefonos
    {
        void Guardar(Telefonos telefono);
        void Borrar(int telefonoId);
        bool Existe(Telefonos telefono);
        bool EstaRelacionada(Telefonos telefono);
        int GetCantidad(int? telefonoId, int? clienteid, string texto=null);
        List<TelefonoDto> GetTelefonosPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, int? empleadoId, string texto=null);
        Telefonos GetTelefonoPorId(int telefonoId);
        List<TelefonoComboDto> GetTelefonosCombos(int paisId);
    }
}
