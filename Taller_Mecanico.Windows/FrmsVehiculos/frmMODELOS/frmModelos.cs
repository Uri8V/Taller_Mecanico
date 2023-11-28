using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Modelos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmMODELOS;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmModelos : Form
    {
        public frmModelos()
        {
            InitializeComponent();
            _servicio = new ServicioModelos();
            _servicioMarca = new ServiciosMarcas();

        }
        private List<ModelosDto> lista;
        private IServicioModelos _servicio;
        private IServiciosMarcas _servicioMarca;
        int paginaActual = 1;
        int registros = 0;
        int paginas = 0;
        int registrosPorPagina = 3;

        int? marca = null;


        private void toolStripButtonCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            marca = null;
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
            btnAnterior.Enabled = true;
            btnPrimero.Enabled = true;
            btnSiguiente.Enabled = true;
            btnUltimo.Enabled = true;
        }

        private void frmModelos_Load(object sender, EventArgs e)
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
            if (marca==null)
            {
                lista = _servicio.GetModelosPorPagina(registrosPorPagina, paginaActual, marca);
            }
            else
            {
                lista = _servicio.GetModelosPorPagina(registrosPorPagina, paginaActual, marca);
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
            frmModelosAE frm = new frmModelosAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            var modelo = frm.GetModelo();
            if (!_servicio.Existe(modelo))
            {
                _servicio.Guardar(modelo);
                MessageBox.Show("Modelo Agregado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Question);
                registros = _servicio.GetCantidad(null);
                paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado(); 
            }
            else
            {
                MessageBox.Show("El modelo ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ModelosDto modeloABorrar = (ModelosDto)r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea eliminar el modelo: {modeloABorrar.Modelo}, {modeloABorrar.Marca}?", "Confirmar Selcción", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            Model MODELOaborrar = _servicio.GetModelosPorId(modeloABorrar.IdModelo);
            //Falta metodo de objeto relacionado
            if (!_servicio.EstaRelacionada(MODELOaborrar))
            {
                GridHelpers.QuitarFila(dgvDatos, r);
                _servicio.Borrar(modeloABorrar.IdModelo);
                RecargarGrilla(); 
            }
            else
            {
                MessageBox.Show("El modelo no se puede eliminar porque esta relacionado con algún Vehiculo", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            var r = dgvDatos.SelectedRows[0];
            ModelosDto modeloDto = (ModelosDto)r.Tag;
            ModelosDto CopiaModelo = (ModelosDto)modeloDto.Clone();

            Model modelos = _servicio.GetModelosPorId(modeloDto.IdModelo);
            try
            {
                frmModelosAE frm = new frmModelosAE() { Text = "Editar Cliente" };
                frm.SetModelos(modelos);
                DialogResult dr = frm.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    GridHelpers.SetearFila(r, CopiaModelo);

                    return;
                }
                modelos = frm.GetModelo();
                if (!_servicio.Existe(modelos))
                {
                    if (modelos != null)
                    {
                        //Crear el dto
                        modeloDto.IdModelo = modelos.IdModelo;
                        modeloDto.Modelo = modelos.Modelo;
                        modeloDto.Marca = _servicioMarca.GetMarcasPorId(modelos.IdMarca).NombreMarca;
                        GridHelpers.SetearFila(r, modeloDto);
                        _servicio.Guardar(modelos);
                    }
                    else
                    {
                        //Recupero la copia del dto
                        GridHelpers.SetearFila(r, CopiaModelo);
                    }
                }
                else
                {
                    MessageBox.Show("El modelo ya existe!!!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                GridHelpers.SetearFila(r, CopiaModelo);
                throw;
            }
        }
        private void toolStripButtonFiltrar_Click(object sender, EventArgs e)
        {
            frmSeleccionarMarca frm = new frmSeleccionarMarca();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                HabilitarBotones();
                return;
            }
            var MArca = frm.GetMarca();
            registros = _servicio.GetCantidad(MArca.MarcaId);
            marca = MArca.MarcaId;
            paginas = formHelper.CalcularPaginas(registros, registrosPorPagina);
            lista = _servicio.GetModelosPorPagina(registrosPorPagina, paginaActual, marca);
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
