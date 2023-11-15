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
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmTiposDeClientes : Form
    {
        public frmTiposDeClientes()
        {
            InitializeComponent();
            _servicios= new ServiciosDeTiposDeClientes();
        }



        private readonly ServiciosDeTiposDeClientes _servicios;
        private List<TiposDeClientes> lista;

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmTiposDeClientesAE frm = new frmTiposDeClientesAE();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
               TiposDeClientes nuevaTipo = frm.GetTipoDeCliente();
                if (!_servicios.Existe(nuevaTipo))
                {
                    _servicios.Guardar(nuevaTipo);
                    DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                    GridHelpers.SetearFila(r, nuevaTipo);
                    MostrarCantidad();
                    GridHelpers.AgregarFila(dgvDatos, r);
                    MessageBox.Show("Tipo de cliente agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El Tipo de cliente ya existe en la base de Datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }



        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            MostrarCantidad();
            lista = _servicios.GetTiposDeClientes();
            foreach (var tipo in lista)
            {
                DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                GridHelpers.SetearFila(r, tipo);
                GridHelpers.AgregarFila(dgvDatos, r);
            }
        }

        private void MostrarCantidad()
        {
            txtCantidadMarcas.Text = _servicios.GetCantidad(null).ToString();
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            TiposDeClientes tipo = (TiposDeClientes)r.Tag;
            try
            {
                //Se debe controlar que este relacionada
                DialogResult dr = MessageBox.Show($"¿Desea eliminar el Tipo de Cliente: {tipo.TipoCliente}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicios.Borrar(tipo.IdTipoCliente);
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
            TiposDeClientes tipo = (TiposDeClientes)r.Tag;
            TiposDeClientes tipoCopia = (TiposDeClientes)tipo.Clone();
            try
            {
                frmTiposDeClientesAE frm = new frmTiposDeClientesAE();
                frm.SetMarca(tipo);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel) { return; }
                tipo = frm.GetTipoDeCliente();
                if (!_servicios.Existe(tipo))
                {
                    _servicios.Guardar(tipo);
                    GridHelpers.SetearFila(r, tipo);
                    MessageBox.Show("Tipo de cliente editado editado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GridHelpers.SetearFila(r, tipoCopia);
                    MessageBox.Show("el Tipo de cliente ya existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                GridHelpers.SetearFila(r, tipoCopia);
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmTiposDeClientes_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
        }
    }
}
