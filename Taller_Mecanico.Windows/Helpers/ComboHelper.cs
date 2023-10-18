using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taller_Mecanico.Entidades.Entidades;
using Taller_Mecanico.Servicios.Interfaces;
using Taller_Mecanico.Servicios.Servicios;

namespace Taller_Mecanico.Windows.Helpers
{
    public static class ComboHelper
    {
        public static void CargarComboTipoCliente(ref ComboBox combo)
        {
            IServiciosDeTiposDeClientes serviciosTipoCliente = new ServiciosDeTiposDeClientes();
            var lista = serviciosTipoCliente.GetTiposDeClientes();
            var defaultPais = new TiposDeClientes()
            {
                IdTipoCliente = 0,
                TipoCliente = "Seleccione el Tipo de Cliente"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "TipoCliente";
            combo.ValueMember = "IdTipoCliente";
            combo.SelectedIndex = 0;
        }
        //public static void CargarComboCiudades(ref ComboBox combo, int paisId)
        //{
        //    IServiciosCiudades serviciosCiudades = new ServiciosCiudades();
        //    var lista = serviciosCiudades.GetCiudadesCombo(paisId);
        //    var defaultCiudad = new CiudadComboDto()
        //    {
        //        CiudadId = 0,
        //        NombreCiudad = "Seleccione Ciudad"
        //    };
        //    lista.Insert(0, defaultCiudad);
        //    combo.DataSource = lista;
        //    combo.DisplayMember = "NombreCiudad";
        //    combo.ValueMember = "CiudadId";
        //    combo.SelectedIndex = 0;

        //}

        //public static void CargarComboCategorias(ref ComboBox combo)
        //{
        //    IServiciosCategorias serviciosCategorias = new ServiciosCategorias();
        //    var lista = serviciosCategorias.GetCategorias();
        //    var defaultCategoria = new Categoria()
        //    {
        //        CategoriaId = 0,
        //        NombreCategoria = "Seleccione Categoría"
        //    };
        //    lista.Insert(0, defaultCategoria);
        //    combo.DataSource = lista;
        //    combo.DisplayMember = "NombreCategoria";
        //    combo.ValueMember = "CategoriaId";
        //    combo.SelectedIndex = 0;
        //}

        //public static void CargarComboProveedores(ref ComboBox combo)
        //{
        //    IServiciosProveedores serviciosProveedores = new ServiciosProveedores();
        //    var lista = serviciosProveedores.GetProveedores();
        //    var defaultProveedor = new ProveedorListDto()
        //    {
        //        ProveedorId = 0,
        //        NombreProveedor = "Seleccione Proveedor"
        //    };
        //    lista.Insert(0, defaultProveedor);
        //    combo.DataSource = lista;
        //    combo.DisplayMember = "NombreProveedor";
        //    combo.ValueMember = "ProveedorId";
        //    combo.SelectedIndex = 0;
        //}
    }
}
