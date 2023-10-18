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
                string insertQuery = "INSERT INTO TiposDePagos (NombreDePago) VALUES(@TipoPago); SELECT SCOPE_IDENTITY()";
                conn.Open();
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@TipoPago", SqlDbType.NVarChar);
                    comando.Parameters["@TipoPago"].Value = tipo.TipoPago;

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
                    string updateQuery = "UPDATE TiposDePagos SET NombreDePago=@TipoPago WHERE IdTipoDePago=@IdTiposPagos";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@TipoPago", SqlDbType.NVarChar);
                        comando.Parameters["@TipoPago"].Value =tipo.TipoPago;

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
                        selectQuery = "SELECT COUNT(*) FROM TiposDePagos WHERE NombreDePago=@TipoPago";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDePagos WHERE NombreDePago=@TipoPago AND IdTipoDePago!=@IdTiposPagos";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@TipoPago", SqlDbType.NVarChar);
                        comando.Parameters["@TipoPago"].Value = tipo.TipoPago;

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

        private TipoDePagos ConstruirTipoDePago(SqlDataReader reader)
        {
            TipoDePagos tipo = new TipoDePagos()
            {
                IdTipoPagos = reader.GetInt32(0),
                TipoPago = reader.GetString(1)
            };
            return tipo;
        }
    }
}
