using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Mecanico.Windows.Helpers
{
    public static class formHelper
    {

        public static int CalcularPaginas(int registros, int registrosPorPagina)
        {
            if (registros < registrosPorPagina)
            {
                return 1;
            }
            else if (registros % registrosPorPagina == 0)
            {
                return registros / registrosPorPagina;
            }
            else
            {
                return registros / registrosPorPagina + 1;
            }
        }
    }
}
