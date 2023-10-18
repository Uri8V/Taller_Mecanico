using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Windows.Helpers
{
    public static class GridHelpers
    {
        public static void LimpiarGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }
        public static DataGridViewRow ConstruirFila(DataGridView dgv)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgv);
            return r;

        }
        public static void SetearFila(DataGridViewRow r, object obj)
        {
            switch (obj)
            {
                case Marca marca:
                    r.Cells[0].Value = marca.NombreMarca;
                    break;
                case TipoVehiculo tipo:
                    r.Cells[0].Value = tipo.NombreTipoVehiculo;
                    break;
                case TipoDePagos tipos:
                    r.Cells[0].Value = tipos.TipoPago;
                    break;
                case TiposDeClientes tipos:
                    r.Cells[0].Value = tipos.TipoCliente;
                    break;
                case Roles rol:
                    r.Cells[0].Value = rol.Rol;
                    break;
                case HorasLaborales horas:
                    r.Cells[0].Value = horas.Fecha.ToShortDateString();
                    r.Cells[1].Value = horas.HoraInicio;
                    r.Cells[2].Value = horas.HoraFin;
                    r.Cells[3].Value = horas.horasExtras;
                    r.Cells[4].Value = horas.horaslaborales;
                    break;
                case ClienteDto clientes:
                    r.Cells[0].Value = clientes.Nombre;
                    r.Cells[1].Value = clientes.Apellido;
                    r.Cells[2].Value = clientes.Documento;
                    r.Cells[3].Value = clientes.Domicilio;
                    r.Cells[4].Value = clientes.CUIT;
                    r.Cells[5].Value = clientes.TipoCliente;
                    break;
                //case CiudadDto ciudad:
                //    r.Cells[0].Value = ciudad.NombrePais;
                //    r.Cells[1].Value = ciudad.NombreCiudad;
                //    break;
                //case Categoria categoria:
                //    r.Cells[0].Value = categoria.NombreCategoria;
                //    r.Cells[1].Value = categoria.Descripcion;
                //    break;
                //case ClienteListDto cliente:
                //    r.Cells[0].Value = $"{cliente.Apellido}, {cliente.Nombre}";
                //    r.Cells[1].Value = cliente.NombrePais;
                //    r.Cells[2].Value = cliente.NombreCiudad;
                //    break;
                //case ProveedorListDto proveedor:
                //    r.Cells[0].Value = proveedor.NombreProveedor;
                //    r.Cells[1].Value = proveedor.NombrePais;
                //    r.Cells[2].Value = proveedor.NombreCiudad;
                //    break;
                //case ProductoListDto producto:
                //    r.Cells[0].Value = producto.NombreProducto;
                //    r.Cells[1].Value = producto.Categoria;
                //    r.Cells[2].Value = producto.PrecioUnitario;
                //    r.Cells[3].Value = producto.UnidadesEnStock;
                //    r.Cells[4].Value = producto.Suspendido;
                //    break;

            }
            r.Tag = obj;

        }
        public static void AgregarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Add(r);
        }

        public static void QuitarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Remove(r);
        }
    }
}
