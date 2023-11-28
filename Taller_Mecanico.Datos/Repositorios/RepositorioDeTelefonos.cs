using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeTelefonos : IRepositorioDeTelefonos
    {
        private readonly string CadenaDeConexion;
        public RepositorioDeTelefonos()
        {
            CadenaDeConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }
        public void Agregar(Telefonos telefono)
        {
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                if (telefono.IdCliente != 0)
                {
                    string selectQuery = @"INSERT INTO Telefonos (IdCliente, IdEmpleado, Telefono, TipoDeTelefono) 
                                    Values (@IdCliente, NULL, @Telefono, @TipoDeTelefono); SELECT SCOPE_IDENTITY();";
                    int id = conn.ExecuteScalar<int>(selectQuery, telefono);
                    telefono.IdTelefono = id;
                }
                else
                {
                string selectQuery = @"INSERT INTO Telefonos (IdCliente, IdEmpleado, Telefono, TipoDeTelefono) 
                                    Values (NULL, @IdEmpleado, @Telefono, @TipoDeTelefono); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, telefono);
                telefono.IdTelefono = id;

                }
            }
        }

        public void Borrar(int telefonoId)
        {
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string deleteQuery = "DELETE FROM Telefonos WHERE IdTelefono=@IdTelefono";
                conn.Execute(deleteQuery, new { IdTelefono = telefonoId });

            }
        }

        public void Editar(Telefonos telefono)
        {
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string updateQuery;
                if (telefono.IdEmpleado!=0)
                {
                   updateQuery = @"UPDATE Telefonos SET IdCliente=NULL, IdEmpleado=@IdEmpleado, Telefono=@Telefono, TipoDeTelefono=@TipoDeTelefono 
                WHERE IdTelefono=@IdTelefono"; 
                }
                else
                {
                    updateQuery = @"UPDATE Telefonos SET IdCliente=@IdCliente, IdEmpleado=NULL, Telefono=@Telefono, TipoDeTelefono=@TipoDeTelefono 
                WHERE IdTelefono=@IdTelefono";
                }
                conn.Execute(updateQuery, telefono);
            }
        }

        public bool EstaRelacionada(Telefonos telefono)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Telefonos telefono)
        {
            var cantidad = 0;
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery;
                if (telefono.IdTelefono == 0)
                {
                    if (telefono.IdCliente!=0)
                    {
                        selectQuery = @"SELECT COUNT(*) FROM Telefonos 
                            WHERE Telefono=@Telefono AND TipoDeTelefono=@TipoDeTelefono AND IdCliente=@IdCliente";
                        cantidad = conn.ExecuteScalar<int>(
                            selectQuery, new { Telefono = telefono.Telefono, TipoDeTelefono = telefono.TipoDeTelefono, IdCliente = telefono.IdCliente });
                    }
                    else
                    {
                        selectQuery = @"SELECT COUNT(*) FROM Telefonos 
                            WHERE Telefono=@Telefono AND TipoDeTelefono=@TipoDeTelefono AND IdEmpleado =@IdEmpleado";
                        cantidad = conn.ExecuteScalar<int>(
                            selectQuery, new { Telefono = telefono.Telefono, TipoDeTelefono = telefono.TipoDeTelefono, IdEmpleado = telefono.IdEmpleado });
                    }
                }
                else
                {
                    if (telefono.IdCliente != 0)
                    {
                        selectQuery = @"SELECT COUNT(*) FROM Telefonos 
                            WHERE Telefono=@Telefono AND TipoDeTelefono=@TipoDeTelefono AND IdCliente=@IdCliente AND IdTelefono!=@IdTelefono";
                        cantidad = conn.ExecuteScalar<int>(
                            selectQuery, new { Telefono = telefono.Telefono, TipoDeTelefono = telefono.TipoDeTelefono, IdCliente = telefono.IdCliente, IdTelefono=telefono.IdTelefono });
                    }
                    else
                    {
                        selectQuery = @"SELECT COUNT(*) FROM Telefonos 
                            WHERE Telefono=@Telefono AND TipoDeTelefono=@TipoDeTelefono AND IdEmpleado =@IdEmpleado AND IdTelefono!=@IdTelefono";
                        cantidad = conn.ExecuteScalar<int>(
                            selectQuery, new { Telefono = telefono.Telefono, TipoDeTelefono = telefono.TipoDeTelefono, IdEmpleado = telefono.IdEmpleado, IdTelefono = telefono.IdTelefono });
                    }
                }
            }
            return cantidad > 0;
        }

        public int GetCantidad(int? clienteid, int? telefonoId, string texto = null)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery;
                if (telefonoId == null && clienteid==null && texto==null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Telefonos";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else if(clienteid==null && texto==null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Telefonos 
                        WHERE IdEmpleado=@telefonoId";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { telefonoId = telefonoId });
                }
                else if(telefonoId==null && texto== null)
                {
                    selectQuery = @"SELECT COUNT(*) FROM Telefonos 
                        WHERE IdCliente=@clienteid";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { clienteid = clienteid });
                }
                else if(texto!=null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Telefonos WHERE TipoDeTelefono LIKE @texto ";
                    texto = $"%{texto}%";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new {texto});
                }
            }
            return cantidad;
        }

        public Telefonos GetTelefonoPorId(int IdTelefono)
        {
            Telefonos telefonos = null;
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                string selectQuery = @"SELECT IdTelefono, IdCliente, IdEmpleado, Telefono, TipoDeTelefono 
                    FROM Telefonos WHERE IdTelefono=@IdTelefono";
                telefonos = conn.QuerySingleOrDefault<Telefonos>(selectQuery,
                    new { IdTelefono = IdTelefono });
            }
            return telefonos;
        }

        public List<TelefonoComboDto> GetTelefonosCombos(int paisId)
        {
            throw new NotImplementedException();
        }

        public List<TelefonoDto> GetTelefonosPorPagina(int registrosPorPagina, int paginaActual, int? clienteid, int? empleadoid, string texto=null)
        {
            List<TelefonoDto> lista = new List<TelefonoDto>();
            using (var conn = new SqlConnection(CadenaDeConexion))
            {
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.AppendLine("SELECT");
                selectQuery.AppendLine("t.IdTelefono,");
                selectQuery.AppendLine("c.CUIT,");
                selectQuery.AppendLine("e.Apellido AS ApellidoEmpleado,");
                selectQuery.AppendLine("e.Nombre AS NombreEmpleado,");
                selectQuery.AppendLine("e.Documento AS DocumentoEmpleado,");
                selectQuery.AppendLine("c.Documento AS DocumentoCliente,");
                selectQuery.AppendLine("c.Nombre AS NombreCliente,");
                selectQuery.AppendLine("c.Apellido AS ApellidoCliente,");
                selectQuery.AppendLine("t.Telefono,");
                selectQuery.AppendLine("t.TipoDeTelefono");
                selectQuery.AppendLine("FROM Telefonos t");
                selectQuery.AppendLine("LEFT JOIN Empleados e ON e.IdEmpleado = t.IdEmpleado");
                selectQuery.AppendLine("LEFT JOIN Clientes c ON c.IdCliente = t.IdCliente");
    
                if (clienteid != null || empleadoid != null || texto!=null)
                {
                    selectQuery.AppendLine("WHERE c.IdCLiente = @clienteid OR e.IdEmpleado = @empleadoid OR t.TipoDeTelefono LIKE @texto ");
                }
                selectQuery.AppendLine("ORDER BY c.Apellido, e.Apellido");
                selectQuery.AppendLine("OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY");

                texto = $"%{texto}%";
                var parametros = new
                {
                    texto,
                    clienteid,
                    empleadoid,
                    cantidadRegistros = registrosPorPagina * (paginaActual - 1),
                    cantidadPorPagina = registrosPorPagina
                };

                lista = conn.Query<TelefonoDto>(selectQuery.ToString(), parametros).ToList();
            }
            return lista;
        }
    }
}
