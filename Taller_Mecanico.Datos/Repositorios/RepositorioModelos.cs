using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioModelos : IRepositorioModelos
    {
        private readonly string CadenaDeConexion;
        public RepositorioModelos()
        {
            CadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }
        public void Agregar(Model modelos)
        {
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery = @"INSERT INTO Modelos (Modelo, IdMarca) 
                                    Values (@Modelo, @IdMarca); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, modelos);
                modelos.IdModelo = id;
            }
        }

        public void Borrar(int Idmodelo)
        {
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string deleteQuery = @"DELETE FROM Modelos WHERE IdModelo=@Idmodelo";
                conn.Execute(deleteQuery, new {IdModelo= Idmodelo });

            }
        }

        public void Editar(Model modelos)
        {
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string updateQuery = @"UPDATE Modelos SET Modelo=@Modelo, IdMarca=@IdMarca
                    WHERE IdModelo=@IdModelo";
                conn.Execute(updateQuery, modelos);
            }
        }

        public bool EstaRelacionada(Model modelos)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Model  modelos)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? IdMarca)
        {
            int cantidad = 0;
            string selectQuery;
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                if (IdMarca == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Modelos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = "SELECT COUNT(*) FROM Modelos WHERE IdMarca=@IdMarca ";
                    cantidad=conn.ExecuteScalar<int>(selectQuery, new {IdMarca=IdMarca});
                }
            }
            return cantidad;
        }

        public List<Model> GetModelosCombos()
        {
            List<Model> lista;
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery = @"SELECT IdModelo, Modelo FROM Modelos
                        ORDER BY Modelo";
                lista = conn.Query<Model>(selectQuery).ToList();

            }
            return lista;
        }

        public Model GetModelosPorId(int IdModelo)
        {
            Model modelos = null;
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery = @"SELECT IdModelo, Modelo, IdMarca 
                    FROM Modelos WHERE IdModelo=@IdModelo";
                modelos = conn.QuerySingleOrDefault<Model>(selectQuery,
                    new { IdModelo = IdModelo });
            }
            return modelos;
        }

        public List<ModelosDto> GetModelosPorPagina(int registrosPorPagina, int paginaActual, int? marca)
        {
            List<ModelosDto> lista = new List<ModelosDto>();
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery;
                if (marca != null)
                {
                    selectQuery = @"SELECT m.IdModelo, m.Modelo, mm.Marca FROM Modelos m
                                  INNER JOIN Marcas mm ON m.IdMarca=mm.IdMarca WHERE m.Marca=@marca ORDER BY Modelo
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = registrosPorPagina * (paginaActual - 1);

                    lista = conn.Query<ModelosDto>(selectQuery, new
                    {
                        marca= marca.Value,
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = registrosPorPagina
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT m.IdModelo, m.Modelo, mm.Marca FROM Modelos m
                                  INNER JOIN Marcas mm ON m.IdMarca=mm.IdMarca ORDER BY m.Modelo
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = registrosPorPagina * (paginaActual - 1);

                    lista = conn.Query<ModelosDto>(selectQuery, new
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
