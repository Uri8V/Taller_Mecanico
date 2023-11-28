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
                vehiculosServicios.IdVehiculosSevicios = id;
            }
        }

        public void Borrar(int IdVehiculosSevicios)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM VehiculosServicios WHERE IdVehiculosSevicios=@IdVehiculosSevicios";
                conn.Execute(deleteQuery, new { IdVehiculosSevicios = IdVehiculosSevicios });
            }
        }

        public void Editar(VehiculosServicios IdVehiculosSevicios)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string updateQuery = @"UPDATE VehiculosServicios SET IdVehiculo=@IdVehiculo,IdMovimiento=@IdMovimiento, IdCliente=@IdCliente, Descripcion=@Descripcion, Debe=@Debe, Haber=@Haber, Fecha=@Fecha
                WHERE IdVehiculosSevicios=@IdVehiculosSevicios";
                conn.Execute(updateQuery, IdVehiculosSevicios);
            }
        }

        public bool EstaRelacionada(VehiculosServicios vehiculosServicios)
        {
            throw new NotImplementedException();
        }

        public bool Existe(VehiculosServicios vehiculosServicios)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (vehiculosServicios.IdVehiculosSevicios == 0)
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                            WHERE IdVehiculo=@IdVehiculo AND IdMovimiento=@IdMovimiento AND IdCliente=@IdCliente AND Fecha=@Fecha";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { IdVehiculo =vehiculosServicios.IdVehiculo, IdMovimiento =vehiculosServicios.IdMovimiento, IdCliente =vehiculosServicios.IdCliente, Fecha =vehiculosServicios.Fecha });
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                WHERE IdVehiculo=@IdVehiculo AND IdMovimiento=@IdMovimiento AND IdCliente=@IdCliente AND Fecha=@Fecha AND IdVehiculosSevicios!=@IdVehiculosSevicios";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { IdVehiculo = vehiculosServicios.IdVehiculo, IdMovimiento = vehiculosServicios.IdMovimiento, IdCliente = vehiculosServicios.IdCliente, Fecha = vehiculosServicios.Fecha, IdVehiculosSevicios=vehiculosServicios.IdVehiculosSevicios });

                }
            }
            return cantidad > 0;
        }

        public int GetCantidad(int? IdVehiculo, int? IdMovimiento, int? IdCliente, DateTime? FechaServicios)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (IdVehiculo == null && IdMovimiento==null && IdCliente==null && FechaServicios==null)
                {
                    selectQuery = "SELECT COUNT(*) FROM VehiculosServicios";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else if(IdVehiculo != null && IdMovimiento==null && IdCliente==null && FechaServicios == null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                        WHERE (IdVehiculo=@IdVehiculo)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdVehiculo = IdVehiculo });
                }
                else if (IdVehiculo == null && IdMovimiento != null && IdCliente == null && FechaServicios == null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                        WHERE (IdMovimiento=@IdMovimiento)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdMovimiento = IdMovimiento });
                }
                else if (IdVehiculo == null && IdMovimiento == null && IdCliente != null && FechaServicios == null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                        WHERE (IdCliente=@IdCliente)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdCliente = IdCliente });
                }
                else if(IdVehiculo == null && IdMovimiento == null && IdCliente == null && FechaServicios != null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM VehiculosServicios 
                        WHERE (Fecha=CONVERT(DATE,@FechaServicios))";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { FechaServicios = FechaServicios });
                }
            }
            return cantidad;
        }

        public List<VehiculosServicios> GetVehiculoServicioCombos()
        {
            List<VehiculosServicios> lista = new List<VehiculosServicios>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT vs.IdVehiculosSevicios, e.Documento FROM Servicos s ";
                lista = conn.Query<VehiculosServicios>(selectQuery).ToList();
            }
            return lista;
        }

        public VehiculosServicios GetVehiculoServicioPorId(int IdVehiculoServicios)
        {
            VehiculosServicios vehiculosServicios = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT vs.IdVehiculosSevicios,vs.IdVehiculo,vs.IdMovimiento,vs.IdCliente,vs.Descripcion, vs.Debe, vs.Haber, vs.Fecha
                    FROM VehiculosServicios vs WHERE vs.IdVehiculosSevicios=@IdVehiculoServicios";
                vehiculosServicios = conn.QuerySingleOrDefault<VehiculosServicios>(selectQuery,
                    new { IdVehiculoServicios = IdVehiculoServicios });
            }
            return vehiculosServicios;
        }

        public List<VehiculosServiciosDto> GetVehiculoServicioPorPagina(int registrosPorPagina, int paginaActual, int? IdVehiculo, int? IdMovimiento, int? IdCliente, DateTime? FechaServicios)
        {
            List<VehiculosServiciosDto> lista = new List<VehiculosServiciosDto>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT vs.IdVehiculosSevicios, v.Patente, m.Servicio, m.Debe as DebeServicio, c.Apellido, c.Nombre,c.Documento,c.CUIT,vs.Descripcion,vs.Debe,vs.Haber,vs.Fecha");

                selectQuery.AppendLine("FROM VehiculosServicios vs");
                selectQuery.AppendLine("INNER JOIN Vehiculos v ON v.IdVehiculo=vs.IdVehiculo");
                selectQuery.AppendLine("INNER JOIN Movimientos m ON vs.IdMovimiento=m.IdMovimiento");
                selectQuery.AppendLine("INNER JOIN Clientes c ON c.IdCliente=vs.IdCliente");

                if (IdVehiculo != null || IdMovimiento != null || IdCliente != null || FechaServicios!=null)
                {
                    selectQuery.AppendLine("WHERE vs.IdVehiculo = @IdVehiculo OR vs.IdMovimiento=@IdMovimiento OR vs.Fecha=CONVERT(DATE,@FechaServicios)");
                }

                selectQuery.AppendLine("ORDER BY vs.Fecha,c.Apellido,c.Nombre");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { IdVehiculo,IdMovimiento,IdCliente,FechaServicios, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

                lista = conn.Query<VehiculosServiciosDto>(selectQuery.ToString(), parametros).ToList();
               
            }
            return lista;
        }
    }
}
