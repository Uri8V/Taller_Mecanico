using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeReservas:IRepositorioDeReservas
    {
        private readonly string cadenaConexion;
        public RepositorioDeReservas()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Reservas reserva)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"INSERT INTO Reservas (IdCliente, FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, SePresento, EsSobreturno) 
                                    Values ( @IdCliente, @FechaEntrada, @HoraEntrada, @FechaSalida, @HoraSalida, @SePresento, @EsSobreturno); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, reserva);
                reserva.IdReserva = id;
            }
        }

        public void Borrar(int reservaId)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Reservas WHERE IdReserva=@IdReserva";
                conn.Execute(deleteQuery, new { IdReserva = reservaId });
            }
        }

        public void Editar(Reservas reserva)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string updateQuery = @"UPDATE Reservas SET IdCliente=@IdCliente, FechaEntrada=@FechaEntrada, HoraEntrada=@HoraEntrada, FechaSalida=@FechaSalida, HoraSalida=@HoraSalida, SePresento=@SePresento, EsSobreturno=@EsSobreturno
                WHERE IdReserva=@IdReserva";
                conn.Execute(updateQuery, reserva);
            }
        }

        public bool EstaRelacionada(Reservas reserva)
        {

            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {

                string selectQuery = @"SELECT COUNT(*) FROM Historiales WHERE IdReserva=@IdReserva";
                cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdReserva = reserva.IdReserva });
            }
            return cantidad > 0;
        }

        public bool Existe(Reservas reserva)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (reserva.IdReserva == 0)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Reservas 
                            WHERE IdCliente=@IdCliente AND FechaEntrada=@FechaEntrada AND HoraEntrada=@HoraEntrada";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { IdCliente =reserva.IdCliente, FechaEntrada = reserva.FechaEntrada, HoraEntrada = reserva.HoraEntrada });
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Reservas 
                            WHERE IdCliente=@IdCliente AND FechaEntrada=@FechaEntrada AND HoraEntrada=@HoraEntrada AND IdReserva!=@IdReserva";
                    cantidad = conn.ExecuteScalar<int>(
                        selectQuery, new { IdCliente = reserva.IdCliente, FechaEntrada = reserva.FechaEntrada, HoraEntrada = reserva.HoraEntrada, IdReserva = reserva.IdReserva });
                }
            }
            return cantidad > 0;
        }

        public int GetCantidad(int? reservaId, DateTime? FechaEntrada)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (reservaId == null && FechaEntrada==null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Reservas";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else if(reservaId!=null && FechaEntrada==null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Reservas 
                        WHERE (IdCliente=@reservaId)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { reservaId = reservaId });
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Reservas 
                        WHERE FechaEntrada=CONVERT(DATE, @FechaEntrada)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { FechaEntrada = FechaEntrada });
                }
            }
            return cantidad;
        }

        public List<ReservaComboDto> GetReservasCombos()
        {
            List<ReservaComboDto> lista;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT r.IdReserva, CONCAT(CONCAT(UPPER(c.Apellido),' ',c.Nombre,' (',c.Documento,') '),' | Fecha de Entrada: ' ,r.FechaEntrada,' | Hora de Entrada: ',r.HoraEntrada ) AS Info FROM Reservas r
                                       INNER JOIN Clientes c ON r.IdCliente=c.IdCliente
                                       ORDER BY r.FechaEntrada";
                lista = conn.Query<ReservaComboDto>(selectQuery).ToList();

            }
            return lista;
        }

        public Reservas GetReservasPorId(int IdReserva)
        {
            Reservas reservas = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT IdReserva, IdCliente,FechaEntrada, HoraEntrada, FechaSalida, HoraSalida, SePresento, EsSobreturno 
                    FROM Reservas WHERE IdReserva=@IdReserva";
                reservas = conn.QuerySingleOrDefault<Reservas>(selectQuery,
                    new { IdReserva = IdReserva });
            }
            return reservas;
        }

        public List<ReservaDto> GetReservasPorPagina(int registrosPorPagina, int paginaActual, int? clienteId, DateTime? FechaEntrada)
        {
            List<ReservaDto> lista = new List<ReservaDto>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT r.IdReserva,c.Nombre,c.Apellido, c.Documento, c.CUIT, r.FechaEntrada, r.HoraEntrada, r.FechaSalida, r.HoraSalida, r.SePresento, r.EsSobreturno");
                selectQuery.AppendLine("FROM Reservas r");
                selectQuery.AppendLine("INNER JOIN Clientes c ON c.IdCliente = r.IdCliente");

                if (clienteId!=null || FechaEntrada!=null)
                {
                    selectQuery.AppendLine("WHERE c.IdCliente = @clienteId OR r.FechaEntrada=CONVERT(DATE, @FechaEntrada)");
                }

                selectQuery.AppendLine("ORDER BY r.FechaEntrada");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { clienteId, FechaEntrada, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

                lista = conn.Query<ReservaDto>(selectQuery.ToString(), parametros).ToList();
                return lista;
            }
        }
    }
}
