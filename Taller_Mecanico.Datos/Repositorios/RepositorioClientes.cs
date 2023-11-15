using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioClientes:IRepositorioClientes
    {
        private readonly string cadenaConexion;

        public RepositorioClientes()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Clientes cliente)
        {
            using (var conn= new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"INSERT INTO Clientes (Nombre, Apellido, Documento, Domicilio, CUIT, IdTipoCliente) 
                                    Values (@Nombre, @Apellido, @Documento, @Domicilio, @CUIT, @IdTipoCliente); SELECT SCOPE_IDENTITY();";
                int id=conn.ExecuteScalar<int>(selectQuery, cliente);
                cliente.IdCliente = id;
            }
        }

        public void Borrar(int clienteId)
        {
            using (var conn= new SqlConnection(cadenaConexion))
            {
                string deleteQuery = "DELETE FROM Clientes WHERE IdCliente=@IdCliente";
                conn.Execute(deleteQuery, new { IdCliente = clienteId });

            }
        }

        public void Editar(Clientes cliente)
        {
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string updateQuery = @"UPDATE Clientes SET Nombre=@Nombre, Apellido=@Apellido, Documento=@Documento, Domicilio=@Domicilio, CUIT=@CUIT,
                    IdTipoCliente=@IdTipoCliente WHERE IdCliente=@IdCliente";
                conn.Execute(updateQuery, cliente);
            }
        }

        public bool EstaRelacionada(Clientes cliente)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Clientes cliente)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? TipoClienteId)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (TipoClienteId == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Clientes";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Clientes 
                        WHERE IdTipoCliente=@IdTipoCliente";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new {IdTipoCliente  = TipoClienteId });
                }
            }
            return cantidad;
        }

        public List<Clientes> GetClientesCombos()
        {
            List<Clientes> lista = new List<Clientes>();
            using (var conn= new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT c.IdCliente, c.Documento FROM Clientes c ";
                lista = conn.Query<Clientes>(selectQuery).ToList();
            }
            return lista;
        }

        public Clientes GetClientePorId(int clienteId)
        {
            Clientes cliente = null;
            using (var conn = new SqlConnection(cadenaConexion))
            {
                string selectQuery = @"SELECT IdCliente, Nombre, Apellido, Documento, Domicilio, CUIT, IdTipoCliente 
                    FROM Clientes WHERE IdCliente=@IdCliente";
                cliente = conn.QuerySingleOrDefault<Clientes>(selectQuery,
                    new { IdCliente = clienteId });
            }
            return cliente;
        }

        public List<ClienteDto> GetClientesPorPagina(int cantidad, int paginaActual, int? tipo)
        {
            List<ClienteDto> lista= new List<ClienteDto>();
            using (var conn= new SqlConnection(cadenaConexion))
            {
                string selectQuery;
                if (tipo != null)
                {
                    selectQuery = @"SELECT IdCliente, c.Nombre, c.Apellido, c.Documento, c.Domicilio, c.CUIT, t.TipoCliente FROM Clientes c
                                  INNER JOIN TiposDeClientes t ON c.IdTipoCliente=t.IdTipoCLiente WHERE t.TipoCliente=@tipo ORDER BY Apellido, Nombre 
                                  OFF SET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = cantidad * (paginaActual - 1);

                    lista = conn.Query<ClienteDto>(selectQuery, new
                    {
                        tipo=tipo.Value,
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = cantidad
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT IdCliente, c.Nombre, c.Apellido, c.Documento, c.Domicilio, c.CUIT, t.TipoCliente FROM Clientes c
                                  INNER JOIN TiposDeClientes t ON c.IdTipoCliente=t.IdTipoCLiente ORDER BY Apellido, Nombre 
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = cantidad * (paginaActual - 1);

                    lista = conn.Query<ClienteDto>(selectQuery, new
                    {
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = cantidad
                    }).ToList();
                }
                return lista;
            }
        }
    }
}
