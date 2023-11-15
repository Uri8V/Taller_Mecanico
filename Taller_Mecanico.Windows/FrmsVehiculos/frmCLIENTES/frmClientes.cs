using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
            _servicio = new ServiciosClientes();
            _servicioTipoCliente = new ServiciosDeTiposDeClientes();
        }
        private List<ClienteDto> lista;
        private IServiciosClientes _servicio;
        private IServiciosDeTiposDeClientes _servicioTipoCliente;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? tipo = null;

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            RcargarGrilla();
        }

        private void RcargarGrilla()
        {
            registros = _servicio.GetCantidad(null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            lista=_servicio.GetClientesPorPagina(registrosPorPagina, paginaActual, tipo);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
           dgvDatos.Rows.Clear();
           foreach (var item in lista)
           {
             DataGridViewRow r=GridHelpers.ConstruirFila(dgvDatos);
                GridHelpers.SetearFila(r, item);
                GridHelpers.AgregarFila(dgvDatos, r);
           }
           lblRegistros.Text=registros.ToString();
           lblPaginas.Text=paginas.ToString();
           lblPaginaActual.Text=paginaActual.ToString();
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
            RcargarGrilla();
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            toolStripButtonFiltrar.BackColor = SystemColors.Control;
            toolStripButtonEditar.Enabled = true;
            toolStripButtonBorrar.Enabled = true;
            toolStripButtonAgregar.Enabled = true;
            toolStripButtonFiltrar.Enabled = true;
        }

        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmClienteAE frm= new frmClienteAE();
            DialogResult DR= frm.ShowDialog(this);
            if (DR == DialogResult.Cancel)
            {
                return;
            }
            var cliente = frm.GetCliente();
            _servicio.Guardar(cliente);
            registros = _servicio.GetCantidad(null);
            paginas=formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ClienteDto clienteABorrar= (ClienteDto)r.Tag;

            DialogResult dr = MessageBox.Show($"¿Desea eliminar el cliente: {clienteABorrar.Apellido}, {clienteABorrar.Nombre}, {clienteABorrar.Documento}?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            GridHelpers.QuitarFila(dgvDatos, r);
            _servicio.Borrar(clienteABorrar.IdCliente);
            RcargarGrilla();
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ClienteDto clienteDto = (ClienteDto)r.Tag;
            ClienteDto CopiaCliente= (ClienteDto)clienteDto.Clone();

            Clientes clientes= _servicio.GetClientePorId(clienteDto.IdCliente);
            try
            {
                frmClienteAE frm = new frmClienteAE() { Text = "Editar Cliente" };
                frm.SetCliente(clientes);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaCliente);

                    return;
                }
                clientes = frm.GetCliente();
                if (clientes != null)
                {
                    //Crear el dto
                    clienteDto.IdCliente = clientes.IdCliente;
                    clienteDto.Nombre = clientes.Nombre;
                    clienteDto.Apellido = clientes.Apellido;
                    clienteDto.Documento=clientes.Documento;
                    clienteDto.Domicilio=clientes.Domicilio;
                    clienteDto.CUIT=clientes.CUIT;
                    //clienteDto.TipoCliente = clientes.TiposDeClientes.TipoCliente;
                    clienteDto.TipoCliente = _servicioTipoCliente.GetClientesPorId(clientes.IdTipoCliente).TipoCliente;
                    GridHelpers.SetearFila(r, clienteDto);
                    _servicio.Guardar(clientes);
                }
                else
                {
                    //Recupero la copia del dto
                    GridHelpers.SetearFila(r, CopiaCliente);

                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaCliente);
                throw;
            }
        }

        private void toolStripButtonFiltrar_Click(object sender, EventArgs e)
        {
            frmSeleccionarTipoCliente frm= new frmSeleccionarTipoCliente();
            DialogResult dr = frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                return;
            }
            var tipocliente = frm.GetTipoCliente();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
