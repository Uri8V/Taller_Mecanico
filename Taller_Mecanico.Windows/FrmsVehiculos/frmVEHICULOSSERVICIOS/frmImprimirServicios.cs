using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmVEHICULOSSERVICIOS
{
    public partial class frmImprimirServicios : Form
    {
        public frmImprimirServicios(VehiculosServiciosDto vehiculosServiciosDto)
        {
            InitializeComponent();
            _servicios = new ServiciosVehiculosServicios();
            _serviciosClientes = new ServiciosClientes();
            _serviciosMovimientos = new ServiciosMovimientos();
            _serviciosVehiculos = new ServiciosVehiculos();
            vehiculosServiciosDTO = vehiculosServiciosDto;
        }
        private VehiculosServiciosDto vehiculosServiciosDTO;
        private List<VehiculosServiciosDto> lista;
        private IServiciosVehiculosServicios _servicios;
        private IServiciosVehiculos _serviciosVehiculos;
        private IServiciosClientes _serviciosClientes;
        private IServiciosMovimientos _serviciosMovimientos;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmImprimirServicios_Load(object sender, EventArgs e)
        {
            RecarGrilla();
        }

        private void RecarGrilla()
        {
            if (vehiculosServiciosDTO.CUIT != "")
            {
                lista = _servicios.GetVehiculoServicioPorCliente(vehiculosServiciosDTO.CUIT);

            }
            else
            {
                lista = _servicios.GetVehiculoServicioPorCliente(vehiculosServiciosDTO.Documento);
            }
            MostraDatosEnGrilla();
        }

        private void MostraDatosEnGrilla()
        {
            decimal total = 0;
            decimal haber = 0;
            dgvDatos.Rows.Clear();
            foreach (var item in lista)
            {
                if (lista.IndexOf(item)==0)
                {
                    txtCliente.Text = $"{item.Apellido.ToUpper()}, {item.Nombre}";
                    txtDocCUIT.Text = $"{item.Documento}{item.CUIT}";
                }
                DataGridViewRow r = ConstruirFila();
                CrearFila(r, item);
                AgregarFila(r);
                haber += item.Haber;
                total += item.DebeServicio;
            }
            txtTotal.Text = (total-haber).ToString();
        }

        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }

        private void CrearFila(DataGridViewRow r, VehiculosServiciosDto item)
        {   
            r.Cells[0].Value = item.Fecha.ToShortDateString();
            r.Cells[1].Value = item.Patente;
            r.Cells[2].Value = item.Servicio;
            r.Cells[3].Value = item.Descripcion;
            r.Cells[4].Value=item.Haber.ToString();
            r.Cells[5].Value = (item.Debe-item.Haber).ToString();
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r= new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ImprimirHelper.ImprimirFactura(lista);
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
