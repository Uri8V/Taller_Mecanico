using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeVehiculosServicios:IRepositorioDeVehiculosServicios
    {
        private readonly string cadenaConexion;
        public RepositorioDeVehiculosServicios()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(VehiculosServicios vehiculosServicios)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"INSERT INTO VehiculosServicios (IdVehiculo,IdMovimiento, IdCliente, Descripcion, Debe, Haber, Fecha) 
                                    Values (@IdVehiculo,@IdMovimiento, @IdCliente, @Descripcion, @Debe, @Haber, @Fecha); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, vehiculosServicios);
                vehiculosServicios.IdVehiculoServicios = id;
            }
        }

        public void Borrar(int IdVehiculoServicios)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM VehiculosServicios WHERE IdVehiculoServicios=@IdVehiculoServicios";
                conn.Execute(deleteQuery, new { IdVehiculoServicios = IdVehiculoServicios });
            }
        }

        public void Editar(VehiculosServicios vehiculosServicios)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string updateQuery = @"UPDATE VehiculosServicios SET IdVehiculo=@IdVehiculo,IdMovimiento=@IdMovimiento, IdCliente=@IdCliente, Descripcion=@Descripcion, Debe=@Debe, Haber=@Haber, Fecha=@Fecha
                WHERE IdVehiculoServicios=@IdVehiculoServicios";
                conn.Execute(updateQuery, vehiculosServicios);
            }
        }

        public bool EstaRelacionada(VehiculosServicios vehiculosServicios)
        {
            throw new NotImplementedException();
        }

        public bool Existe(VehiculosServicios vehiculosServicios)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdVehiculo)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (IdVehiculo == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM VehiculosServicios";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                        WHERE (IdVehiculo=@IdVehiculo)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdVehiculo = IdVehiculo });
                }
            }
            return cantidad;
        }

        public List<VehiculosServicios> GetVehiculoServicioCombos()
        {
            throw new NotImplementedException();
        }

        public VehiculosServicios GetVehiculoServicioPorId(int IdVehiculoServicios)
        {
            VehiculosServicios vehiculosServicios = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT vs.IdVehiculoServicios,vs.IdVehiculo,vs.IdMovimiento,vs.IdCliente,vs.Descripcion, vs.Debe, vs.Haber, vs.Fecha
                    FROM VehiculoServicios vs WHERE vs.IdVehiculoServicios=@IdVehiculoServicios";
                vehiculosServicios = conn.QuerySingleOrDefault<VehiculosServicios>(selectQuery,
                    new { IdVehiculoServicios = IdVehiculoServicios });
            }
            return vehiculosServicios;
        }

        public List<VehiculosServiciosDto> GetVehiculoServicioPorPagina(int registrosPorPagina, int paginaActual, int? IdVehiculo)
        {
            List<VehiculosServiciosDto> lista = new List<VehiculosServiciosDto>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT vs.IdVehiculosSevicios, v.Patente, m.Servicio, m.Debe as DebeServicio, m.Senia, c.Apellido, c.Nombre,c.Documento,vs.Descripcion,vs.Debe,vs.Haber,vs.Fecha");

                selectQuery.AppendLine("FROM VehiculosServicios vs");
                selectQuery.AppendLine("INNER JOIN Vehiculos v ON v.IdVehiculo=vs.IdVehiculo");
                selectQuery.AppendLine("INNER JOIN Movimientos m ON vs.IdMovimiento=m.IdMovimiento");
                selectQuery.AppendLine("INNER JOIN Clientes c ON c.IdCliente=vs.IdCliente");

                if (IdVehiculo != null)
                {
                    selectQuery.AppendLine("WHERE vs.IdVehiculo = @IdVehiculo");
                }

                selectQuery.AppendLine("ORDER BY vs.Fecha");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { IdVehiculo, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

                lista = conn.Query<VehiculosServiciosDto>(selectQuery.ToString(), parametros).ToList();
                return lista;
            }
        }
    }
}
