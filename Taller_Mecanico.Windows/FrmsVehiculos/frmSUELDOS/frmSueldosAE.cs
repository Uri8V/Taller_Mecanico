﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Historiales;
using Taller_Mecanico.Entidades.Dtos.HorasLaborales;
using Taller_Mecanico.Entidades.Dtos.Sueldos;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;
using Taller_Mecanico.Windows.FrmsVehiculos.frmHISTORIALES;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmSUELDOS
{
    public partial class frmSueldosAE : Form
    {
        public frmSueldosAE()
        {
            InitializeComponent();
            _serviciosDeHorasLaborales = new ServiciosDeHorasLaborales();
            _serviciosHistoriales= new ServiciosHistoriales();
        }
        private readonly IServiciosDeHorasLaborales _serviciosDeHorasLaborales;
        private readonly IServiciosHistoriales _serviciosHistoriales;
        private Sueldos sueldos;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboHorasLaborales(ref comboHorasLaborales);
            ComboHelper.CargarComboHistoriales(ref comboHistorial);
            if (sueldos != null)
            {
                txtTotalAPagar.Text = sueldos.TotalAPagar.ToString();
                txtTotalExtra.Text = sueldos.TotalExtra.ToString();
                comboHistorial.SelectedValue = sueldos.IdHistorial;
                comboHorasLaborales.SelectedValue = sueldos.IdHorasLaborales;
                txtValorPorHora.Text = _serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).ValorPorHora.ToString();
                txtValorPorHoraExtra.Text = _serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).ValorPorHoraExtra.ToString();
            }
        }
        internal Sueldos GetSueldo()
        {
            return sueldos;
        }

        internal void SetSueldo(Sueldos sueldo)
        {
           this.sueldos = sueldo;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (sueldos == null)
                {
                    sueldos = new Sueldos();
                }
                sueldos.IdHistorial = (int)comboHistorial.SelectedValue;
                HistorialComboDto historialSeleccionado= (HistorialComboDto)comboHistorial.SelectedItem;
                sueldos.Historial = _serviciosHistoriales.GetHistorialPorId(historialSeleccionado.IdHistorial);
                sueldos.IdHorasLaborales = (int)comboHorasLaborales.SelectedValue;
                HorasLaboralesComboDto horasSeleccionadas = (HorasLaboralesComboDto)comboHorasLaborales.SelectedItem;
                sueldos.HoraLaboral = _serviciosDeHorasLaborales.GetHorasLaboralesPorId(horasSeleccionadas.IdHorasLaborales);
                sueldos.TotalAPagar = _serviciosDeHorasLaborales.GetHorasLaboralesPorId(sueldos.IdHorasLaborales).horaslaborales * _serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).ValorPorHora;
                if (_serviciosDeHorasLaborales.GetHorasLaboralesPorId(sueldos.IdHorasLaborales).horasExtras == 0)
                {
                    sueldos.TotalExtra = 0;
                }
                else
                {              
                    sueldos.TotalExtra = _serviciosDeHorasLaborales.GetHorasLaboralesPorId(sueldos.IdHorasLaborales).horasExtras * _serviciosHistoriales.GetHistorialPorId(sueldos.IdHistorial).ValorPorHoraExtra;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (comboHistorial.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboHistorial, "Debe seleccionar un Historial");
            }
            if (comboHorasLaborales.SelectedIndex == 0)
            {
                valido = false;
                errorProvider1.SetError(comboHorasLaborales, "Debe seleccionar una Fecha");
            }
            return valido;
        }

        private void btnAgregarHistorial_Click(object sender, EventArgs e)
        {
            frmHistoriales frm= new frmHistoriales();
            DialogResult dr= frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboHistoriales(ref comboHistorial);
                return;
            }
        }

        private void btnAgregarHorasLaborales_Click(object sender, EventArgs e)
        {
            frmHorariosLaborales fm= new frmHorariosLaborales();
            DialogResult dialog= fm.ShowDialog(this);
            if(dialog == DialogResult.Cancel)
            {
                ComboHelper.CargarComboHorasLaborales(ref comboHorasLaborales);
                return;
            }
        }

        private void comboHorasLaborales_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarTotalAPagar();
        }
        private void MostrarTotalAPagar()
        {
            if (comboHistorial.SelectedIndex>0)
            {
                txtValorPorHora.Text = (_serviciosHistoriales.GetHistorialPorId((int)comboHistorial.SelectedValue).ValorPorHora).ToString();
                txtValorPorHoraExtra.Text = (_serviciosHistoriales.GetHistorialPorId((int)comboHistorial.SelectedValue).ValorPorHoraExtra).ToString();
            }
            else
            {
                txtValorPorHora.Text=0.ToString();
                txtValorPorHoraExtra.Text=0.ToString();
            }

            if (comboHorasLaborales.SelectedIndex != 0 && comboHistorial.SelectedIndex != 0)
            {
                txtTotalAPagar.Text = (_serviciosDeHorasLaborales.GetHorasLaboralesPorId((int)comboHorasLaborales.SelectedValue).horaslaborales * _serviciosHistoriales.GetHistorialPorId((int)comboHistorial.SelectedValue).ValorPorHora).ToString();
                if (_serviciosDeHorasLaborales.GetHorasLaboralesPorId((int)comboHorasLaborales.SelectedValue).horasExtras == 0)
                {
                    txtTotalExtra.Text = 0.ToString();
                }
                else
                {
                    txtTotalExtra.Text = (_serviciosDeHorasLaborales.GetHorasLaboralesPorId((int)comboHorasLaborales.SelectedValue).horasExtras * _serviciosHistoriales.GetHistorialPorId((int)comboHistorial.SelectedValue).ValorPorHoraExtra).ToString();
                }
            }
            else
            {
                txtTotalAPagar.Text=0.ToString();
                txtTotalExtra.Text=0.ToString();
            }
        }

        private void comboHistorial_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarTotalAPagar();
        }
    }
}
