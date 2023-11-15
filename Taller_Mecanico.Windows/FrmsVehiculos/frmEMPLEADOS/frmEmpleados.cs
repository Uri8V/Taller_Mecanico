using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Dtos.Empleados;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();
            _servicio = new ServiciosEmpleados();
            _servicioRoles= new ServiciosRoles();
        }
        private List<EmpleadoDto> lista;
        private IServiciosEmpleados _servicio;
        private IServiciosDeRoles _servicioRoles;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? rol=null;
        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
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

        private void frmEmpleados_Load(object sender, EventArgs e)
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
            lista = _servicio.GetEmpleadosPorPagina(registrosPorPagina, paginaActual, rol);
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
            frmEmpleadoAE frm= new frmEmpleadoAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var empleado = frm.GetEmpleado();
            //preguntar si existe
            _servicio.Guardar(empleado);
            MessageBox.Show("Empleado Agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
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
            EmpleadoDto empleadoABorrar = (EmpleadoDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el empleado: {empleadoABorrar.Apellido}, {empleadoABorrar.Nombre}, DNI:{empleadoABorrar.Documento}?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            //Falta metodo de objeto relacionado
            GridHelpers.QuitarFila(dgvDatos, r);
            _servicio.Borrar(empleadoABorrar.IdEmpleado);
            RecargarGrilla();
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            EmpleadoDto empleadoDto = (EmpleadoDto)r.Tag;
            EmpleadoDto CopiaEmpleado = (EmpleadoDto)empleadoDto.Clone();

            Empleado empleados = _servicio.GetEmpleadoPorId(empleadoDto.IdEmpleado);
            try
            {
               frmEmpleadoAE frm = new frmEmpleadoAE() { Text = "Editar Empleado" };
                frm.SetEmpleado(empleados);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaEmpleado);

                    return;
                }
                empleados = frm.GetEmpleado();
                if (empleados != null)
                {
                    //Crear el dto
                    empleadoDto.IdEmpleado = empleados.IdEmpleado;
                    empleadoDto.Nombre= empleados.Nombre;
                    empleadoDto.Documento= empleados.Documento;
                    empleadoDto.Rol= _servicioRoles.GetRolesPorId(empleados.IdRolEmpleado).Rol;
                    GridHelpers.SetearFila(r, empleadoDto);
                    _servicio.Guardar(empleados);
                }
                else
                {
                    //Recupero la copia del dto
                    GridHelpers.SetearFila(r, CopiaEmpleado);

                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaEmpleado);
                throw;
            }
        }
    }
}
