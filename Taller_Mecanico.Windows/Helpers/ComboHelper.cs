using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.HorasLaborales;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;

namespace Taller_Mecanico.Windows.Helpers
{
    public static class ComboHelper
    {
        public static void CargarComboTipoCliente(ref ComboBox combo)
        {
            IServiciosDeTiposDeClientes serviciosTipoCliente = new ServiciosDeTiposDeClientes();
            var lista = serviciosTipoCliente.GetTiposDeClientes();
            var defaultPais = new TiposDeClientes()
            {
                IdTipoCliente = 0,
                TipoCliente = "Seleccione el Tipo de Cliente"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "TipoCliente";
            combo.ValueMember = "IdTipoCliente";
            combo.SelectedIndex = 0;
        }
        public static void CargarComboTipoDePago(ref ComboBox combo)
        {
            IServiciosTipoDePago serviciosTipoPago = new ServiciosTiposDePagos();
            var lista =serviciosTipoPago.GetTipoDePagos();
            var defaultPais = new TipoDePagos()
            {
                IdTipoPagos = 0,
                NombreDePago = "Seleccione el Tipo de Pago"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreDePago";
            combo.ValueMember = "IdTipoPagos";
            combo.SelectedIndex = 0;
        }
        public static void CargarComboMarcas(ref ComboBox combo)
        {
            IServiciosMarcas serviciosMarca = new ServiciosMarcas();
            var lista = serviciosMarca.GetMarcas();
            var defaultMarca = new Marca()
            {
                MarcaId = 0,
                NombreMarca = "Seleccione Marca"
            };
            lista.Insert(0, defaultMarca);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreMarca";
            combo.ValueMember = "MarcaId";
            combo.SelectedIndex = 0;
        }

        public static void CargarComboRol(ref ComboBox combo)
        {
            IServiciosDeRoles serviciosRol = new ServiciosRoles();
            var lista = serviciosRol.GetRoles();
            var defaultCategoria = new Roles()
            {
                IdRolEmpleado = 0,
                Rol = "Seleccione Rol"
            };
            lista.Insert(0, defaultCategoria);
            combo.DataSource = lista;
            combo.DisplayMember = "Rol";
            combo.ValueMember = "IdRolEmpleado";
            combo.SelectedIndex = 0;
        }

        internal static void CargarComboTipoVehiculo(ref ComboBox combo)
        {
            IServiciosTipoDeVehiculo serviciosTipoVehiculo = new SeviciosTipoDeVehiculo();
            var lista = serviciosTipoVehiculo.GetTipoVehiculos();
            var defaultTipoVehiculo = new TipoVehiculo()
            {
                IdTipoVehiculo=0,
                NombreTipoVehiculo="Seleccione el Tipo de Vehiculo"
            };
            lista.Insert(0, defaultTipoVehiculo);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreTipoVehiculo";
            combo.ValueMember = "IdTipoVehiculo";
            combo.SelectedIndex = 0;
        }

        internal static void CargarComboModelo(ref ComboBox combo)
        {
            IServicioModelos serviciosModelos = new ServicioModelos();
            var lista = serviciosModelos.GetModelosCombos();
            var defaultModelos = new Model()
            {
                IdModelo = 0,
                Modelo = "Seleccione el Modelo"
            };
            lista.Insert(0, defaultModelos);
            combo.DataSource = lista;
            combo.DisplayMember = "Modelo";
            combo.ValueMember = "IdModelo";
            combo.SelectedIndex = 0;
        }

        public static void CargarComboClientesPersonas(ref ComboBox combo)
        {
            IServiciosClientes serviciosClientes = new ServiciosClientes();
            var lista = serviciosClientes.GetClientesCombos();
            var defaultProveedor = new Clientes()
            {
                IdCliente = 0,
                Documento="Seleccione Cliente"
            };
            lista.Insert(0, defaultProveedor);
            combo.DataSource = lista;
            combo.DisplayMember = "Documento";
            combo.ValueMember = "IdCliente";
            combo.SelectedIndex = 0;
        }
        public static void CargarComboClientesEmpresas(ref ComboBox combo)
        {
            IServiciosClientes serviciosClientes = new ServiciosClientes();
            var lista = serviciosClientes.GetClientesCombosEmpresa();
            var defaultProveedor = new Clientes()
            {
                IdCliente = 0,
                CUIT = "Seleccione Empresa"
            };
            lista.Insert(0, defaultProveedor);
            combo.DataSource = lista;
            combo.DisplayMember = "CUIT";
            combo.ValueMember = "IdCliente";
            combo.SelectedIndex = 0;
        }

        internal static void CargarComboEmpleados(ref ComboBox combo)
        {
            IServiciosEmpleados serviciosEmpleados = new ServiciosEmpleados();
            var lista = serviciosEmpleados.GetEmpleadosCombos();
            var defaultEmpleado = new Empleado()
            {
                IdEmpleado = 0,
                Documento = "Seleccione Empleado"
            };
            lista.Insert(0, defaultEmpleado);
            combo.DataSource = lista;
            combo.DisplayMember = "Documento";
            combo.ValueMember = "IdEmpleado";
            combo.SelectedIndex = 0;
        }
        internal static void CargarComboVehiculos(ref ComboBox combo)
        {
            IServiciosVehiculos serviciosVehiculos = new ServiciosVehiculos();
            var lista = serviciosVehiculos.GetVehiculoCombos();
            var defaultEmpleado = new Vehiculos()
            {
                IdVehiculo = 0,
                Patente = "Seleccione el Vehiculo"
            };
            lista.Insert(0, defaultEmpleado);
            combo.DataSource = lista;
            combo.DisplayMember = "Patente";
            combo.ValueMember = "IdVehiculo";
            combo.SelectedIndex = 0;
        }
        internal static void CargarComboReservas(ref ComboBox combo)
        {
            IServiciosReservas serviciosReserva = new ServiciosReservas();
            var lista = serviciosReserva.GetReservasCombos();
            var defaultEmpleado = new ReservaComboDto()
            {
                IdReserva = 0,
                Info="Seleccione la Reserva"
            };
            lista.Insert(0, defaultEmpleado);
            combo.DataSource = lista;
            combo.DisplayMember = "Info";
            combo.ValueMember = "IdReserva";
            combo.SelectedIndex = 0;
        }
        internal static void CargarComboHistoriales(ref ComboBox combo)
        {
            IServiciosHistoriales serviciosHistoriales = new ServiciosHistoriales();
            var lista = serviciosHistoriales.GetHistorialesCombos();
            var defaultEmpleado = new HistorialComboDto()
            {
                IdHistorial=0,
                Info="Seleccione el Historial"
                
            };
            lista.Insert(0, defaultEmpleado);
            combo.DataSource = lista;
            combo.DisplayMember = "Info";
            combo.ValueMember = "IdHistorial";
            combo.SelectedIndex = 0;
        }
        internal static void CargarComboHorasLaborales(ref ComboBox combo)
        {
            IServiciosDeHorasLaborales serviciosHorasLaborales = new ServiciosDeHorasLaborales();
            var lista = serviciosHorasLaborales.GetHorasLaboralesCombo();
            var defaultEmpleado = new HorasLaboralesComboDto()
            {
                IdHorasLaborales=0,
                Info="Seleccione una Jornada"
            };
            lista.Insert(0, defaultEmpleado);
            combo.DataSource = lista;
            combo.DisplayMember = "Info";
            combo.ValueMember = "IdHorasLaborales";
            combo.SelectedIndex = 0;
        }
        internal static void CargarComboMovimiento(ref ComboBox combo)
        {
            IServiciosMovimientos serviciosMovimientos = new ServiciosMovimientos();
            var lista = serviciosMovimientos.GetMovimientosCombos();
            var defaultEmpleado = new Movimientos()
            {
                IdMovimiento = 0,
                Servicio = "Seleccione el Servicio"
            };
            lista.Insert(0, defaultEmpleado);
            combo.DataSource = lista;
            combo.DisplayMember = "Servicio";
            combo.ValueMember = "IdMovimiento";
            combo.SelectedIndex = 0;
        }
    }
}
