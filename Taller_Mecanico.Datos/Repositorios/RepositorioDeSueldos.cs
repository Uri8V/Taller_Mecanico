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
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeSueldos:IRepositorioDeSueldos
    {
        private readonly string cadenaConexion;
        public RepositorioDeSueldos()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Sueldos sueldos)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"INSERT INTO Sueldos (IdHistorial, IdHorasLaborales, TotalAPagar,TotalExtra) 
                                    Values ( @IdHistorial, @IdHorasLaborales, @TotalAPagar,@TotalExtra); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, sueldos);
                sueldos.IdSueldo = id;
            }
        }

        public void Borrar(int IdSueldo)
        {

            using (var conn = new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Sueldos WHERE IdSueldo=@IdSueldo";
                conn.Execute(deleteQuery, new { IdSueldo = IdSueldo });
            }
        }

        public void Editar(Sueldos sueldos)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string updateQuery = @"UPDATE Sueldos SET IdHistorial=@IdHistorial, IdHorasLaborales=@IdHorasLaborales, TotalAPagar=@TotalAPagar, TotalExtra=@TotalExtra
                WHERE IdSueldo=@IdSueldo";
                conn.Execute(updateQuery, sueldos);
            }
        }

        public bool EstaRelacionada(Sueldos sueldos)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Sueldos sueldos)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdHistorial)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (IdHistorial == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Sueldos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Sueldos 
                        WHERE (IdHistorial=@IdHistorial)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdHistorial = IdHistorial });
                }
            }
            return cantidad;
        }

        public List<Sueldos> GetSueldosCombos()
        {
            throw new NotImplementedException();
        }

        public Sueldos GetSueldosPorId(int IdSueldo)
        {
            Sueldos sueldo = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT s.IdHistorial,s.IdHorasLaborales,s.TotalAPagar,s.TotalExtra
                    FROM Sueldos s WHERE IdSueldo=@IdSueldo";
                sueldo = conn.QuerySingleOrDefault<Sueldos>(selectQuery,
                    new { IdSueldo = IdSueldo });
            }
            return sueldo;
        }

        public List<SueldosDto> GetSueldosPorPagina(int registrosPorPagina, int paginaActual, int? IdHistorial)
        {
            List<SueldosDto> lista = new List<SueldosDto>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT s.IdSueldo,hl.Fecha,hl.HorasExtras, hl.HorasLaborales, s.TotalAPagar, s.TotalExtra, e.Documento,e.Apellido, e.Nombre,h.ValorPorHora, h.ValorPorHoraExtra");

                selectQuery.AppendLine("FROM Sueldos s");
                selectQuery.AppendLine("INNER JOIN Historiales h ON s.IdHistorial=h.IdHistorial");
                selectQuery.AppendLine("INNER JOIN HorasLaborales hl ON hl.IdHorasLaborales= s.IdHorasLaborales");
                selectQuery.AppendLine("INNER JOIN Empleados e ON e.IdEmpleado=h.IdEmpleado");


                if (IdHistorial != null)
                {
                    selectQuery.AppendLine("WHERE h.IdHistorial = @IdHistorial");
                }

                selectQuery.AppendLine("ORDER BY hl.Fecha");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { IdHistorial, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

                lista = conn.Query<SueldosDto>(selectQuery.ToString(), parametros).ToList();
                return lista;
            }
        }
    }
}
