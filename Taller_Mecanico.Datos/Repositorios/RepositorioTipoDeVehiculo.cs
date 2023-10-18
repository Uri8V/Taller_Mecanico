using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioTipoDeVehiculo : IRepositorioTipoDeVehiculo
    {
        private readonly string cadenaConexion;
        public RepositorioTipoDeVehiculo()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(TipoVehiculo tipo)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string insertQuery = "INSERT INTO TiposDeVehiculo (TipoVehiculo) VALUES(@NombreTipoVehiculo); SELECT SCOPE_IDENTITY()";
                conn.Open();
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@NombreTipoVehiculo", SqlDbType.NVarChar);
                    comando.Parameters["@NombreTipoVehiculo"].Value = tipo.NombreTipoVehiculo;

                    int TipoVehiculoId = Convert.ToInt32(comando.ExecuteScalar());
                    tipo.TipoVehiculoId = TipoVehiculoId;
                }
            }
        }

        public void Borrar(int tipoVehiculoId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM TiposDeVehiculo WHERE IdTipoVehiculo=@TipoVehiculoId";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@TipoVehiculoId", SqlDbType.Int);
                        comando.Parameters["@TipoVehiculoId"].Value = tipoVehiculoId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(TipoVehiculo tipo)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string updateQuery = "UPDATE TiposDeVehiculo SET TipoVehiculo=@NombreTipoVehiculo Where IdTipoVehiculo=@TipoVehiculoId";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@NombreTipoVehiculo", SqlDbType.NVarChar);
                        comando.Parameters["@NombreTipoVehiculo"].Value = tipo.NombreTipoVehiculo;

                        comando.Parameters.Add("@TipoVehiculoId", SqlDbType.NVarChar);
                        comando.Parameters["@TipoVehiculoId"].Value = tipo.TipoVehiculoId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TipoVehiculo tipo)
        {
            try
            {
                int cantidad;
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (tipo.TipoVehiculoId == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDeVehiculo WHERE TipoVehiculo=@NombreTipoVehiculo";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM TiposDeVehiculo WHERE TipoVehiculo=@NombreTipoVehiculo AND IdTipoVehiculo!=@TipoVehiculoId";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@NombreTipoVehiculo", SqlDbType.NVarChar);
                        comando.Parameters["@NombreTipoVehiculo"].Value = tipo.NombreTipoVehiculo;

                        if (tipo.TipoVehiculoId != 0)
                        {
                            comando.Parameters.Add("@TipoVehiculoId", SqlDbType.Int);
                            comando.Parameters["@TipoVehiculoId"].Value = tipo.TipoVehiculoId;
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
                    selectQuery = "SELECT COUNT(*) FROM TiposDeVehiculo";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
            }
            return cantidad;
        }

        public List<TipoVehiculo> GetTipoVehiculos()
        {
            List<TipoVehiculo> lista = new List<TipoVehiculo>();
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT IdTipoVehiculo, TipoVehiculo FROM TiposDeVehiculo
                         ORDER BY TipoVehiculo";
                conn.Open();
                using (var comando = new SqlCommand(selectQuery, conn))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipo = ConstruirTipoVehiculo(reader);

                            lista.Add(tipo);
                        }

                    }
                }
            }
            return lista;
        }

        private TipoVehiculo ConstruirTipoVehiculo(SqlDataReader reader)
        {
            TipoVehiculo tipo = new TipoVehiculo()
            {
                TipoVehiculoId = reader.GetInt32(0),
                NombreTipoVehiculo = reader.GetString(1)
            };
            return tipo;
        }
    }
}
