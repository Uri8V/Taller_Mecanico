using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmMOVIMIENTOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOSSERVICIOS
{
    public partial class frmVehiculosServicios : Form
    {
        public frmVehiculosServicios()
        {
            InitializeComponent();
            _servicio = new ServiciosVehiculosServicios();
            _serviciosClientes = new ServiciosClientes();
            _serviciosVehiculos = new ServiciosVehiculos();
            _servicioMovimientos = new ServiciosMovimientos();
        }

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmVehiculosServicios_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        private List<VehiculosServiciosDto> lista;
        private IServiciosVehiculosServicios _servicio;
        private IServiciosMovimientos _servicioMovimientos;
        private IServiciosVehiculos _serviciosVehiculos;
        private IServiciosClientes _serviciosClientes;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? IdVehiculo = null;


        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();

            HabilitarBotones();
        }

        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            lista = _servicio.GetVehiculoServicioPorPagina(registrosPorPagina, paginaActual, IdVehiculo);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelpers.LimpiarGrilla(dgvDatos);
            foreach (var item in lista)
            {
                DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                GridHelpers.SetearFila(r, item);
                GridHelpers.AgregarFila(dgvDatos, r);
            }
            lblRegistros.Text = registros.ToString();
            lblPaginas.Text = paginas.ToString();
            lblPaginaActual.Text = paginaActual.ToString();
        }

        private void HabilitarBotones()
        {
            toolStripButtonFiltrar.BackColor = SystemColors.Control;
            toolStripButtonEditar.Enabled = true;
            toolStripButtonBorrar.Enabled = true;
            toolStripButtonAgregar.Enabled = true;
            toolStripButtonFiltrar.Enabled = true;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual == paginas)
            {
                return;
            }
            paginaActual++;
            MostrarPaginado();

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual == 1)
            {
                return;
            }
            paginaActual--;
            MostrarPaginado();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = paginas;
            MostrarPaginado();
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            MostrarPaginado();
        }
        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmVehiculosServiciosAE frm = new frmVehiculosServiciosAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var Servicios = frm.GetServicio();
            //preguntar si existe
            _servicio.Guardar(Servicios);
            MessageBox.Show("Servicio agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
            registros = _servicio.GetCantidad(null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            VehiculosServiciosDto ServicioABorrar = (VehiculosServiciosDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el Servicio {ServicioABorrar.Servicio} del Cliente {ServicioABorrar.Apellido.ToUpper()}, {ServicioABorrar.Nombre} ({ServicioABorrar.Documento}) con el vehiculo de la patente ({ServicioABorrar.Patente}) el cual debe (${ServicioABorrar.DebeServicio})?", "Confirmar Selección", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            //Falta metodo de objeto relacionado
            GridHelpers.QuitarFila(dgvDatos, r);
            _servicio.Borrar(ServicioABorrar.IdVehiculoSevicios);
            RecargarGrilla();
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            VehiculosServiciosDto vehiculosServiciosDto = (VehiculosServiciosDto)r.Tag;
            VehiculosServiciosDto CopiaServicio = (VehiculosServiciosDto)vehiculosServiciosDto.Clone();

            VehiculosServicios servicios = _servicio.GetVehiculoServicioPorId(vehiculosServiciosDto.IdVehiculoSevicios);
            try
            {
                frmVehiculosServiciosAE frm = new frmVehiculosServiciosAE() { Text = "Editar Servicio" };
                frm.SetServicio(servicios);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaServicio);

                    return;
                }
                servicios = frm.GetServicio();
                if (servicios != null)
                {
                    //Crear el dto
                   vehiculosServiciosDto.IdVehiculoSevicios= servicios.IdVehiculoServicios;
                    vehiculosServiciosDto.Patente = _serviciosVehiculos.GetVehiculoPorId(servicios.IdVehiculo).Patente;
                    vehiculosServiciosDto.Servicio = _servicioMovimientos.GetMovimientosPorId(servicios.IdMovimiento).Servicio;
                    vehiculosServiciosDto.DebeServicio = _servicioMovimientos.GetMovimientosPorId(servicios.IdMovimiento).Debe;
                    vehiculosServiciosDto.Senia=_servicioMovimientos.GetMovimientosPorId(servicios.IdMovimiento).Senia;
                    vehiculosServiciosDto.Apellido = _serviciosClientes.GetClientePorId(servicios.IdCliente).Apellido;
                    vehiculosServiciosDto.Nombre = _serviciosClientes.GetClientePorId(servicios.IdCliente).Nombre;
                    vehiculosServiciosDto.Documento = _serviciosClientes.GetClientePorId(servicios.IdCliente).Documento;
                    vehiculosServiciosDto.Descripcion = servicios.Descripcion;
                    vehiculosServiciosDto.Debe= servicios.Debe;
                    vehiculosServiciosDto.Haber= servicios.Haber;
                    vehiculosServiciosDto.Fecha= servicios.Fecha;
                    GridHelpers.SetearFila(r, vehiculosServiciosDto);
                    _servicio.Guardar(servicios);
                }
                else
                {
                    //Recupero la copia del dto
                    GridHelpers.SetearFila(r, servicios);

                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaServicio);
                throw;
            }
        }
    }
}
