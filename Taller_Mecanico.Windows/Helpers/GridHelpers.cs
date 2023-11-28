using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;

namespace Taller_Mecanico.Windows.Helpers
{
    public static class GridHelpers
    {
        public static void LimpiarGrilla(DataGridView dgv)
        {
            dgv.Rows.Clear();
        }
        public static DataGridViewRow ConstruirFila(DataGridView dgv)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dgv);
            return r;

        }
        public static void SetearFila(DataGridViewRow r, object obj)
        {
            switch (obj)
            {
                case Marca marca:
                    r.Cells[0].Value = marca.NombreMarca;
                    break;
                case TipoVehiculo tipo:
                    r.Cells[0].Value = tipo.NombreTipoVehiculo;
                    break;
                case TipoDePagos tipos:
                    r.Cells[0].Value = tipos.NombreDePago;
                    break;
                case TiposDeClientes tipos:
                    r.Cells[0].Value = tipos.TipoCliente;
                    break;
                case Roles rol:
                    r.Cells[0].Value = rol.Rol;
                    break;
                case HorasLaborales horas:
                    r.Cells[0].Value = horas.Fecha.ToShortDateString();
                    r.Cells[1].Value = horas.HoraInicio;
                    r.Cells[2].Value = horas.HoraFin;
                    r.Cells[3].Value = horas.horasExtras;
                    r.Cells[4].Value = horas.horaslaborales;
                    break;
                case ClienteDto clientes:
                    r.Cells[0].Value = clientes.Nombre;
                    r.Cells[1].Value = clientes.Apellido;
                    r.Cells[2].Value = clientes.Documento;
                    r.Cells[3].Value = clientes.Domicilio;
                    r.Cells[4].Value = clientes.CUIT;
                    r.Cells[5].Value = clientes.TipoCliente;
                    break;
                case ModelosDto modelo:
                    r.Cells[0].Value = modelo.Modelo;
                    r.Cells[1].Value = modelo.Marca;
                    break;
                case EmpleadoDto empleado:
                    r.Cells[0].Value = empleado.Nombre;
                    r.Cells[1].Value = empleado.Apellido;
                    r.Cells[2].Value = empleado.Documento;
                    r.Cells[3].Value = empleado.Rol;
                    break;
                case VehiculoDto vehiculo:
                    r.Cells[0].Value = vehiculo.Patente;
                    r.Cells[1].Value = vehiculo.Kilometros;
                    r.Cells[2].Value = vehiculo.TipoVehiculo;
                    r.Cells[3].Value = vehiculo.Modelo;
                    break;
                case TelefonoDto telefono:
                    if (!string.IsNullOrEmpty(telefono.DocumentoCliente) || !string.IsNullOrEmpty(telefono.CUIT))
                    {
                        if (!string.IsNullOrEmpty(telefono.DocumentoCliente))
                        { 
                            r.Cells[0].Value = $"{telefono.ApellidoCliente.ToUpper()}, {telefono.NombreCliente} ({telefono.DocumentoCliente})";
                        }
                        else
                        {
                            r.Cells[0].Value = $"{telefono.ApellidoCliente.ToUpper()}, {telefono.NombreCliente} ({telefono.CUIT})";
                        }
                        r.Cells[1].Value = "";
                        r.Cells[2].Value = telefono.Telefono;
                        r.Cells[3].Value = telefono.TipoDeTelefono;
                    }
                    else
                    {
                        r.Cells[0].Value = "";
                        r.Cells[1].Value = $"{telefono.ApellidoEmpleado.ToUpper()}, {telefono.NombreEmpleado} ({telefono.DocumentoEmpleado})";
                        r.Cells[2].Value = telefono.Telefono;
                        r.Cells[3].Value = telefono.TipoDeTelefono;
                    }
                    break;
                case ReservaDto reserva:
                    if (reserva.Documento != "")
                    {
                    r.Cells[0].Value = $"{reserva.Apellido.ToUpper()}, {reserva.Nombre} ({reserva.Documento})";

                    }
                    else
                    {
                        r.Cells[0].Value = $"{reserva.Apellido.ToUpper()}, {reserva.Nombre} ({reserva.CUIT})";
                    }
                    r.Cells[1].Value = reserva.FechaEntrada.ToShortDateString();
                    r.Cells[2].Value = reserva.HoraEntrada;
                    if (reserva.FechaSalida == new DateTime(2023, 01, 01) && reserva.HoraSalida == TimeSpan.Zero)
                    {
                        r.Cells[3].Value = "Se desconoce el día en el que el vehiculo este en condiciones";
                        r.Cells[4].Value = "Se desconoce la hora en el que el vehículo este en condiciones";
                    }
                    else
                    {
                        r.Cells[3].Value = reserva.FechaSalida.ToShortDateString();
                        r.Cells[4].Value = reserva.HoraSalida;
                    }
                    if (reserva.SePresento == true)
                    {
                        r.Cells[5].Value = "SI";
                    }
                    else
                    {
                        r.Cells[5].Value = "NO";
                    }
                    if (reserva.EsSobreturno == true)
                    {
                        r.Cells[6].Value = "SI";
                    }
                    else
                    {
                        r.Cells[6].Value = "NO";
                    }
                    break;
                case HistorialDto historial:
                    r.Cells[0].Value = $"{historial.Apellido.ToUpper()}, {historial.Nombre} ({historial.Documento})";
                    r.Cells[1].Value = historial.FechaEntrada.ToShortDateString();
                    r.Cells[2].Value = historial.HoraEntrada.ToString();
                    r.Cells[3].Value = $"{historial.ApellidoCliente.ToUpper()}, {historial.NombreCliente} ({historial.DocumentoCliente})";
                    r.Cells[4].Value=historial.ValorPorHora.ToString();
                    r.Cells[5].Value=historial.ValorPorHoraExtra.ToString();
                    r.Cells[6].Value = historial.Patente;
                    break;
                case SueldosDto sueldos:
                    r.Cells[0].Value = $"{sueldos.Apellido.ToUpper()}, {sueldos.Nombre} ({sueldos.Documento})";
                    r.Cells[1].Value = sueldos.Fecha.ToShortDateString();
                    r.Cells[2].Value = sueldos.HorasLaborales.ToString();
                    r.Cells[3].Value = sueldos.HorasExtras.ToString();
                    r.Cells[4].Value = sueldos.ValorPorHora.ToString();
                    r.Cells[5].Value = sueldos.ValorPorHoraExtra.ToString();
                    r.Cells[6].Value = sueldos.TotalAPagar.ToString();
                    r.Cells[7].Value = sueldos.TotalExtra.ToString();
                    break;
                case MovimientosDto movimientos:
                    r.Cells[0].Value = movimientos.Servicio;
                    r.Cells[1].Value = movimientos.Debe;
                    r.Cells[2].Value = movimientos.NombreDePago;
                    break;
                case VehiculosServiciosDto servicios:
                    r.Cells[0].Value = servicios.Patente;
                    r.Cells[1].Value = $"{servicios.Apellido.ToUpper()}, {servicios.Nombre} ({servicios.Documento} {servicios.CUIT})";
                    r.Cells[2].Value = $"{servicios.Servicio}, Debe:{servicios.DebeServicio}";
                    r.Cells[3].Value = (servicios.Debe - servicios.Haber).ToString();
                    if (servicios.Debe - servicios.Haber <= 0)
                    {
                        r.Cells[3].Style.BackColor=Color.Purple ;
                    }
                    else
                    {
                        r.Cells[3].Style.BackColor= Color.White;
                    }
                    r.Cells[4].Value = servicios.Haber;
                    r.Cells[5].Value = servicios.Descripcion;
                    if (servicios.Fecha!=new DateTime(2023,01,01))
                    {
                        r.Cells[6].Value = servicios.Fecha.ToShortDateString();
                    }
                    else
                    {
                        r.Cells[6].Value = "Aún no se realizó el Servicio";
                    }
                     break;
            }
            r.Tag = obj;

        }
        public static void AgregarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Add(r);
        }

        public static void QuitarFila(DataGridView dgv, DataGridViewRow r)
        {
            dgv.Rows.Remove(r);
        }

        internal static void MostrarDatosEnGrilla<T>(DataGridView dgv, List<T> lista)
        {
            GridHelpers.LimpiarGrilla(dgv);
            foreach (var obj in lista)
            {
                DataGridViewRow r = GridHelpers.ConstruirFila(dgv);
                GridHelpers.SetearFila(r, obj);
                GridHelpers.AgregarFila(dgv, r);
            }
        }
    }
}
