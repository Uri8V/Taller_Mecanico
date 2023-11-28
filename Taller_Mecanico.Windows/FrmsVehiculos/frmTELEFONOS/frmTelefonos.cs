using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Dtos.Telefono;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmTelefonos : Form
    {
        public frmTelefonos()
        {
            InitializeComponent();
            _servicio = new ServiciosTelefonos();
            _servicioEmpleados = new ServiciosEmpleados();
            _serviciosClientes= new ServiciosClientes();
        }
        private List<TelefonoDto> lista;
        private IServiciosTelefonos _servicio;
        private IServiciosEmpleados _servicioEmpleados;
        private IServiciosClientes _serviciosClientes;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? empleado = null;
        int? cliente = null;
        string Texto=null;
      
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            empleado = null;
            cliente=null;
            Texto=null;
            RecargarGrilla();
            HabilitarBotones();
        }

        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null, null,null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            if (empleado != null || cliente != null || Texto!=null)
            {
                lista = _servicio.GetTelefonosPorPagina(registrosPorPagina, paginaActual, cliente, empleado, Texto);
            }
            else
            {
                lista = _servicio.GetTelefonosPorPagina(registrosPorPagina, paginaActual, cliente, empleado, Texto);
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
        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
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

        private void frmTelefonos_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmTelefonosAE frm = new frmTelefonosAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var telefono = frm.GetTelefono();
            //preguntar si existe
            if (!_servicio.Existe(telefono))
            {
                _servicio.Guardar(telefono);
                MessageBox.Show("Telefono Agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null, null, null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado(); 
            }
            else
            {
                MessageBox.Show("El telefono ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            TelefonoDto telefonoABorrar = (TelefonoDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el Telefono: {telefonoABorrar.ApellidoCliente} {telefonoABorrar.NombreCliente}({telefonoABorrar.DocumentoCliente}) {telefonoABorrar.ApellidoEmpleado} {telefonoABorrar.NombreEmpleado}({telefonoABorrar.DocumentoEmpleado}), {telefonoABorrar.Telefono}?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            //Falta metodo de objeto relacionado
            GridHelpers.QuitarFila(dgvDatos, r);
            _servicio.Borrar(telefonoABorrar.IdTelefono);
            RecargarGrilla();
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            TelefonoDto telefonoDto = (TelefonoDto)r.Tag;
            TelefonoDto CopiaTelefono = (TelefonoDto)telefonoDto.Clone();

            Telefonos telefono = _servicio.GetTelefonoPorId(telefonoDto.IdTelefono);
            try
            {
                frmTelefonosAE frm = new frmTelefonosAE() { Text = "Editar Vehiculo" };
                frm.SetTelefono(telefono);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaTelefono);

                    return;
                }
                telefono = frm.GetTelefono();
                if (!_servicio.Existe(telefono))
                {
                    if (telefono != null)
                    {
                        //Crear el dto
                        telefonoDto.IdTelefono = telefono.IdTelefono;
                        telefonoDto.Telefono = telefono.Telefono;
                        telefonoDto.TipoDeTelefono = telefono.TipoDeTelefono;
                        if (telefono.IdEmpleado != 0)
                        {
                            telefonoDto.ApellidoEmpleado = _servicioEmpleados.GetEmpleadoPorId(telefono.IdEmpleado).Apellido;
                            telefonoDto.NombreEmpleado = _servicioEmpleados.GetEmpleadoPorId(telefono.IdEmpleado).Nombre;
                            telefonoDto.DocumentoEmpleado = _servicioEmpleados.GetEmpleadoPorId(telefono.IdEmpleado).Documento;
                        }
                        else
                        {
                            telefonoDto.ApellidoCliente = _serviciosClientes.GetClientePorId(telefono.IdCliente).Apellido;
                            telefonoDto.NombreCliente = _serviciosClientes.GetClientePorId(telefono.IdCliente).Nombre;
                            telefonoDto.DocumentoCliente = _serviciosClientes.GetClientePorId(telefono.IdCliente).Documento;
                        }
                        GridHelpers.SetearFila(r, telefonoDto);
                        _servicio.Guardar(telefono);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, telefono);

                    }

                }
                else
                {
                    MessageBox.Show("El telefono ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaTelefono);
                throw;
            }
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSeleccionarCliente frm = new frmSeleccionarCliente();
            DialogResult dr= frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                return;
            }
            var clienteSeleccionado = frm.GetCliente();
            registros = _servicio.GetCantidad(clienteSeleccionado.IdCliente,null, null);
            cliente = clienteSeleccionado.IdCliente;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetTelefonosPorPagina(registrosPorPagina, paginaActual, cliente, empleado, Texto);
            DesabilitarBotones();
            MostrarDatosEnGrilla();
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
            empleado = empleadoSeleccionado.IdEmpleado;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetTelefonosPorPagina(registrosPorPagina, paginaActual, cliente, empleado, Texto);
            DesabilitarBotones();
            MostrarDatosEnGrilla();
        }

        private void tipoDeTelefonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSeleccionarPorNombre frm = new frmSeleccionarPorNombre();
            DialogResult dr = frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                return;
            }
            var texto = frm.GetTexto();
            registros = _servicio.GetCantidad(null, null, texto);
            Texto = texto;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetTelefonosPorPagina(registrosPorPagina, paginaActual, cliente, empleado, Texto);
            DesabilitarBotones();
            MostrarDatosEnGrilla();
        }
    }
}
