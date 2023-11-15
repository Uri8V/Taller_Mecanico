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
    public partial class frmTipoDePago : Form
    {
        public frmTipoDePago()
        {
            InitializeComponent();
            _servicios=new ServiciosTiposDePagos();
        }

       
        private readonly ServiciosTiposDePagos _servicios;
        private List<TipoDePagos> lista;

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmTipoDePagoAE frm= new frmTipoDePagoAE();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                TipoDePagos nuevaTipo = frm.GetTipoDePago();
                if (!_servicios.Existe(nuevaTipo))
                {
                    _servicios.Guardar(nuevaTipo);
                    DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                    GridHelpers.SetearFila(r, nuevaTipo);
                    MostrarCantidad();
                    GridHelpers.AgregarFila(dgvDatos, r);
                    MessageBox.Show("Tipo de pago agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El Tipo de pago ya existe en la base de Datos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            lista = _servicios.GetTipoDePagos();
            foreach (var tipo in lista)
            {
                DataGridViewRow r = GridHelpers.ConstruirFila(dgvDatos);
                GridHelpers.SetearFila(r, tipo);
                GridHelpers.AgregarFila(dgvDatos, r);
            }
        }

        private void MostrarCantidad()
        {
            txtCantidadTiposPago.Text = _servicios.GetCantidad(null).ToString();
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            TipoDePagos tipo = (TipoDePagos)r.Tag;
            try
            {
                //Se debe controlar que este relacionada
                DialogResult dr = MessageBox.Show($"¿Desea eliminar el Tipo de Pago: {tipo.NombreDePago}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) { return; }
                _servicios.Borrar(tipo.IdTipoPagos);
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
            TipoDePagos tipo = (TipoDePagos)r.Tag;
            TipoDePagos tipoCopia = (TipoDePagos)tipo.Clone();
            try
            {
                frmTipoDePagoAE frm = new frmTipoDePagoAE();
                frm.SetMarca(tipo);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel) { return; }
                tipo = frm.GetTipoDePago();
                if (!_servicios.Existe(tipo))
                {
                    _servicios.Guardar(tipo);
                    GridHelpers.SetearFila(r, tipo);
                    MessageBox.Show("Tipo de pago editado editado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GridHelpers.SetearFila(r, tipoCopia);
                    MessageBox.Show("el Tipo de Pago ya existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                GridHelpers.SetearFila(r, tipoCopia);
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmTipoDePago_Load(object sender, EventArgs e)
        {
            MostrarDatosEnGrilla();
        }
    }
}
