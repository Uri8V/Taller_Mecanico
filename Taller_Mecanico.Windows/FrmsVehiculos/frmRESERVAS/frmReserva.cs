using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Reservas;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmRESERVAS
{
    public partial class frmReserva : Form
    {
        public frmReserva()
        {
            InitializeComponent();
            _servicio = new ServiciosReservas();
            _servicioClientes = new ServiciosClientes();
        }
        private List<ReservaDto> lista;
        private IServiciosReservas _servicio;
        private IServiciosClientes _servicioClientes;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? cliente = null;
        DateTime? fecha = null;

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmReserva_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null, null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            if (fecha == null && cliente == null)
            {
                lista = _servicio.GetReservasPorPagina(registrosPorPagina, paginaActual, cliente, fecha);
            }
            else
            {
                lista = _servicio.GetReservasPorPagina(registrosPorPagina, paginaActual, cliente, fecha);
            }
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
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

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            cliente = null;
            fecha = null;
            RecargarGrilla();
            HabilitarBotones();
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
        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmReservasAE frm = new frmReservasAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var reserva = frm.GetReserva();
            //preguntar si existe
            if (!_servicio.Existe(reserva))
            {
                _servicio.Guardar(reserva);
                MessageBox.Show("Reserva Agregada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null, null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado(); 
            }
            else
            {
                MessageBox.Show("La Reserva ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ReservaDto reservaABorrar = (ReservaDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar la Reserva: {reservaABorrar.Apellido}, {reservaABorrar.Nombre} ({reservaABorrar.Documento} {reservaABorrar.CUIT})?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            //Falta metodo de objeto relacionado
            Reservas RESERVAaborrar = _servicio.GetReservasPorId(reservaABorrar.IdReserva);
            if (!_servicio.EstaRelacionada(RESERVAaborrar))
            {
                GridHelpers.QuitarFila(dgvDatos, r);
                _servicio.Borrar(reservaABorrar.IdReserva);
                RecargarGrilla();
            }
            else
            {
                MessageBox.Show("La Reserva no se puede borrar porque esta relacionada con un Historial", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ReservaDto reservaDto = (ReservaDto)r.Tag;
            ReservaDto CopiaReserva = (ReservaDto)reservaDto.Clone();

            Reservas reservas = _servicio.GetReservasPorId(reservaDto.IdReserva);
            try
            {
                frmReservasAE frm = new frmReservasAE() { Text = "Editar Reserva" };
                frm.SetReserva(reservas);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaReserva);

                    return;
                }
                reservas = frm.GetReserva();
                if (!_servicio.Existe(reservas))
                {
                    if (reservas != null)
                    {
                        //Crear el dto
                        reservaDto.IdReserva = reservas.IdReserva;
                        reservaDto.SePresento = reservas.SePresento;
                        reservaDto.EsSobreturno = reservas.EsSobreturno;
                        reservaDto.Nombre = _servicioClientes.GetClientePorId(reservas.IdCliente).Nombre;
                        reservaDto.Apellido = _servicioClientes.GetClientePorId(reservas.IdCliente).Apellido;
                        reservaDto.Documento = _servicioClientes.GetClientePorId(reservas.IdCliente).Documento;
                        reservaDto.CUIT = _servicioClientes.GetClientePorId(reservas.IdCliente).CUIT;
                        reservaDto.FechaEntrada = reservas.FechaEntrada;
                        reservaDto.HoraEntrada = reservas.HoraEntrada;
                        reservaDto.FechaSalida = reservas.FechaSalida;
                        reservaDto.HoraSalida = reservas.HoraSalida;
                        GridHelpers.SetearFila(r, reservaDto);
                        _servicio.Guardar(reservas);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, reservas);

                    } 
                }
                else
                {
                    MessageBox.Show("La Reserva ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaReserva);
                throw;
            }
        }

        private void fechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHorariosLaboralesFiltro frm = new frmHorariosLaboralesFiltro();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            DateTime Fecha = frm.GetFecha();
            registros = _servicio.GetCantidad(cliente, Fecha);
            fecha = Fecha;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
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
            var Cliente = frm.GetCliente();
            registros = _servicio.GetCantidad(Cliente.IdCliente, fecha);
            cliente = Cliente.IdCliente;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            DesabilitarBotones();
            MostrarPaginado();
        }
    }
}
