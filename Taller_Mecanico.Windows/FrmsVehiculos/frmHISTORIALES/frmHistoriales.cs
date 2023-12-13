using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmRESERVAS;
using Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmHISTORIALES
{
    public partial class frmHistoriales : Form
    {
        public frmHistoriales()
        {
            InitializeComponent();
            _servicio = new ServiciosHistoriales();
            _servicioEmpleados = new ServiciosEmpleados();
            _serviciosVehiculos = new ServiciosVehiculos();
            serviciosReservas= new ServiciosReservas();
            _serviciosClientes= new ServiciosClientes();
        }

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private List<HistorialDto> lista;
        private IServiciosHistoriales _servicio;
        private IServiciosEmpleados _servicioEmpleados;
        private IServiciosReservas serviciosReservas;
        private IServiciosVehiculos _serviciosVehiculos;
        private IServiciosClientes _serviciosClientes;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? IdCliente = null;
        int? IdEmpleado = null;
        DateTime? Fecha = null;

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            IdCliente= null;
            IdEmpleado= null;
            Fecha= null;
            RecargarGrilla();
            HabilitarBotones();
        }

        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null,null,null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            if (IdCliente==null && IdEmpleado==null && Fecha==null)
            {
                lista = _servicio.GetHistorialesPorPagina(registrosPorPagina, paginaActual, IdCliente, IdEmpleado, Fecha);
            }
            else
            {
                lista = _servicio.GetHistorialesPorPagina(registrosPorPagina, paginaActual, IdCliente, IdEmpleado, Fecha);
            }
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
            btnAnterior.Enabled = true;
            btnPrimero.Enabled = true;
            btnSiguiente.Enabled = true;
            btnUltimo.Enabled = true;
        }

        private void frmHistoriales_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
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
            frmHistorialesAE frm = new frmHistorialesAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var historial = frm.GetHistorial();
            //preguntar si existe
            if (!_servicio.Existe(historial))
            {
                _servicio.Guardar(historial);
                MessageBox.Show("Historial agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null, null, null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado(); 
            }
            else
            {
                MessageBox.Show("El Historial ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            HistorialDto historialABorrar = (HistorialDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar la Historial: Patente:{historialABorrar.Patente}| Fecha:{historialABorrar.FechaEntrada.ToShortDateString()}, {historialABorrar.HoraEntrada}| CLIENTE:{historialABorrar.ApellidoCliente.ToUpper()}, {historialABorrar.NombreCliente} DOC:{historialABorrar.DocumentoCliente}?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            Historiales historial = _servicio.GetHistorialPorId(historialABorrar.IdHistorial);
            //Falta metodo de objeto relacionado
            if (!_servicio.EstaRelacionada(historial))
            {
                GridHelpers.QuitarFila(dgvDatos, r);
                _servicio.Borrar(historialABorrar.IdHistorial);
                RecargarGrilla();
            }
            else
            {
                MessageBox.Show("El Historial no se puede borrar porque esta relacionado con algún sueldo", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            HistorialDto historialDto = (HistorialDto)r.Tag;
            HistorialDto CopiaHistorial = (HistorialDto)historialDto.Clone();

            Historiales historiales = _servicio.GetHistorialPorId(historialDto.IdHistorial);
            try
            {
                frmHistorialesAE frm = new frmHistorialesAE() { Text = "Editar Historial" };
                frm.SetHistorial(historiales);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaHistorial);

                    return;
                }
                historiales = frm.GetHistorial();
                if (!_servicio.Existe(historiales))
                {
                    if (historiales != null)
                    {
                        //Crear el dto
                        historialDto.IdHistorial = historiales.IdHistorial;
                        historialDto.FechaEntrada = serviciosReservas.GetReservasPorId(historiales.IdReserva).FechaEntrada;
                        historialDto.HoraEntrada = serviciosReservas.GetReservasPorId(historiales.IdReserva).HoraEntrada;
                        historialDto.ValorPorHoraExtra = historiales.ValorPorHoraExtra;
                        historialDto.ValorPorHora = historiales.ValorPorHora;
                        historialDto.Apellido = _servicioEmpleados.GetEmpleadoPorId(historiales.IdEmpleado).Apellido;
                        historialDto.Nombre = _servicioEmpleados.GetEmpleadoPorId(historiales.IdEmpleado).Nombre;
                        historialDto.Documento = _servicioEmpleados.GetEmpleadoPorId(historiales.IdEmpleado).Documento;
                        historialDto.Patente = _serviciosVehiculos.GetVehiculoPorId(historiales.IdVehiculo).Patente;
                        historialDto.NombreCliente = _serviciosClientes.GetClientePorId(serviciosReservas.GetReservasPorId(historiales.IdReserva).IdCliente).Nombre;
                        historialDto.ApellidoCliente = _serviciosClientes.GetClientePorId(serviciosReservas.GetReservasPorId(historiales.IdReserva).IdCliente).Apellido;
                        historialDto.DocumentoCliente = _serviciosClientes.GetClientePorId(serviciosReservas.GetReservasPorId(historiales.IdReserva).IdCliente).Documento;
                        GridHelpers.SetearFila(r, historialDto);
                        _servicio.Guardar(historiales);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, historiales);

                    } 
                }
                else
                {
                    MessageBox.Show("El Historial ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaHistorial);
                throw;
            }
        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSeleccionarEmpleado frm = new frmSeleccionarEmpleado();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var empleadoSeleccionado = frm.GetEmpleado();
            registros = _servicio.GetCantidad(null, empleadoSeleccionado.IdEmpleado, null);
            IdEmpleado = empleadoSeleccionado.IdEmpleado;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            paginaActual = formHelper.RetornoPrimerPagina(registrosPorPagina, paginaActual);
            MostrarPaginado();
            DesabilitarBotones();
        }
        private void DesabilitarBotones()
        {
            toolStripButtonFiltrar.BackColor = Color.DarkViolet;
            toolStripButtonEditar.Enabled = false;
            toolStripButtonBorrar.Enabled = false;
            toolStripButtonAgregar.Enabled = false;
            toolStripButtonFiltrar.Enabled = false;
            if (paginas == 1)
            {
                btnAnterior.Enabled = false;
                btnPrimero.Enabled = false;
                btnSiguiente.Enabled = false;
                btnUltimo.Enabled = false;
            }
            else
            {
                btnAnterior.Enabled = true;
                btnPrimero.Enabled = true;
                btnSiguiente.Enabled = true;
                btnUltimo.Enabled = true;
            }

        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSeleccionarCliente frm = new frmSeleccionarCliente();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var clienteSeleccionado = frm.GetCliente();
            registros = _servicio.GetCantidad(clienteSeleccionado.IdCliente, null, null);
            IdCliente = clienteSeleccionado.IdCliente;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            paginaActual = formHelper.RetornoPrimerPagina(registrosPorPagina, paginaActual);
            MostrarPaginado();
            DesabilitarBotones();
        }

        private void fechaEntradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHorariosLaboralesFiltro frm = new frmHorariosLaboralesFiltro();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            DateTime FechaSeleccionado = frm.GetFecha();
            registros = _servicio.GetCantidad(IdCliente, IdEmpleado, FechaSeleccionado);
            Fecha = FechaSeleccionado;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            paginaActual = formHelper.RetornoPrimerPagina(registrosPorPagina, paginaActual);
            MostrarPaginado();
            DesabilitarBotones();
        }
    }
}
