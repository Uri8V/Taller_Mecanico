using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Taller_Mecanico.Entidades.Dtos.VehiculoServicio;

namespace Taller_Mecanico.Windows.Helpers
{
    public static class ImprimirHelper
    {
        private static void GuardarPdfImagen(string completo, string PaginaHTML_Texto)
        {
            using (FileStream stream = new FileStream(completo, FileMode.Create))
            {
                //Creamos un nuevo documento y lo definimos como PDF
                Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(new Phrase(""));

                //Agregamos la imagen del banner al documento
                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.shop,
                //	System.Drawing.Imaging.ImageFormat.Png);
                //img.ScaleToFit(60, 60);
                //img.Alignment = iTextSharp.text.Image.UNDERLYING;

                ////img.SetAbsolutePosition(10,100);
                //img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                //pdfDoc.Add(img);


                using (StringReader sr = new StringReader(PaginaHTML_Texto))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                }

                pdfDoc.Close();
                stream.Close();
                Process.Start($"{completo}"); //Muestra el reporte
            }
        }


        private static void CrearCarpetaReportes()
        {
            var path = Environment.CurrentDirectory;
            var carpeta = "Servicios";
            var completo = Path.Combine(path, carpeta);
            if (!Directory.Exists(completo))
            {
                Directory.CreateDirectory(completo);
            }
        }
        private static int contador = 0;
        private static bool llenardatos;
        private static decimal totalhaber ;
        private static decimal totaldebe;
        public static void ImprimirFactura(List<VehiculosServiciosDto> Servicios)
        {
            CrearCarpetaReportes();
            contador++;llenardatos = true;totalhaber = 0;totaldebe = 0;
            var path = Environment.CurrentDirectory + @"\Servicios";
            var archivo = $"{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}.pdf";
            var completo = Path.Combine(path, archivo);
            string PdfFile = Properties.Resources.NuevaFactura.ToString();
            PdfFile = PdfFile.Replace("@Nro", contador.ToString().PadLeft(8, '0'));
            PdfFile = PdfFile.Replace("@FECHA",DateTime.Now.ToShortDateString());
            string lineas = string.Empty;
            foreach (var datosCliente in Servicios)
            {
                if (llenardatos)
                {
                    llenardatos = false;
                    PdfFile = PdfFile.Replace("@CLIENTE", $"{datosCliente.Apellido.ToUpper()}, {datosCliente.Nombre}");
                    PdfFile = PdfFile.Replace("@DOCUMENTO", $"{datosCliente.Documento}{datosCliente.CUIT}");
                }
                lineas += "<tr>";
                lineas += "<td>" + datosCliente.Fecha.ToShortDateString() + "</td>";
                lineas += "<td>" + datosCliente.Patente + "</td>";
                lineas += "<td>" + datosCliente.Servicio + "</td>";
                lineas += "<td>" + datosCliente.Descripcion + "</td>";
                lineas += "<td style='text-align: right;'>" + datosCliente.Haber.ToString("N2") + "</td>";
                lineas += "<td style='text-align: right;'>" + (datosCliente.Debe-datosCliente.Haber).ToString("N2") + "</td>";
                lineas += "</tr>";
                totaldebe += datosCliente.Debe;
                totalhaber+= datosCliente.Haber;
            }
            PdfFile = PdfFile.Replace("@FILAS", lineas);
            PdfFile = PdfFile.Replace("@TOTAL", (totaldebe-totalhaber).ToString("N2"));
            GuardarPdfImagen(completo, PdfFile);

        }
    }
}
