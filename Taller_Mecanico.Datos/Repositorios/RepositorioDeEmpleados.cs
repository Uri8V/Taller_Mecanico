﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller_Mecanico.Comun.Interfaces;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Datos.Repositorios
{
    public class RepositorioDeEmpleados : IRepositorioDeEmpleados
    {
        private readonly string CadenaConexion;
        public RepositorioDeEmpleados()
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Empleado empleado)
        {
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"INSERT INTO Empleados (Nombre, Apellido, Documento, IdRolEmpleado) 
                                    Values (@Nombre, @Apellido, @Documento, @IdRolEmpleado); SELECT SCOPE_IDENTITY();";
                int id = conn.ExecuteScalar<int>(selectQuery, empleado);
                empleado.IdEmpleado = id;
            }
        }

        public void Borrar(int empleadoId)
        {
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string deleteQuery = "DELETE FROM Empleados WHERE IdEmpleado=@IdEmpleado";
                conn.Execute(deleteQuery, new { IdEmpleado=empleadoId });

            }
        }

        public void Editar(Empleado empleado)
        {
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string updateQuery = @"UPDATE Empleados SET Nombre=@Nombre, Apellido=@Apellido, Documento=@Documento, IdRolEmpleado=@IdRolEmpleado 
                WHERE IdEmpleado=@IdEmpleado";
                conn.Execute(updateQuery, empleado);
            }
        }

        public bool EstaRelacionada(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Empleado empleado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad(int? empleadoId)
        {
            int cantidad = 0;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery;
                if (empleadoId == null)
                {
                    selectQuery = "SELECT COUNT(*) FROM Empleados";
                    cantidad = conn.ExecuteScalar<int>(selectQuery);
                }
                else
                {
                    selectQuery = @"SELECT COUNT(*) FROM Empleados 
                        WHERE IdRolEmpleado=@empleadoId";
                    cantidad = conn.ExecuteScalar<int>(selectQuery, new { IdRolEmpleado = empleadoId });
                }
            }
            return cantidad;
        }

        public List<EmpleadoDto> GetEmpleadosPorPagina(int registrosPorPagina, int paginaActual, int? roles)
        {
            List<EmpleadoDto> lista = new List<EmpleadoDto>();
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery;
                if (roles != null)
                {
                    selectQuery = @"SELECT IdEmpleado, e.Nombre, e.Apellido, e.Documento, r.Rol FROM Empleados e
                                  INNER JOIN Roles r ON r.IdRolEmpleado=e.IdRolEmpleado
                                  WHERE r.RolEmpleado=@roles 
                                  ORDER BY e.Apellido, e.Nombre 
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = registrosPorPagina * (paginaActual - 1);

                    lista = conn.Query<EmpleadoDto>(selectQuery, new
                    {
                        rol= roles.Value,
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = registrosPorPagina
                    }).ToList();
                }
                else
                {
                    selectQuery = @"SELECT IdEmpleado, e.Nombre, e.Apellido, e.Documento, r.Rol FROM Empleados e
                                  INNER JOIN Roles r ON r.IdRolEmpleado=e.IdRolEmpleado
                                  ORDER BY e.Apellido, e.Nombre 
                                  OFFSET @cantidadRegistros ROWS FETCH NEXT @CantidadPorPagina ROWS ONLY";
                    var registrosSateados = registrosPorPagina * (paginaActual - 1);

                    lista = conn.Query<EmpleadoDto>(selectQuery, new
                    {
                        cantidadRegistros = registrosSateados,
                        cantidadPorPagina = registrosPorPagina
                    }).ToList();
                }
                return lista;
            }
        }

        public Empleado GetEmpleadoPorId(int empleadoId)
        {
            Empleado empleado = null;
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"SELECT IdEmpleado, Nombre, Apellido, Documento, IdRolEmpleado 
                    FROM Empleados WHERE IdEmpleado=@IdEmpleado";
                empleado = conn.QuerySingleOrDefault<Empleado>(selectQuery,
                    new { IdEmpleado = empleadoId });
            }
            return empleado;
        }

        public List<Empleado> GetEmpleadosCombos()
        {
            List<Empleado> lista = new List<Empleado>();
            using (var conn = new SqlConnection(CadenaConexion))
            {
                string selectQuery = @"SELECT e.IdEmpleado, e.Documento FROM Empleados e ";
                lista = conn.Query<Empleado>(selectQuery).ToList();
            }
            return lista;
        }
    }
}
