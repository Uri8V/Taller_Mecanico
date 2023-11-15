using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Empleados;
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

        public bool EstaRelacionada(Entidades.Entidades.Vehiculos vehiculos)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Entidades.Entidades.Vehiculos vehiculos)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? vehiculoId)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (vehiculoId == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Vehiculos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Vehiculos 
                        WHERE (IdModelo=@vehiculoId) OR (IdTipoVehiculo=@vehiculoId)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { Idvehiculo = vehiculoId });
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
                vehiculo = conn.QuerySingleOrDefault<Entidades.Entidades.Vehiculos>(selectQuery,
                    new { IdVehiculo = vehiculoId });
            }
            return vehiculo;
        }

        public List<VehiculoDto> GetVehiculosPorPagina(int registrosPorPagina, int paginaActual, int? tipo , int? model)
        {
            List<VehiculoDto> lista = new List<VehiculoDto>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (tipo != null || model!=null)
                {
                    selectQuery = @"SELECT v.IdVehiculo, v.Patente, v.Kilometros, t.TipoVehiculo, m.Modelo FROM Vehiculos v
                                  INNER JOIN TiposDeVehiculo t ON v.IdTipoVehiculo=v.IdTipoVehiculo
                                  INNER JOIN Modelos m ON m.IdModelo=v.IdModelo
                                  WHERE v.TipoVehiculo=@tipo OR m.Modelo=@modelo 
                                  ORDER BY t.TipoVehiculo, m.Modelo 
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = registrosPorPagina * (paginaActual - 1);

                    lista = conn.Query<VehiculoDto>(selectQuery, new
                    {
                        tipo = tipo.Value,
                        modelo = model.Value,
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = registrosPorPagina
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT v.IdVehiculo, v.Patente, v.Kilometros, t.TipoVehiculo, m.Modelo FROM Vehiculos v
                                   INNER JOIN TiposDeVehiculo T ON t.IdTipoVehiculo=v.IdTipoVehiculo
                                   INNER JOIN Modelos m ON m.IdModelo=v.IdModelo
                                  ORDER BY t.TipoVehiculo, m.Modelo 
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = registrosPorPagina * (paginaActual - 1);

                    lista = conn.Query<VehiculoDto>(selectQuery, new
                    {
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = registrosPorPagina
                    }).ToList();
                }
                return lista;
            }
        }
    }
}
