using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeMarcas : IRepositorioDeMarcas
    {
        private readonly string cadenaConexion;

        public RepositorioDeMarcas()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Marca marca)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string insertQuery = "INSERT INTO Marcas (Marca) VALUES(@NombreMarca); SELECT SCOPE_IDENTITY()";
                conn.Open();
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@NombreMarca", SqlDbType.NVarChar);
                    comando.Parameters["@NombreMarca"].Value = marca.NombreMarca;

                    int MarcaId = Convert.ToInt32(comando.ExecuteScalar());
                    marca.MarcaId =MarcaId;
                }
            }
        }

        public void Borrar(int marcaId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Marcas WHERE IdMarca=@MarcaId";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@MarcaId", SqlDbType.Int);
                        comando.Parameters["@MarcaId"].Value = marcaId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(Marca marca)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Marcas SET Marca=@NombreMarca WHERE IdMarca=@MarcaId";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@NombreMarca", SqlDbType.NVarChar);
                        comando.Parameters["@NombreMarca"].Value = marca.NombreMarca;

                        comando.Parameters.Add("@MarcaId", SqlDbType.NVarChar);
                        comando.Parameters["@MarcaId"].Value = marca.MarcaId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Existe(Marca marca)
        {
            try
            {
                int cantidad;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (marca.MarcaId == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM Marcas WHERE Marca=@NombreMarca";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM Marcas WHERE Marca=@NombreMarca AND IdMarca!=@MarcaId";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@NombreMarca", SqlDbType.NVarChar);
                        comando.Parameters["@NombreMarca"].Value = marca.NombreMarca;

                        if (marca.MarcaId != 0)
                        {
                            comando.Parameters.Add("@MarcaId", SqlDbType.Int);
                            comando.Parameters["@MarcaId"].Value = marca.MarcaId;
                        }
                        cantidad = (int)comando.ExecuteScalar();
                    }
                }
                return cantidad > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(string textoFiltro=null)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Marcas";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
            }
            return cantidad;
        }

        public List<Marca> GetMarcas()
        {
            List<Marca> lista = new List<Marca>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT IdMarca, Marca FROM Marcas
                         ORDER BY Marca";
                conn.Open();
                using (var comando = new SqlCommand(selectQuery, conn))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var marca = ConstruirMarca(reader);

                            lista.Add(marca);
                        }

                    }
                }
            }
            return lista;
        }

        public Marca GetMarcasPorId(int IdMarca)
        {
            Marca marca = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string selectQuery = @"SELECT IdMarca, Marca 
                    FROM Marcas WHERE IdMarca=@IdMarca";

                using (var cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.Add("@IdMarca", SqlDbType.Int);
                    cmd.Parameters["@IdMarca"].Value = IdMarca;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            marca = ConstruirMarca(reader);
                        }
                    }
                }
            }
            return marca;
        }

        private Marca ConstruirMarca(SqlDataReader reader)
        {
            Marca marca = new Marca()
            {
                MarcaId = reader.GetInt32(0),
                NombreMarca = reader.GetString(1),
            };
           return marca;
        }

        public bool EstaRelacionado(Marca marca)
        {
            int cantidad = 0;
            using (IDbConnection conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = "SELECT COUNT(*) FROM Modelos WHERE IdMarca=@IdMarca";
                cantidad = conn.QuerySingle<int>(selectQuery, new { IdMarca =marca.MarcaId });
            }
            return cantidad > 0;
        }
    }
}
