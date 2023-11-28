using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeVehiculos : IRepositorioDeVehiculos
    {
        private readonly string cadenaConexion;
        public RepositorioDeVehiculos()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }
        public void Agregar(Vehiculos vehiculos)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"INSERT INTO Vehiculos (Patente, Kilometros, IdTipoVehiculo, IdModelo) 
                                    Values (@Patente, @Kilometros, @IdTipoVehiculo, @IdModelo); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, vehiculos);
                vehiculos.IdVehiculo = id;
            }
        }

        public void Borrar(int vehiculoId)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Vehiculos WHERE IdVehiculo=@IdVehiculo";
                conn.Execute(deleteQuery, new { IdVehiculo = vehiculoId });

            }
        }

        public void Editar(Vehiculos vehiculos)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string updateQuery = @"UPDATE Vehiculos SET Patente=@Patente, Kilometros=@Kilometros, IdTipoVehiculo=@IdTipoVehiculo, IdModelo=@IdModelo 
                WHERE IdVehiculo=@IdVehiculo";
                conn.Execute(updateQuery, vehiculos);
            }
        }

        public bool EstaRelacionada(Vehiculos vehiculos)
        {
            int cantidadHistoriales = 0;
            int cantidadVehiculosServicios = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT COUNT(*) FROM Historiales WHERE IdVehiculo=@IdVehiculo;
                                          SELECT COUNT(*) FROM VehiculosServicios WHERE IdVehiculo = @IdVehiculo";
                using (var resultado = conn.QueryMultiple(selectQuery, new { IdVehiculo = vehiculos.IdVehiculo }))
                {
                    cantidadHistoriales = resultado.Read<int>().First();
                    cantidadVehiculosServicios = resultado.Read<int>().First();
                }
            }
            return cantidadHistoriales + cantidadVehiculosServicios > 0;
        }

        public bool Existe(Vehiculos vehiculos)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (vehiculos.IdVehiculo == 0)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Vehiculos 
                            WHERE Patente=@Patente";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { Patente = vehiculos.Patente });
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Vehiculos 
                WHERE Patente=@Patente AND  IdVehiculo!=@IdVehiculo";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { Patente = vehiculos.Patente ,IdVehiculo = vehiculos.IdVehiculo});
                }
            }
            return cantidad > 0;
        }

        public int GetCantidad(int? IdModelo, int? IdTipoVehiculo)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (IdModelo == null && IdTipoVehiculo==null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Vehiculos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else if(IdModelo == null && IdTipoVehiculo!=null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Vehiculos 
                        WHERE  (IdTipoVehiculo=@IdTipoVehiculo)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdTipoVehiculo = IdTipoVehiculo });
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Vehiculos 
                        WHERE  (IdModelo=@IdModelo)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdModelo = IdModelo });

                }
            }
            return cantidad;
        }

        public List<Vehiculos> GetVehiculoCombos()
        {
            List<Vehiculos> lista;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT IdVehiculo, Patente FROM Vehiculos
                        ORDER BY Patente";
                lista = conn.Query<Vehiculos>(selectQuery).ToList();

            }
            return lista;
        }

        public Vehiculos GetVehiculoPorId(int vehiculoId)
        {
            Vehiculos vehiculo = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT IdVehiculo, Patente, Kilometros, IdTipoVehiculo, IdModelo 
                    FROM Vehiculos WHERE IdVehiculo=@IdVehiculo";
                vehiculo = conn.QuerySingleOrDefault<Vehiculos>(selectQuery,
                    new { IdVehiculo = vehiculoId });
            }
            return vehiculo;
        }

        public List<VehiculoDto> GetVehiculosPorPagina(int registrosPorPagina, int paginaActual, int? model , int? tipo)
        {
            List<VehiculoDto> lista = new List<VehiculoDto>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT");
                selectQuery.AppendLine("v.IdVehiculo,");
                selectQuery.AppendLine("v.Patente,");
                selectQuery.AppendLine("v.Kilometros,");
                selectQuery.AppendLine("t.TipoVehiculo,");
                selectQuery.AppendLine("m.Modelo");
                selectQuery.AppendLine("FROM Vehiculos v");
                selectQuery.AppendLine("INNER JOIN Modelos m ON v.IdModelo = m.IdModelo");
                selectQuery.AppendLine("INNEr JOIN TiposDeVehiculo t ON v.IdTipoVehiculo = t.IdTipoVehiculo");

                if (tipo != null || model != null)
                {
                    selectQuery.AppendLine("WHERE  m.IdModelo= @model OR t.IdTipoVehiculo = @tipo ");
                }
                selectQuery.AppendLine("ORDER BY t.TipoVehiculo, m.Modelo");
                selectQuery.AppendLine("OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY");

                var parametros = new
                {
                   model,
                   tipo,
                    cantidadRegistros = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina
                };

                lista = conn.Query<VehiculoDto>(selectQuery.ToString(), parametros).ToList();

            }

            return lista;
        }
    }
}
