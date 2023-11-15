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

namespace Taller_Mecanico.Windows.FrmsVehiculos
{
    public partial class frmHorariosLaboralesAE : Form
    {
        public frmHorariosLaboralesAE()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (horas != null)
            {
                dateTimePickerFecha.Value = horas.Fecha;
                dateTimePickerInicio.Value = DateTime.Today.Add(horas.HoraInicio);
                dateTimePickerFin.Value = DateTime.Today.Add(horas.HoraFin);
            }
        }
        private void frmHorariosLaboralesAE_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        HorasLaborales horas;
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if(horas== null)
                {
                    horas= new HorasLaborales();
                }
                horas.Fecha = (DateTime)dateTimePickerFecha.Value;
                horas.HoraInicio =new TimeSpan(dateTimePickerInicio.Value.Hour,
                    dateTimePickerInicio.Value.Minute,
                    dateTimePickerInicio.Value.Second);
                horas.HoraFin = new TimeSpan(dateTimePickerFin.Value.Hour,
                    dateTimePickerFin.Value.Minute,
                    dateTimePickerFin.Value.Second);
                horas.horaslaborales=SacarHorasLaborales(horas.HoraInicio, horas.HoraFin);
                if (horas.horaslaborales > 8)
                {
                    horas.horasExtras = SacarHorasExtra(horas.HoraInicio, horas.HoraFin);
                }
                DialogResult = DialogResult.OK;
            }
        }

        private int SacarHorasExtra(TimeSpan horaInicio, TimeSpan horaFin)
        {
            int cantidad = 0;
            int extra = 0;
            cantidad = horaFin.Hours - horaInicio.Hours;
            if (cantidad > 8)
            {
              extra = cantidad - 8;
            }
            return extra;
        }

        private int SacarHorasLaborales(TimeSpan horaInicio, TimeSpan horaFin)
        {
            int cantidad = 0;
            return cantidad = horaFin.Hours - horaInicio.Hours;
          
        }

        private bool ValidarDatos()
        {
            errorProvider1.Clear();
            bool validar = true;
            if (dateTimePickerFecha.Value.Date > DateTime.Now.Date)
            {
                errorProvider1.SetError(dateTimePickerFecha, "Debe ingresar la fecha actual");
                validar = false;
            }
            if(dateTimePickerFin.Value.Hour>DateTime.Now.Hour)
            {
                errorProvider1.SetError(dateTimePickerFin, "La hora debe ser no puede sobrepasar las horas actuales");
                validar = false;
            }
            if(dateTimePickerInicio.Value.Hour> DateTime.Now.Hour)
            {
                errorProvider1.SetError(dateTimePickerInicio, "Debe ingresar la hora en la que se inicio la jornada");
                validar = false;
            }
            else if (dateTimePickerInicio.Value.Hour>dateTimePickerFin.Value.Hour)
            {
                errorProvider1.SetError(dateTimePickerInicio, "Debe ingresar la hora en la que se inicio la jornada");
                validar = false;
            }
            return validar;
        }

        internal HorasLaborales GetHorasLaborales()
        {
            return horas;
        }

        internal void SetMarca(HorasLaborales horas)
        {
          this.horas= horas;
        }
    }
}
