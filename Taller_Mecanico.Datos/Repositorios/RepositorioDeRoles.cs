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
    public class RepositorioDeRoles:IRepositorioDeRoles
    {
        private readonly string cadenaDeConexion;
        public RepositorioDeRoles()
        {
            cadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Roles rol)
        {

            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string insertQuery = "INSERT INTO Roles (Rol) VALUES(@Rol); SELECT SCOPE_IDENTITY()";
                conn.Open();
                using (var comando = new SqlCommand(insertQuery, conn))
                {
                    comando.Parameters.Add("@Rol", SqlDbType.NVarChar);
                    comando.Parameters["@Rol"].Value = rol.Rol;

                    int rolId = Convert.ToInt32(comando.ExecuteScalar());
                    rol.IdRolEmpleado = rolId;
                }
            }
        }

        public void Borrar(int rolId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Roles WHERE IdRolEmpleado=@IdRolEmpleado";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@IdRolEmpleado", SqlDbType.Int);
                        comando.Parameters["@IdRolEmpleado"].Value = rolId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(Roles rol)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Roles SET Rol=@Rol WHERE IdRolEmpleado=@IdRolEmpleado";
                    using (var comando = new SqlCommand(updateQuery, conn))
                    {
                        comando.Parameters.Add("@Rol", SqlDbType.NVarChar);
                        comando.Parameters["@Rol"].Value = rol.Rol;

                        comando.Parameters.Add("@IdRolEmpleado", SqlDbType.NVarChar);
                        comando.Parameters["@IdRolEmpleado"].Value = rol.IdRolEmpleado;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Roles rol)
        {
            try
            {
                int cantidad;
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (rol.IdRolEmpleado == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM Roles WHERE Rol=@Rol";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM Roles WHERE Rol=@Rol AND IdRolEmpleado!=@IdRolEmpleado";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@Rol", SqlDbType.NVarChar);
                        comando.Parameters["@Rol"].Value = rol.Rol;

                        if (rol.IdRolEmpleado != 0)
                        {
                            comando.Parameters.Add("@IdRolEmpleado", SqlDbType.Int);
                            comando.Parameters["@IdRolEmpleado"].Value = rol.IdRolEmpleado;
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
                    selectQuery = "SELECT COUNT(*) FROM Roles";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
            }
            return cantidad;
        }

        public List<Roles> GetRoles()
        {
            List<Roles> lista = new List<Roles>();
            using (var conn = new SqlConnection(cadenaDeConexion))
            {
                string selectQuery = @"SELECT IdRolEmpleado, Rol FROM Roles
                         ORDER BY Rol";
                conn.Open();
                using (var comando = new SqlCommand(selectQuery, conn))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rol = ConstruirRol(reader);

                            lista.Add(rol);
                        }

                    }
                }
            }
            return lista;
        }

        private Roles ConstruirRol(SqlDataReader reader)
        {
            Roles rol = new Roles()
            {
                IdRolEmpleado = reader.GetInt32(0),
                Rol = reader.GetString(1)
            };
            return rol;
        }
    }
}
