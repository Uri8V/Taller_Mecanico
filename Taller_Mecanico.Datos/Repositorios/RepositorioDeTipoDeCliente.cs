using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Entidades;
using Dapper;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeTipoDeCliente : IRepositorioTipoDeCliente
    {
        private readonly string cadenaDeConexion;
        public RepositorioDeTipoDeCliente()
        {
            cadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }
        public void Agregar(TiposDeClientes tipo)
        {
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string insertQuery = "INSERT INTO TiposDeClientes (TipoCliente) VALUES(@TipoCliente); SELECT SCOPE_IDENTITY()";
                conn.Open();
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@TipoCliente", SqlDbType.NVarChar);
                    comando.Parameters["@TipoCliente"].Value = tipo.TipoCliente;

                    int tipoId = Convert.ToInt32(comando.ExecuteScalar());
                    tipo.IdTipoCliente = tipoId;
                }
            }
        }

        public void Borrar(int TipoDeClienteId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM TiposDeClientes WHERE IdTipoCliente=@IdTipoCliente";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@IdTipoCliente", SqlDbType.Int);
                        comando.Parameters["@IdTipoCliente"].Value = TipoDeClienteId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(TiposDeClientes tipo)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string updateQuery = "UPDATE TiposDeClientes SET TipoCliente=@TipoCliente WHERE IdTipoCliente=@IdTipoCliente";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@TipoCliente", SqlDbType.NVarChar);
                        comando.Parameters["@TipoCliente"].Value = tipo.TipoCliente;

                        comando.Parameters.Add("@IdTipoCliente", SqlDbType.NVarChar);
                        comando.Parameters["@IdTipoCliente"].Value = tipo.IdTipoCliente;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TiposDeClientes tipo)
        {
            try
            {
                int cantidad;
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (tipo.IdTipoCliente == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDeClientes WHERE TipoCliente=@TipoCliente";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDeClientes WHERE TipoCliente=@TipoCliente AND IdTipoCliente!=@IdTipoCliente";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@TipoCliente", SqlDbType.NVarChar);
                        comando.Parameters["@TipoCliente"].Value = tipo.TipoCliente;

                        if (tipo.IdTipoCliente != 0)
                        {
                            comando.Parameters.Add("@IdTipoCliente", SqlDbType.Int);
                            comando.Parameters["@IdTipoCliente"].Value = tipo.IdTipoCliente;
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

        public int GetCantidad(string textoFiltro)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string selectQuery;
                if (textoFiltro == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM TiposDeClientes";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
            }
            return cantidad;
        }

        public TiposDeClientes GetTipoClientePorId(int tipoId)
        {
            TiposDeClientes tipo = null;
            using (var conn = new SqlConnection(cadenaDeConexion))
            {  
                conn.Open();
                string selectQuery = @"SELECT IdTipoCliente, TipoCliente 
                    FROM TiposDeClientes WHERE IdTipoCliente=@tipoId";
           
                using (var cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.Add("@tipoId", SqlDbType.Int);
                    cmd.Parameters["@tipoId"].Value = tipoId;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                           tipo= ConstruirTipoDePago(reader);
                        }
                    }
                }
            }
           return tipo;
            
        }

        public List<TiposDeClientes> GetTiposDeClientes()
        {
            List<TiposDeClientes> lista = new List<TiposDeClientes>();
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string selectQuery = @"SELECT IdTipoCliente, TipoCliente FROM TiposDeClientes
                         ORDER BY TipoCliente";
                conn.Open();
                using (var comando = new SqlCommand(selectQuery, conn))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoDeCliente = ConstruirTipoDePago(reader);

                            lista.Add(tipoDeCliente);
                        }

                    }
                }
            }
            return lista;
        }

        private TiposDeClientes ConstruirTipoDePago(SqlDataReader reader)
        {
            TiposDeClientes tipo = new TiposDeClientes()
            {
                IdTipoCliente = reader.GetInt32(0),
                TipoCliente = reader.GetString(1)
            };
            return tipo;
        }
    }
}
