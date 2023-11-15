namespace Taller_Mecanico.Entidades.Entidades
{
    public class Model
    {
        public Model()
        {
        }

        public Model(string modelo)
        {
            Modelo = modelo;
        }

        public int IdModelo { get; set; }
        public string Modelo { get; set; }
        public int IdMarca { get; set; }
        public Marca Marca { get; set; }
    }
}
