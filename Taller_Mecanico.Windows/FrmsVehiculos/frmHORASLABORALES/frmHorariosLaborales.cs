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
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsMarcas;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmHorariosLaborales : Form
    {
        public frmHorariosLaborales()
        {
            InitializeComponent();
            _servicios = new ServiciosDeHorasLaborales();
        }
        private IServiciosDeHorasLaborales _servicios;
        private List<HorasLaborales> lista;

        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButtonAgregar_Click(object sender, EventArgs e)
        {
            frmHorariosLaboralesAE frm= new frmHorariosLaboralesAE();
            DialogResult dr= frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            HorasLaborales horas = frm.GetHorasLaborales();
            if (!_servicios.Existe(horas))
            {
                _servicios.Guardar(horas);
                DataGridViewRow r=GridHelpers.ConstruirFila(dgvDatos);
                GridHelpers.SetearFila(r, horas);
                GridHelpers.AgregarFila(dgvDatos,r);
                MostrarDatos(null);
            }
            else
            {
                MessageBox.Show("Las horas de esta fecha ya existen");
            }
        }

        private void MostrarDatos(DateTime? Fecha)
        {
            if(Fecha == null)
            {
                txtCantidadMarcas.Text = _servicios.GetCantidad(null).ToString();
            }
            else
            {
                txtCantidadMarcas.Text=_servicios.GetCantidad(Fecha).ToString();
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if(dgvDatos.Rows.Count == 0)
            {
                return;
            }
            var r= dgvDatos.SelectedRows[0];
            HorasLaborales horas =(HorasLaborales) r.Tag;
            DialogResult dr = MessageBox.Show($"Desea eliminar las horas de la fecha {horas.Fecha.ToShortDateString()}?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            if (!_servicios.EstRelacionada(horas))
            {
                _servicios.Borrar(horas.IdHorasLaborales);
                GridHelpers.QuitarFila(dgvDatos, r);
                MostrarDatos(null);
                MessageBox.Show("Las Horas de esa fecha han sido eliminadas"); 
            }
            else
            {
                MessageBox.Show("No se pueden borrar debido a que estan relacionadas con algún sueldo", "IINFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            HorasLaborales horas = (HorasLaborales)r.Tag;
            HorasLaborales horasCopia = (HorasLaborales)horas.Clone();
            try
            {
                frmHorariosLaboralesAE frm = new frmHorariosLaboralesAE();
                frm.SetMarca(horas);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel) { return; }
                horas = frm.GetHorasLaborales();
                if (!_servicios.Existe(horas))
                {
                    _servicios.Guardar(horas);
                    GridHelpers.SetearFila(r, horas);
                    MessageBox.Show("Horas editadas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GridHelpers.SetearFila(r, horasCopia);
                    MessageBox.Show("Las horas ya existen", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                GridHelpers.SetearFila(r, horasCopia);
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmHorariosLaborales_Load(object sender, EventArgs e)
        {
            lista = _servicios.GetHoras();
            MostrarDatosEnGrilla();
            MostrarDatos(null);
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
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            lista = _servicios.GetHoras();
            MostrarDatosEnGrilla();
            MostrarDatos(null);
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            toolStripButtonAgregar.Enabled = true;
            toolStripButtonBorrar.Enabled = true;
            toolStripButtonEditar.Enabled = true;
            toolStripButtonFiltrar.Enabled = true;
            toolStripButtonFiltrar.BackColor = Color.White;
        }

        private void toolStripButtonFiltrar_Click(object sender, EventArgs e)
        {
            DesabilitarBotones();
            frmHorariosLaboralesFiltro frm = new frmHorariosLaboralesFiltro();
            DialogResult dr = frm.ShowDialog(this);
            if(dr == DialogResult.Cancel) 
            {
                HabilitarBotones();
                return; 
            }
            DateTime horas=frm.GetFecha();
            lista = _servicios.Filtrar(horas);
            MostrarDatos(horas);
            MostrarDatosEnGrilla();
        }

        private void DesabilitarBotones()
        {
            toolStripButtonFiltrar.BackColor = Color.DarkViolet;
            toolStripButtonEditar.Enabled = false;
            toolStripButtonBorrar.Enabled = false;
            toolStripButtonAgregar.Enabled = false;
            toolStripButtonFiltrar.Enabled = false;
        }

    }
}
