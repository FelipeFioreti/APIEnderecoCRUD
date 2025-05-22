namespace APITrabalhoWilton.DTOs
{
    public record GetEnderecoViaCepDTO
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
    }
}
