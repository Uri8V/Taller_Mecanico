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
        int GetCantidad(string telefonoId = null);
        List<TelefonoDto> GetTelefonosPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, int? empleadoId);
        Telefonos GetTelefonoPorId(int telefonoId);
        List<TelefonoComboDto> GetTelefonosCombos(int paisId);
    }
}
