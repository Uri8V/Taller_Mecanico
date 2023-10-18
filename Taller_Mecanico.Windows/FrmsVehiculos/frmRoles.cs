using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsMarcas;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
            _servicios= new ServiciosRoles();

        }
        private readonly ServiciosRoles _servicios;
        private List<Roles> lista;

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmRolesAE frm = new frmRolesAE();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                Roles nuevaRol = frm.GetRol();
                if (!_servicios.Existe(nuevaRol))
                {
                    _servicios.Guardar(nuevaRol);
                    DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                    GridHelpers.SetearFila(r, nuevaRol);
                    MostrarCantidad();
                    GridHelpers.AgregarFila(dgvDatos, r);
                    MessageBox.Show("Rol agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El Rol ya existe en la base de Datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            MostrarCantidad();
            lista = _servicios.GetRoles();
            foreach (var rol in lista)
            {
                DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                GridHelpers.SetearFila(r, rol);
                GridHelpers.AgregarFila(dgvDatos, r);
            }
        }

        private void MostrarCantidad()
        {
            txtCantidadRoles.Text = _servicios.GetCantidad(null).ToString();
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Roles rol = (Roles)r.Tag;
            try
            {
                //Se debe controlar que este relacionada
                DialogResult dr = MessageBox.Show($"¿Desea eliminar el rol: {rol.Rol}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicios.Borrar(rol.IdRolEmpleado);
                GridHelpers.QuitarFila(dgvDatos, r);
                MostrarCantidad();
                MessageBox.Show("Registro Borrado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            Roles rol = (Roles)r.Tag;
            Roles rolCopia = (Roles)rol.Clone();
            try
            {
                frmRolesAE frm = new frmRolesAE();
                frm.SetRol(rol);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel) { return; }
                rol = frm.GetRol();
                if (!_servicios.Existe(rol))
                {
                    _servicios.Guardar(rol);
                    GridHelpers.SetearFila(r, rol);
                    MessageBox.Show("Rol editado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GridHelpers.SetearFila(r, rolCopia);
                    MessageBox.Show("El Rol ya existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                GridHelpers.SetearFila(r, rolCopia);
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
        }
    }
}
