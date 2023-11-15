using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Entidades;
using Dapper;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeTiposDePago:IRepositorioDeTiposDepago
    {
        private readonly string cadenaDeConexion;
        public RepositorioDeTiposDePago()
        {
            cadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(TipoDePagos tipo)
        {
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string insertQuery = "INSERT INTO TiposDePagos (NombreDePago) VALUES(@NombreDePago); SELECT SCOPE_IDENTITY()";
                conn.Open();
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@NombreDePago", SqlDbType.NVarChar);
                    comando.Parameters["@NombreDePago"].Value = tipo.NombreDePago;

                    int tipoId = Convert.ToInt32(comando.ExecuteScalar());
                    tipo.IdTipoPagos = tipoId;
                }
            }
        }

        public void Borrar(int TipoDePagoId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM TiposDePagos WHERE IdTipoDePago=@IdTiposPagos";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@IdTiposPagos", SqlDbType.Int);
                        comando.Parameters["@IdTiposPagos"].Value = TipoDePagoId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(TipoDePagos tipo)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string updateQuery = "UPDATE TiposDePagos SET NombreDePago=@NombreDePago WHERE IdTipoDePago=@IdTiposPagos";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@NombreDePago", SqlDbType.NVarChar);
                        comando.Parameters["@NombreDePago"].Value =tipo.NombreDePago;

                        comando.Parameters.Add("@IdTiposPagos", SqlDbType.NVarChar);
                        comando.Parameters["@IdTiposPagos"].Value = tipo.IdTipoPagos;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TipoDePagos tipo)
        {
            try
            {
                int cantidad;
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (tipo.IdTipoPagos== 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDePagos WHERE NombreDePago=@Tipo";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDePagos WHERE NombreDePago=@Tipo AND IdTipoDePago!=@IdTiposPagos";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@Tipo", SqlDbType.NVarChar);
                        comando.Parameters["@Tipo"].Value = tipo.NombreDePago;

                        if (tipo.IdTipoPagos != 0)
                        {
                            comando.Parameters.Add("@IdTiposPagos", SqlDbType.Int);
                            comando.Parameters["@IdTiposPagos"].Value = tipo.IdTipoPagos;
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
                    selectQuery = "SELECT COUNT(*) FROM TiposDepagos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
            }
            return cantidad;
        }

        public List<TipoDePagos> GetTipoDePagos()
        {
            List<TipoDePagos> lista = new List<TipoDePagos>();
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string selectQuery = @"SELECT IdTipoDePago, NombreDePago FROM TiposDePagos
                         ORDER BY NombreDePago";
                conn.Open();
                using (var comando = new SqlCommand(selectQuery, conn))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoDePago = ConstruirTipoDePago(reader);

                            lista.Add(tipoDePago);
                        }

                    }
                }
            }
            return lista;
        }

        public TipoDePagos GetTipoDePagosPorId(int IdTipoDePago)
        {
            TipoDePagos tipo = null;
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                conn.Open();
                string selectQuery = @"SELECT IdTipoDePago,NombreDePago
                    FROM TiposDePagos WHERE IdTipoDePago=@IdTipoDePago";

                using (var cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.Add("@IdTipoDePago", SqlDbType.Int);
                    cmd.Parameters["@IdTipoDePago"].Value = IdTipoDePago;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            tipo = ConstruirTipoDePago(reader);
                        }
                    }
                }
            }
            return tipo;
        }

        private TipoDePagos ConstruirTipoDePago(SqlDataReader reader)
        {
            TipoDePagos tipo = new TipoDePagos()
            {
                IdTipoPagos = reader.GetInt32(0),
                NombreDePago = reader.GetString(1)
            };
            return tipo;
        }
    }
}
