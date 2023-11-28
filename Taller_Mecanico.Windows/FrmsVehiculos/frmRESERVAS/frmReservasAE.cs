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
using Taller_Mecanico.Windows.Helpers;

namespace Taller_Mecanico.Windows.FrmsVehiculos.frmRESERVAS
{
    public partial class frmReservasAE : Form
    {
        public frmReservasAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ComboHelper.CargarComboClientesEmpresas(ref comboEmpresa);
            ComboHelper.CargarComboClientesPersonas(ref comboBoxCliente);
            if (reserva != null)
            {
                dateTimePickerFechaDeEntrada.Value = reserva.FechaEntrada;
                dateTimePickerHoraDeEntrada.Value = DateTime.Today.Add(reserva.HoraEntrada);
                if (checkBoxEmpresa.Checked)
                {
                    comboEmpresa.SelectedValue = reserva.IdCliente;
                }
                else
                {
                comboBoxCliente.SelectedValue = reserva.IdCliente;
                }
                dateTimePickerFechaDeSalida.Value = reserva.FechaSalida;
                dateTimePickerHoraDeSalida.Value = DateTime.Today.Add(reserva.HoraSalida);
                checkBoxEsSobreturno.Checked = reserva.EsSobreturno;
                checkBoxSePresento.Checked = reserva.SePresento;
            }
        }
        private Reservas reserva;
        internal Reservas GetReserva()
        {
            return reserva;
        }

        internal void SetReserva(Reservas reservas)
        {
            this.reserva = reservas;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (reserva == null)
                {
                    reserva = new Reservas();
                }
                if (checkBoxEmpresa.Checked)
                {
                    reserva.Cliente = (Clientes)comboEmpresa.SelectedItem;
                    reserva.IdCliente = (int)comboEmpresa.SelectedValue;
                }
                else
                {
                reserva.Cliente = (Clientes)comboBoxCliente.SelectedItem;
                reserva.IdCliente = (int)comboBoxCliente.SelectedValue;
                }
                reserva.FechaEntrada = dateTimePickerFechaDeEntrada.Value.Date;
                reserva.HoraEntrada = new TimeSpan(dateTimePickerHoraDeEntrada.Value.Hour, dateTimePickerHoraDeEntrada.Value.Minute, dateTimePickerHoraDeEntrada.Value.Second);
                if (checkBoxEsSobreturno.Checked)
                {
                    reserva.EsSobreturno = true;
                }
                else
                {
                    reserva.EsSobreturno = false;
                }
                if (checkBoxSePresento.Checked)
                {
                    reserva.SePresento = true;
                }
                else
                {
                    reserva.SePresento = false;
                }
                reserva.FechaSalida = dateTimePickerFechaDeSalida.Value.Date;

                if (dateTimePickerHoraDeSalida.Value.Hour == 0)
                {

                    TimeSpan.TryParse(dateTimePickerHoraDeSalida.Value.Hour.ToString(), out TimeSpan timeSpan);
                    reserva.HoraSalida = timeSpan;
                }
                else
                {
                    reserva.HoraSalida = new TimeSpan(dateTimePickerHoraDeSalida.Value.Hour, dateTimePickerHoraDeSalida.Value.Minute, dateTimePickerHoraDeSalida.Value.Second);
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (dateTimePickerFechaDeEntrada.Value.Date < new DateTime(2023,01,01))
            {
                valido = false;
                errorProvider1.SetError(dateTimePickerFechaDeEntrada, "Debe ingresar una fecha coherente ");
            }
            else if (dateTimePickerFechaDeSalida.Value.Date != new DateTime(2023, 01, 01))
            {
                if (dateTimePickerFechaDeEntrada.Value.Date > dateTimePickerFechaDeSalida.Value.Date)
                {
                    valido = false;
                    errorProvider1.SetError(dateTimePickerFechaDeEntrada, "La fecha de entrada no puede ser mayor a la de salida");
                }
            }
            if (dateTimePickerFechaDeSalida.Value.Date == dateTimePickerFechaDeEntrada.Value.Date)
            {
                if (dateTimePickerHoraDeEntrada.Value.Hour == dateTimePickerHoraDeSalida.Value.Hour)
                {
                    valido = false;
                    errorProvider1.SetError(dateTimePickerHoraDeSalida, "Las horas no pueden coincidir");
                }
            }
            if (dateTimePickerHoraDeSalida.Value.Hour == DateTime.Now.Hour)
            {
                valido = false;
                errorProvider1.SetError(dateTimePickerHoraDeSalida, "Debe ingresar una hora coherente ");
            }

            return valido;
        }

        private void frmReservasAE_Load(object sender, EventArgs e)
        {
            if (checkBoxEmpresa.Checked)
            {
                comboBoxCliente.Enabled = false;
                comboEmpresa.Enabled = true;
            }
            else
            {
                comboBoxCliente.Enabled = true;
                comboEmpresa.Enabled = false;
            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes();
            DialogResult dr= frm.ShowDialog(this);
            if(dr == DialogResult.Cancel)
            {
                ComboHelper.CargarComboClientesPersonas(ref comboBoxCliente);
                ComboHelper.CargarComboClientesEmpresas(ref comboEmpresa);
                return;
            }
        }

        private void checkBoxEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxEmpresa.Checked)
            {
                comboBoxCliente.Enabled = false;
                comboEmpresa.Enabled = true;
            }
            else
            {
                comboBoxCliente.Enabled = true;
                comboEmpresa.Enabled= false;
            }
        }
    }
}
