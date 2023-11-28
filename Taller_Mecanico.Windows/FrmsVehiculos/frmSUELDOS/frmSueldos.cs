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
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmHISTORIALES;
using Taller_Mecanico.Windows.FrmsVehiculos.frmTELEFONOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmSUELDOS
{
    public partial class frmSueldos : Form
    {
        public frmSueldos()
        {
            InitializeComponent();
            _servicio = new ServicioSueldos();
            _serviciosDeHorasLaborales= new ServiciosDeHorasLaborales();
            _serviciosHistoriales= new ServiciosHistoriales();
            _serviciosEmpleados= new ServiciosEmpleados();
        }

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();    
        }
        private List<SueldosDto> lista;
        private IServiciosSueldos _servicio;
        private IServiciosHistoriales _serviciosHistoriales;
        private IServiciosDeHorasLaborales _serviciosDeHorasLaborales;
        private IServiciosEmpleados _serviciosEmpleados;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? IdEmpleado = null;
        DateTime? Fecha = null;

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            IdEmpleado = null;
            Fecha=null;
            RecargarGrilla();
            HabilitarBotones();
        }

        private void RecargarGrilla()
        {
            registros = _servicio.GetCantidad(null, null);
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            MostrarPaginado();
        }

        private void MostrarPaginado()
        {
            if (IdEmpleado==null && Fecha==null)
            {
                lista = _servicio.GetSueldosPorPagina(registrosPorPagina, paginaActual, IdEmpleado, Fecha);
            }
            else
            {
                lista = _servicio.GetSueldosPorPagina(registrosPorPagina, paginaActual, IdEmpleado, Fecha);
            }
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelpers.LimpiarGrilla(dgvDatos);
            foreach (var item in lista)
            {
                DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                item.TotalExtra= item.ValorPorHoraExtra * item.HorasExtras;
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

        private void frmSueldos_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }
        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmSueldosAE frm = new frmSueldosAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var sueldo = frm.GetSueldo();
            //MODIFICAR EL FORMULARIO AE
            if (!_servicio.Existe(sueldo))
            {
                _servicio.Guardar(sueldo);
                MessageBox.Show("Sueldo agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null, null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado(); 
            }
            else
            {
                MessageBox.Show("El sueldo ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            SueldosDto sueldoABorrar = (SueldosDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el Sueldo de:{sueldoABorrar.Apellido.ToUpper()}, {sueldoABorrar.Nombre} ({sueldoABorrar.Documento})| Fecha:{sueldoABorrar.Fecha.ToShortDateString()}| Horas Laborales:{sueldoABorrar.HorasLaborales} | Horas Extra:{sueldoABorrar.HorasExtras}?", "Confirmar Selección", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            //Acá hace falta el metodo de relación?
            GridHelpers.QuitarFila(dgvDatos, r);
            _servicio.Borrar(sueldoABorrar.IdSueldo);
            RecargarGrilla();
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            SueldosDto sueldoDto = (SueldosDto)r.Tag;
            SueldosDto CopiaSueldo = (SueldosDto)sueldoDto.Clone();

            Sueldos sueldos = _servicio.GetSueldosPorId(sueldoDto.IdSueldo);
            try
            {
                frmSueldosAE frm = new frmSueldosAE() { Text = "Editar Sueldo" };
                frm.SetSueldo(sueldos);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaSueldo);

                    return;
                }
                sueldos = frm.GetSueldo();
                if (!_servicio.Existe(sueldos))
                {
                    if (sueldos != null)
                    {
                        //Crear el dto
                        sueldoDto.IdSueldo = sueldos.IdSueldo;
                        sueldoDto.Fecha = _serviciosDeHorasLaborales.GetHorasLaboralesPorId(sueldos.IdHorasLaborales).Fecha;
                        sueldoDto.HorasLaborales = _serviciosDeHorasLaborales.GetHorasLaboralesPorId(sueldos.IdHorasLaborales).horaslaborales;
                        sueldoDto.HorasExtras = _serviciosDeHorasLaborales.GetHorasLaboralesPorId(sueldos.IdHorasLaborales).horasExtras;
                        sueldoDto.ValorPorHora = _serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).ValorPorHora;
                        sueldoDto.ValorPorHoraExtra = _serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).ValorPorHoraExtra;
                        sueldoDto.Apellido = _serviciosEmpleados.GetEmpleadoPorId(_serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).IdEmpleado).Apellido;
                        sueldoDto.Nombre = _serviciosEmpleados.GetEmpleadoPorId(_serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).IdHistorial).Nombre;
                        sueldoDto.Documento = _serviciosEmpleados.GetEmpleadoPorId(_serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).IdEmpleado).Documento;
                        sueldoDto.TotalAPagar = sueldos.TotalAPagar;
                        sueldoDto.TotalExtra = sueldos.TotalExtra;
                        GridHelpers.SetearFila(r, sueldoDto);
                        _servicio.Guardar(sueldos);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, sueldos);

                    }

                }
                else
                {
                    MessageBox.Show("El sueldo ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaSueldo);
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
            registros = _servicio.GetCantidad(empleadoSeleccionado.IdEmpleado, null);
            IdEmpleado = empleadoSeleccionado.IdEmpleado;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetSueldosPorPagina(registrosPorPagina, paginaActual, IdEmpleado, Fecha);
            DesabilitarBotones();
            MostrarDatosEnGrilla();
        }

        private void fechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHorariosLaboralesFiltro frm = new frmHorariosLaboralesFiltro();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            DateTime FechaSeleccionado = frm.GetFecha();
            registros = _servicio.GetCantidad(null, FechaSeleccionado);
            Fecha = FechaSeleccionado;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetSueldosPorPagina(registrosPorPagina, paginaActual,IdEmpleado, Fecha);
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
    }
}
