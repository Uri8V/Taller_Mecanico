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
            int cantidad = 0;
            using (var conn = new SqlConnection(CadenaConexion))
            {

                string selectQuery = @"SELECT COUNT(*) FROM Sueldos WHERE IdHistorial=@IdHistorial";
                cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdHistorial = historial.IdHistorial });
            }
            return cantidad > 0;
        }

        public bool Existe(Historiales historial)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery;
                if (historial.IdHistorial == 0)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Historiales 
                            WHERE IdEmpleado=@IdEmpleado AND IdReserva=@IdReserva AND IdVehiculo=@IdVehiculo";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { IdEmpleado = historial.IdEmpleado, IdReserva=historial.IdReserva, IdVehiculo=historial.IdVehiculo });
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Historiales 
                WHERE IdEmpleado=@IdEmpleado AND IdReserva=@IdReserva AND IdVehiculo=@IdVehiculo AND IdHistorial!=@IdHistorial";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { IdEmpleado = historial.IdEmpleado, IdReserva = historial.IdReserva, IdVehiculo=historial.IdVehiculo, IdHistorial=historial.IdHistorial });
                }
            }
            return cantidad > 0;
        }

        public int GetCantidad(int? IdCliente, int? IdEmpleado, DateTime? Fecha)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery;
                if (IdCliente == null && IdEmpleado == null && Fecha==null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Historiales";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else if(IdCliente!=null && IdEmpleado == null && Fecha==null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Historiales h
                        INNER JOIN Reservas r ON r.IdReserva=h.IdReserva
                        WHERE (r.IdCliente=@IdCliente)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdCliente = IdCliente });
                }
                else if(IdCliente==null && IdEmpleado != null && Fecha == null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Historiales 
                        WHERE (IdEmpleado=@IdEmpleado)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdEmpleado = IdEmpleado });
                }
                else if(IdCliente==null && IdEmpleado == null && Fecha != null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Historiales h
                        INNER JOIN Reservas r ON r.IdReserva=h.IdReserva 
                        WHERE (r.FechaEntrada=CONVERT(DATE, @Fecha))";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { Fecha = Fecha });
                }
            }
            return cantidad;
        }

        public List<HistorialComboDto> GetHistorialesCombos()
        {
            List<HistorialComboDto>lista;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"SELECT h.IdHistorial, CONCAT('Patente: ',v.Patente,' | Empleado: ',CONCAT(UPPER(e.Apellido),' ',e.Nombre,' (',e.Documento,')'),' | Fecha: ',r.FechaEntrada, ' | Hora: ', r.HoraEntrada ) AS Info
                                       FROM Historiales h
                                       INNER JOIN Vehiculos V ON h.IdVehiculo=v.IdVehiculo
                                       INNER JOIN Empleados e ON h.IdEmpleado=e.IdEmpleado
                                       INNER JOIN Reservas r ON r.IdReserva=h.IdReserva 
                                       ORDER BY r.FechaEntrada";
                lista = conn.Query<HistorialComboDto>(selectQuery).ToList();

            }
            return lista;
        }

        public List<HistorialDto> GetHistorialesPorPagina(int registrosPorPagina, int paginaActual, int? IdCliente, int? IdEmpleado, DateTime? Fecha)
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

                if (IdCliente != null || IdEmpleado!=null || Fecha!=null)
                {
                    selectQuery.AppendLine("WHERE r.IdCliente = @IdCliente OR h.IdEmpleado=@IdEmpleado OR r.FechaEntrada=CONVERT(DATE, @Fecha)");
                }

                selectQuery.AppendLine("ORDER BY r.FechaEntrada");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { IdCliente,IdEmpleado,Fecha, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

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
