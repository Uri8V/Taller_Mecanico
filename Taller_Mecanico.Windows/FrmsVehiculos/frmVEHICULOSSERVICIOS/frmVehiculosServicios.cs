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
using Taller_Mecanico.Entidades.Dtos.Movimientos;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmFILTROS;
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
            lista = _servicio.GetVehiculoServicioPorPagina(registrosPorPagina,paginaActual,IdVehiculo,IDMovimiento,IdCliente,fecha);
            BuscarCliente(lista, texto);
            GridHelpers.MostrarDatosEnGrilla<VehiculosServiciosDto>(dgvDatos, lista);
        }
        string texto = "";
        private List<VehiculosServiciosDto> lista;
        private IServiciosVehiculosServicios _servicio;
        private IServiciosMovimientos _servicioMovimientos;
        private IServiciosVehiculos _serviciosVehiculos;
        private IServiciosClientes _serviciosClientes;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 4;

        int? IdVehiculo = null;
        int? IDMovimiento = null;
        int? IdCliente = null;
        DateTime? fecha = null;

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            IdVehiculo = null;
            IDMovimiento=null;
            IdCliente= null;
            fecha=null;
            RecargarGrilla();
            HabilitarBotones();
        }

        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null, null, null, null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            if (IdVehiculo == null && IDMovimiento==null && IdCliente==null && fecha==null)
            {
                lista = _servicio.GetVehiculoServicioPorPagina(registrosPorPagina, paginaActual, IdVehiculo, IDMovimiento, IdCliente, fecha);
            }
            else
            {
                lista = _servicio.GetVehiculoServicioPorPagina(registrosPorPagina, paginaActual, IdVehiculo, IDMovimiento, IdCliente, fecha);
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
            toolStripDropDownButtonFiltrar.BackColor = SystemColors.Control;
            toolStripButtonEditar.Enabled = true;
            toolStripButtonBorrar.Enabled = true;
            toolStripButtonAgregar.Enabled = true;
            toolStripDropDownButtonFiltrar.Enabled = true;
            btnAnterior.Enabled = true;
            btnPrimero.Enabled = true;
            btnSiguiente.Enabled = true;
            btnUltimo.Enabled = true;

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
            if (!_servicio.Existe(Servicios))
            {
                _servicio.Guardar(Servicios);
                MessageBox.Show("Servicio agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null, null, null, null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            else
            {
                MessageBox.Show("El servicio ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            _servicio.Borrar(ServicioABorrar.IdVehiculosSevicios);
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

            VehiculosServicios servicios = _servicio.GetVehiculoServicioPorId(vehiculosServiciosDto.IdVehiculosSevicios);
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
                if (!_servicio.Existe(servicios))
                {
                    if (servicios != null)
                    {
                        //Crear el dto
                        vehiculosServiciosDto.IdVehiculosSevicios = servicios.IdVehiculosSevicios;
                        vehiculosServiciosDto.Patente = _serviciosVehiculos.GetVehiculoPorId(servicios.IdVehiculo).Patente;
                        vehiculosServiciosDto.Servicio = _servicioMovimientos.GetMovimientosPorId(servicios.IdMovimiento).Servicio;
                        vehiculosServiciosDto.DebeServicio = _servicioMovimientos.GetMovimientosPorId(servicios.IdMovimiento).Debe;
                        vehiculosServiciosDto.Apellido = _serviciosClientes.GetClientePorId(servicios.IdCliente).Apellido;
                        vehiculosServiciosDto.Nombre = _serviciosClientes.GetClientePorId(servicios.IdCliente).Nombre;
                        vehiculosServiciosDto.CUIT = _serviciosClientes.GetClientePorId(servicios.IdCliente).CUIT;
                        vehiculosServiciosDto.Documento = _serviciosClientes.GetClientePorId(servicios.IdCliente).Documento;
                        vehiculosServiciosDto.Descripcion = servicios.Descripcion;
                        vehiculosServiciosDto.Debe = servicios.Debe;
                        vehiculosServiciosDto.Haber = servicios.Haber;
                        vehiculosServiciosDto.Fecha = servicios.Fecha;
                        GridHelpers.SetearFila(r, vehiculosServiciosDto);
                        _servicio.Guardar(servicios);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, servicios);
                    }
                }
                else
                {
                    MessageBox.Show("El servicio ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaServicio);
                throw;
            }
        }
        private void DesabilitarBotones()
        {
            toolStripDropDownButtonFiltrar.BackColor = Color.DarkViolet;
            toolStripButtonEditar.Enabled = false;
            toolStripButtonBorrar.Enabled = false;
            toolStripButtonAgregar.Enabled = false;
            toolStripDropDownButtonFiltrar.Enabled = false;
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

        private void fechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHorariosLaboralesFiltro frm = new frmHorariosLaboralesFiltro();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            DateTime Fecha = frm.GetFecha();
            registros = _servicio.GetCantidad(IdVehiculo,IDMovimiento,IdCliente,Fecha);
            fecha = Fecha;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
            DesabilitarBotones();
        }

        private void vehiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSeleccionarServicios frm = new frmSeleccionarServicios();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            Movimientos movimiento = frm.GetMovimiento();
            registros = _servicio.GetCantidad(IdVehiculo, movimiento.IdMovimiento, IdCliente, fecha);
            IDMovimiento = movimiento.IdMovimiento;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
            DesabilitarBotones();
        }

        private void vehiculoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmSeleccionarVehiculo frm = new frmSeleccionarVehiculo();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            Vehiculos vehiculo = frm.GetVehiculos();
            registros = _servicio.GetCantidad(vehiculo.IdVehiculo, IDMovimiento, IdCliente, fecha);
            IdVehiculo = vehiculo.IdVehiculo;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
            DesabilitarBotones();
        }

        private void BuscarCliente(List<VehiculosServiciosDto> serviciosVehiculosDto, string texto)
        {
            var listaFiltrada = serviciosVehiculosDto;
            if (texto.Length != 0)
            {
                Func<VehiculosServiciosDto, bool> condicion = c => c.Apellido.ToUpper().Contains(texto.ToUpper()) || c.Nombre.ToUpper().Contains(texto.ToUpper())||c.CUIT.Contains(texto.ToUpper())||c.Documento.Contains(texto.ToUpper());
                listaFiltrada = serviciosVehiculosDto.Where(condicion).ToList();

            }
            GridHelpers.MostrarDatosEnGrilla<VehiculosServiciosDto>(dgvDatos, listaFiltrada);
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var r = dgvDatos.Rows[e.RowIndex];
            VehiculosServiciosDto vehiculosServiciosDto = (VehiculosServiciosDto)r.Tag;
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var r = dgvDatos.Rows[e.RowIndex];
            VehiculosServiciosDto serviciosVehiculos = (VehiculosServiciosDto)r.Tag;
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            var texto = toolStripTextBox1.Text;
            BuscarCliente(lista, texto);
        }
    }
}
