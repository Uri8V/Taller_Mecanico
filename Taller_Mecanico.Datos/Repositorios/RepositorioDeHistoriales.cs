using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeHistoriales : IRepositorioDeHistoriales
    {
        private readonly string CadenaConexion;
        public RepositorioDeHistoriales()
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }
        public void Agregar(Historiales historial)
        {
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"INSERT INTO Historiales (IdEmpleado, IdReserva, IdVehiculo, ValorPorHora, ValorPorHoraExtra) 
                                    Values ( @IdEmpleado, @IdReserva, @IdVehiculo, @ValorPorHora, @ValorPorHoraExtra); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, historial);
                historial.IdHistorial= id;
            }
        }

        public void Borrar(int IdHistorial)
        {
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string deleteQuery = "DELETE FROM Historiales WHERE IdHistorial=@IdHistorial";
                conn.Execute(deleteQuery, new { IdHistorial = IdHistorial });
            }
        }

        public void Editar(Historiales historial)
        {
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string updateQuery = @"UPDATE Historiales SET IdEmpleado=@IdEmpleado, IdVehiculo=@IdVehiculo, IdReserva=@IdReserva, ValorPorHora=@ValorPorHora, ValorPorHoraExtra=@ValorPorHoraExtra
                WHERE IdHistorial=@IdHistorial";
                conn.Execute(updateQuery, historial);
            }
        }

        public bool EstaRelacionada(Historiales historial)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Historiales historial)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdHistorial)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery;
                if (IdHistorial == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Historiales";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Historiales 
                        WHERE (IdHistorial=@IdHistorial)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdHistorial = IdHistorial });
                }
            }
            return cantidad;
        }

        public List<Historiales> GetHistorialesCombos()
        {
            List<Historiales> lista;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"SELECT IdHistorial FROM Historiales 
                        ORDER BY IdHistorial";
                lista = conn.Query<Historiales>(selectQuery).ToList();

            }
            return lista;
        }

        public List<HistorialDto> GetHistorialesPorPagina(int registrosPorPagina, int paginaActual, int? ReservaId)
        {
            List<HistorialDto> lista = new List<HistorialDto>();
            using (var conn = new SqlConnection(CadenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT h.IdHistorial,e.Nombre,e.Apellido, e.Documento, r.FechaEntrada, r.HoraEntrada, v.Patente, c.Documento as DocumentoCliente,c.Apellido as ApellidoCliente, c.Nombre as NombreCliente,h.ValorPorHora, h.ValorPorHoraExtra");

                selectQuery.AppendLine("FROM Historiales h");
                selectQuery.AppendLine("INNER JOIN Reservas r ON r.IdReserva=h.IdReserva");
                selectQuery.AppendLine("INNER JOIN Empleados e ON e.IdEmpleado=h.IdEmpleado");
                selectQuery.AppendLine("INNER JOIN Vehiculos v ON v.IdVehiculo=h.IdVehiculo");
                selectQuery.AppendLine("INNER JOIN Clientes c ON c.IdCliente=r.IdCliente");

                if (ReservaId != null)
                {
                    selectQuery.AppendLine("WHERE r.IdReserva = @ReservaId");
                }

                selectQuery.AppendLine("ORDER BY r.FechaEntrada");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { ReservaId, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

                lista = conn.Query<HistorialDto>(selectQuery.ToString(), parametros).ToList();
                return lista;
            }
        }

        public Historiales GetHistorialPorId(int IdHistorial)
        {
            Historiales historial = null;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"SELECT h.IdHistorial,h.IdVehiculo,h.IdReserva,h.IdEmpleado,h.ValorPorHora, h.ValorPorHoraExtra
                    FROM Historiales h WHERE IdHistorial=@IdHistorial";
                historial = conn.QuerySingleOrDefault<Historiales>(selectQuery,
                    new { IdHistorial = IdHistorial });
            }
            return historial;
        }
    }
}
