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
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Dtos.Vehiculos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmVehiculos : Form
    {
        public frmVehiculos()
        {
            InitializeComponent();
            _servicio = new ServiciosVehiculos();
            _servicioModelos= new ServicioModelos();
            _servicioTipoVehiculo = new SeviciosTipoDeVehiculo();
        }
        private List<VehiculoDto> lista;
        private IServiciosVehiculos _servicio;
        private IServiciosTipoDeVehiculo _servicioTipoVehiculo;
        private IServicioModelos _servicioModelos;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? tipo = null;
        int? modelo = null;
        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmVehiculos_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            lista = _servicio.GetVehiculosPorPagina(registrosPorPagina, paginaActual, tipo, modelo);
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
        }
        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmVehiculosAE frm = new frmVehiculosAE ();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var vehiculo = frm.GetVehiculo();
            //preguntar si existe
            _servicio.Guardar(vehiculo);
            MessageBox.Show("Vehiculo Agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
            VehiculoDto vehiculoABorrar = (VehiculoDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el vehiculo: Patente({vehiculoABorrar.Patente}), Modelo:{vehiculoABorrar.Modelo}?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            //Falta metodo de objeto relacionado
            GridHelpers.QuitarFila(dgvDatos, r);
            _servicio.Borrar(vehiculoABorrar.IdVehiculo);
            RecargarGrilla();
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            VehiculoDto vehiculoDto = (VehiculoDto)r.Tag;
            VehiculoDto CopiaVehiculo = (VehiculoDto)vehiculoDto.Clone();

            Entidades.Entidades.Vehiculos vehiculos = _servicio.GetVehiculoPorId(vehiculoDto.IdVehiculo);
            try
            {
                frmVehiculosAE frm = new frmVehiculosAE() { Text = "Editar Vehiculo" };
                frm.SetVehiculo(vehiculos);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaVehiculo);

                    return;
                }
                vehiculos = frm.GetVehiculo();
                if (vehiculos != null)
                {
                    //Crear el dto
                    vehiculoDto.IdVehiculo = vehiculos.IdVehiculo;
                    vehiculoDto.Patente=vehiculos.Patente;
                    vehiculoDto.Kilometros = vehiculos.Kilometros;
                    vehiculoDto.Modelo = _servicioModelos.GetModelosPorId(vehiculos.IdModelo).Modelo;
                    vehiculoDto.TipoVehiculo=_servicioTipoVehiculo.GetTipoVehiculosPorId(vehiculos.IdTipoVehiculo).NombreTipoVehiculo;
                    GridHelpers.SetearFila(r, vehiculoDto);
                    _servicio.Guardar(vehiculos);
                }
                else
                {
                    //Recupero la copia del dto
                    GridHelpers.SetearFila(r, vehiculos);

                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaVehiculo);
                throw;
            }
        }
    }
}
