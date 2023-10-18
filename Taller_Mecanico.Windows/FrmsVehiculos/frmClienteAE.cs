﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Dtos.Clientes;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmClienteAE : Form
    {
        public frmClienteAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboTipoCliente(ref comboTipoCliente);
            if(cliente!=null)
            {
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtDomicilio.Text = cliente.Domicilio;
                txtDocumento.Text = cliente.Documento;
                txtCUIT.Text = cliente.CUIT;
                comboTipoCliente.SelectedValue=cliente.IdTipoCliente;
                esEdicion = true;
            }
        }
        private bool esEdicion = false;
        private Clientes cliente;
        internal Clientes GetCliente()
        {
            return cliente;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmClienteAE_Load(object sender, EventArgs e)
        {
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                if (cliente == null)
                {
                    cliente= new Clientes();
                }
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Documento = txtDocumento.Text;
                cliente.Domicilio = txtDomicilio.Text;
                cliente.CUIT = txtCUIT.Text;
                cliente.TiposDeClientes = (TiposDeClientes)comboTipoCliente.SelectedItem;
                cliente.IdTipoCliente = (int)comboTipoCliente.SelectedValue;
                DialogResult = DialogResult.OK;
            }
        }

        private bool Validar()
        {
            bool validar = true;
            errorProvider1.Clear();
            if (esEdicion==false)
            {
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    errorProvider1.SetError(txtNombre, "DEBE Ingresar un nombre");
                    validar = false;
                }
                if (string.IsNullOrEmpty(txtApellido.Text))
                {
                    errorProvider1.SetError(txtApellido, "DEBE Ingresar un Apellido");
                    validar = false;
                }
                if ((string.IsNullOrEmpty(txtDocumento.Text)) && (string.IsNullOrEmpty(txtCUIT.Text)))
                {
                    errorProvider1.SetError(txtCUIT, "DEBE Ingresar un CUIT");
                    errorProvider1.SetError(txtDocumento, "DEBE Ingresar un Documento");
                    MessageBox.Show("DEBE INGRESAR UN DOCUMENTO O UN CUIT", "MENSAJE INFORMATIVO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    validar = false;
                }
                if (string.IsNullOrEmpty(txtDomicilio.Text))
                {
                    errorProvider1.SetError(txtDomicilio, "DEBE Ingresar un Domicilio");
                    validar = false;
                } 
            }
            return validar;
        }

        internal void SetCliente(Clientes cliente)
        {
            this.cliente = cliente;
        }
    }
}
