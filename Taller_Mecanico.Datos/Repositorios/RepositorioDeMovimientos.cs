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
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeMovimientos:IRepositorioDeMovimientos
    {
        private readonly string candenaConexion;
        public RepositorioDeMovimientos()
        {
            candenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Movimientos movimientos)
        {
            using (var conn = new SqlConnection(candenaConexion))
            {
                string selectQuery = @"INSERT INTO Movimientos (Servicio, Debe, Senia, IdTipoDePago) 
                                    Values (@Servicio, @Debe, @Senia, @IdTipoDePago); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, movimientos);
                movimientos.IdMovimiento = id;
            }
        }

        public void Borrar(int IdMovimiento)
        {
            using (var conn = new SqlConnection(candenaConexion))
            {
                string deleteQuery = "DELETE FROM Movimientos WHERE IdMovimiento=@IdMovimiento";
                conn.Execute(deleteQuery, new { IdMovimiento = IdMovimiento });
            }
        }

        public void Editar(Movimientos movimientos)
        {
            using (var conn = new SqlConnection(candenaConexion))
            {
                string updateQuery = @"UPDATE Movimientos SET Servicio=@Servicio, Debe=@Debe, Senia=@Senia, IdTipoDePago=@IdTipoDePago
                WHERE IdMovimiento=@IdMovimiento";
                conn.Execute(updateQuery, movimientos);
            }
        }

        public bool EstaRelacionada(Movimientos movimientos)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Movimientos movimientos)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdTipodePago)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(candenaConexion))
            {
                string selectQuery;
                if (IdTipodePago == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Movimientos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Movimientos 
                        WHERE (IdTipoDepago=@IdTipodePago)";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdTipodePago = IdTipodePago });
                }
            }
            return cantidad;
        }

        public List<Movimientos> GetMovimientosCombos()
        {
            throw new NotImplementedException();
        }

        public Movimientos GetMovimientosPorId(int IdMovimiento)
        {
            Movimientos movimiento = null;
            using (var conn = new SqlConnection(candenaConexion))
            {
                string selectQuery = @"SELECT m.IdMovimiento,m.Servicio,m.Debe,m.Senia,m.IdTipoDePago
                    FROM Movimientos m WHERE m.IdMovimiento=@IdMovimiento";
                movimiento = conn.QuerySingleOrDefault<Movimientos>(selectQuery,
                    new { IdMovimiento = IdMovimiento });
            }
            return movimiento;
        }

        public List<MovimientosDto> GetMovimientosPorPagina(int registrosPorPagina, int paginaActual, int? IdTipoDePago)
        {
            List<MovimientosDto> lista = new List<MovimientosDto>();
            using (var conn = new SqlConnection(candenaConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT m.IdMovimiento,m.Servicio,m.Debe, m.Senia, t.NombreDePago");

                selectQuery.AppendLine("FROM Movimientos m");
                selectQuery.AppendLine("INNER JOIN TiposDePagos t ON t.IdTipoDePago=m.IdTipoDePago");

                if (IdTipoDePago != null)
                {
                    selectQuery.AppendLine("WHERE t.IdTipoDePago = @IdTipoDePago");
                }

                selectQuery.AppendLine("ORDER BY t.NombreDePago");
                selectQuery.AppendLine("OFFSET @registrosSaltados ROWS FETCH NEXT @registrosPorPagina ROWS ONLY");

                var parametros = new { IdTipoDePago, registrosSaltados = registrosPorPagina * (paginaActual - 1), registrosPorPagina };

                lista = conn.Query<MovimientosDto>(selectQuery.ToString(), parametros).ToList();
                return lista;
            }
        }
    }
}
