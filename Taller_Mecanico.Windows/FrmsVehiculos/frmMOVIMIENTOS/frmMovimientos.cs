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
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmSUELDOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmMOVIMIENTOS
{
    public partial class frmMovimientos : Form
    {
        public frmMovimientos()
        {
            InitializeComponent();    
            _servicio = new ServiciosMovimientos();
            _serviciosTipoDePago = new ServiciosTiposDePagos();

        }

        private void frmMovimientos_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private List<MovimientosDto> lista;
        private IServiciosMovimientos _servicio;
        private IServiciosTipoDePago _serviciosTipoDePago;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? IdTipoDePago = null;


        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            IdTipoDePago = null;
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
            if (IdTipoDePago == null)
            {
                lista = _servicio.GetMovimientosPorPagina(registrosPorPagina, paginaActual, IdTipoDePago);
            }
            else
            {
                lista = _servicio.GetMovimientosPorPagina(registrosPorPagina, paginaActual, IdTipoDePago);

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
            frmMovimientosAE frm = new frmMovimientosAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var movimiento = frm.GetMovimiento();
            //preguntar si existe
            if (!_servicio.Existe(movimiento))
            {
                _servicio.Guardar(movimiento);
                MessageBox.Show("movimiento agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            else
            {
                MessageBox.Show("El Servicio ya existe!!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            MovimientosDto movimientoABorrar = (MovimientosDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el Movimiento de: {movimientoABorrar.Servicio} del cual debe (${movimientoABorrar.Debe})?", "Confirmar Selección", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            Movimientos MOVIMIENTOaborrar = _servicio.GetMovimientosPorId(movimientoABorrar.IdMovimiento);
            //Falta metodo de objeto relacionado
            if (!_servicio.EstaRelacionada(MOVIMIENTOaborrar))
            {
                GridHelpers.QuitarFila(dgvDatos, r);
                _servicio.Borrar(movimientoABorrar.IdMovimiento);
                RecargarGrilla();
            }
            else
            {
                MessageBox.Show("El Servicio no se puede borrar debido a que esta relacionado con algún Vehiculo ", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            MovimientosDto movimientoDto = (MovimientosDto)r.Tag;
            MovimientosDto CopiaMovimiento = (MovimientosDto)movimientoDto.Clone();

            Movimientos movimientos = _servicio.GetMovimientosPorId(movimientoDto.IdMovimiento);
            try
            {
                frmMovimientosAE frm = new frmMovimientosAE() { Text = "Editar Movimiento" };
                frm.SetMovimiento(movimientos);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaMovimiento);

                    return;
                }
                movimientos = frm.GetMovimiento();
                if (!_servicio.Existe(movimientos))
                {
                    if (movimientos != null)
                    {
                        //Crear el dto
                        movimientoDto.IdMovimiento = movimientos.IdMovimiento;
                        movimientoDto.Servicio = movimientos.Servicio;
                        movimientoDto.Debe = movimientos.Debe;
                        movimientoDto.NombreDePago = _serviciosTipoDePago.GetTipoDePagosPorId(movimientos.IdTipoDePago).NombreDePago;
                        GridHelpers.SetearFila(r, movimientoDto);
                        _servicio.Guardar(movimientos);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, movimientos);

                    } 
                }
                else
                {
                    MessageBox.Show("El Servicio ya existe!!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaMovimiento);
                throw;
            }
        }

        private void toolStripButtonFiltrar_Click(object sender, EventArgs e)
        {
            frmSeleccionarTipoDePago frm = new frmSeleccionarTipoDePago();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                HabilitarBotones();
                return;
            }
            var tipoPago = frm.GetTipoPago();
            registros = _servicio.GetCantidad(tipoPago.IdTipoPagos);
            IdTipoDePago = tipoPago.IdTipoPagos;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetMovimientosPorPagina(registrosPorPagina, paginaActual, tipoPago.IdTipoPagos);
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
            if (paginas==1)
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
    }
}
