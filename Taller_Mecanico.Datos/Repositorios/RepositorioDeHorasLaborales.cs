using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeHorasLaborales:IRepositorioDeHorasLaborales
    {
        private readonly string cadenaDeConexion;
        public RepositorioDeHorasLaborales()
        {
            cadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(HorasLaborales horas)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    string selectQuery = @"INSERT INTO HorasLaborales (Fecha, HoraDeInicio, HoraDeFin, HorasExtras, HorasLaborales) 
                                      VALUES(@Fecha,@HoraInicio,@HoraFin,@horasExtras,@horasLaborales); SELECT SCOPE_IDENTITY()";
                    conn.Open();
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {          
                        comando.Parameters.AddWithValue("@Fecha", horas.Fecha);

                        comando.Parameters.AddWithValue("@HoraInicio", horas.HoraInicio);

                        comando.Parameters.AddWithValue("@HoraFin",horas.HoraFin);

                        comando.Parameters.AddWithValue("@horasExtras", horas.horasExtras);

                        comando.Parameters.AddWithValue("@horasLaborales", horas.horaslaborales);

                        int horasLaboralesId = Convert.ToInt32(comando.ExecuteScalar());
                        horasLaboralesId = horas.IdHorasLaborales;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int horasLaboralesId)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM HorasLaborales WHERE IdHorasLaborales=@IdHorasLaborales";
                    using (var comando = new SqlCommand(deleteQuery, conn))
                    {
                        comando.Parameters.Add("@IdHorasLaborales", SqlDbType.Int);
                        comando.Parameters["@IdHorasLaborales"].Value = horasLaboralesId;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(HorasLaborales horas)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string editQuery = "UPDATE HorasLaborales SET Fecha=@Fecha,HoraDeInicio=@HoraInicio, HoraDeFin=@HoraFin, HorasExtras=@horasExtras, HorasLaborales=@horasLaborales WHERE IdHorasLaborales=@IdHorasLaborales";
                    using (var comando = new SqlCommand(editQuery, conn))
                    {
                        comando.Parameters.Add("@Fecha", SqlDbType.Date);
                        comando.Parameters["@Fecha"].Value = horas.Fecha;

                        comando.Parameters.Add("@HoraInicio", SqlDbType.Time);
                        comando.Parameters["@HoraInicio"].Value = horas.HoraInicio;

                        comando.Parameters.Add("@HoraFin", SqlDbType.Time);
                        comando.Parameters["@HoraFin"].Value = horas.HoraFin;

                        comando.Parameters.Add("@horasExtras", SqlDbType.Int);
                        comando.Parameters["@horasExtras"].Value = horas.horasExtras;

                        comando.Parameters.Add("@horasLaborales", SqlDbType.Int);
                        comando.Parameters["@horasLaborales"].Value = horas.horaslaborales;

                        comando.Parameters.Add("@IdHorasLaborales", SqlDbType.Int);
                        comando.Parameters["@IdHorasLaborales"].Value = horas.IdHorasLaborales;

                        comando.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(HorasLaborales horas)
        {
            try
            {
                int cantidad;
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (horas.IdHorasLaborales == 0)
                    {
                        selectQuery = "SELECT COUNT(*) FROM HorasLaborales WHERE Fecha=@Fecha";
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM HorasLaborales WHERE Fecha=@Fecha AND IdHorasLaborales!=@IdHorasLaborales";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@Fecha", SqlDbType.Date);
                        comando.Parameters["@Fecha"].Value = horas.Fecha;

                        if (horas.IdHorasLaborales != 0)
                        {
                            comando.Parameters.Add("@IdHorasLaborales", SqlDbType.Int);
                            comando.Parameters["@IdHorasLaborales"].Value = horas.IdHorasLaborales;
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

        public List<HorasLaborales> Filtrar(DateTime horas)
        {
            try
            {
                List<HorasLaborales> lista= new List<HorasLaborales>();
                using (var conn = new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string selectQuery = @"SELECT IdHorasLaborales, Fecha, HoraDeInicio, HoraDeFin, HorasExtras, HorasLaborales FROM HorasLaborales WHERE Fecha=@Fecha";
                    using (var comm = new SqlCommand(selectQuery, conn))
                    {
                        comm.Parameters.Add("@Fecha", SqlDbType.Date);
                        comm.Parameters["@Fecha"].Value=horas;
                        using (var reader= comm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var horasLaborales = ConstruirHorasLaborales(reader);
                                lista.Add(horasLaborales);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(DateTime? Fecha)
        {
            int cantidad=0;
            try
            {
                using (var conn= new SqlConnection(cadenaDeConexion))
                {
                    conn.Open();
                    string selectQuery;
                    if (Fecha == null)
                    {
                        selectQuery = "SELECT COUNT(*) FROM HORASLABORALES";
                        cantidad = conn.ExecuteScalar<int>(selectQuery);
                    }
                    else
                    {
                        selectQuery = "SELECT COUNT(*) FROM HorasLaborales WHERE Fecha=@Fecha";
                    }
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        if (Fecha.HasValue)
                        {
                            comando.Parameters.Add("@Fecha", SqlDbType.Date);
                            comando.Parameters["@Fecha"].Value=Fecha.Value;
                        }
                        cantidad = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
                return cantidad;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HorasLaborales> GetHoras()
        {
           List<HorasLaborales> lista= new List<HorasLaborales>();
            using (var conn= new SqlConnection(cadenaDeConexion))
            {
                conn.Open();
                string selectQuery = "SELECT IdHorasLaborales, Fecha, HoraDeInicio, HoraDeFin, HorasExtras, HorasLaborales FROM HorasLaborales ORDER BY Fecha";
                using (var comando = new SqlCommand(selectQuery, conn))
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var horas = ConstruirHorasLaborales(reader);
                            lista.Add(horas);
                        }
                    }
                }
            }
            return lista;
        }

        private HorasLaborales ConstruirHorasLaborales(SqlDataReader reader)
        {
            HorasLaborales horas = new HorasLaborales()
            {
                IdHorasLaborales = reader.GetInt32(0),
                Fecha = reader.GetDateTime(1),
                HoraInicio = reader.GetTimeSpan(2),
                HoraFin = reader.GetTimeSpan(3),
                horasExtras = reader.GetInt32(4),
                horaslaborales = reader.GetInt32(5),
            };
            return horas;
        }
    }
}
