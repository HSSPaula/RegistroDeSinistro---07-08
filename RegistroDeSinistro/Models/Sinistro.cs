namespace RegistroDeSinistro.Models
{
    public class Sinistro
    {
        public Sinistro(int id, DateTime data, string tipo, string rua, int numero, string bairro, string cidade, string estado, string pais)
        {
            Id = id;
            Data = data;
            Tipo = tipo;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
        }
        public Sinistro()
        {
            
        }
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
