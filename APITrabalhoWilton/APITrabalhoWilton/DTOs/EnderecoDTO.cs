using APITrabalhoWilton.Models;

namespace APITrabalhoWilton.DTOs
{
    public record EnderecoDTO 
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; } = String.Empty;
        public string Complemento { get; set; } = String.Empty;
        public string Numero { get; set; } = String.Empty;
        public string Bairro { get; set; } = String.Empty;
        public string Cidade { get; set; } = String.Empty;
        public string UF { get; set; } = String.Empty;
    }
}
