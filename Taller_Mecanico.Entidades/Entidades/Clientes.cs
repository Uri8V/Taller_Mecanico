namespace Taller_Mecanico.Entidades.Entidades
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string Domicilio { get; set; }
        public string CUIT { get; set; }
        public int IdTipoCliente { get; set; }
        public TiposDeClientes TiposDeClientes { get; set; }
    }
}
